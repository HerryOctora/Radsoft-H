using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;



namespace RFSRepository
{
    public class FundClientAgentSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundClientAgentSetup] " +
                            "([FundClientAgentSetupPK],[HistoryPK],[Status],[FundClientPK],[AgentPK],[Date],";

        string _paramaterCommand = "@FundClientPK,@AgentPK,@Date,";

        //2
        private FundClientAgentSetup setFundClientAgentSetup(SqlDataReader dr)
        {
            FundClientAgentSetup M_FundClientAgentSetup = new FundClientAgentSetup();
            M_FundClientAgentSetup.FundClientAgentSetupPK = Convert.ToInt32(dr["FundClientAgentSetupPK"]);
            M_FundClientAgentSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundClientAgentSetup.Status = Convert.ToInt32(dr["Status"]);
            M_FundClientAgentSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundClientAgentSetup.Notes = Convert.ToString(dr["Notes"]);
            M_FundClientAgentSetup.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_FundClientAgentSetup.ID = Convert.ToString(dr["ID"]);
            M_FundClientAgentSetup.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_FundClientAgentSetup.AgentID = Convert.ToString(dr["AgentID"]);
            M_FundClientAgentSetup.Date = dr["Date"].ToString();
            M_FundClientAgentSetup.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundClientAgentSetup.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundClientAgentSetup.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundClientAgentSetup.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundClientAgentSetup.EntryTime = dr["EntryTime"].ToString();
            M_FundClientAgentSetup.UpdateTime = dr["UpdateTime"].ToString();
            M_FundClientAgentSetup.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundClientAgentSetup.VoidTime = dr["VoidTime"].ToString();
            M_FundClientAgentSetup.DBUserID = dr["DBUserID"].ToString();
            M_FundClientAgentSetup.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundClientAgentSetup.LastUpdate = dr["LastUpdate"].ToString();
            M_FundClientAgentSetup.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FundClientAgentSetup;
        }

        public List<FundClientAgentSetup> FundClientAgentSetup_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientAgentSetup> L_FundClientAgentSetup = new List<FundClientAgentSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"

                                Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                B.FundClientPK FundClientPK, B.ID + ' - ' + B.Name ID, C.ID AgentID  ,* from FundClientAgentSetup A
                                left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in(1,2) 
                                left join Agent C on A.AgentPK = C.AgentPK and C.status in(1,2)                             
                             where A.status = @status 
                                                     
                               ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"
                                Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                B.FundClientPK FundClientPK, B.ID + ' - ' + B.Name ID, C.ID AgentID  ,* from FundClientAgentSetup A
                                left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in(1,2) 
                                left join Agent C on A.AgentPK = C.AgentPK and C.status in(1,2)
                        ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClientAgentSetup.Add(setFundClientAgentSetup(dr));
                                }
                            }
                            return L_FundClientAgentSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundClientAgentSetup_Add(FundClientAgentSetup _FundClientAgentSetup, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],lastupdate)" +
                                 "Select isnull(max(FundClientAgentSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from FundClientAgentSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientAgentSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(FundClientAgentSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from FundClientAgentSetup";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundClientPK", _FundClientAgentSetup.FundClientPK);
                        cmd.Parameters.AddWithValue("@AgentPK", _FundClientAgentSetup.AgentPK);
                        cmd.Parameters.AddWithValue("@Date", _FundClientAgentSetup.Date);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundClientAgentSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FundClientAgentSetup");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundClientAgentSetup_Update(FundClientAgentSetup _FundClientAgentSetup, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_FundClientAgentSetup.FundClientAgentSetupPK, _FundClientAgentSetup.HistoryPK, "FundClientAgentSetup");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClientAgentSetup set status=2,Notes=@Notes,FundClientPK=@FundClientPK,AgentPK=@AgentPK,Date=@Date," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where FundClientAgentSetupPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _FundClientAgentSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundClientAgentSetup.FundClientAgentSetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundClientAgentSetup.Notes);
                            cmd.Parameters.AddWithValue("@FundClientPK", _FundClientAgentSetup.FundClientPK);
                            cmd.Parameters.AddWithValue("@AgentPK", _FundClientAgentSetup.AgentPK);
                            cmd.Parameters.AddWithValue("@Date", _FundClientAgentSetup.Date);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientAgentSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientAgentSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClientAgentSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundClientAgentSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundClientAgentSetup.FundClientAgentSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientAgentSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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
                                cmd.CommandText = "Update FundClientAgentSetup set Notes=@Notes,FundClientPK=@FundClientPK,AgentPK=@AgentPK,Date=@Date," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where FundClientAgentSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientAgentSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundClientAgentSetup.FundClientAgentSetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundClientAgentSetup.Notes);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientAgentSetup.FundClientPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _FundClientAgentSetup.AgentPK);
                                cmd.Parameters.AddWithValue("@Date", _FundClientAgentSetup.Date);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientAgentSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_FundClientAgentSetup.FundClientAgentSetupPK, "FundClientAgentSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundClientAgentSetup where FundClientAgentSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundClientAgentSetup.FundClientAgentSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientAgentSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientAgentSetup.FundClientPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _FundClientAgentSetup.AgentPK);
                                cmd.Parameters.AddWithValue("@Date", _FundClientAgentSetup.Date);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientAgentSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundClientAgentSetup set status= 4,Notes=@Notes, " +
                                " LastUpdate=@lastupdate where FundClientAgentSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundClientAgentSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundClientAgentSetup.FundClientAgentSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientAgentSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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

        public void FundClientAgentSetup_Approved(FundClientAgentSetup _FundClientAgentSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientAgentSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where FundClientAgentSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientAgentSetup.FundClientAgentSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientAgentSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientAgentSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientAgentSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundClientAgentSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientAgentSetup.FundClientAgentSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientAgentSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void FundClientAgentSetup_Reject(FundClientAgentSetup _FundClientAgentSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientAgentSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where FundClientAgentSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientAgentSetup.FundClientAgentSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientAgentSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientAgentSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientAgentSetup set status= 2,lastupdate=@lastupdate where FundClientAgentSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientAgentSetup.FundClientAgentSetupPK);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void FundClientAgentSetup_Void(FundClientAgentSetup _FundClientAgentSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientAgentSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate  " +
                            "where FundClientAgentSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientAgentSetup.FundClientAgentSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientAgentSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientAgentSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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

        public bool CheckHassAdd(int _fundclientpk, int _agentpk, DateTime _Date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"if EXISTS (
                        select * from FundClientAgentSetup where AgentPK = @AgentPK and Date = @Date and FundClientPK = @FundClientPK and status in(1,2)
                        )
                        BEGIN
	                        select 1 Result
                        END
                        ELSE
                        BEGIN
	                        select 0 Result
                        END";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundclientpk);
                        cmd.Parameters.AddWithValue("@AgentPK", _agentpk);
                        cmd.Parameters.AddWithValue("@Date", _Date);

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


        public string Transfer_Agent(FundClientAgentSetup _fundClientAgentSetup)
        {
            try
            {

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFundClient = "";


                        if (!_host.findString(_fundClientAgentSetup.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_fundClientAgentSetup.FundClientFrom))
                        {
                            _paramFundClient = " and FundClientPK in ( " + _fundClientAgentSetup.FundClientFrom + " ) ";
                        }
                        else
                        {
                            _paramFundClient = "";
                        }

                        cmd.CommandText = @"
                        --declare @AgentPK int
                        --declare @ValueDate datetime
                        --declare @UsersID nvarchar(50)
                        --declare @Lastupdate datetime

                        --set @AgentPK = 1
                        --set @ValueDate = '11/18/2020'
                        --set @UsersID = 'sa'
                        --set @Lastupdate = getdate()

                        --DROP TABLE #TransferFundClient

                        declare @MaxFundClientAgentSetupPK int
                        declare @CFundClientPK int 
                        declare @CSellingAgentPK int


                        CREATE TABLE #TransferFundClient
                        (
	                        FundClientPK int,
	                        AgentPK int
                        )

                        insert into #TransferFundClient
                        select FundClientPK,SellingAgentPK from FundClient 
                        where SellingAgentPK = @AgentFrom and status in (1,2) " + _paramFundClient + @"


                        Declare A Cursor For
                        select * from #TransferFundClient

                        Open A
                        Fetch next From A
                        into @CFundClientPK,@CSellingAgentPK

                        While @@Fetch_status = 0
                        BEGIN

                        Select @MaxFundClientAgentSetupPK = max(FundClientAgentSetupPK) + 1 from FundClientAgentSetup
                        set @MaxFundClientAgentSetupPK = isnull(@MaxFundClientAgentSetupPK,1)

                        update FundClientAgentSetup set status = 3 where Date = @ValueDate and FundClientPK = @CFundClientPK and AgentPk = @CSellingAgentPK 

                        insert into [dbo].FundClientAgentSetup
                        (FundClientAgentSetupPK
                        ,[HistoryPK]
                        ,[Status]
                        ,[FundClientPK]
                        ,[AgentPK]
                        ,[Date]
                        ,[EntryUsersID]
                        ,[EntryTime]
                        ,[LastUpdate]
                        )

                        select @MaxFundClientAgentSetupPK,1,2,@CFundClientPK,@CSellingAgentPK,@ValueDate,@UsersID,@LastUpdate,@LastUpdate
                                

                        update FundClient set  SellingAgentPK = @AgentTo where FundClientPK = @CFundClientPK and status in (1,2)

                        fetch next From A into @CFundClientPK,@CSellingAgentPK
                        end
                        Close A
                        Deallocate A


                        select 'Transfer Agent Success' Result



                         ";

                        cmd.Parameters.AddWithValue("@ValueDate", _fundClientAgentSetup.Date);
                        cmd.Parameters.AddWithValue("@AgentFrom", _fundClientAgentSetup.AgentFrom);
                        cmd.Parameters.AddWithValue("@AgentTo", _fundClientAgentSetup.AgentTo);
                        cmd.Parameters.AddWithValue("@UsersID", _fundClientAgentSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

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