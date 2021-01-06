using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class BackLoadSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[BackLoadSetup] " +
                            "([BackLoadSetupPK],[HistoryPK],[Status],[Date],[FundPK],[HoldingPeriod],[MinimumSubs],[RedempFeePercent],";
        string _paramaterCommand = "@Date,@FundPK,@HoldingPeriod,@MinimumSubs,@RedempFeePercent,";

        private BackLoadSetup setBackLoadSetup(SqlDataReader dr)
        {
            BackLoadSetup M_BackLoadSetup = new BackLoadSetup();
            M_BackLoadSetup.BackLoadSetupPK = Convert.ToInt32(dr["BackLoadSetupPK"]);
            M_BackLoadSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_BackLoadSetup.Status = Convert.ToInt32(dr["Status"]);
            M_BackLoadSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_BackLoadSetup.Notes = Convert.ToString(dr["Notes"]);

            M_BackLoadSetup.Date = Convert.ToDateTime(dr["Date"]);
            M_BackLoadSetup.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_BackLoadSetup.FundName = Convert.ToString(dr["FundName"]);
            M_BackLoadSetup.HoldingPeriod = Convert.ToInt32(dr["HoldingPeriod"]);
            M_BackLoadSetup.MinimumSubs = Convert.ToInt32(dr["MinimumSubs"]);
            M_BackLoadSetup.RedempFeePercent = Convert.ToDecimal(dr["RedempFeePercent"]);

            M_BackLoadSetup.EntryUsersID = dr["EntryUsersID"].ToString();
            M_BackLoadSetup.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_BackLoadSetup.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_BackLoadSetup.VoidUsersID = dr["VoidUsersID"].ToString();
            M_BackLoadSetup.EntryTime = dr["EntryTime"].ToString();
            M_BackLoadSetup.UpdateTime = dr["UpdateTime"].ToString();
            M_BackLoadSetup.ApprovedTime = dr["ApprovedTime"].ToString();
            M_BackLoadSetup.VoidTime = dr["VoidTime"].ToString();
            M_BackLoadSetup.DBUserID = dr["DBUserID"].ToString();
            M_BackLoadSetup.DBTerminalID = dr["DBTerminalID"].ToString();
            M_BackLoadSetup.LastUpdate = dr["LastUpdate"].ToString();
            M_BackLoadSetup.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_BackLoadSetup;
        }

        public List<BackLoadSetup> BackLoadSetup_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BackLoadSetup> L_BackLoadSetup = new List<BackLoadSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when a.status=1 then 'PENDING' else Case When a.status = 2 then 'APPROVED' else Case when a.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.Name FundName,* from BackLoadSetup A left join fund B on A.FundPK = B.FundPK and B.Status in(1,2) where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when a.status=1 then 'PENDING' else Case When a.status = 2 then 'APPROVED' else Case when a.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.Name FundName,*  from BackLoadSetup A left join fund B on A.FundPK = B.FundPK and B.Status in(1,2) order by a.FundPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_BackLoadSetup.Add(setBackLoadSetup(dr));
                                }
                            }
                            return L_BackLoadSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BackLoadSetup_Add(BackLoadSetup _backloadsetup, bool _havePrivillege)
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
                                 "Select isnull(max(BackLoadSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From BackLoadSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _backloadsetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(BackLoadSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From BackLoadSetup";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);

                        cmd.Parameters.AddWithValue("@Date", _backloadsetup.Date);
                        cmd.Parameters.AddWithValue("@FundPK", _backloadsetup.FundPK);
                        cmd.Parameters.AddWithValue("@HoldingPeriod", _backloadsetup.HoldingPeriod);
                        cmd.Parameters.AddWithValue("@MinimumSubs", _backloadsetup.MinimumSubs);
                        cmd.Parameters.AddWithValue("@RedempFeePercent", _backloadsetup.RedempFeePercent);

                        cmd.Parameters.AddWithValue("@EntryUsersID", _backloadsetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "BackLoadSetup");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BackLoadSetup_Update(BackLoadSetup _backloadsetup, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_backloadsetup.BackLoadSetupPK, _backloadsetup.HistoryPK, "BackLoadSetup");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BackLoadSetup set status=2,Notes=@Notes,Date=@Date,FundPK=@FundPK,HoldingPeriod=@HoldingPeriod,MinimumSubs=@MinimumSubs,RedempFeePercent=@RedempFeePercent," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where BackLoadSetupPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _backloadsetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _backloadsetup.BackLoadSetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _backloadsetup.Notes);

                            cmd.Parameters.AddWithValue("@Date", _backloadsetup.Date);
                            cmd.Parameters.AddWithValue("@FundPK", _backloadsetup.FundPK);
                            cmd.Parameters.AddWithValue("@HoldingPeriod", _backloadsetup.HoldingPeriod);
                            cmd.Parameters.AddWithValue("@MinimumSubs", _backloadsetup.MinimumSubs);
                            cmd.Parameters.AddWithValue("@RedempFeePercent", _backloadsetup.RedempFeePercent);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _backloadsetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _backloadsetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BackLoadSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BackLoadSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _backloadsetup.BackLoadSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _backloadsetup.EntryUsersID);
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
                                cmd.CommandText = "Update BackLoadSetup set Notes=@Notes,Date=@Date,FundPK=@FundPK,HoldingPeriod=@HoldingPeriod,MinimumSubs=@MinimumSubs,RedempFeePercent=@RedempFeePercent," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where BackLoadSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _backloadsetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _backloadsetup.BackLoadSetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _backloadsetup.Notes);

                                cmd.Parameters.AddWithValue("@Date", _backloadsetup.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _backloadsetup.FundPK);
                                cmd.Parameters.AddWithValue("@HoldingPeriod", _backloadsetup.HoldingPeriod);
                                cmd.Parameters.AddWithValue("@MinimumSubs", _backloadsetup.MinimumSubs);
                                cmd.Parameters.AddWithValue("@RedempFeePercent", _backloadsetup.RedempFeePercent);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _backloadsetup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_backloadsetup.BackLoadSetupPK, "BackLoadSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From BackLoadSetup where BackLoadSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _backloadsetup.BackLoadSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _backloadsetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);

                                cmd.Parameters.AddWithValue("@Date", _backloadsetup.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _backloadsetup.FundPK);
                                cmd.Parameters.AddWithValue("@HoldingPeriod", _backloadsetup.HoldingPeriod);
                                cmd.Parameters.AddWithValue("@MinimumSubs", _backloadsetup.MinimumSubs);
                                cmd.Parameters.AddWithValue("@RedempFeePercent", _backloadsetup.RedempFeePercent);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _backloadsetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update BackLoadSetup set status= 4,Notes=@Notes," +
                                    " LastUpdate=@lastupdate " +
                                    " where BackLoadSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _backloadsetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _backloadsetup.BackLoadSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _backloadsetup.HistoryPK);
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

        public void BackLoadSetup_Approved(BackLoadSetup _backloadsetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BackLoadSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate  " +
                            "where BackLoadSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _backloadsetup.BackLoadSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _backloadsetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _backloadsetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BackLoadSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BackLoadSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _backloadsetup.BackLoadSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _backloadsetup.ApprovedUsersID);
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

        public void BackLoadSetups_Reject(BackLoadSetup _backloadsetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BackLoadSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where BackLoadSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _backloadsetup.BackLoadSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _backloadsetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _backloadsetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BackLoadSetup set status= 2,LastUpdate=@LastUpdate where BackLoadSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _backloadsetup.BackLoadSetupPK);
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

        public void BackLoadSetup_Void(BackLoadSetup _backloadsetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BackLoadSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where BackLoadSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _backloadsetup.BackLoadSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _backloadsetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _backloadsetup.VoidUsersID);
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

        //public List<BackLoadSetupCombo> BackLoadSetup_Combo()
        //{

        //    try
        //    {
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            List<BackLoadSetupCombo> L_BackLoadSetup = new List<BackLoadSetupCombo>();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {
        //                cmd.CommandText = "SELECT  BackLoadSetupPK,ID +' - '+ Name ID, Name FROM [BackLoadSetup]  where status = 2 order by BackLoadSetupPK";
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    if (dr.HasRows)
        //                    {
        //                        while (dr.Read())
        //                        {
        //                            BackLoadSetupCombo M_BackLoadSetup = new BackLoadSetupCombo();
        //                            M_BackLoadSetup.BackLoadSetupPK = Convert.ToInt32(dr["BackLoadSetupPK"]);
        //                            M_BackLoadSetup.ID = Convert.ToString(dr["ID"]);
        //                            M_BackLoadSetup.Name = Convert.ToString(dr["Name"]);
        //                            L_BackLoadSetup.Add(M_BackLoadSetup);
        //                        }

        //                    }
        //                    return L_BackLoadSetup;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }


        //}

        public bool Validate_AddBackLoadSetup(int FundPK, BackLoadSetup _backloadsetup)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";

                        if (!_host.findString(_backloadsetup.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_backloadsetup.FundFrom))
                        {
                            _paramFund = "And A.FundPK in ( " + _backloadsetup.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandText = @"if Exists(select * From BackLoadSetup A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        where A.Status in (1,2) and B.Type <> 10  and A.FundPk = @FundPk " + _paramFund + ") BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END";
                        cmd.Parameters.AddWithValue("@FundPk", FundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);

                            }
                            return false;
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
