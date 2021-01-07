using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class SwitchingFundReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[SwitchingFund] " +
                            "([SwitchingFundPK],[HistoryPK],[Status],[FundFromPK],[FundToPK],[Type],";
        string _paramaterCommand = "@FundFromPK,@FundToPK,@Type,";



        //2
        private SwitchingFund setSwitchingFund(SqlDataReader dr)
        {
            SwitchingFund M_SwitchingFund = new SwitchingFund();

            M_SwitchingFund.SwitchingFundPK = Convert.ToInt32(dr["SwitchingFundPK"]);
            M_SwitchingFund.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_SwitchingFund.Status = Convert.ToInt32(dr["Status"]);
            M_SwitchingFund.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_SwitchingFund.Notes = Convert.ToString(dr["Notes"]);
            M_SwitchingFund.FundFromPK = Convert.ToInt32(dr["FundFromPK"]);
            M_SwitchingFund.FundFromID = Convert.ToString(dr["FundFromID"]);
            M_SwitchingFund.FundToPK = Convert.ToInt32(dr["FundToPK"]);
            M_SwitchingFund.FundToID = Convert.ToString(dr["FundToID"]);
            M_SwitchingFund.Type = Convert.ToInt32(dr["Type"]);
            M_SwitchingFund.TypeDesc = dr["TypeDesc"].ToString();
            M_SwitchingFund.EntryUsersID = dr["EntryUsersID"].ToString();
            M_SwitchingFund.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_SwitchingFund.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_SwitchingFund.VoidUsersID = dr["VoidUsersID"].ToString();
            M_SwitchingFund.EntryTime = dr["EntryTime"].ToString();
            M_SwitchingFund.UpdateTime = dr["UpdateTime"].ToString();
            M_SwitchingFund.ApprovedTime = dr["ApprovedTime"].ToString();
            M_SwitchingFund.VoidTime = dr["VoidTime"].ToString();
            M_SwitchingFund.DBUserID = dr["DBUserID"].ToString();
            M_SwitchingFund.DBTerminalID = dr["DBTerminalID"].ToString();
            M_SwitchingFund.LastUpdate = dr["LastUpdate"].ToString();
            M_SwitchingFund.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_SwitchingFund;
        }

        public List<SwitchingFund> SwitchingFund_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {


                    DbCon.Open();
                    List<SwitchingFund> L_SwitchingFund = new List<SwitchingFund>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END 
                            StatusDesc,B.FundPK FundFromPK, B.Name FundFromID, C.FundPK FundToPK, C.Name FundToID, 
							case when A.Type = 1 then 'APERD' else 'Non-APERD'  end TypeDesc,* from SwitchingFund A
                            left join Fund B on A.FundFromPK = B.FundPK and B.Status = 2
                            left join Fund C on A.FundToPK = C.FundPK and C.Status = 2
                            where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {

                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END 
                            StatusDesc,B.FundPK FundFromPK, B.Name FundFromID, C.FundPK FundToPK, C.Name FundToID, 
							case when A.Type = 1 then 'APERD' else 'Non-APERD'  end TypeDesc,* from SwitchingFund A
                            left join Fund B on A.FundFromPK = B.FundPK and B.Status = 2
                            left join Fund C on A.FundToPK = C.FundPK and C.Status = 2";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_SwitchingFund.Add(setSwitchingFund(dr));
                                }
                            }
                            return L_SwitchingFund;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int SwitchingFund_Add(SwitchingFund _SwitchingFund, bool _havePrivillege)
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
                                 "Select isnull(max(SwitchingFundPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from SwitchingFund";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _SwitchingFund.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate) " +
                                "Select isnull(max(SwitchingFundPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from SwitchingFund";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundFromPK", _SwitchingFund.FundFromPK);
                        cmd.Parameters.AddWithValue("@FundToPK", _SwitchingFund.FundToPK);
                        cmd.Parameters.AddWithValue("@Type", _SwitchingFund.Type);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _SwitchingFund.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "SwitchingFund");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int SwitchingFund_Update(SwitchingFund _SwitchingFund, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_SwitchingFund.SwitchingFundPK, _SwitchingFund.HistoryPK, "SwitchingFund");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update SwitchingFund set status=2, Notes=@Notes,FundFromPK=@FundFromPK,FundToPK=@FundToPK,Type=@Type,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where SwitchingFundPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _SwitchingFund.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _SwitchingFund.SwitchingFundPK);
                            cmd.Parameters.AddWithValue("@Notes", _SwitchingFund.Notes);
                            cmd.Parameters.AddWithValue("@FundFromPK", _SwitchingFund.FundFromPK);
                            cmd.Parameters.AddWithValue("@FundToPK", _SwitchingFund.FundToPK);
                            cmd.Parameters.AddWithValue("@Type", _SwitchingFund.Type);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _SwitchingFund.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _SwitchingFund.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update SwitchingFund set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where SwitchingFundPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _SwitchingFund.SwitchingFundPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _SwitchingFund.EntryUsersID);
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
                                cmd.CommandText = "Update SwitchingFund set Notes=@Notes,FundFromPK=@FundFromPK,FundToPK=@FundToPK,Type=@Type," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where SwitchingFundPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _SwitchingFund.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _SwitchingFund.SwitchingFundPK);
                                cmd.Parameters.AddWithValue("@Notes", _SwitchingFund.Notes);
                                cmd.Parameters.AddWithValue("@FundFromPK", _SwitchingFund.FundFromPK);
                                cmd.Parameters.AddWithValue("@FundToPK", _SwitchingFund.FundToPK);
                                cmd.Parameters.AddWithValue("@Type", _SwitchingFund.Type);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _SwitchingFund.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_SwitchingFund.SwitchingFundPK, "SwitchingFund");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                   "From SwitchingFund where SwitchingFundPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _SwitchingFund.SwitchingFundPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _SwitchingFund.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundFromPK", _SwitchingFund.FundFromPK);
                                cmd.Parameters.AddWithValue("@FundToPK", _SwitchingFund.FundToPK);
                                cmd.Parameters.AddWithValue("@Type", _SwitchingFund.Type);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _SwitchingFund.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update SwitchingFund set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@lastupdate " +
                                    " where SwitchingFundPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _SwitchingFund.Notes);
                                cmd.Parameters.AddWithValue("@PK", _SwitchingFund.SwitchingFundPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _SwitchingFund.HistoryPK);
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

        public void SwitchingFund_Approved(SwitchingFund _SwitchingFund)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SwitchingFund set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where SwitchingFundpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SwitchingFund.SwitchingFundPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SwitchingFund.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _SwitchingFund.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update SwitchingFund set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where SwitchingFundPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _SwitchingFund.SwitchingFundPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SwitchingFund.ApprovedUsersID);
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

        public void SwitchingFund_Reject(SwitchingFund _SwitchingFund)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SwitchingFund set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                            "where SwitchingFundpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SwitchingFund.SwitchingFundPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SwitchingFund.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SwitchingFund.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update SwitchingFund set status= 2,LastUpdate=@LastUpdate  where SwitchingFundPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _SwitchingFund.SwitchingFundPK);
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

        public void SwitchingFund_Void(SwitchingFund _SwitchingFund)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SwitchingFund set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where SwitchingFundpk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SwitchingFund.SwitchingFundPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SwitchingFund.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SwitchingFund.VoidUsersID);
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
        public List<SwitchingFundLookup> Get_FundRefByFundPK(int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SwitchingFundLookup> L_SwitchingFund = new List<SwitchingFundLookup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"select B.FundPK FundFromPK, B.Name FundFromID, C.FundPK FundToPK, C.ID + ' - ' + C.Name as FundToID from SwitchingFund A
                            left join Fund B on A.FundFromPK = B.FundPK and B.Status = 2
                            left join Fund C on A.FundToPK = C.FundPK and C.Status = 2 where A.status = 2 and A.FundFromPK = @FundPK";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);

                

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    SwitchingFundLookup M_SwitchingFund = new SwitchingFundLookup();
                                    M_SwitchingFund.FundPKFrom = Convert.ToInt32(dr["FundFromPK"]);
                                    M_SwitchingFund.FundIDFrom = dr["FundFromID"].ToString();
                                    M_SwitchingFund.FundPKTo = Convert.ToInt32(dr["FundToPK"]);
                                    M_SwitchingFund.FundIDTo = dr["FundToID"].ToString();
                                    L_SwitchingFund.Add(M_SwitchingFund);
                                }

                            }
                            return L_SwitchingFund;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool Get_CheckAlreadyHasApprovedSwitchingFund(int _fundFromPK, int _fundToPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "select * from SwitchingFund where status = 2 and FundFromPK = @FundFromPK and FundToPK = @FundToPK ";

                        cmd.Parameters.AddWithValue("@FundFromPK", _fundFromPK);
                        cmd.Parameters.AddWithValue("@FundToPK", _fundToPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;

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