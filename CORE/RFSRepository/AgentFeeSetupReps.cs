using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class AgentFeeSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[AgentFeeSetup] " +
                            "([AgentFeeSetupPK],[HistoryPK],[Status],[AgentPK],[Date],[RangeFrom],[RangeTo],[DateAmortize],[MIFeeAmount],[MIFeePercent],[FeeType],[TypeTrx],[FundPK],";
        string _paramaterCommand = "@AgentPK,@Date,@RangeFrom,@RangeTo,@DateAmortize,@MIFeeAmount,@MIFeePercent,@FeeType,@TypeTrx,@FundPK,";

        //2
        private AgentFeeSetup setAgentFeeSetup(SqlDataReader dr)
        {
            AgentFeeSetup M_AgentFeeSetup = new AgentFeeSetup();
            M_AgentFeeSetup.AgentFeeSetupPK = Convert.ToInt32(dr["AgentFeeSetupPK"]);
            M_AgentFeeSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AgentFeeSetup.Status = Convert.ToInt32(dr["Status"]);
            M_AgentFeeSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_AgentFeeSetup.Notes = Convert.ToString(dr["Notes"]);
            M_AgentFeeSetup.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_AgentFeeSetup.AgentID = dr["AgentID"].ToString();
            M_AgentFeeSetup.AgentName = dr["AgentName"].ToString();
            M_AgentFeeSetup.Date = dr["Date"].ToString();
            M_AgentFeeSetup.RangeFrom = Convert.ToDecimal(dr["RangeFrom"]);
            M_AgentFeeSetup.RangeTo = Convert.ToDecimal(dr["RangeTo"]);
            M_AgentFeeSetup.DateAmortize = dr["DateAmortize"].ToString();
            M_AgentFeeSetup.MIFeeAmount = Convert.ToDecimal(dr["MIFeeAmount"]);
            M_AgentFeeSetup.MIFeePercent = Convert.ToDecimal(dr["MIFeePercent"]);
            M_AgentFeeSetup.FeeType = Convert.ToInt32(dr["FeeType"]);
            M_AgentFeeSetup.FeeTypeDesc = dr["FeeTypeDesc"].ToString();
            M_AgentFeeSetup.TypeTrx = Convert.ToInt32(dr["TypeTrx"]);
            M_AgentFeeSetup.TypeTrxDesc = dr["TypeTrxDesc"].ToString();
            M_AgentFeeSetup.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_AgentFeeSetup.FundID = dr["FundID"].ToString();
            M_AgentFeeSetup.EntryUsersID = dr["EntryUsersID"].ToString();
            M_AgentFeeSetup.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_AgentFeeSetup.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_AgentFeeSetup.VoidUsersID = dr["VoidUsersID"].ToString();
            M_AgentFeeSetup.EntryTime = dr["EntryTime"].ToString();
            M_AgentFeeSetup.UpdateTime = dr["UpdateTime"].ToString();
            M_AgentFeeSetup.ApprovedTime = dr["ApprovedTime"].ToString();
            M_AgentFeeSetup.VoidTime = dr["VoidTime"].ToString();
            M_AgentFeeSetup.DBUserID = dr["DBUserID"].ToString();
            M_AgentFeeSetup.DBTerminalID = dr["DBTerminalID"].ToString();
            M_AgentFeeSetup.LastUpdate = dr["LastUpdate"].ToString();
            M_AgentFeeSetup.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_AgentFeeSetup;
        }

        public List<AgentFeeSetup> AgentFeeSetup_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AgentFeeSetup> L_AgentFeeSetup = new List<AgentFeeSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID AgentID,B.Name AgentName,C.DescOne FeeTypeDesc,D.DescOne TypeTrxDesc,E.ID FundID, A.* from AgentFeeSetup A
                            left join Agent B on A.AgentPK = B.AgentPK and B.Status = 2
                            left join MasterValue C on A.FeeType = C.Code and C.Status = 2 and C.ID = 'AgentFeeType'
                            left join MasterValue D on A.TypeTrx = D.Code and D.Status = 2 and D.ID = 'AgentFeeTrxType'
                            left join Fund E on A.FundPK = E.FundPK and E.Status in (1,2)
                            where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID AgentID,B.Name AgentName,C.DescOne FeeTypeDesc,D.DescOne TypeTrxDesc,E.ID FundID, A.* from AgentFeeSetup A
                            left join Agent B on A.AgentPK = B.AgentPK and B.Status = 2
                            left join MasterValue C on A.FeeType = C.Code and C.Status = 2 and C.ID = 'AgentFeeType'
                            left join MasterValue D on A.TypeTrx = D.Code and D.Status = 2 and D.ID = 'AgentFeeTrxType'
                            left join Fund E on A.FundPK = E.FundPK and E.Status in (1,2)
                            order by AgentPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AgentFeeSetup.Add(setAgentFeeSetup(dr));
                                }
                            }
                            return L_AgentFeeSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int AgentFeeSetup_Add(AgentFeeSetup _AgentFeeSetup, bool _havePrivillege)
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
                                 "Select isnull(max(AgentFeeSetupPk),0) + 1,1,2," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from AgentFeeSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AgentFeeSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(AgentFeeSetupPk),0) + 1,1,2," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from AgentFeeSetup";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@AgentPK", _AgentFeeSetup.AgentPK);
                        cmd.Parameters.AddWithValue("@Date", _AgentFeeSetup.Date);
                        if (_AgentFeeSetup.DateAmortize == null)
                        {
                            cmd.Parameters.AddWithValue("@DateAmortize", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DateAmortize", _AgentFeeSetup.DateAmortize);
                        }
                        cmd.Parameters.AddWithValue("@RangeFrom", _AgentFeeSetup.RangeFrom);
                        cmd.Parameters.AddWithValue("@RangeTo", _AgentFeeSetup.RangeTo);
                        cmd.Parameters.AddWithValue("@MIFeeAmount", _AgentFeeSetup.MIFeeAmount);
                        cmd.Parameters.AddWithValue("@MIFeePercent", _AgentFeeSetup.MIFeePercent);
                        cmd.Parameters.AddWithValue("@FeeType", _AgentFeeSetup.FeeType);
                        cmd.Parameters.AddWithValue("@TypeTrx", _AgentFeeSetup.TypeTrx);
                        cmd.Parameters.AddWithValue("@FundPK", _AgentFeeSetup.FundPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _AgentFeeSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "AgentFeeSetup");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int AgentFeeSetup_Update(AgentFeeSetup _AgentFeeSetup, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_AgentFeeSetup.AgentFeeSetupPK, _AgentFeeSetup.HistoryPK, "AgentFeeSetup");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AgentFeeSetup set status=2, Notes=@Notes,AgentPK=@AgentPK,Date=@Date,RangeFrom=@RangeFrom,RangeTo=@RangeTo,DateAmortize=@DateAmortize,MIFeeAmount=@MIFeeAmount,MIFeePercent=@MIFeePercent,FeeType=@FeeType,TypeTrx=@TypeTrx,FundPK=@FundPK," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where AgentFeeSetupPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _AgentFeeSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _AgentFeeSetup.AgentFeeSetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _AgentFeeSetup.Notes);
                            cmd.Parameters.AddWithValue("@AgentPK", _AgentFeeSetup.AgentPK);
                            cmd.Parameters.AddWithValue("@Date", _AgentFeeSetup.Date);
                            cmd.Parameters.AddWithValue("@RangeFrom", _AgentFeeSetup.RangeFrom);
                            cmd.Parameters.AddWithValue("@RangeTo", _AgentFeeSetup.RangeTo);
                            cmd.Parameters.AddWithValue("@DateAmortize", _AgentFeeSetup.DateAmortize);
                            cmd.Parameters.AddWithValue("@MIFeeAmount", _AgentFeeSetup.MIFeeAmount);
                            cmd.Parameters.AddWithValue("@MIFeePercent", _AgentFeeSetup.MIFeePercent);
                            cmd.Parameters.AddWithValue("@FeeType", _AgentFeeSetup.FeeType);
                            cmd.Parameters.AddWithValue("@TypeTrx", _AgentFeeSetup.TypeTrx);
                            cmd.Parameters.AddWithValue("@FundPK", _AgentFeeSetup.FundPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _AgentFeeSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AgentFeeSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AgentFeeSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AgentFeeSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _AgentFeeSetup.AgentFeeSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _AgentFeeSetup.EntryUsersID);
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
                                cmd.CommandText = "Update AgentFeeSetup set Notes=@Notes,AgentPK=@AgentPK,Date=@Date,RangeFrom=@RangeFrom,RangeTo=@RangeTo,DateAmortize=@DateAmortize,MIFeeAmount=@MIFeeAmount,MIFeePercent=@MIFeePercent,FeeType=@FeeType,TypeTrx=@TypeTrx,FundPK=@FundPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where AgentFeeSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _AgentFeeSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _AgentFeeSetup.AgentFeeSetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _AgentFeeSetup.Notes);
                                cmd.Parameters.AddWithValue("@AgentPK", _AgentFeeSetup.AgentPK);
                                cmd.Parameters.AddWithValue("@Date", _AgentFeeSetup.Date);
                                cmd.Parameters.AddWithValue("@RangeFrom", _AgentFeeSetup.RangeFrom);
                                cmd.Parameters.AddWithValue("@RangeTo", _AgentFeeSetup.RangeTo);
                                cmd.Parameters.AddWithValue("@DateAmortize", _AgentFeeSetup.DateAmortize);
                                cmd.Parameters.AddWithValue("@MIFeeAmount", _AgentFeeSetup.MIFeeAmount);
                                cmd.Parameters.AddWithValue("@MIFeePercent", _AgentFeeSetup.MIFeePercent);
                                cmd.Parameters.AddWithValue("@FeeType", _AgentFeeSetup.FeeType);
                                cmd.Parameters.AddWithValue("@TypeTrx", _AgentFeeSetup.TypeTrx);
                                cmd.Parameters.AddWithValue("@FundPK", _AgentFeeSetup.FundPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AgentFeeSetup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_AgentFeeSetup.AgentFeeSetupPK, "AgentFeeSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From AgentFeeSetup where AgentFeeSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _AgentFeeSetup.AgentFeeSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AgentFeeSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _AgentFeeSetup.AgentPK);
                                cmd.Parameters.AddWithValue("@Date", _AgentFeeSetup.Date);
                                cmd.Parameters.AddWithValue("@RangeFrom", _AgentFeeSetup.RangeFrom);
                                cmd.Parameters.AddWithValue("@RangeTo", _AgentFeeSetup.RangeTo);
                                cmd.Parameters.AddWithValue("@DateAmortize", _AgentFeeSetup.DateAmortize);
                                cmd.Parameters.AddWithValue("@MIFeeAmount", _AgentFeeSetup.MIFeeAmount);
                                cmd.Parameters.AddWithValue("@MIFeePercent", _AgentFeeSetup.MIFeePercent);
                                cmd.Parameters.AddWithValue("@FeeType", _AgentFeeSetup.FeeType);
                                cmd.Parameters.AddWithValue("@TypeTrx", _AgentFeeSetup.TypeTrx);
                                cmd.Parameters.AddWithValue("@FundPK", _AgentFeeSetup.FundPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AgentFeeSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update AgentFeeSetup set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where AgentFeeSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _AgentFeeSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _AgentFeeSetup.AgentFeeSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AgentFeeSetup.HistoryPK);
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

        public void AgentFeeSetup_Approved(AgentFeeSetup _AgentFeeSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AgentFeeSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where AgentFeeSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AgentFeeSetup.AgentFeeSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _AgentFeeSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _AgentFeeSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AgentFeeSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AgentFeeSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _AgentFeeSetup.AgentFeeSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AgentFeeSetup.ApprovedUsersID);
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

        public void AgentFeeSetup_Reject(AgentFeeSetup _AgentFeeSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AgentFeeSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where AgentFeeSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AgentFeeSetup.AgentFeeSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _AgentFeeSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AgentFeeSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AgentFeeSetup set status= 2,LastUpdate=@LastUpdate where AgentFeeSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _AgentFeeSetup.AgentFeeSetupPK);
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

        public void AgentFeeSetup_Void(AgentFeeSetup _AgentFeeSetup)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AgentFeeSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where AgentFeeSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AgentFeeSetup.AgentFeeSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _AgentFeeSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AgentFeeSetup.VoidUsersID);
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

        public string Add_Validate(AgentFeeSetup _agentFeeSetup)
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
                        declare @FeeTypeDesc nvarchar(50)

                        select @FeeTypeDesc = DescOne from MasterValue where ID = 'AgentFeeType' and status = 2 and Code = @FeeType


                        declare @AgentFeeSetup table
                        (
                        FundPK int,
                        AgentPK int,
                        Date datetime,
                        RangeFrom numeric(32,4),
                        RangeTo numeric(32,4),
                        FeeType int
                        )


                        insert into @AgentFeeSetup
                        select FundPK,AgentPK,Date,RangeFrom,RangeTo,FeeType from AgentFeeSetup 
                        where status in (1,2) and Date = @Date and FundPK = @FundPK  and AgentPK = @AgentPK


                        IF @FeeType not in (2,3,4,5)
                        BEGIN
	                        IF EXISTS(select * from @AgentFeeSetup)
	                        BEGIN
		                        select 'Fee Type : ' + @FeeTypeDesc + ', Cannot Insert Another Data in this days !' Result
	                        END
                            ELSE
                            BEGIN
                                select 'FALSE' Result
                            END
                        END
                        ELSE
                        BEGIN
	                        IF NOT EXISTS(select * from @AgentFeeSetup where FeeType <> @FeeType)
	                        BEGIN
		                        IF EXISTS(
		                        SELECT * FROM AgentFeeSetup 
		                        WHERE (@RangeFrom BETWEEN RangeFrom AND RangeTo 
			                        OR @RangeTo BETWEEN RangeFrom AND RangeTo
			                        OR RangeTo BETWEEN @RangeTo AND @RangeFrom
			                        OR RangeFrom BETWEEN @RangeFrom AND @RangeTo) and FundPK = @FundPK  and AgentPK = @AgentPK and Date = @Date and FeeType = @FeeType and status in (1,2)
		                        )
		                        BEGIN
			                        select 'Fee Type : ' + @FeeTypeDesc + ', Cannot Insert Data, Please Check Range From and To !' Result
		                        END
		                        ELSE
		                        BEGIN
			                        select 'FALSE' Result
		                        END
	                        END
	                        ELSE
	                        BEGIN
		                        select 'Fee Type : ' + @FeeTypeDesc + ', Cannot Insert Another Type in this days !' Result
	                        END
                        END
                         ";

                        cmd.Parameters.AddWithValue("@Date", _agentFeeSetup.Date);
                        cmd.Parameters.AddWithValue("@FundPK", _agentFeeSetup.FundPK);
                        cmd.Parameters.AddWithValue("@AgentPK", _agentFeeSetup.AgentPK);
                        cmd.Parameters.AddWithValue("@FeeType", _agentFeeSetup.FeeType);
                        cmd.Parameters.AddWithValue("@RangeFrom", _agentFeeSetup.RangeFrom);
                        cmd.Parameters.AddWithValue("@RangeTo", _agentFeeSetup.RangeTo);

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