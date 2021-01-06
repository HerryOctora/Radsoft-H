using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class AdvisoryFeeReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[AdvisoryFee] " +
                            "([AdvisoryFeePK],[HistoryPK],[Status],[ProjectName],[StartDate],[EndDate],[Remarks],[ProjectValue]," +
                            "[Status1Project],[Status1ProjectDate],[Status2Project],[Status2ProjectDate],[Status3Project],[Status3ProjectDate],[Status4Project],[Status4ProjectDate],[Status5Project],[Status5ProjectDate],";
        string _paramaterCommand = "@ProjectName,@StartDate,@EndDate,@Remarks,@ProjectValue,@Status1Project,@Status1ProjectDate,@Status2Project,@Status2ProjectDate,@Status3Project,@Status3ProjectDate,@Status4Project,@Status4ProjectDate,@Status5Project,@Status5ProjectDate,";


        private AdvisoryFee setAdvisoryFee(SqlDataReader dr)
        {
            AdvisoryFee M_AdvisoryFee = new AdvisoryFee();
            M_AdvisoryFee.AdvisoryFeePK = Convert.ToInt32(dr["AdvisoryFeePK"]);
            M_AdvisoryFee.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AdvisoryFee.Status = Convert.ToInt32(dr["Status"]);
            M_AdvisoryFee.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_AdvisoryFee.Notes = Convert.ToString(dr["Notes"]);

            M_AdvisoryFee.ProjectName = dr["ProjectName"].ToString();
            M_AdvisoryFee.StartDate = Convert.ToDateTime(dr["StartDate"]);
            M_AdvisoryFee.EndDate = Convert.ToDateTime(dr["EndDate"]);
            M_AdvisoryFee.Remarks = dr["Remarks"].ToString();
            M_AdvisoryFee.ProjectValue = Convert.ToDecimal(dr["ProjectValue"]);
            M_AdvisoryFee.Status1Project = dr["Status1Project"].ToString();
            M_AdvisoryFee.Status1ProjectDate = Convert.ToDateTime(dr["Status1ProjectDate"]);
            M_AdvisoryFee.Status2Project = dr["Status2Project"].ToString();
            M_AdvisoryFee.Status2ProjectDate = Convert.ToDateTime(dr["Status2ProjectDate"]);
            M_AdvisoryFee.Status3Project = dr["Status3Project"].ToString();
            M_AdvisoryFee.Status3ProjectDate = Convert.ToDateTime(dr["Status3ProjectDate"]);
            M_AdvisoryFee.Status4Project = dr["Status4Project"].ToString();
            M_AdvisoryFee.Status4ProjectDate = Convert.ToDateTime(dr["Status4ProjectDate"]);
            M_AdvisoryFee.Status5Project = dr["Status5Project"].ToString();
            M_AdvisoryFee.Status5ProjectDate = Convert.ToDateTime(dr["Status5ProjectDate"]);

            M_AdvisoryFee.EntryUsersID = dr["EntryUsersID"].ToString();
            M_AdvisoryFee.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_AdvisoryFee.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_AdvisoryFee.VoidUsersID = dr["VoidUsersID"].ToString();
            M_AdvisoryFee.EntryTime = dr["EntryTime"].ToString();
            M_AdvisoryFee.UpdateTime = dr["UpdateTime"].ToString();
            M_AdvisoryFee.ApprovedTime = dr["ApprovedTime"].ToString();
            M_AdvisoryFee.VoidTime = dr["VoidTime"].ToString();
            M_AdvisoryFee.DBUserID = dr["DBUserID"].ToString();
            M_AdvisoryFee.DBTerminalID = dr["DBTerminalID"].ToString();
            M_AdvisoryFee.LastUpdate = dr["LastUpdate"].ToString();
            M_AdvisoryFee.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_AdvisoryFee;
        }

        public List<AdvisoryFee> AdvisoryFee_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AdvisoryFee> L_AdvisoryFee = new List<AdvisoryFee>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from AdvisoryFee where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from AdvisoryFee order by ProjectName";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AdvisoryFee.Add(setAdvisoryFee(dr));
                                }
                            }
                            return L_AdvisoryFee;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public AdvisoryFeeAddNew AdvisoryFee_Add(AdvisoryFee _AdvisoryFee, bool _havePrivillege)
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
                            cmd.CommandText = "Declare @newAdvisoryFeePK int \n " +
                                 "Select @newAdvisoryFeePK = isnull(max(AdvisoryFeePk),0) + 1 from AdvisoryFee \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate) " +
                                 "Select @newAdvisoryFeePK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate \n " +
                                 "Select @newAdvisoryFeePK newAdvisoryFeePK, 1 newHistoryPK ";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AdvisoryFee.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = "Declare @newAdvisoryFeePK int \n " +
                                 "Select @newAdvisoryFeePK = isnull(max(AdvisoryFeePk),0) + 1 from AdvisoryFee \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select @newAdvisoryFeePK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate \n " +
                                 "Select @newAdvisoryFeePK newAdvisoryFeePK, 1 newHistoryPK ";
                        }


                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);

                        cmd.Parameters.AddWithValue("@ProjectName", _AdvisoryFee.ProjectName);
                        cmd.Parameters.AddWithValue("@StartDate", _AdvisoryFee.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", _AdvisoryFee.EndDate);
                        cmd.Parameters.AddWithValue("@Remarks", _AdvisoryFee.Remarks);
                        cmd.Parameters.AddWithValue("@ProjectValue", _AdvisoryFee.ProjectValue);
                        cmd.Parameters.AddWithValue("@Status1Project", _AdvisoryFee.Status1Project);
                        cmd.Parameters.AddWithValue("@Status1ProjectDate", _AdvisoryFee.Status1ProjectDate);
                        cmd.Parameters.AddWithValue("@Status2Project", _AdvisoryFee.Status2Project);
                        cmd.Parameters.AddWithValue("@Status2ProjectDate", _AdvisoryFee.Status2ProjectDate);
                        cmd.Parameters.AddWithValue("@Status3Project", _AdvisoryFee.Status3Project);
                        cmd.Parameters.AddWithValue("@Status3ProjectDate", _AdvisoryFee.Status3ProjectDate);
                        cmd.Parameters.AddWithValue("@Status4Project", _AdvisoryFee.Status4Project);
                        cmd.Parameters.AddWithValue("@Status4ProjectDate", _AdvisoryFee.Status4ProjectDate);
                        cmd.Parameters.AddWithValue("@Status5Project", _AdvisoryFee.Status5Project);
                        cmd.Parameters.AddWithValue("@Status5ProjectDate", _AdvisoryFee.Status5ProjectDate);
                        

                        cmd.Parameters.AddWithValue("@EntryUsersID", _AdvisoryFee.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new AdvisoryFeeAddNew()
                                {
                                    AdvisoryFeePK = Convert.ToInt32(dr["newAdvisoryFeePK"]),
                                    HistoryPK = Convert.ToInt32(dr["newHistoryPK"]),
                                    Message = "Insert AdvisoryFee Header Success"
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

        public int AdvisoryFee_Update(AdvisoryFee _AdvisoryFee, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_AdvisoryFee.AdvisoryFeePK, _AdvisoryFee.HistoryPK, "AdvisoryFee");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AdvisoryFee set status=2, Notes=@Notes,ProjectName=@ProjectName,ClientName=@ClientName,AcqValue=@AcqValue,AcqSharePercentage=@AcqSharePercentage,StatusProject=@StatusProject,Remarks=@Remarks," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where AdvisoryFeePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _AdvisoryFee.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _AdvisoryFee.AdvisoryFeePK);
                            cmd.Parameters.AddWithValue("@Notes", _AdvisoryFee.Notes);

                            cmd.Parameters.AddWithValue("@ProjectName", _AdvisoryFee.ProjectName);
                            cmd.Parameters.AddWithValue("@StartDate", _AdvisoryFee.StartDate);
                            cmd.Parameters.AddWithValue("@EndDate", _AdvisoryFee.EndDate);
                            cmd.Parameters.AddWithValue("@Remarks", _AdvisoryFee.Remarks);
                            cmd.Parameters.AddWithValue("@ProjectValue", _AdvisoryFee.ProjectValue);
                            cmd.Parameters.AddWithValue("@Status1Project", _AdvisoryFee.Status1Project);
                            cmd.Parameters.AddWithValue("@Status1ProjectDate", _AdvisoryFee.Status1ProjectDate);
                            cmd.Parameters.AddWithValue("@Status2Project", _AdvisoryFee.Status2Project);
                            cmd.Parameters.AddWithValue("@Status2ProjectDate", _AdvisoryFee.Status2ProjectDate);
                            cmd.Parameters.AddWithValue("@Status3Project", _AdvisoryFee.Status3Project);
                            cmd.Parameters.AddWithValue("@Status3ProjectDate", _AdvisoryFee.Status3ProjectDate);
                            cmd.Parameters.AddWithValue("@Status4Project", _AdvisoryFee.Status4Project);
                            cmd.Parameters.AddWithValue("@Status4ProjectDate", _AdvisoryFee.Status4ProjectDate);
                            cmd.Parameters.AddWithValue("@Status5Project", _AdvisoryFee.Status5Project);
                            cmd.Parameters.AddWithValue("@Status5ProjectDate", _AdvisoryFee.Status5ProjectDate);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _AdvisoryFee.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AdvisoryFee.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AdvisoryFee set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AdvisoryFeePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _AdvisoryFee.AdvisoryFeePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _AdvisoryFee.EntryUsersID);
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
                                cmd.CommandText = "Update AdvisoryFee set Notes=@Notes,ProjectName=@ProjectName,ClientName=@ClientName,AcqValue=@AcqValue,AcqSharePercentage=@AcqSharePercentage,StatusProject=@StatusProject,Remarks=@Remarks," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where AdvisoryFeePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _AdvisoryFee.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _AdvisoryFee.AdvisoryFeePK);
                                cmd.Parameters.AddWithValue("@Notes", _AdvisoryFee.Notes);

                                cmd.Parameters.AddWithValue("@ProjectName", _AdvisoryFee.ProjectName);
                                cmd.Parameters.AddWithValue("@StartDate", _AdvisoryFee.StartDate);
                                cmd.Parameters.AddWithValue("@EndDate", _AdvisoryFee.EndDate);
                                cmd.Parameters.AddWithValue("@Remarks", _AdvisoryFee.Remarks);
                                cmd.Parameters.AddWithValue("@ProjectValue", _AdvisoryFee.ProjectValue);
                                cmd.Parameters.AddWithValue("@Status1Project", _AdvisoryFee.Status1Project);
                                cmd.Parameters.AddWithValue("@Status1ProjectDate", _AdvisoryFee.Status1ProjectDate);
                                cmd.Parameters.AddWithValue("@Status2Project", _AdvisoryFee.Status2Project);
                                cmd.Parameters.AddWithValue("@Status2ProjectDate", _AdvisoryFee.Status2ProjectDate);
                                cmd.Parameters.AddWithValue("@Status3Project", _AdvisoryFee.Status3Project);
                                cmd.Parameters.AddWithValue("@Status3ProjectDate", _AdvisoryFee.Status3ProjectDate);
                                cmd.Parameters.AddWithValue("@Status4Project", _AdvisoryFee.Status4Project);
                                cmd.Parameters.AddWithValue("@Status4ProjectDate", _AdvisoryFee.Status4ProjectDate);
                                cmd.Parameters.AddWithValue("@Status5Project", _AdvisoryFee.Status5Project);
                                cmd.Parameters.AddWithValue("@Status5ProjectDate", _AdvisoryFee.Status5ProjectDate);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AdvisoryFee.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_AdvisoryFee.AdvisoryFeePK, "AdvisoryFee");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From AdvisoryFee where AdvisoryFeePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _AdvisoryFee.AdvisoryFeePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AdvisoryFee.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);

                                cmd.Parameters.AddWithValue("@ProjectName", _AdvisoryFee.ProjectName);
                                cmd.Parameters.AddWithValue("@StartDate", _AdvisoryFee.StartDate);
                                cmd.Parameters.AddWithValue("@EndDate", _AdvisoryFee.EndDate);
                                cmd.Parameters.AddWithValue("@Remarks", _AdvisoryFee.Remarks);
                                cmd.Parameters.AddWithValue("@ProjectValue", _AdvisoryFee.ProjectValue);
                                cmd.Parameters.AddWithValue("@Status1Project", _AdvisoryFee.Status1Project);
                                cmd.Parameters.AddWithValue("@Status1ProjectDate", _AdvisoryFee.Status1ProjectDate);
                                cmd.Parameters.AddWithValue("@Status2Project", _AdvisoryFee.Status2Project);
                                cmd.Parameters.AddWithValue("@Status2ProjectDate", _AdvisoryFee.Status2ProjectDate);
                                cmd.Parameters.AddWithValue("@Status3Project", _AdvisoryFee.Status3Project);
                                cmd.Parameters.AddWithValue("@Status3ProjectDate", _AdvisoryFee.Status3ProjectDate);
                                cmd.Parameters.AddWithValue("@Status4Project", _AdvisoryFee.Status4Project);
                                cmd.Parameters.AddWithValue("@Status4ProjectDate", _AdvisoryFee.Status4ProjectDate);
                                cmd.Parameters.AddWithValue("@Status5Project", _AdvisoryFee.Status5Project);
                                cmd.Parameters.AddWithValue("@Status5ProjectDate", _AdvisoryFee.Status5ProjectDate);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AdvisoryFee.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update AdvisoryFee set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where AdvisoryFeePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _AdvisoryFee.AdvisoryFeePK);
                                cmd.Parameters.AddWithValue("@PK", _AdvisoryFee.AdvisoryFeePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AdvisoryFee.HistoryPK);
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

        public void AdvisoryFee_Approved(AdvisoryFee _AdvisoryFee)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AdvisoryFee set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where AdvisoryFeePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AdvisoryFee.AdvisoryFeePK);
                        cmd.Parameters.AddWithValue("@historyPK", _AdvisoryFee.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _AdvisoryFee.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AdvisoryFee set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AdvisoryFeePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _AdvisoryFee.AdvisoryFeePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AdvisoryFee.ApprovedUsersID);
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

        public void AdvisoryFee_Reject(AdvisoryFee _AdvisoryFee)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AdvisoryFee set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where AdvisoryFeePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AdvisoryFee.AdvisoryFeePK);
                        cmd.Parameters.AddWithValue("@historyPK", _AdvisoryFee.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AdvisoryFee.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AdvisoryFee set status= 2,LastUpdate=@LastUpdate where AdvisoryFeePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _AdvisoryFee.AdvisoryFeePK);
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

        public void AdvisoryFee_Void(AdvisoryFee _AdvisoryFee)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AdvisoryFee set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where AdvisoryFeePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AdvisoryFee.AdvisoryFeePK);
                        cmd.Parameters.AddWithValue("@historyPK", _AdvisoryFee.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AdvisoryFee.VoidUsersID);
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



        public bool CheckAdvisoryFeeStatus(int _pk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from AdvisoryFee where AdvisoryFeePK = @PK and Status = 1";
                        cmd.Parameters.AddWithValue("@PK", _pk);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
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



    }
}
