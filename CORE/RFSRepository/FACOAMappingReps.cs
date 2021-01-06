using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class FACOAMappingReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FACOAMapping] " +
                            "([FACOAMappingPK],[HistoryPK],[Status],[FundPK],[FACOAAdjustmentPK],[FundJournalAccountPK],[DebitOrCredit],[FundJournalAccountPercent],";
        string _paramaterCommand = "@FundPK,@FACOAAdjustmentPK,@FundJournalAccountPK,@DebitOrCredit,@FundJournalAccountPercent,";

        //2

        private FACOAMapping setFACOAMapping(SqlDataReader dr)
        {
            FACOAMapping M_FACOAMapping = new FACOAMapping();
            M_FACOAMapping.FACOAMappingPK = Convert.ToInt32(dr["FACOAMappingPK"]);
            M_FACOAMapping.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FACOAMapping.Status = Convert.ToInt32(dr["Status"]);
            M_FACOAMapping.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FACOAMapping.Notes = Convert.ToString(dr["Notes"]);
            M_FACOAMapping.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FACOAMapping.FundID = Convert.ToString(dr["FundID"]);
            M_FACOAMapping.FundName = Convert.ToString(dr["FundName"]);
            M_FACOAMapping.FACOAAdjustmentPK = Convert.ToInt32(dr["FACOAAdjustmentPK"]);
            M_FACOAMapping.FACOAAdjustmentID = Convert.ToString(dr["FACOAAdjustmentID"]);
            M_FACOAMapping.FACOAAdjustmentName = Convert.ToString(dr["FACOAAdjustmentName"]);
            M_FACOAMapping.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
            M_FACOAMapping.FundJournalAccountID = Convert.ToString(dr["FundJournalAccountID"]);
            M_FACOAMapping.FundJournalAccountName = Convert.ToString(dr["FundJournalAccountName"]);
            M_FACOAMapping.DebitOrCredit = Convert.ToString(dr["DebitOrCredit"]);
            M_FACOAMapping.FundJournalAccountPercent = Convert.ToDecimal(dr["FundJournalAccountPercent"]);
            M_FACOAMapping.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FACOAMapping.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FACOAMapping.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FACOAMapping.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FACOAMapping.EntryTime = dr["EntryTime"].ToString();
            M_FACOAMapping.UpdateTime = dr["UpdateTime"].ToString();
            M_FACOAMapping.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FACOAMapping.VoidTime = dr["VoidTime"].ToString();
            M_FACOAMapping.DBUserID = dr["DBUserID"].ToString();
            M_FACOAMapping.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FACOAMapping.LastUpdate = dr["LastUpdate"].ToString();
            M_FACOAMapping.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FACOAMapping;
        }

        public List<FACOAMapping> FACOAMapping_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FACOAMapping> L_FACOAMapping = new List<FACOAMapping>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID FACOAAdjustmentID,B.Name FACOAAdjustmentName, C.ID FundJournalAccountID,C.Name FundJournalAccountName,D.ID FundID,D.Name FundName,A.* from FACOAMapping A 
                                              left join FACOAAdjustment B on A.FACOAAdjustmentPK = B.FACOAAdjustmentPK and B.status = 2 
                                              left join FundJournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK  and C.status = 2 
                                              Left join Fund D on A.FundPK = D.FundPK and D.status = 2
                                              where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"SSelect case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID FACOAAdjustmentID,B.Name FACOAAdjustmentName, C.ID FundJournalAccountID,C.Name FundJournalAccountName,D.ID FundID,D.Name FundName,A.* from FACOAMapping A 
                                                left join FACOAAdjustment B on A.FACOAAdjustmentPK = B.FACOAAdjustmentPK and B.status = 2 
                                                left join FundJournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK  and C.status = 2 
                                                Left join Fund D on A.FundPK = D.FundPK and D.status = 2
                                                order by FundPK,FACOAAdjustmentPK,FundJournalAccountPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FACOAMapping.Add(setFACOAMapping(dr));
                                }
                            }
                            return L_FACOAMapping;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FACOAMapping_Add(FACOAMapping _FACOAMapping, bool _havePrivillege)
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
                                 "Select isnull(max(FACOAMappingPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from FACOAMapping";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FACOAMapping.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(FACOAMappingPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from FACOAMapping";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundPK", _FACOAMapping.FundPK);
                        cmd.Parameters.AddWithValue("@FACOAAdjustmentPK", _FACOAMapping.FACOAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _FACOAMapping.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@DebitOrCredit", _FACOAMapping.DebitOrCredit);
                        cmd.Parameters.AddWithValue("@FundJournalAccountPercent", _FACOAMapping.FundJournalAccountPercent);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FACOAMapping.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FACOAMapping");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FACOAMapping_Update(FACOAMapping _FACOAMapping, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FACOAMapping.FACOAMappingPK, _FACOAMapping.HistoryPK, "FACOAMapping"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FACOAMapping set status=2, Notes=@Notes, FundPK=@FundPK,FACOAAdjustmentPK=@FACOAAdjustmentPK,FundJournalAccountPK=@FundJournalAccountPK,DebitOrCredit=@DebitOrCredit,FundJournalAccountPercent=@FundJournalAccountPercent," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FACOAMappingPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FACOAMapping.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FACOAMapping.FACOAMappingPK);
                            cmd.Parameters.AddWithValue("@Notes", _FACOAMapping.Notes);
                            cmd.Parameters.AddWithValue("@FundPK", _FACOAMapping.FundPK);
                            cmd.Parameters.AddWithValue("@FACOAAdjustmentPK", _FACOAMapping.FACOAAdjustmentPK);
                            cmd.Parameters.AddWithValue("@FundJournalAccountPK", _FACOAMapping.FundJournalAccountPK);
                            cmd.Parameters.AddWithValue("@DebitOrCredit", _FACOAMapping.DebitOrCredit);
                            cmd.Parameters.AddWithValue("@FundJournalAccountPercent", _FACOAMapping.FundJournalAccountPercent);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FACOAMapping.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FACOAMapping.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FACOAMapping set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FACOAMappingPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FACOAMapping.FACOAMappingPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FACOAMapping.EntryUsersID);
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
                                cmd.CommandText = "Update FACOAMapping set Notes=@Notes, FundPK=@FundPK,FACOAAdjustmentPK=@FACOAAdjustmentPK,FundJournalAccountPK=@FundJournalAccountPK,DebitOrCredit=@DebitOrCredit,FundJournalAccountPercent=@FundJournalAccountPercent," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where FACOAMappingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FACOAMapping.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FACOAMapping.FACOAMappingPK);
                                cmd.Parameters.AddWithValue("@Notes", _FACOAMapping.Notes);
                                cmd.Parameters.AddWithValue("@FundPK", _FACOAMapping.FundPK);
                                cmd.Parameters.AddWithValue("@FACOAAdjustmentPK", _FACOAMapping.FACOAAdjustmentPK);
                                cmd.Parameters.AddWithValue("@FundJournalAccountPK", _FACOAMapping.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@DebitOrCredit", _FACOAMapping.DebitOrCredit);
                                cmd.Parameters.AddWithValue("@FundJournalAccountPercent", _FACOAMapping.FundJournalAccountPercent);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FACOAMapping.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FACOAMapping.FACOAMappingPK, "FACOAMapping");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FACOAMapping where FACOAMappingPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FACOAMapping.FACOAMappingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FACOAMapping.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundPK", _FACOAMapping.FundPK);
                                cmd.Parameters.AddWithValue("@FACOAAdjustmentPK", _FACOAMapping.FACOAAdjustmentPK);
                                cmd.Parameters.AddWithValue("@FundJournalAccountPK", _FACOAMapping.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@DebitOrCredit", _FACOAMapping.DebitOrCredit);
                                cmd.Parameters.AddWithValue("@FundJournalAccountPercent", _FACOAMapping.FundJournalAccountPercent);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FACOAMapping.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FACOAMapping set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where FACOAMappingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FACOAMapping.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FACOAMapping.FACOAMappingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FACOAMapping.HistoryPK);
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

        public void FACOAMapping_Approved(FACOAMapping _FACOAMapping)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FACOAMapping set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where FACOAMappingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FACOAMapping.FACOAMappingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FACOAMapping.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FACOAMapping.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FACOAMapping set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FACOAMappingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FACOAMapping.FACOAMappingPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FACOAMapping.ApprovedUsersID);
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

        public void FACOAMapping_Reject(FACOAMapping _FACOAMapping)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FACOAMapping set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FACOAMappingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FACOAMapping.FACOAMappingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FACOAMapping.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FACOAMapping.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FACOAMapping set status= 2,LastUpdate=@LastUpdate  where FACOAMappingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FACOAMapping.FACOAMappingPK);
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

        public void FACOAMapping_Void(FACOAMapping _FACOAMapping)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FACOAMapping set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FACOAMappingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FACOAMapping.FACOAMappingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FACOAMapping.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FACOAMapping.VoidUsersID);
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

        public string Validate_CheckCopyFACOAMapping(int _fundTo)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        
                        if exists(select FundPK from FACOAMapping where status = 2 and FundPK = @FundPK)
                        BEGIN
                        SELECT 'Copy Cancel, Fund Already Exists' as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END   ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundTo);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return "SALAH WWOYYYYY";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public string Copy_FACOAMapping(string _usersID,int _paramFundFrom, int _paramFundTo)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        
                        Declare @FundPK int
                        Declare @FACOAAdjustmentPK int
                        Declare @FundJournalAccountPK int
                        Declare @DebitOrCredit nvarchar(1)
                        Declare @FundJournalAccountPercent numeric(22,4)

                        DECLARE A CURSOR FOR 
                        select FACOAAdjustmentPK,FundJournalAccountPK,DebitOrCredit,FundJournalAccountPercent from FACOAMapping 
                        where status = 2 and FundPK = @FundPKFrom

                        Open A
                        Fetch Next From A
                        Into @FACOAAdjustmentPK,@FundJournalAccountPK,@DebitOrCredit,@FundJournalAccountPercent

                        While @@FETCH_STATUS = 0
                        BEGIN       
  
                        declare @FACOAMappingPK int
                        Select @FACOAMappingPK = max(FACOAMappingPK) + 1 From FACOAMapping
                        Insert Into FACOAMapping(FACOAMappingPK,HistoryPK,Status,FundPK,FACOAAdjustmentPK,FundJournalAccountPK,DebitOrCredit,FundJournalAccountPercent,EntryUsersID,EntryTime,LastUpdate)
                        Select @FACOAMappingPK,1,1,@FundPKTo,@FACOAAdjustmentPK,@FundJournalAccountPK,@DebitOrCredit,@FundJournalAccountPercent,@EntryUsersID,@TimeNow,@TimeNow

                        Fetch next From A Into @FACOAAdjustmentPK,@FundJournalAccountPK,@DebitOrCredit,@FundJournalAccountPercent
                        END
                        Close A
                        Deallocate A

                        select 'Copy COA Mapping Success' as Msg
                          ";

                        cmd.Parameters.AddWithValue("@FundPKFrom", _paramFundFrom);
                        cmd.Parameters.AddWithValue("@FundPKTo", _paramFundTo);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _usersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Msg"]);

                            }
                            return "SALAH WWOYYYYY";
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