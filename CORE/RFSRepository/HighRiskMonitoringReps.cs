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
    public class HighRiskMonitoringReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[HighRiskMonitoring] " +
                            "([HighRiskMonitoringPK],[HistoryPK],[Status],[Description],[FundClientPK],[Date],[Reason],[Selected],[HighRiskMonStatus],[HighRiskType],[Notes],";


        string _paramaterCommand = "@Description,FundClientPK,Date,Reason,Selected,@HighRiskMonStatus,HighRiskType,Notes,";

        //2
        private HighRiskMonitoring setHighRiskMonitoring(SqlDataReader dr)
        {
            HighRiskMonitoring M_HighRiskMonitoring = new HighRiskMonitoring();
            M_HighRiskMonitoring.HighRiskMonitoringPK = Convert.ToInt32(dr["HighRiskMonitoringPK"]);
            M_HighRiskMonitoring.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_HighRiskMonitoring.Selected = Convert.ToBoolean(dr["Selected"]);
            M_HighRiskMonitoring.Status = Convert.ToInt32(dr["Status"]);
            M_HighRiskMonitoring.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_HighRiskMonitoring.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_HighRiskMonitoring.FundClientID = Convert.ToString(dr["FundClientID"]);
            M_HighRiskMonitoring.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_HighRiskMonitoring.InvestmentNo = Convert.ToInt32(dr["InvestmentNo"]);
            M_HighRiskMonitoring.HighRiskType = Convert.ToInt32(dr["HighRiskType"]);
            M_HighRiskMonitoring.ClientType = Convert.ToString(dr["ClientType"]);
            M_HighRiskMonitoring.Date = Convert.ToString(dr["Date"]);
            M_HighRiskMonitoring.Reason = Convert.ToString(dr["Reason"]);
            M_HighRiskMonitoring.Description = Convert.ToString(dr["Description"]);
            M_HighRiskMonitoring.BitIsSuspend = dr["BitIsSuspend"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitIsSuspend"]);
            if (_host.CheckColumnIsExist(dr, "KYCRiskProfile"))
            {
                M_HighRiskMonitoring.KYCRiskProfile = Convert.ToInt32(dr["KYCRiskProfile"]);
            }
            M_HighRiskMonitoring.EntryUsersID = dr["EntryUsersID"].ToString();
            M_HighRiskMonitoring.EntryTime = dr["EntryTime"].ToString();
            M_HighRiskMonitoring.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_HighRiskMonitoring.ApprovedTime = dr["ApprovedTime"].ToString();
            M_HighRiskMonitoring.UnApprovedUsersID = dr["UnApprovedUsersID"].ToString();
            M_HighRiskMonitoring.UnApprovedTime = dr["UnApprovedTime"].ToString();
            M_HighRiskMonitoring.DBUserID = dr["DBUserID"].ToString();
            M_HighRiskMonitoring.DBTerminalID = dr["DBTerminalID"].ToString();
            M_HighRiskMonitoring.LastUpdate = dr["LastUpdate"].ToString();
            M_HighRiskMonitoring.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            M_HighRiskMonitoring.UpdateUsersID = Convert.ToString(dr["UpdateUsersID"]);
            M_HighRiskMonitoring.UpdateTime = Convert.ToString(dr["UpdateTime"]);
            M_HighRiskMonitoring.HighRiskMonStatus = Convert.ToInt32(dr["HighRiskMonStatus"]);
            if (_host.CheckColumnIsExist(dr, "HighRiskMonStatusDesc"))
            {
                M_HighRiskMonitoring.HighRiskMonStatusDesc = dr["HighRiskMonStatusDesc"].ToString();
            }
            M_HighRiskMonitoring.Notes = Convert.ToString(dr["Notes"]);
            return M_HighRiskMonitoring;
        }

        public string HighRiskMonitoring_CheckInvestorAndFundRiskProfile(int _fundPK, int _fundClientPK, string _usersID)
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
                        Declare @Reason nvarchar(500)


                        select  @Reason =  'Risk Profile and FundProfile Not Match :' + B.Name + '  Product ID:' + 
                        F.ID  + '  ClientID:' +B.ID + '   InvestorRiskProfile:' +isnull(D.DescOne,'') + '   ProductRiskProfile:' + isnull(E.DescOne,'') 
                        from  fundClient B 
                        left join FundRiskProfile C on C.FundPK = @FundPK
                        left join mastervalue D on B.InvestorsRiskProfile = D.Code and D.ID = 'InvestorsRiskProfile' and D.status = 2
                        left join mastervalue E on C.RiskProfilePK = E.Code and E.ID = 'InvestorsRiskProfile' and E.status = 2
                        left join Fund F on C.FundPK = F.FundPK and F.status = 2
                        where isnull(B.InvestorsRiskProfile,0) <> isnull(C.RiskProfilePK,0)
                        and isnull(B.InvestorsRiskProfile,0) <> 0
                        and B.FundClientPK = @FundClientPK

                        IF (@ClientCode <> '02')
                        BEGIN
                        IF len(@Reason) > 0
                            BEGIN
	                             Declare @PK int
	                             select @PK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
	                             set @PK = isnull(@PK,1)
	                             insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,FundClientPK,Date,Reason,EntryUsersID,LastUpdate,EntryTime)
	                             Select @PK,1,1,@FundClientPK,@Date,@Reason,@UsersID,@Date,@Date
                                 Select @Reason Reason
                            END
                        END
                        ELSE
                        BEGIN

                        select '' Reason
                        END
                        
                        ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return dr["Reason"].ToString();
                                }
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

        public List<HighRiskMonitoring> HighRiskMonitoring_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<HighRiskMonitoring> L_HighRiskMonitoring = new List<HighRiskMonitoring>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                                "select case when H.Status=1 then 'PENDING' else case when H.Status = 2 then 'APPROVED' else case when H.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc, " +
                                "C.ID FundClientID, C.Name FundClientName, H.* " +
                                "from HighRiskMonitoring H " +
                                "left join FundClient C on C.FundClientPK = H.FundClientPK and C.Status = 2 " +
                                "where H.status = @status " +
                                "order by H.HighRiskMonitoringPK, H.Date";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                                "select case when H.Status=1 then 'PENDING' else case when H.Status = 2 then 'APPROVED' else case when H.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc, " +
                                "C.ID FundClientID, C.Name FundClientName, H.* " +
                                "from HighRiskMonitoring H " +
                                "left join FundClient C on C.FundClientPK = H.FundClientPK and C.Status = 2 " +
                                "order by H.HighRiskMonitoringPK, H.Date";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_HighRiskMonitoring.Add(setHighRiskMonitoring(dr));
                                }
                            }
                            return L_HighRiskMonitoring;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int HighRiskMonitoring_Update(HighRiskMonitoring _HighRiskMonitoring, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _dateTimeNow = DateTime.Now;
                int status = _host.Get_Status(_HighRiskMonitoring.HighRiskMonitoringPK, _HighRiskMonitoring.HistoryPK, "HighRiskMonitoring");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update HighRiskMonitoring set Description=@Description,HighRiskMonStatus=@HighRiskMonStatus," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastUpdate " +
                                "where HighRiskMonitoringPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _HighRiskMonitoring.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _HighRiskMonitoring.HighRiskMonitoringPK);
                            cmd.Parameters.AddWithValue("@Description", _HighRiskMonitoring.Description);
                            cmd.Parameters.AddWithValue("@HighRiskMonStatus", _HighRiskMonitoring.HighRiskMonStatus);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _HighRiskMonitoring.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _HighRiskMonitoring.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update HighRiskMonitoring set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastUpdate=@LastUpdate where HighRiskMonitoringPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _HighRiskMonitoring.HighRiskMonitoringPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _HighRiskMonitoring.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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
                                cmd.CommandText = "Update HighRiskMonitoring set Description=@Description,HighRiskMonStatus=@HighRiskMonStatus," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where HighRiskMonitoringPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _HighRiskMonitoring.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _HighRiskMonitoring.HighRiskMonitoringPK);
                                cmd.Parameters.AddWithValue("@Description", _HighRiskMonitoring.Description);
                                cmd.Parameters.AddWithValue("@HighRiskMonStatus", _HighRiskMonitoring.HighRiskMonStatus);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _HighRiskMonitoring.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_HighRiskMonitoring.HighRiskMonitoringPK, "HighRiskMonitoring");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From HighRiskMonitoring where HighRiskMonitoringPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _HighRiskMonitoring.HighRiskMonitoringPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _HighRiskMonitoring.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Description", _HighRiskMonitoring.Description);
                                cmd.Parameters.AddWithValue("@HighRiskMonStatus", _HighRiskMonitoring.HighRiskMonStatus);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _HighRiskMonitoring.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update HighRiskMonitoring set status= 4," +
                                    "LastUpdate=@LastUpdate where HighRiskMonitoringPK = @PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@PK", _HighRiskMonitoring.HighRiskMonitoringPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _HighRiskMonitoring.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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

        public void HighRiskMonitoring_Approved(HighRiskMonitoring _highRiskMonitoring)
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
                        @"Declare @FundClientPK int 
                        Select @FundClientPK = FundClientPK from HighRiskMonitoring where HighRiskMonitoringPK = @PK  and status = 1 

                        update HighRiskMonitoring set status = 2,selected = 0,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate 
                        where HighRiskMonitoringPK = @PK and historypk = @historyPK

                        IF (@ClientCode <> '20')
                        BEGIN
                            update FundClient set KYCRiskProfile = 1 where status = 2 and FundClientPK = @FundClientPK
                        END
                        ";

                        cmd.Parameters.AddWithValue("@PK", _highRiskMonitoring.HighRiskMonitoringPK);
                        cmd.Parameters.AddWithValue("@historyPK", _highRiskMonitoring.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _highRiskMonitoring.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void HighRiskMonitoring_UnApproved(HighRiskMonitoring _highRiskMonitoring)
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
                        "update HighRiskMonitoring set status = 1,selected = 0,UnApprovedUsersID = @UnApprovedUsersID,UnApprovedTime = @UnApprovedTime,LastUpdate = @LastUpdate " +
                        "where HighRiskMonitoringPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _highRiskMonitoring.HighRiskMonitoringPK);
                        cmd.Parameters.AddWithValue("@historyPK", _highRiskMonitoring.HistoryPK);
                        cmd.Parameters.AddWithValue("@UnApprovedUsersID", _highRiskMonitoring.UnApprovedUsersID);
                        cmd.Parameters.AddWithValue("@UnApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<HighRiskMonitoring> HighRiskMonitoring_SelectHighRiskMonitoringDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<HighRiskMonitoring> L_HighRiskMonitoring = new List<HighRiskMonitoring>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = @"
                            select case when H.Status=1 then 'PENDING' else case when H.Status = 2 then 'APPROVED' else case when H.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,C.BitIsSuspend,C.InvestorType,
                            CASE WHEN H.HighRiskType = 2 THEN ''
                            ELSE
	                            CASE WHEN C.InvestorType = '1' THEN 'Individual'
			                            WHEN C.InvestorType = '2' THEN 'Corporate'
				                            END 
                            END As ClientType,                            
                            CASE WHEN H.HighRiskType = 2 THEN 'FUND : ' + F.ID + ' - ' + F.Name + ' ,INSTRUMENT : ' + G.ID + ' - ' + G.Name 
								 WHEN H.HighRiskType = 3 THEN 'FUND : ' + F.ID + ' - ' + F.Name + ' ,FUND CLIENT : ' + C.ID + ' - ' + C.Name
							ELSE C.ID + ' - ' + C.Name END FundClientID, 
                            CASE WHEN H.HighRiskType = 2 THEN '' ELSE C.Name END FundClientName,
                            H.FundClientPK InvestmentNo,
                             H.* 
                            from HighRiskMonitoring H  
							left join Investment I on H.FundClientPK = I.InvestmentPK and I.StatusInvestment in (1,2)
							left join ClientSubscription CS on H.FundClientPK = CS.ClientSubscriptionPK and CS.Status in (1,2)
							left join Fund F on F.FundPK = case when H.HighRiskType = 2 then I.FundPK when H.HighRiskType = 3 then CS.FundPK end and F.Status = 2
							left join Instrument G on I.InstrumentPK = G.InstrumentPK and G.Status = 2
                            left join FundClient C on C.FundClientPK = case when H.HighRiskType = 0 then H.FundClientPK when H.HighRiskType = 3 then CS.FundClientPK end and C.Status in (1,2)
                            where H.status = @status and CONVERT(VARCHAR(10),date,101) between @DateFrom and @DateTo 
                            order by H.HighRiskMonitoringPK, H.Date ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = cmd.CommandText = @"
                            select case when H.Status=1 then 'PENDING' else case when H.Status = 2 then 'APPROVED' else case when H.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,C.BitIsSuspend,C.InvestorType,
                            CASE WHEN H.HighRiskType = 2 THEN ''
                            ELSE
	                            CASE WHEN C.InvestorType = '1' THEN 'Individual'
			                            WHEN C.InvestorType = '2' THEN 'Corporate'
				                            END 
                            END As ClientType,                            
                            CASE WHEN H.HighRiskType = 2 THEN '' ELSE C.ID END FundClientID, 
                            CASE WHEN H.HighRiskType = 2 THEN '' ELSE C.Name END FundClientName,
                            CASE WHEN H.HighRiskType <> 0 THEN H.FundClientPK ELSE 0 END InvestmentNo,
                             H.* 
                            from HighRiskMonitoring H 
                            left join FundClient C on C.FundClientPK = H.FundClientPK and C.Status in (1,2) 
                            where CONVERT(VARCHAR(10),date,101) between @DateFrom and @DateTo 
                            order by H.HighRiskMonitoringPK, H.Date";
                        }


                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_HighRiskMonitoring.Add(setHighRiskMonitoring(dr));
                                }
                            }
                            return L_HighRiskMonitoring;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<HighRiskMonitoring> HighRiskMonitoring_SelectHighRiskMonitoringDateFromToCustomClient20(int _status, DateTime _dateFrom, DateTime _dateTo, int param6)
        {
            try
            {
                string _paramHighRiskType = "";

                if (param6 == 0)
                {
                    _paramHighRiskType = " ";
                }
                else if (param6 == 1)
                {
                    _paramHighRiskType = "and H.HighRiskType in (97,98,99) ";
                }
                else
                {
                    _paramHighRiskType = "and H.HighRiskType not in (97,98,99) ";
                }

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<HighRiskMonitoring> L_HighRiskMonitoring = new List<HighRiskMonitoring>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = @"
                            select case when H.Status=1 then 'PENDING' else case when H.Status = 2 then 'APPROVED' else case when H.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,C.BitIsSuspend,C.InvestorType,
                            CASE WHEN H.HighRiskType = 2 THEN ''
                            ELSE
	                            CASE WHEN C.InvestorType = '1' THEN 'Individual'
			                            WHEN C.InvestorType = '2' THEN 'Corporate'
				                            END 
                            END As ClientType,                            
                            CASE WHEN H.HighRiskType = 2 THEN '' ELSE C.ID + '-' +C.Name END FundClientID, 
                            CASE WHEN H.HighRiskType = 2 THEN '' ELSE C.Name END FundClientName,
                            CASE WHEN H.HighRiskType <> 0 THEN H.FundClientPK ELSE 0 END InvestmentNo,D.DescOne HighRiskMonStatusDesc,

                             H.* 
                            from HighRiskMonitoring H 
                            left join FundClient C on C.FundClientPK = H.FundClientPK and C.Status in (1,2) 
                            left join MasterValue D on H.HighRiskMonStatus = D.Code and D.Status in (1,2) and D.ID = 'HighRiskMonStatus' 
                            where H.status = @status and CONVERT(VARCHAR(10),date,101) between @DateFrom and @DateTo " + _paramHighRiskType + @"
                            order by H.HighRiskMonitoringPK, H.Date ";
                            cmd.Parameters.AddWithValue("@status", _status);

                        }
                        else
                        {
                            cmd.CommandText = cmd.CommandText = @"
                            select case when H.Status=1 then 'PENDING' else case when H.Status = 2 then 'APPROVED' else case when H.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,C.BitIsSuspend,C.InvestorType,
                            CASE WHEN H.HighRiskType = 2 THEN ''
                            ELSE
	                            CASE WHEN C.InvestorType = '1' THEN 'Individual'
			                            WHEN C.InvestorType = '2' THEN 'Corporate'
				                            END 
                            END As ClientType,                            
                            CASE WHEN H.HighRiskType = 2 THEN '' ELSE C.ID  + '-' +C.Name END FundClientID, 
                            CASE WHEN H.HighRiskType = 2 THEN '' ELSE C.Name END FundClientName,
                            CASE WHEN H.HighRiskType <> 0 THEN H.FundClientPK ELSE 0 END InvestmentNo,D.DescOne HighRiskMonStatusDesc,
                             H.* 
                            from HighRiskMonitoring H 
                            left join FundClient C on C.FundClientPK = H.FundClientPK and C.Status in (1,2) 
                            left join MasterValue D on H.HighRiskMonStatus = D.Code and D.Status in (1,2) and D.ID = 'HighRiskMonStatus' 
                            where CONVERT(VARCHAR(10),date,101) between @DateFrom and @DateTo " + _paramHighRiskType + @"
                            order by H.HighRiskMonitoringPK, H.Date";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_HighRiskMonitoring.Add(setHighRiskMonitoring(dr));
                                }
                            }
                            return L_HighRiskMonitoring;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public string ValidateCustomClient08(ClientSubscription _ClientSubs)
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
                        Create Table #HighRiskTemp
                        (Description nvarchar(MAX))
                        DECLARE @combinedString VARCHAR(MAX)   
                        set @combinedString = ''  

                        if (@TotalCashAmount >= (select CapitalPaidIn*0.05 from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 2)) 
                        or ((select CapitalPaidIn*0.05 from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 2) = null)
                        begin
	                        set @combinedString = @combinedString + 'Cash Amount is over 5% CAPITAL PAID IN, MAX : ' + (select cast(cast(CapitalPaidIn*0.05 as numeric(18,0)) as nvarchar) from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 2) + ', '
                        end



                        if (@TotalCashAmount > (select case when Pekerjaan = 1 then 2000000000 when Pekerjaan = 2 then 2000000000 else 0 end from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 1)) 
                        begin
	                        set @combinedString = @combinedString + 'TotalCashAmount is over 2B'
                        end

                        if (@combinedString != '')
                        begin
	                        set @combinedString = SUBSTRING (@combinedString,1,len(@combinedString) - 1)
	                        select @combinedString Result
                        end
                        else
	                        select '' Result

                                        ";

                        cmd.Parameters.AddWithValue("@FundClientPK", _ClientSubs.FundClientPK);
                        cmd.Parameters.AddWithValue("@TotalCashAmount", _ClientSubs.TotalCashAmount);
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

        public string InsertHighRiskMonitoringCustomClient08(string _param1,ClientSubscription _ClientSubs)
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
                        Create Table #HighRiskTemp
                        (Description nvarchar(MAX))
                        DECLARE @combinedString VARCHAR(MAX)   
                        declare @PK int
                        set @combinedString = ''  

                        if (@TotalCashAmount >= (select CapitalPaidIn*0.05 from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 2)) 
                        or ((select CapitalPaidIn*0.05 from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 2) = null)
                        begin
	                        set @combinedString = @combinedString + 'Cash Amount is over 5% CAPITAL PAID IN,Amount : ' + cast(cast(@TotalCashAmount as numeric(18,0)) as nvarchar) +' ,MAX : ' + (select cast(cast(CapitalPaidIn*0.05 as numeric(18,0)) as nvarchar) from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 2) + ', '
                        end

                        if (@TotalCashAmount > (select case when Pekerjaan = 1 then 2000000000 when Pekerjaan = 2 then 2000000000 else 0 end from FundClient where FundClientPK = @FundClientPK and status in (1,2) and InvestorType = 1)) 
                        begin
	                        set @combinedString = @combinedString + 'TotalCashAmount : ' + cast(cast(@TotalCashAmount as numeric(18,0)) as nvarchar) + ', is over 2B'
                        end

                        if (@combinedString != '')
                        begin
                            set @combinedString = SUBSTRING (@combinedString,1,len(@combinedString) - 1)
	                       
	                        select @PK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
	                        set @PK = isnull(@PK,1)
	                        insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,selected,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate)
	                        Select @PK,1,1,0,@FundClientPK,@Date,@combinedString,@UsersID,@LastUpdate,@LastUpdate

                            select @combinedString Result
                        end

                                        ";

                        cmd.Parameters.AddWithValue("@FundClientPK", _ClientSubs.FundClientPK);
                        cmd.Parameters.AddWithValue("@TotalCashAmount", _ClientSubs.TotalCashAmount);
                        cmd.Parameters.AddWithValue("@UsersID", _param1);
                        cmd.Parameters.AddWithValue("@Date", _ClientSubs.ValueDate);
                        cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
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

        public bool CheckHighRiskMonitoringStatus(DateTime _datefrom, DateTime _dateto, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
                }

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select* from HighRiskMonitoring where FundClientPK = (select FundClientPK from ClientSubscription where status = 1 and ValueDate between @datefrom and @dateto and " + paramClientSubscriptionSelected + @" ) and status = 1";
                        cmd.Parameters.AddWithValue("@DateFrom", _datefrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateto);

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

        public HighRiskMonitoring HighRiskMonitoring_CheckMaxUnitFundAndIncomePerAnnum(string _usersID, decimal _cashAmount, int _fundPK, DateTime _valueDate, int _fundClientPK)
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
                        Create Table #Reason (Result int, Reason nvarchar(max))

                        Declare @MaxUnit numeric (18,6)
                        Declare @UnitAmount numeric (18,6)
                        Declare @LastNav numeric (18,6)
                        select @LastNav = [dbo].[FgetLastCloseNav](@ValueDate,@FundPK)
                        select @LastNav = case when @LastNav = 0 then 1000 else @LastNav end 

                        select @UnitAmount = sum(Unit) from (
                        select sum(UnitAmount) Unit from fundclientPosition where FundPK = @FundPK and Date = @ValueDate
                        union all
                        select sum(CashAmount/@LastNav) Unit from ClientSubscription where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
                        union all
                        select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientRedemption where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
                        union all
                        select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKFrom = @FundPK
                        union all
                        select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKTo = @FundPK
                        union all 
                        Select @Amount/@LastNav Unit     
                       
                        ) A
                        Select @MaxUnit = MaxUnits from Fund where FundPK = @FundPK and status in (1,2)

                        IF (@UnitAmount >= @MaxUnit)
                        BEGIN
	                        Insert into #Reason (Result,Reason)
	                        select 1 Result,'Total Unit Subscription Fund : ' + CONVERT(varchar, CAST(@UnitAmount AS money), 1) + ' and Max Unit Fund : ' +  CONVERT(varchar, CAST(@MaxUnit AS money), 1) Reason
                        END
                        declare @IncomePerAnnum numeric(32,0)
                        select @IncomePerAnnum = IncomePerAnnum from (
                        select case when Code = 1 then 9999999 
                        else case when Code = 2 then 50000000 
                        else case when Code = 3 then 100000000 
                        else case when Code = 4 then 500000000
                        else case when Code = 5 then 1000000000
                        else  9990000000 end end end end end  IncomePerAnnum from FundClient A 
                        left join MasterValue B on A.PenghasilanInd = B.Code and B.ID = 'IncomeIND' and B.Status in (1,2)
                        where FundClientPK = @FundClientPK and A.Status =2 and InvestorType = 1
                        union all
                        select  case when Code = 1 then 999999999 
                        else case when Code = 2 then 5000000000 
                        else case when Code = 3 then 10000000000
                        else case when Code = 4 then 50000000000
                        else case when Code = 5 then 100000000000
                        else  99900000000 end end end end end IncomePerAnnum from FundClient A 
                        left join MasterValue B on A.PenghasilanInstitusi = B.Code and B.ID = 'IncomeINS' and B.Status in (1,2)
                        where FundClientPK = @FundClientPK and A.Status = 2 and InvestorType = 2
                        ) A

                        IF (@Amount > @IncomePerAnnum)
                        BEGIN
                        Declare @Reason nvarchar(500)
                        Declare @PK int
                        
                        set @Reason = 'Amount Subscription : ' + CONVERT(varchar, CAST(@Amount AS money), 1) + ' and Max Income Per Annum : ' +  CONVERT(varchar, CAST(@IncomePerAnnum AS money), 1) + ' / Year'

                        Insert into #Reason (Result,Reason)
                        select 1 Result, @Reason


                        END


                        IF EXISTS(select Result,Reason from #Reason)
                        BEGIN
	                        DECLARE @combinedString VARCHAR(MAX)
	                        SELECT @combinedString = COALESCE(@combinedString + '; ', '') + Reason
	                        FROM #Reason
	                        SELECT 1 Result,'Add Cancel, Please Check : ' + @combinedString as Reason

	                        select @PK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
	                        set @PK = isnull(@PK,1)
	                        insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,selected,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate)
	                        Select @PK,1,1,0,@FundClientPK,@valuedate,@combinedString,@UsersID,@valuedate,@LastUpdate
                        END
                        ELSE
                        BEGIN
	                        select 0 Result, '' Reason
                        END
                           ";

                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Amount", _cashAmount);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new HighRiskMonitoring()
                                {
                                    Result = dr["Result"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Result"]),
                                    Reason = dr["Reason"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Reason"]),

                                };
                            }
                            else
                            {
                                return new HighRiskMonitoring()
                                {
                                    Result = 0,
                                    Reason = "",

                                };
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

        public bool HighRiskMonitoring_CheckClientSuspend(int _fundClientPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                            cmd.CommandText = @"
                                Select BitIsSuspend from FundClient where FundClientPK = @FundClientPK and status in (1,2)           
                            ";
                            cmd.Parameters.AddWithValue("@fundClientPK", _fundClientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["BitIsSuspend"]);
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

        public bool HighRiskMonitoring_ValidateDormantClient(int _fundClientPK)
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
                            select  * from fundclient
                            where (isnull(dormantDate,'12/31/2099')  between '02/01/1900'and Getdate()) and BitIsSuspend = 0  and status = 2
                            and FundClientPK = @FundClientPK";
                         cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                         using (SqlDataReader dr = cmd.ExecuteReader())
                         {
                             if (dr.HasRows)
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

        public HighRiskMonitoring InsertHighRiskMonitoring_ExposurePreTrade(string _usersID, HighRiskMonitoring _highRisk)
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
                            

                        --declare @InvestmentPK int
--declare @Type int
--declare @valuedate date
--declare @UsersID nvarchar(200)
--declare @lastupdate datetime

--set @InvestmentPK = 1
--set @Type = 2
--set @valuedate = '2020-04-27'
--set @UsersID = 'admin'
--set @lastupdate = getdate()


DECLARE @Reason nvarchar(1000)
                        DECLARE @Alert nvarchar(50)
                        DECLARE @PK int

                        Declare @Exposure int
                        Declare @ExposureID nvarchar(50)
                        Declare @Parameter nvarchar(50)
                        Declare @AlertExposure int
                        Declare @ExposurePercent numeric(22,4)
                        DECLARE @r varchar(50)

                        Declare @InstrumentID nvarchar(50)
                        select @InstrumentID = B.ID from Investment A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                        where InvestmentPK = @InvestmentPK


                        IF EXISTS(select * from ZTEMP_FUNDEXPOSURE where AlertExposure in (2,4,6,8))
                        BEGIN

	                        Declare A Cursor For
		                        select Exposure,ExposureID,ParameterDesc,AlertExposure,ExposurePercent from ZTEMP_FUNDEXPOSURE where AlertExposure in (2,4,6,8) and Exposure not in (4)
		                        union all
		                        select Exposure,ExposureID,ParameterDesc,AlertExposure,ExposurePercent from ZTEMP_FUNDEXPOSURE where AlertExposure in (2,4,6,8) and Exposure in (4) and ParameterDesc = @InstrumentID
	                        Open A
	                        Fetch Next From A
	                        INTO @Exposure,@ExposureID,@Parameter,@AlertExposure,@ExposurePercent    
	                        While @@FETCH_STATUS = 0  
	                        BEGIN
		                        select @Alert = case when @AlertExposure = 2 then 'Max Exposure % ' else case when @AlertExposure = 4 then 'Min Exposure % '
						                        else case when @AlertExposure = 6 then 'Max Value ' else 'Min Value ' end end end
	
		                        select @Reason = 'ExposureID : ' + @ExposureID + ', Parameter : ' + @Parameter + ', Exposure Percent : ' +cast(@ExposurePercent as nvarchar) + ' %'

								set @r = ''

                                SELECT @r = coalesce(@r, '') + n
                                FROM (SELECT top 50 
                                CHAR(number) n FROM
                                master..spt_values
                                WHERE type = 'P' AND 
                                (number between ascii(0) and ascii(9)
                                or number between ascii('A') and ascii('Z')
                                or number between ascii('a') and ascii('z'))
                                ORDER BY newid()) a

								while exists (select * from HighRiskMonitoring where SecurityEmail = @r)
                                begin
	                                SELECT @r = coalesce(@r, '') + n
	                                FROM (SELECT top 50 
	                                CHAR(number) n FROM
	                                master..spt_values
	                                WHERE type = 'P' AND 
	                                (number between ascii(0) and ascii(9)
	                                or number between ascii('A') and ascii('Z')
	                                or number between ascii('a') and ascii('z'))
	                                ORDER BY newid()) a
                                end
								

		                        select @PK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
		                        set @PK = isnull(@PK,1)

		                        insert into HighRiskMonitoring(HighRiskMonitoringPK,HistoryPK,status,HighRiskType,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate,Selected,SecurityEmail,EmailExpiredTime)
		                        Select @PK,1,1,@Type,@InvestmentPK,@valuedate,@Reason,@UsersID,@LastUpdate,@LastUpdate,0,@r,DATEADD(MINUTE," + Tools._EmailSessionTime + @",@lastUpdate)

	                        Fetch next From A   
	                        Into @Exposure,@ExposureID,@Parameter,@AlertExposure,@ExposurePercent               
	                        End                  
	                        Close A                  
	                        Deallocate A 



                        END
                           ";

                        cmd.Parameters.AddWithValue("@ValueDate", _highRisk.Date);
                        cmd.Parameters.AddWithValue("@InvestmentPK", _highRisk.InvestmentPK);
                        cmd.Parameters.AddWithValue("@Type", _highRisk.HighRiskType);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.CommandTimeout = 0;

                        cmd.ExecuteNonQuery();
                    }
                    return null;
                }
            }

            catch (Exception err)
            {
                throw err;
            }

        }

        public string Validate_CheckExposurePreTrade(Investment _investment)
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
                        string _paramInstrumentType = "";
                        string _paramInvestmentPK = "";

                        if (!_host.findString(_investment.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investment.stringInvestmentFrom))
                        {
                            _paramInvestmentPK = " And A.InvestmentPK in (" + _investment.stringInvestmentFrom + ") ";
                        }
                        else
                        {
                            _paramInvestmentPK = "";
                        }

                        if (_investment.FundID != "0")
                        {
                            _paramFund = " And A.FundPK = @FundPK ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        if (_investment.InstrumentTypePK == 2)
                        {
                            _paramInstrumentType = " and InstrumentTypePK not in (1,5,6) ";
                        }
                        else
                        {
                            _paramInstrumentType = " and InstrumentTypePK = @InstrumentTypePK ";
                        }
                        cmd.CommandText = @"

                            Create Table #HighRiskTemp
(Description nvarchar(MAX))
DECLARE @combinedString VARCHAR(MAX)                  
Declare @InvestmentPK int
Declare @BitInvestmentHighRisk bit

DECLARE A CURSOR FOR 
select InvestmentPK,BitInvestmentHighRisk from Investment A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
where StatusInvestment = 1 and ValueDate between @ValueDateFrom and @ValueDateTo and TrxType = @TrxType " + _paramInvestmentPK + _paramInstrumentType + _paramFund + @"
Open A
Fetch Next From A
Into @investmentPK,@BitInvestmentHighRisk

While @@FETCH_STATUS = 0
BEGIN


IF(@BitInvestmentHighRisk = 1)
BEGIN
    if exists(select * from HighRiskMonitoring where status = 1 and FundClientPK = @InvestmentPK and HighRiskType = 2) -- Investment
    BEGIN
        Insert into #HighRiskTemp
        select C.ID + ' - ' + B.ID + ' - ' +  CONVERT(varchar, CAST(A.Amount AS money), 1) from Investment A 
        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2
        left join Fund C on A.FundPK = C.FundPK and C.status = 2
        where InvestmentPK = @InvestmentPK
    END
    
END



Fetch next From A Into @investmentPK,@BitInvestmentHighRisk
END
Close A
Deallocate A 

IF exists(select * from #HighRiskTemp)
BEGIN
SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Description
FROM #HighRiskTemp
SELECT 'Approve Cancel, Please Check High Risk Monitoring in : ' + @combinedString as Result 
END
ELSE
BEGIN
select '' Result
END


                        

                                        ";

                        if (_investment.FundID != "0")
                        {
                            cmd.Parameters.AddWithValue("@FundPK", _investment.FundID);
                        }
                        cmd.Parameters.AddWithValue("@ValueDateFrom", _investment.DateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _investment.DateTo);
                        cmd.Parameters.AddWithValue("@TrxType", _investment.TrxType);
                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _investment.InstrumentTypePK);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

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

        public void HighRiskMonitoring_Reject(HighRiskMonitoring _highRiskMonitoring)
         {
             try
             {
                 DateTime _datetimeNow = DateTime.Now;
                 using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                 {
                     DbCon.Open();
                     using (SqlCommand cmd = DbCon.CreateCommand())
                     {
                         cmd.CommandText = @"
                            Declare @FundClientPK int 
                            Select @FundClientPK = FundClientPK from HighRiskMonitoring where HighRiskMonitoringPK = @PK  and status = 1


                            update HighRiskMonitoring set status = 3,selected = 0,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate 
                            where HighRiskMonitoringPK = @PK and historypk = @historyPK
                         
                            update FundClient set KYCRiskProfile = 3 where status = 2 and FundClientPK = @FundClientPK
                         ";
                         cmd.Parameters.AddWithValue("@PK", _highRiskMonitoring.HighRiskMonitoringPK);
                         cmd.Parameters.AddWithValue("@historyPK", _highRiskMonitoring.HistoryPK);
                         cmd.Parameters.AddWithValue("@VoidUsersID", _highRiskMonitoring.VoidUsersID);
                         cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                         cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                         cmd.ExecuteNonQuery();
                     }
                     using (SqlCommand cmd = DbCon.CreateCommand())
                     {
                         cmd.CommandText = "Update HighRiskMonitoring set status= 2,LastUpdate=@LastUpdate where HighRiskMonitoringPK = @PK and status = 4";
                         cmd.Parameters.AddWithValue("@PK", _highRiskMonitoring.HighRiskMonitoringPK);
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

        public void HighRiskMonitoring_Void(HighRiskMonitoring _highRiskMonitoring)
         {
             try
             {
                 DateTime _datetimeNow = DateTime.Now;
                 using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                 {
                     DbCon.Open();
                     using (SqlCommand cmd = DbCon.CreateCommand())
                     {
                         cmd.CommandText = "update HighRiskMonitoring set status = 3,selected = 0,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                             "where HighRiskMonitoringPK = @PK and historypk = @historyPK";
                         cmd.Parameters.AddWithValue("@PK", _highRiskMonitoring.HighRiskMonitoringPK);
                         cmd.Parameters.AddWithValue("@historyPK", _highRiskMonitoring.HistoryPK);
                         cmd.Parameters.AddWithValue("@VoidUsersID", _highRiskMonitoring.VoidUsersID);
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

        public string Validate_CheckDescription(int _highRiskMonitoringPK, int _historyPK)
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
                        if exists(select * from HighRiskMonitoring where status = 1 and (Description is null or Description = '') and HighRiskMonitoringPK = @HighRiskMonitoringPK and HistoryPK = @HistoryPK)
                        BEGIN
                        SELECT 'Approve Cancel, Please Update Description First!' Result
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END    ";

                         cmd.Parameters.AddWithValue("@HighRiskMonitoringPK", _highRiskMonitoringPK);
                         cmd.Parameters.AddWithValue("@HistoryPK", _historyPK);
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

        public string Validate_CheckDataHighRiskSetup(int _highRiskMonitoringPK, int _historyPK, string _UsersID)
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
                        declare @UsersPK int

                        select @UsersPK = userspk from users where id = @UsersID and status = 2


                        if exists(select * from HighRiskMonitoring a
                        left join investment b on a.FundClientPK = b.InvestmentPK
                        left join HighRiskSetup d on b.FundPK = d.FundPK where HighRiskMonitoringPK = @HighRiskMonitoringPK and a.status = 1 and a.HistoryPK = @HistoryPK and d.UsersPK is null)
                        BEGIN
                        SELECT 'true' Result
                        END
                        ELSE
                        BEGIN
                        if exists(select * from HighRiskSetup a
                        left join investment b on a.FundPK = b.FundPK
                        left join HighRiskMonitoring d on b.InvestmentPK = d.FundClientPK where HighRiskMonitoringPK = @HighRiskMonitoringPK and d.status = 1 and d.HistoryPK = @HistoryPK and a.UsersPK = @UsersPK)
                        BEGIN
                        SELECT 'true' Result
                        END
                        ELSE
                        BEGIN
                        select 'False' Result
                        END
                        END";

                         cmd.Parameters.AddWithValue("@HighRiskMonitoringPK", _highRiskMonitoringPK);
                         cmd.Parameters.AddWithValue("@HistoryPK", _historyPK);
                         cmd.Parameters.AddWithValue("@UsersID", _UsersID);
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

        public void HighRiskMonitoring_SuspendBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
         {
             try
             {
                 DateTime _datetimeNow = DateTime.Now;
                 using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                 {
                     DbCon.Open();
                     using (SqlCommand cmd = DbCon.CreateCommand())
                     {
                         cmd.CommandText = @" Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                        Select @Time,@PermissionID,'HighRiskMonitoring',HighRiskMonitoringPK,1,'Suspend by Selected Data',@UsersID,@IPAddress,@Time  from HighRiskMonitoring where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 
  
                        update FundClient set BitIsSuspend = 1,SuspendBy = @UsersID,SuspendTime = @Time
                        where FundClientPK in 
                        (
                        Select FundClientPK from HighRiskMonitoring where CONVERT(VARCHAR(10),date,101) between @DateFrom and @DateTo and Status = 1 and Selected  = 1
                        ) and status in (1,2) ";

                         cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                         cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                         cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                         cmd.Parameters.AddWithValue("@UsersID", _usersID);
                         cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                         cmd.ExecuteNonQuery();
                     }
                 }
             }
             catch (Exception err)
             {
                 throw err;
             }

         }

        public HighRiskMonitoring InsertHighRiskMonitoring_IncomeExposure(string _usersID, HighRiskMonitoring _highRisk)
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
                            Create Table #Reason (Result int, Reason nvarchar(max))

                            Declare @Nav table
                            (
                            FundPK int,
                            CloseNav numeric(18,8)
                            )

                            Declare @FundPK int
                            Declare @VarAmount numeric(18,4)

                            DECLARE A CURSOR FOR 

                            select distinct FundPK from FundClientPosition where Date =(
	                            Select Max(Date) from FundClientPosition where Date <= @Date and FundClientPK = @FundClientPK
                            ) and FundClientPK = @FundClientPK

                            Open A
                            Fetch Next From A
                            Into @FundPK

                            While @@FETCH_STATUS = 0
                            BEGIN

                            insert into @Nav (FundPK,CloseNav)
                            select FundPK,Nav from CloseNAV
                            where Date =(
	                            Select Max(Date) from CloseNAV where status = 2 and Date <= @Date and FundPK = @FundPK
                            ) and status = 2 and FundPK = @FundPK

                            Fetch next From A Into @FundPK
                            END
                            Close A
                            Deallocate A 



                            select @VarAmount = sum(UnitAmount * CloseNav) + @ParamAmount from FundClientPosition A
                            left join @Nav B on A.FundPK = B.FundPK
                            where A.Date =(
	                            Select Max(Date) from FundClientPosition where Date <= @Date and A.FundPK = B.FundPK
                            ) and FundClientPK = @FundClientPK

                            --select UnitAmount,CloseNAV,@ParamAmount from FundClientPosition A
                            --left join @Nav B on A.FundPK = B.FundPK
                            --where A.Date =(
                            --    Select Max(Date) from FundClientPosition where Date <= @Date and A.FundPK = B.FundPK
                            --) and FundClientPK = @FundClientPK



                            declare @IncomePerAnnum numeric(32,0)
                            select @IncomePerAnnum = IncomePerAnnum * 3 from (
                            select case when Code = 1 then 9999999 
                            else case when Code = 2 then 50000000 
                            else case when Code = 3 then 100000000 
                            else case when Code = 4 then 500000000
                            else case when Code = 5 then 1000000000
                            else  9990000000 end end end end end  IncomePerAnnum from FundClient A 
                            left join MasterValue B on A.PenghasilanInd = B.Code and B.ID = 'IncomeIND' and B.Status in (1,2)
                            where FundClientPK = @FundClientPK and A.Status =2 and InvestorType = 1
                            union all
                            select  case when Code = 1 then 999999999 
                            else case when Code = 2 then 5000000000 
                            else case when Code = 3 then 10000000000
                            else case when Code = 4 then 50000000000
                            else case when Code = 5 then 100000000000
                            else  99900000000 end end end end end IncomePerAnnum from FundClient A 
                            left join MasterValue B on A.PenghasilanInstitusi = B.Code and B.ID = 'IncomeINS' and B.Status in (1,2)
                            where FundClientPK = @FundClientPK and A.Status = 2 and InvestorType = 2
                            ) A

                            IF (@VarAmount > @IncomePerAnnum)
                            BEGIN
	                            Declare @Reason nvarchar(500)
	                            Declare @PK int
                        
	                            set @Reason = 'Total Amount Subscription : ' + CONVERT(varchar, CAST(@VarAmount AS money), 1) + ' and Max Income Per Annum : ' +  CONVERT(varchar, CAST(@IncomePerAnnum AS money), 1) + ' / Year'

	                            Insert into #Reason (Result,Reason)
	                            select 1 Result, @Reason

                            END



                            IF EXISTS(select Result,Reason from #Reason)
                            BEGIN
	                            DECLARE @combinedString VARCHAR(MAX)
	                            SELECT @combinedString = COALESCE(@combinedString + '; ', '') + Reason
	                            FROM #Reason


	                            select @PK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
	                            set @PK = isnull(@PK,1)
	                            insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,selected,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate)
	                            Select @PK,1,1,0,@FundClientPK,@Date,@combinedString,@UsersID,@LastUpdate,@LastUpdate

                                select 1 Result, @combinedString Reason
                            END
                            ELSE
                            BEGIN
                                select 0 Result, '' Reason
                            END

                            



                        
                           ";

                         cmd.Parameters.AddWithValue("@Date", _highRisk.Date);
                         cmd.Parameters.AddWithValue("@FundClientPK", _highRisk.FundClientPK);
                         cmd.Parameters.AddWithValue("@ParamAmount", _highRisk.Amount);
                         cmd.Parameters.AddWithValue("@UsersID", _usersID);
                         cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                         using (SqlDataReader dr = cmd.ExecuteReader())
                         {
                             if (dr.HasRows)
                             {
                                 dr.Read();
                                 return new HighRiskMonitoring()
                                 {
                                     Result = dr["Result"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Result"]),
                                     Reason = dr["Reason"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Reason"]),

                                 };
                             }
                             else
                             {
                                 return new HighRiskMonitoring()
                                 {
                                     Result = 0,
                                     Reason = "",

                                 };
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

        public string Check_CounterpartExposureFromHighRisk(HighRiskMonitoring _highRiskMonitoring)
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

                        
                        Create Table #HighRiskTemp
                        (ID nvarchar(50))
                        
                        Insert Into #HighRiskTemp(ID)
                        select distinct B.ID from HighRiskMonitoring A
                        left join Counterpart B on A.FundClientPK = B.CounterpartPK and B.status = 2
                        left join Investment C on A.FundClientPK = C.CounterpartPK
                        where A.status = 1 and Date between @DateFrom and @DateFrom and HighriskType = 3  and statusInvestment = 2 
                        and statusDealing = 1 and OrderStatus in ('O','P') and ValueDate between @DateFrom and @DateFrom and SelectedDealing = 1

                        if exists(select distinct B.ID from HighRiskMonitoring A
                        left join Counterpart B on A.FundClientPK = B.CounterpartPK and B.status = 2
                        left join Investment C on A.FundClientPK = C.CounterpartPK 
                        where A.status = 1 and Date between @DateFrom and @DateFrom and HighriskType = 3 and statusInvestment = 2 
                        and statusDealing = 1 and OrderStatus in ('O','P') and ValueDate between @DateFrom and @DateFrom and SelectedDealing = 1)
                        BEGIN
                        DECLARE @combinedString VARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + ID
                        FROM #HighRiskTemp
                        SELECT 'Match Cancel, Please Check High Risk Monitoring in Counterpart : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END ";

                         cmd.Parameters.AddWithValue("@DateFrom", _highRiskMonitoring.DateFrom);
                         cmd.Parameters.AddWithValue("@DateTo", _highRiskMonitoring.DateTo);
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

        public HighRiskMonitoring HighRiskMonitoring_CheckHighRiskMonitoringFromSubscription(ClientSubscription _clientSubscription)
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
                        
--declare @ValueDate date
--declare @FundPK int
--declare @Amount numeric(22,8)
--declare @FundClientPK int
--declare @UsersID nvarchar(20)
--declare @LastUpdate datetime

--set @ValueDate = '2020-06-18'
--set @FundPK = 14
--set @FundClientPK = 1172
--set @Amount = 137196
--set @UsersID = 'admin'
--set @LastUpdate = getdate()

--drop table #Reason


Create Table #Reason (Result int, Reason nvarchar(max))

Declare @MaxUnit numeric (18,6)
Declare @UnitAmount numeric (18,6)
Declare @LastNav numeric (18,6)
select @LastNav = [dbo].[FgetLastCloseNav](@ValueDate,@FundPK)
select @LastNav = case when @LastNav = 0 then 1000 else @LastNav end 


-- Check Income Per Annum

select @UnitAmount = sum(Unit) from (
select sum(UnitAmount) Unit from fundclientPosition where FundPK = @FundPK and Date = @ValueDate
union all
select sum(CashAmount/@LastNav) Unit from ClientSubscription where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
union all
select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientRedemption where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
union all
select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKFrom = @FundPK
union all
select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKTo = @FundPK
union all 
Select @Amount/@LastNav Unit     
                       
) A
Select @MaxUnit = MaxUnits from Fund where FundPK = @FundPK and status in (1,2)

IF (@UnitAmount >= @MaxUnit)
BEGIN
	Insert into #Reason (Result,Reason)
	select 1 Result,'Total Unit Subscription Fund : ' + CONVERT(varchar, CAST(@UnitAmount AS money), 1) + ' and Max Unit Fund : ' +  CONVERT(varchar, CAST(@MaxUnit AS money), 1) Reason
END
declare @IncomePerAnnum numeric(32,0)
select @IncomePerAnnum = IncomePerAnnum from (
select case when Code = 1 then 9999999 
else case when Code = 2 then 50000000 
else case when Code = 3 then 100000000 
else case when Code = 4 then 500000000
else case when Code = 5 then 1000000000
else  9990000000 end end end end end  IncomePerAnnum from FundClient A 
left join MasterValue B on A.PenghasilanInd = B.Code and B.ID = 'IncomeIND' and B.Status in (1,2)
where FundClientPK = @FundClientPK and A.Status =2 and InvestorType = 1
union all
select  case when Code = 1 then 999999999 
else case when Code = 2 then 5000000000 
else case when Code = 3 then 10000000000
else case when Code = 4 then 50000000000
else case when Code = 5 then 100000000000
else  99900000000 end end end end end IncomePerAnnum from FundClient A 
left join MasterValue B on A.PenghasilanInstitusi = B.Code and B.ID = 'IncomeINS' and B.Status in (1,2)
where FundClientPK = @FundClientPK and A.Status = 2 and InvestorType = 2
) A

IF (@Amount > @IncomePerAnnum)
BEGIN
Declare @Reason nvarchar(500)
Declare @PK int
                        
set @Reason = 'Amount Subscription : ' + CONVERT(varchar, CAST(@Amount AS money), 1) + ' and Max Income Per Annum : ' +  CONVERT(varchar, CAST(@IncomePerAnnum AS money), 1) + ' / Year'

Insert into #Reason (Result,Reason)
select 1 Result, @Reason


END


-- CheckInvestor And FundRiskProfile
Declare @Reason2 nvarchar(500)


select  @Reason2 =  'Risk Profile and FundProfile Not Match :' + isnull(B.Name,'') + '  Product ID:' + 
isnull(F.ID,'')  + '  ClientID:' + isnull(B.ID,'') + '   InvestorRiskProfile:' +isnull(D.DescOne,'') + '   ProductRiskProfile:' + isnull(E.DescOne,'') 
from  fundClient B 
left join FundRiskProfile C on C.FundPK = @FundPK
left join mastervalue D on B.InvestorsRiskProfile = D.Code and D.ID = 'InvestorsRiskProfile' and D.status = 2
left join mastervalue E on C.RiskProfilePK = E.Code and E.ID = 'InvestorsRiskProfile' and E.status = 2
left join Fund F on C.FundPK = F.FundPK and F.status = 2
where isnull(B.InvestorsRiskProfile,0) <> isnull(C.RiskProfilePK,0)
and isnull(B.InvestorsRiskProfile,0) <> 0
and B.FundClientPK = @FundClientPK

if @Reason2 != ''
begin
	Insert into #Reason (Result,Reason)
	select 1 Result, isnull(@Reason2,'')
end


IF EXISTS(select Result,Reason from #Reason)
BEGIN
	DECLARE @combinedString VARCHAR(MAX)
	SELECT @combinedString = COALESCE(@combinedString + '; ', '') + Reason
	FROM #Reason
	SELECT 1 Result,'Add Cancel, Please Check : ' + @combinedString as Reason

	--select @PK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
	--set @PK = isnull(@PK,1)
	--insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,selected,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate)
	--Select @PK,1,1,0,@FundClientPK,@valuedate,@combinedString,@UsersID,@valuedate,@LastUpdate
END
ELSE
BEGIN
	select 0 Result, '' Reason
END

                           ";

                        cmd.Parameters.AddWithValue("@ValueDate", _clientSubscription.ValueDate);
                        cmd.Parameters.AddWithValue("@FundPK", _clientSubscription.FundPK);
                        cmd.Parameters.AddWithValue("@Amount", _clientSubscription.CashAmount);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientSubscription.FundClientPK);
                        cmd.Parameters.AddWithValue("@UsersID", _clientSubscription.EntryUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.CommandTimeout = 0;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new HighRiskMonitoring()
                                {
                                    Result = dr["Result"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Result"]),
                                    Reason = dr["Reason"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Reason"]),

                                };
                            }
                            else
                            {
                                return new HighRiskMonitoring()
                                {
                                    Result = 0,
                                    Reason = "",

                                };
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

        public HighRiskMonitoring HighRiskMonitoring_InsertHighRiskMonitoringFromSubscription(ClientSubscription _clientSubscription)
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
                        
Create Table #Reason (Result int, Reason nvarchar(max))

Declare @MaxUnit numeric (18,6)
Declare @UnitAmount numeric (18,6)
Declare @LastNav numeric (18,6)
select @LastNav = [dbo].[FgetLastCloseNav](@ValueDate,@FundPK)
select @LastNav = case when @LastNav = 0 then 1000 else @LastNav end 


-- Check Income Per Annum

select @UnitAmount = sum(Unit) from (
select sum(UnitAmount) Unit from fundclientPosition where FundPK = @FundPK and Date = @ValueDate
union all
select sum(CashAmount/@LastNav) Unit from ClientSubscription where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
union all
select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientRedemption where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
union all
select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKFrom = @FundPK
union all
select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKTo = @FundPK
union all 
Select @Amount/@LastNav Unit     
                       
) A
Select @MaxUnit = MaxUnits from Fund where FundPK = @FundPK and status in (1,2)

IF (@UnitAmount >= @MaxUnit)
BEGIN
	Insert into #Reason (Result,Reason)
	select 1 Result,'Total Unit Subscription Fund : ' + CONVERT(varchar, CAST(@UnitAmount AS money), 1) + ' and Max Unit Fund : ' +  CONVERT(varchar, CAST(@MaxUnit AS money), 1) Reason
END
declare @IncomePerAnnum numeric(32,0)
select @IncomePerAnnum = IncomePerAnnum from (
select case when Code = 1 then 9999999 
else case when Code = 2 then 50000000 
else case when Code = 3 then 100000000 
else case when Code = 4 then 500000000
else case when Code = 5 then 1000000000
else  9990000000 end end end end end  IncomePerAnnum from FundClient A 
left join MasterValue B on A.PenghasilanInd = B.Code and B.ID = 'IncomeIND' and B.Status in (1,2)
where FundClientPK = @FundClientPK and A.Status =2 and InvestorType = 1
union all
select  case when Code = 1 then 999999999 
else case when Code = 2 then 5000000000 
else case when Code = 3 then 10000000000
else case when Code = 4 then 50000000000
else case when Code = 5 then 100000000000
else  99900000000 end end end end end IncomePerAnnum from FundClient A 
left join MasterValue B on A.PenghasilanInstitusi = B.Code and B.ID = 'IncomeINS' and B.Status in (1,2)
where FundClientPK = @FundClientPK and A.Status = 2 and InvestorType = 2
) A

IF (@Amount > @IncomePerAnnum)
BEGIN
Declare @Reason nvarchar(500)
Declare @PK int
                        
set @Reason = 'Amount Subscription : ' + CONVERT(varchar, CAST(@Amount AS money), 1) + ' and Max Income Per Annum : ' +  CONVERT(varchar, CAST(@IncomePerAnnum AS money), 1) + ' / Year'

Insert into #Reason (Result,Reason)
select 1 Result, @Reason


END


-- CheckInvestor And FundRiskProfile
Declare @Reason2 nvarchar(500)


select  @Reason2 =  'Risk Profile and FundProfile Not Match :' + B.Name + '  Product ID:' + 
F.ID  + '  ClientID:' +B.ID + '   InvestorRiskProfile:' +isnull(D.DescOne,'') + '   ProductRiskProfile:' + isnull(E.DescOne,'') 
from  fundClient B 
left join FundRiskProfile C on C.FundPK = @FundPK
left join mastervalue D on B.InvestorsRiskProfile = D.Code and D.ID = 'InvestorsRiskProfile' and D.status = 2
left join mastervalue E on C.RiskProfilePK = E.Code and E.ID = 'InvestorsRiskProfile' and E.status = 2
left join Fund F on C.FundPK = F.FundPK and F.status = 2
where isnull(B.InvestorsRiskProfile,0) <> isnull(C.RiskProfilePK,0)
and isnull(B.InvestorsRiskProfile,0) <> 0
and B.FundClientPK = @FundClientPK

Insert into #Reason (Result,Reason)
select 1 Result, isnull(@Reason2,'')




IF EXISTS(select Result,Reason from #Reason)
BEGIN
	DECLARE @combinedString VARCHAR(MAX)
	SELECT @combinedString = COALESCE(@combinedString + '; ', '') + Reason
	FROM #Reason
	SELECT 1 Result,'Add Cancel, Please Check : ' + @combinedString as Reason

	select @PK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
	set @PK = isnull(@PK,1)
	insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,selected,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate)
	Select @PK,1,1,0,@FundClientPK,@valuedate,@combinedString,@UsersID,@valuedate,@LastUpdate
END
ELSE
BEGIN
	select 0 Result, '' Reason
END

                           ";

                        cmd.Parameters.AddWithValue("@ValueDate", _clientSubscription.ValueDate);
                        cmd.Parameters.AddWithValue("@FundPK", _clientSubscription.FundPK);
                        cmd.Parameters.AddWithValue("@Amount", _clientSubscription.CashAmount);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientSubscription.FundClientPK);
                        cmd.Parameters.AddWithValue("@UsersID", _clientSubscription.EntryUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new HighRiskMonitoring()
                                {
                                    Result = dr["Result"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Result"]),
                                    Reason = dr["Reason"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Reason"]),

                                };
                            }
                            else
                            {
                                return new HighRiskMonitoring()
                                {
                                    Result = 0,
                                    Reason = "",

                                };
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

        public int HighRiskMonitoring_ApprovedFromEmail(int _InvestmentPK, string _SecurityEmail)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" 
                        declare @result int

                        select @result = case when EmailExpiredTime >= getdate() then 1 else 0 end from HighRiskMonitoring 
                        where FundClientPK = @InvestmentPK and status = 1 and SecurityEmail = @SecurityEmail --and EmailExpiredTime >= getdate()

                        update HighRiskMonitoring set status = 2,selected = 0,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate = @Time 
                        where FundClientPK = @InvestmentPK and status = 1 and SecurityEmail = @SecurityEmail and EmailExpiredTime >= getdate()

                        select isnull(@result,2) Result
                        ";

                        cmd.Parameters.AddWithValue("@InvestmentPK", _InvestmentPK);
                        cmd.Parameters.AddWithValue("@SecurityEmail", _SecurityEmail);
                        cmd.Parameters.AddWithValue("@UsersID", Tools._EmailHighRiskMonitoring);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                int A = Convert.ToInt32(dr["Result"]);
                                return A;
                            }
                            else
                                return 0;
                        }
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int HighRiskMonitoring_RejectFromEmail(int _InvestmentPK, string _SecurityEmail)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" 
                        declare @result int

                        select @result = case when EmailExpiredTime >= getdate() then 1 else 0 end from HighRiskMonitoring 
                        where FundClientPK = @InvestmentPK and status = 1 and SecurityEmail = @SecurityEmail --and EmailExpiredTime >= getdate()

                        update HighRiskMonitoring set status = 3,selected = 0,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate = @Time 
                        where FundClientPK = @InvestmentPK and status = 1 and SecurityEmail = @SecurityEmail and EmailExpiredTime >= getdate()

                        select isnull(@result,2) Result
                        ";

                        cmd.Parameters.AddWithValue("@InvestmentPK", _InvestmentPK);
                        cmd.Parameters.AddWithValue("@SecurityEmail", _SecurityEmail);
                        cmd.Parameters.AddWithValue("@UsersID", Tools._EmailHighRiskMonitoring);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["Result"]);
                            }
                            else
                                return 0;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string EmailExposure(string _usersID, string _sessionID, HighRiskMonitoring _highRisk)
        {
            try
            {
                string _approved, _reject;
                _approved = Tools._urlAPIHighRiskMonitoring + "/Radsoft/HighRiskMonitoring/ApprovedFromEmail/";
                _reject = Tools._urlAPIHighRiskMonitoring + "/Radsoft/HighRiskMonitoring/RejectFromEmail/";
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            declare @BodyMessage nvarchar(max)
                            declare @SecurityEmail nvarchar(50)
                            declare @UsersID nvarchar(50)

                            set @BodyMessage = ''

                            select @SecurityEmail = SecurityEmail, @UsersID = EntryUsersID from HighRiskMonitoring where FundClientPK = @InvestmentPK and HighRiskType = 2  and status = 1 and EmailExpiredTime >= getdate()

                            if (@ClientCode = '17')
                            begin
                                select @BodyMessage = @BodyMessage + Reason
                                 + '<br /><br /> '
                                 from HighRiskMonitoring where date = @date and FundClientPK = @InvestmentPK and HighRiskType = 2 and description = 'Range Price'
                            end
                            else
                            begin
                                select @BodyMessage = @BodyMessage + Reason
                                 + '<br /><br /> Approved <a href=" + _approved + @"'+ cast(@InvestmentPK as nvarchar) +'/' + @SecurityEmail +' target=_blank>Click Here</a> | Reject ' +
                                 '<a href=" + _reject + @"' + '/' + cast(@InvestmentPK as nvarchar) + '/' + @SecurityEmail + ' target=_blank>Click Here</a>'
                                 from HighRiskMonitoring where date = @date and FundClientPK = @InvestmentPK and HighRiskType = 2 and description = 'Range Price'
                            end

                            

                            Select @BodyMessage BodyMessage
                           ";

                        cmd.Parameters.AddWithValue("@date", _highRisk.Date);
                        cmd.Parameters.AddWithValue("@InvestmentPK", _highRisk.InvestmentPK);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["BodyMessage"].ToString();
                            }
                            else
                            {
                                return "";
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

        public string EmailExposureByImport(string _Date)
        {
            try
            {
                string _approved, _reject;
                _approved = Tools._urlAPIHighRiskMonitoring + "/Radsoft/HighRiskMonitoring/ApprovedFromEmail/";
                _reject = Tools._urlAPIHighRiskMonitoring + "/Radsoft/HighRiskMonitoring/RejectFromEmail/";
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            declare @BodyMessage nvarchar(max)
                            declare @SecurityEmail nvarchar(50)
                            declare @UsersID nvarchar(50)

                            set @BodyMessage = ''

							DECLARE 
								@InvestmentPK int,
								@HighRiskMonitoringPK int

							DECLARE A CURSOR FOR 
								select FundClientPK,HighRiskMonitoringPK from HighRiskMonitoring where status = 1 and SecurityEmail is null and EmailExpiredTime is null

							OPEN A;

							FETCH NEXT FROM A INTO @InvestmentPK, @HighRiskMonitoringPK

							WHILE @@FETCH_STATUS = 0
								BEGIN

									set @SecurityEmail = ''

									SELECT @SecurityEmail = coalesce(@SecurityEmail, '') + n
									FROM (SELECT top 50 
									CHAR(number) n FROM
									master..spt_values
									WHERE type = 'P' AND 
									(number between ascii(0) and ascii(9)
									or number between ascii('A') and ascii('Z')
									or number between ascii('a') and ascii('z'))
									ORDER BY newid()) a

									while exists (select * from HighRiskMonitoring where SecurityEmail = @SecurityEmail)
									begin
										SELECT @SecurityEmail = coalesce(@SecurityEmail, '') + n
										FROM (SELECT top 50 
										CHAR(number) n FROM
										master..spt_values
										WHERE type = 'P' AND 
										(number between ascii(0) and ascii(9)
										or number between ascii('A') and ascii('Z')
										or number between ascii('a') and ascii('z'))
										ORDER BY newid()) a
									end

									update HighRiskMonitoring set SecurityEmail = @SecurityEmail, EmailExpiredTime = DATEADD(MINUTE," + Tools._EmailSessionTime + @",getdate())
									where date = @date and FundClientPK = @InvestmentPK and HighRiskType = 2 and HighRiskMonitoringPK = @HighRiskMonitoringPK

                                    if (@ClientCode = '17')
                                    begin
                                        select @BodyMessage = @BodyMessage + Reason
									     + '<br /><br /> '
									     from HighRiskMonitoring where date = @date and FundClientPK = @InvestmentPK and HighRiskType = 2 and HighRiskMonitoringPK = @HighRiskMonitoringPK
                                    end
                                    else
                                    begin
                                        select @BodyMessage = @BodyMessage + Reason
									     + '<br /><br /> Approved <a href=" + _approved + @"'+ cast(@InvestmentPK as nvarchar) +'/' + @SecurityEmail +' target=_blank>Click Here</a> | Reject ' +
									     '<a href=" + _reject + @"' + '/' + cast(@InvestmentPK as nvarchar) + '/' + @SecurityEmail + ' target=_blank>Click Here</a> <br /><br />'
									     from HighRiskMonitoring where date = @date and FundClientPK = @InvestmentPK and HighRiskType = 2 and HighRiskMonitoringPK = @HighRiskMonitoringPK
                                    end

									

									 --select reason from HighRiskMonitoring where date = @date and FundClientPK = @InvestmentPK and HighRiskType = 2 and HighRiskMonitoringPK = @HighRiskMonitoringPK

									FETCH NEXT FROM A INTO @InvestmentPK, @HighRiskMonitoringPK
								END;

							CLOSE A;

							DEALLOCATE A;


                            Select @BodyMessage BodyMessage
                           ";

                        cmd.Parameters.AddWithValue("@date", _Date);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["BodyMessage"].ToString();
                            }
                            else
                            {
                                return "";
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

        public string EmailFundExposure(string _usersID, string _sessionID, HighRiskMonitoring _highRisk)
        {
            try
            {
                string _approved, _reject;
                _approved = Tools._urlAPIHighRiskMonitoring + "/Radsoft/HighRiskMonitoring/ApprovedFromEmail/";
                _reject = Tools._urlAPIHighRiskMonitoring + "/Radsoft/HighRiskMonitoring/RejectFromEmail/";
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            declare @BodyMessage nvarchar(max)
                            declare @SecurityEmail nvarchar(50)
                            declare @ParameterDesc nvarchar(200)
                            declare @ExposurePercent numeric(22,4)
                            declare @MaxExposurePercent numeric(22,4)

                            set @BodyMessage = ''


							DECLARE A CURSOR FOR 
								select B.ParameterDesc,B.ExposurePercent,B.MaxExposurePercent,A.SecurityEmail from HighRiskMonitoring A
                                left join ZTEMP_FUNDEXPOSURE B on cast('ExposureID : ' + B.ExposureID + ', Parameter : ' + B.ParameterDesc + ', Exposure Percent : ' + cast(cast(B.ExposurePercent as numeric(22,4)) as nvarchar) + ' %' as nvarchar(max)) = A.Reason
                                where status = 1 and HighRiskType = 2 and Description is null and date = @date and FundClientPK = @InvestmentPK

							OPEN A;

							FETCH NEXT FROM A INTO @ParameterDesc,@ExposurePercent,@MaxExposurePercent,@SecurityEmail

							WHILE @@FETCH_STATUS = 0
								BEGIN
                                    if (@ClientCode = '17')
                                    begin
                                        select @BodyMessage = @BodyMessage + @ParameterDesc + ' <br /> Trx Date&emsp;&emsp;&nbsp;&nbsp;: ' + convert(nvarchar,cast(@date as date),106) + ' <br /> Exposure&emsp;&emsp;&nbsp;: ' + cast(@ExposurePercent as nvarchar) 
                                        + ' % <br /> Max Exposure&nbsp;: ' +  cast(@MaxExposurePercent as nvarchar) + ' % <br /> Entry&emsp;&emsp;&emsp;&emsp;: 
                                        ' + @UsersID + '<br /><br />'
                                    end
                                    else
                                    begin
                                        select @BodyMessage = @BodyMessage + @ParameterDesc + ' <br /> Trx Date&emsp;&emsp;&nbsp;&nbsp;: ' + convert(nvarchar,cast(@date as date),106) + ' <br /> Exposure&emsp;&emsp;&nbsp;: ' + cast(@ExposurePercent as nvarchar) 
                                        + ' % <br /> Max Exposure&nbsp;: ' +  cast(@MaxExposurePercent as nvarchar) + ' % <br /> Entry&emsp;&emsp;&emsp;&emsp;: 
                                        ' + @UsersID + '<br /><br /> Approved <a href=" + _approved + @"'+ cast(@InvestmentPK as nvarchar) +'/' + @SecurityEmail +' target=_blank>Click Here</a> | Reject ' +
                                         '<a href=" + _reject + @"' + '/' + cast(@InvestmentPK as nvarchar) + '/' + @SecurityEmail + ' target=_blank>Click Here</a> <br /><br />'
                                    end

									


									FETCH NEXT FROM A INTO @ParameterDesc,@ExposurePercent,@MaxExposurePercent,@SecurityEmail
								END;

							CLOSE A;

							DEALLOCATE A;
                            

                            Select @BodyMessage BodyMessage
                           ";

                        cmd.Parameters.AddWithValue("@date", _highRisk.Date);
                        cmd.Parameters.AddWithValue("@InvestmentPK", _highRisk.InvestmentPK);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.CommandTimeout = 0;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["BodyMessage"].ToString();
                            }
                            else
                            {
                                return "";
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

        public HighRiskMonitoring InsertHighRiskMonitoring_ExposureFromOMSEquity(string _usersID, string _ValueDate)
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
                            

                        
--declare @valuedate date
--declare @UsersID nvarchar(200)
--declare @lastupdate datetime

--set @valuedate = '2020-05-20'
--set @UsersID = 'admin'
--set @lastupdate = getdate()

DECLARE @Reason nvarchar(1000)
DECLARE @Alert nvarchar(50)
DECLARE @PK int
DECLARE @InvestmentPK int
Declare @Exposure int
Declare @ExposureID nvarchar(50)
Declare @Parameter nvarchar(50)
Declare @AlertExposure int
Declare @ExposurePercent numeric(22,4)
DECLARE @r varchar(50)

IF EXISTS(select * from ZTEMP_FUNDEXPOSURE_IMPORT where AlertExposure in (2,4,6,8))
BEGIN

	Declare A Cursor For
		select Exposure,ExposureID,ParameterDesc,AlertExposure,ExposurePercent,InvestmentPK from ZTEMP_FUNDEXPOSURE_IMPORT where AlertExposure in (2,4,6,8)
	Open A
	Fetch Next From A
	INTO @Exposure,@ExposureID,@Parameter,@AlertExposure,@ExposurePercent,@InvestmentPK
	While @@FETCH_STATUS = 0  
	BEGIN
		select @Alert = case when @AlertExposure = 2 then 'Max Exposure % ' else case when @AlertExposure = 4 then 'Min Exposure % '
						else case when @AlertExposure = 6 then 'Max Value ' else 'Min Value ' end end end
	
		select @Reason = 'ExposureID : ' + @ExposureID + ', Parameter : ' + @Parameter + ', Exposure Percent : ' +cast(@ExposurePercent as nvarchar) + ' %'

		set @r = ''

        SELECT @r = coalesce(@r, '') + n
        FROM (SELECT top 50 
        CHAR(number) n FROM
        master..spt_values
        WHERE type = 'P' AND 
        (number between ascii(0) and ascii(9)
        or number between ascii('A') and ascii('Z')
        or number between ascii('a') and ascii('z'))
        ORDER BY newid()) a

		while exists (select * from HighRiskMonitoring where SecurityEmail = @r)
        begin
	        SELECT @r = coalesce(@r, '') + n
	        FROM (SELECT top 50 
	        CHAR(number) n FROM
	        master..spt_values
	        WHERE type = 'P' AND 
	        (number between ascii(0) and ascii(9)
	        or number between ascii('A') and ascii('Z')
	        or number between ascii('a') and ascii('z'))
	        ORDER BY newid()) a
        end
								

		select @PK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
		set @PK = isnull(@PK,1)

		insert into HighRiskMonitoring(HighRiskMonitoringPK,HistoryPK,status,HighRiskType,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate,Selected,SecurityEmail,EmailExpiredTime)
		Select @PK,1,1,2,@InvestmentPK,@valuedate,@Reason,@UsersID,@LastUpdate,@LastUpdate,0,@r,DATEADD(MINUTE," + Tools._EmailSessionTime + @",@lastUpdate)

	Fetch next From A   
	Into @Exposure,@ExposureID,@Parameter,@AlertExposure,@ExposurePercent,@InvestmentPK              
	End                  
	Close A                  
	Deallocate A
END 


                           ";

                        cmd.Parameters.AddWithValue("@ValueDate", _ValueDate);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.CommandTimeout = 0;

                        cmd.ExecuteNonQuery();
                    }
                    return null;
                }
            }

            catch (Exception err)
            {
                throw err;
            }

        }

        public string EmailFundExposureFromImportOMSEquity(string _usersID, string _sessionID, string _ValueDate)
        {
            try
            {
                string _approved, _reject;
                _approved = Tools._urlAPIHighRiskMonitoring + "/Radsoft/HighRiskMonitoring/ApprovedFromEmail/";
                _reject = Tools._urlAPIHighRiskMonitoring + "/Radsoft/HighRiskMonitoring/RejectFromEmail/";
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            declare @BodyMessage nvarchar(max)
                            declare @SecurityEmail nvarchar(50)
                            declare @ParameterDesc nvarchar(200)
                            declare @ExposurePercent numeric(22,4)
                            declare @MaxExposurePercent numeric(22,4)
                            declare @InvestmentPK int

                            set @BodyMessage = ''


							DECLARE A CURSOR FOR 
								select B.ParameterDesc,B.ExposurePercent,B.MaxExposurePercent,A.SecurityEmail,B.InvestmentPK from HighRiskMonitoring A
                                left join ZTEMP_FUNDEXPOSURE_IMPORT B on cast('ExposureID : ' + B.ExposureID + ', Parameter : ' + B.ParameterDesc + ', Exposure Percent : ' + cast(cast(B.ExposurePercent as numeric(22,4)) as nvarchar) + ' %' as nvarchar(max)) = A.Reason
                                where status = 1 and HighRiskType = 2 and Description is null and date = @date

							OPEN A;

							FETCH NEXT FROM A INTO @ParameterDesc,@ExposurePercent,@MaxExposurePercent,@SecurityEmail,@InvestmentPK

							WHILE @@FETCH_STATUS = 0
								BEGIN
                                    if (@ClientCode = '17')
                                    begin
                                        select @BodyMessage = @BodyMessage + @ParameterDesc + ' <br /> Trx Date&emsp;&emsp;&nbsp;&nbsp;: ' + convert(nvarchar,cast(@date as date),106) + ' <br /> Exposure&emsp;&emsp;&nbsp;: ' + cast(@ExposurePercent as nvarchar) 
                                        + ' % <br /> Max Exposure&nbsp;: ' +  cast(@MaxExposurePercent as nvarchar) + ' % <br /> Entry&emsp;&emsp;&emsp;&emsp;: 
                                        ' + @UsersID + '<br /><br />'
                                    end
                                    else
                                    begin
                                        select @BodyMessage = @BodyMessage + @ParameterDesc + ' <br /> Trx Date&emsp;&emsp;&nbsp;&nbsp;: ' + convert(nvarchar,cast(@date as date),106) + ' <br /> Exposure&emsp;&emsp;&nbsp;: ' + cast(@ExposurePercent as nvarchar) 
                                        + ' % <br /> Max Exposure&nbsp;: ' +  cast(@MaxExposurePercent as nvarchar) + ' % <br /> Entry&emsp;&emsp;&emsp;&emsp;: 
                                        ' + @UsersID + '<br /><br /> Approved <a href=" + _approved + @"'+ cast(@InvestmentPK as nvarchar) +'/' + @SecurityEmail +' target=_blank>Click Here</a> | Reject ' +
                                         '<a href=" + _reject + @"' + '/' + cast(@InvestmentPK as nvarchar) + '/' + @SecurityEmail + ' target=_blank>Click Here</a> <br /><br />'
                                    end

									


									FETCH NEXT FROM A INTO @ParameterDesc,@ExposurePercent,@MaxExposurePercent,@SecurityEmail,@InvestmentPK
								END;

							CLOSE A;

							DEALLOCATE A;
                            

                            Select @BodyMessage BodyMessage
                           ";

                        cmd.Parameters.AddWithValue("@date", _ValueDate);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.CommandTimeout = 0;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["BodyMessage"].ToString();
                            }
                            else
                            {
                                return "";
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

        public int HighRiskMonitoring_ValidationHighRiskNikko(int _fundClientPK, int _UnitRegistryType)
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
                            if exists ( select fundclientpk from HighRiskMonitoring where HighRiskType = 98 and status in (1,2) and FundClientPK = @FundClientPK)
                            begin
	                            select 1 Result
                            end
                            else if @UnitRegistryType = 1 and exists (select fundclientpk from FundClient where FundClientpk = @FundClientPK and status in (1,2) and (IFUACode = '' or SID = ''))
	                            select 2 Result
                            else
	                            select 0 Result

";
                        cmd.Parameters.AddWithValue("@UnitRegistryType", _UnitRegistryType);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["Result"]);
                            }
                            else
                            {
                                return 0;
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

        public string UpdateKYCFromHighRiskMonitoring(string _param1, HighRiskMonitoring _HighRiskMonitoring)
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
                            declare @FundClientPK int
                            declare @message nvarchar(1000)

                            select @FundClientPK = FundClientPK from HighRiskMonitoring where HighRiskMonitoringPK = @HighRiskMonitoringPK and HistoryPK = @HistoryPK

                            select @message = 'KYC before : ' + B.DescOne from FundClient A
                            left join MasterValue B on A.KYCRiskProfile = B.Code and B.ID = 'KYCRiskProfile' and B.status in (1,2)
                            where A.FundClientPK = @FundClientPK and A.Status in (1,2)

                            update HighRiskMonitoring set Notes = @Notes, LastUpdate = @LastUpdate, UpdateUsersID = @UsersID, UpdateTime = @LastUpdate where HighRiskMonitoringPK = @HighRiskMonitoringPK and HistoryPK = @HistoryPK

                            update FundClient set KYCRiskProfile = @KYCRiskProfile, Notes = @message, LastUpdate = @LastUpdate, UpdateUsersID = @UsersID, UpdateTime = @LastUpdate where FundClientPK = @FundClientPK and status in (1,2)

                            select 'Success' Result
                                        ";

                        cmd.Parameters.AddWithValue("@HighRiskMonitoringPK", _HighRiskMonitoring.HighRiskMonitoringPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _HighRiskMonitoring.HistoryPK);
                        cmd.Parameters.AddWithValue("@KYCRiskProfile", _HighRiskMonitoring.KYCRiskProfile);
                        cmd.Parameters.AddWithValue("@UsersID", _param1);
                        cmd.Parameters.AddWithValue("@Notes", _HighRiskMonitoring.Notes);
                        cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
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

        public string HighRiskMonitoring_Check100MilClient(DateTime _dateFrom)
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
                            Declare @DateMinOne datetime

                            set @DateMinOne = dbo.FWorkingDay(@Date,-1)

                            insert into ZRDO_SWIVEL_HELPDESK(Radsource,ticketpriorities,ticketstatus,ticketcategories,ticket_title,description,source,cf_962,cf_968,cf_976,cf_1267,bitsent)
                            Select 
	                             3,'Urgent','OPEN','Compliance Issue'
		                            ,'Customer total investments > IDR 100M'
		                            ,'NA'
		                            ,'TA'
		                            ,'NA','NA',B.FrontID,'Total Balance : ' +  cast(sum(isnull(A.UnitAmount * isnull(C.NAV,D.NAV),0)) as nvarchar(100)) ,0
                            From FundClientPosition A
                            LEFT JOIN FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                            LEFT JOIN CloseNAV C on A.FundPK = C.FundPK and C.status in (1,2) and C.Date = @Date
                            LEFT JOIN CloseNAV D on A.FundPK = D.FundPK and D.status in (1,2) and D.Date = @DateMinOne
                            Where A.Date = @Date and isnull(B.FrontID,'') <> ''
                            group by B.FrontID
                            HAVING sum(isnull(A.UnitAmount * isnull(C.NAV,D.NAV),0)) > 100000000

                            select 'GENERATE SUCCESS' Result

";
                        cmd.Parameters.AddWithValue("@Date", _dateFrom);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["Result"].ToString();
                            }
                            else
                            {
                                return "";
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

        public string HighRiskMonitoring_ValidateInvestorAndFundRiskProfile(int _fundPK, int _fundClientPK, string _usersID)
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
                        Declare @Reason nvarchar(500)


                        select  @Reason =  'Risk Profile and FundProfile Not Match :' + B.Name + '  Product ID:' + 
                        F.ID  + '  ClientID:' +B.ID + '   InvestorRiskProfile:' +isnull(D.DescOne,'') + '   ProductRiskProfile:' + isnull(E.DescOne,'') 
                        from  fundClient B 
                        left join FundRiskProfile C on C.FundPK = @FundPK
                        left join mastervalue D on B.InvestorsRiskProfile = D.Code and D.ID = 'InvestorsRiskProfile' and D.status = 2
                        left join mastervalue E on C.RiskProfilePK = E.Code and E.ID = 'InvestorsRiskProfile' and E.status = 2
                        left join Fund F on C.FundPK = F.FundPK and F.status = 2
                        where isnull(B.InvestorsRiskProfile,0) <> isnull(C.RiskProfilePK,0)
                        and isnull(B.InvestorsRiskProfile,0) <> 0
                        and B.FundClientPK = @FundClientPK

                        IF (@ClientCode <> '02')
                        BEGIN
                        IF len(@Reason) > 0
                            BEGIN
                                 Select @Reason Reason
                            END
                        END
                        ELSE
                        BEGIN

                        select '' Reason
                        END
                        
                        ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return dr["Reason"].ToString();
                                }
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

        public HighRiskMonitoring HighRiskMonitoring_ValidateMaxUnitFundAndIncomePerAnnum(string _usersID, decimal _cashAmount, int _fundPK, DateTime _valueDate, int _fundClientPK)
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
                        Create Table #Reason (Result int, Reason nvarchar(max))

                        Declare @MaxUnit numeric (18,6)
                        Declare @UnitAmount numeric (18,6)
                        Declare @LastNav numeric (18,6)
                        select @LastNav = [dbo].[FgetLastCloseNav](@ValueDate,@FundPK)
                        select @LastNav = case when @LastNav = 0 then 1000 else @LastNav end 

                        select @UnitAmount = sum(Unit) from (
                        select sum(UnitAmount) Unit from fundclientPosition where FundPK = @FundPK and Date = @ValueDate
                        union all
                        select sum(CashAmount/@LastNav) Unit from ClientSubscription where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
                        union all
                        select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientRedemption where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
                        union all
                        select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKFrom = @FundPK
                        union all
                        select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKTo = @FundPK
                        union all 
                        Select @Amount/@LastNav Unit     
                       
                        ) A
                        Select @MaxUnit = MaxUnits from Fund where FundPK = @FundPK and status in (1,2)

                        IF (@UnitAmount >= @MaxUnit)
                        BEGIN
	                        Insert into #Reason (Result,Reason)
	                        select 1 Result,'Total Unit Subscription Fund : ' + CONVERT(varchar, CAST(@UnitAmount AS money), 1) + ' and Max Unit Fund : ' +  CONVERT(varchar, CAST(@MaxUnit AS money), 1) Reason
                        END
                        declare @IncomePerAnnum numeric(32,0)
                        select @IncomePerAnnum = IncomePerAnnum from (
                        select case when Code = 1 then 9999999 
                        else case when Code = 2 then 50000000 
                        else case when Code = 3 then 100000000 
                        else case when Code = 4 then 500000000
                        else case when Code = 5 then 1000000000
                        else  9990000000 end end end end end  IncomePerAnnum from FundClient A 
                        left join MasterValue B on A.PenghasilanInd = B.Code and B.ID = 'IncomeIND' and B.Status in (1,2)
                        where FundClientPK = @FundClientPK and A.Status =2 and ClientCategory = 1
                        union all
                        select  case when Code = 1 then 999999999 
                        else case when Code = 2 then 5000000000 
                        else case when Code = 3 then 10000000000
                        else case when Code = 4 then 50000000000
                        else case when Code = 5 then 100000000000
                        else  99900000000 end end end end end IncomePerAnnum from FundClient A 
                        left join MasterValue B on A.PenghasilanInstitusi = B.Code and B.ID = 'IncomeINS' and B.Status in (1,2)
                        where FundClientPK = @FundClientPK and A.Status = 2 and ClientCategory = 2
                        ) A

                        IF (@Amount > @IncomePerAnnum)
                        BEGIN
                        Declare @Reason nvarchar(500)
                        Declare @PK int
                        
                        set @Reason = 'Amount Subscription : ' + CONVERT(varchar, CAST(@Amount AS money), 1) + ' and Max Income Per Annum : ' +  CONVERT(varchar, CAST(@IncomePerAnnum AS money), 1) + ' / Year'

                        Insert into #Reason (Result,Reason)
                        select 1 Result, @Reason


                        END


                        IF EXISTS(select Result,Reason from #Reason)
                        BEGIN
	                        DECLARE @combinedString VARCHAR(MAX)
	                        SELECT @combinedString = COALESCE(@combinedString + '; ', '') + Reason
	                        FROM #Reason
	                        SELECT 1 Result,'Add Cancel, Please Check : ' + @combinedString as Reason
                        END
                        ELSE
                        BEGIN
	                        select 0 Result, '' Reason
                        END
                           ";

                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Amount", _cashAmount);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new HighRiskMonitoring()
                                {
                                    Result = dr["Result"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Result"]),
                                    Reason = dr["Reason"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Reason"]),

                                };
                            }
                            else
                            {
                                return new HighRiskMonitoring()
                                {
                                    Result = 0,
                                    Reason = "",

                                };
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

        public void HighRiskMonitoring_InsertHighRiskMonitoringMaxUnitFundAndIncomePerAnnum(string _usersID, HighRiskMonitoring _highRisk)
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
                        Create Table #Reason (Result int, Reason nvarchar(max))

                        Declare @MaxUnit numeric (18,6)
                        Declare @UnitAmount numeric (18,6)
                        Declare @LastNav numeric (18,6)
                        select @LastNav = [dbo].[FgetLastCloseNav](@ValueDate,@FundPK)
                        select @LastNav = case when @LastNav = 0 then 1000 else @LastNav end 

                        select @UnitAmount = sum(Unit) from (
                        select sum(UnitAmount) Unit from fundclientPosition where FundPK = @FundPK and Date = @ValueDate
                        union all
                        select sum(CashAmount/@LastNav) Unit from ClientSubscription where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
                        union all
                        select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientRedemption where status not in (3,4)and ValueDate = @ValueDate and FundPK = @FundPK
                        union all
                        select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) * -1 Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKFrom = @FundPK
                        union all
                        select sum(case when UnitAmount = 0 then CashAmount/@LastNav else UnitAmount End) Unit from ClientSwitching where status not in (3,4)and ValueDate = @ValueDate and FundPKTo = @FundPK
                        union all 
                        Select @Amount/@LastNav Unit     
                       
                        ) A
                        Select @MaxUnit = MaxUnits from Fund where FundPK = @FundPK and status in (1,2)

                        IF (@UnitAmount >= @MaxUnit)
                        BEGIN
	                        Insert into #Reason (Result,Reason)
	                        select 1 Result,'Total Unit Subscription Fund : ' + CONVERT(varchar, CAST(@UnitAmount AS money), 1) + ' and Max Unit Fund : ' +  CONVERT(varchar, CAST(@MaxUnit AS money), 1) Reason
                        END
                        declare @IncomePerAnnum numeric(32,0)
                        select @IncomePerAnnum = IncomePerAnnum from (
                        select case when Code = 1 then 9999999 
                        else case when Code = 2 then 50000000 
                        else case when Code = 3 then 100000000 
                        else case when Code = 4 then 500000000
                        else case when Code = 5 then 1000000000
                        else  9990000000 end end end end end  IncomePerAnnum from FundClient A 
                        left join MasterValue B on A.PenghasilanInd = B.Code and B.ID = 'IncomeIND' and B.Status in (1,2)
                        where FundClientPK = @FundClientPK and A.Status =2 and ClientCategory = 1
                        union all
                        select  case when Code = 1 then 999999999 
                        else case when Code = 2 then 5000000000 
                        else case when Code = 3 then 10000000000
                        else case when Code = 4 then 50000000000
                        else case when Code = 5 then 100000000000
                        else  99900000000 end end end end end IncomePerAnnum from FundClient A 
                        left join MasterValue B on A.PenghasilanInstitusi = B.Code and B.ID = 'IncomeINS' and B.Status in (1,2)
                        where FundClientPK = @FundClientPK and A.Status = 2 and ClientCategory = 2
                        ) A

                        IF (@Amount > @IncomePerAnnum)
                        BEGIN
                        Declare @Reason nvarchar(500)
                        Declare @PK int
                        
                        set @Reason = 'Amount Subscription : ' + CONVERT(varchar, CAST(@Amount AS money), 1) + ' and Max Income Per Annum : ' +  CONVERT(varchar, CAST(@IncomePerAnnum AS money), 1) + ' / Year'

                        Insert into #Reason (Result,Reason)
                        select 1 Result, @Reason


                        END


                        IF EXISTS(select Result,Reason from #Reason)
                        BEGIN
	                        DECLARE @combinedString VARCHAR(MAX)
	                        SELECT @combinedString = COALESCE(@combinedString + '; ', '') + Reason
	                        FROM #Reason

	                        select @PK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
	                        set @PK = isnull(@PK,1)
	                        insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,selected,FundClientPK,Date,Reason,Description,EntryUsersID,EntryTime,LastUpdate)
	                        Select @PK,1,1,0,@FundClientPK,@valuedate,@combinedString,@Notes,@UsersID,@valuedate,@LastUpdate
                        END
                           ";

                        cmd.Parameters.AddWithValue("@ValueDate", _highRisk.NAVDate);
                        cmd.Parameters.AddWithValue("@FundPK", _highRisk.FundPK);
                        cmd.Parameters.AddWithValue("@Amount", _highRisk.CashAmount);
                        cmd.Parameters.AddWithValue("@FundClientPK", _highRisk.FundClientPK);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Notes", _highRisk.Notes);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception err)
            {
                throw err;
            }

        }

        public void HighRiskMonitoring_InsertHighRiskMonitoringInvestorAndFundRiskProfile(string _usersID, HighRiskMonitoring _highRisk)
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
                        Declare @Reason nvarchar(500)


                        select  @Reason =  'Risk Profile and FundProfile Not Match :' + B.Name + '  Product ID:' + 
                        F.ID  + '  ClientID:' +B.ID + '   InvestorRiskProfile:' +isnull(D.DescOne,'') + '   ProductRiskProfile:' + isnull(E.DescOne,'') 
                        from  fundClient B 
                        left join FundRiskProfile C on C.FundPK = @FundPK
                        left join mastervalue D on B.InvestorsRiskProfile = D.Code and D.ID = 'InvestorsRiskProfile' and D.status = 2
                        left join mastervalue E on C.RiskProfilePK = E.Code and E.ID = 'InvestorsRiskProfile' and E.status = 2
                        left join Fund F on C.FundPK = F.FundPK and F.status = 2
                        where isnull(B.InvestorsRiskProfile,0) <> isnull(C.RiskProfilePK,0)
                        and isnull(B.InvestorsRiskProfile,0) <> 0
                        and B.FundClientPK = @FundClientPK

                        IF len(@Reason) > 0
                            BEGIN
	                             Declare @PK int
	                             select @PK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
	                             set @PK = isnull(@PK,1)
	                             insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,FundClientPK,Date,Reason,Description,EntryUsersID,LastUpdate,EntryTime)
	                             Select @PK,1,1,@FundClientPK,@Date,@Reason,@Notes,@UsersID,@Date,@Date
                            END
                        END
                        
                        ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _highRisk.FundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _highRisk.FundPK);
                        cmd.Parameters.AddWithValue("@Notes", _highRisk.Notes);
                        cmd.Parameters.AddWithValue("@Date", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public string HighRiskMonitoring_InsertIntoHighRiskMonitoringByClientSubscription(string _userID, DateTime _valueDate, List<ValidateClientSubscription> _Validation)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    foreach (var _obj in _Validation)
                    {
                        ValidateClientSubscription _m = new ValidateClientSubscription();
                        _m.Reason = _obj.Reason;
                        _m.Notes = _obj.Notes;
                        _m.FundClientPK = _obj.FundClientPK;

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {


                            cmd.CommandText = @"
                                                declare @maxPK int

                                                select @maxPK = max(HighRiskMonitoringPK) from HighRiskMonitoring

                                                set @maxPK = isnull(@maxPK,0) + 1 

                                                insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,FundClientPK,Date,Reason,Description,EntryUsersID,LastUpdate,EntryTime)
	                                            Select @maxPK,1,1,@FundClientPK,cast(@ValueDate as date),@Reason,@Notes,@UsersID,@LastUpdate,@LastUpdate
                                          ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@Reason", _m.Reason);
                            cmd.Parameters.AddWithValue("@Notes", _m.Notes);
                            cmd.Parameters.AddWithValue("@FundClientPK", _m.FundClientPK);
                            cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                            cmd.Parameters.AddWithValue("@UsersID", _userID);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();

                        }
                    }


                    return "";
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string HighRiskMonitoring_InsertIntoHighRiskMonitoringByUnitRegistry(string _userID, DateTime _valueDate, List<ValidateUnitRegistry> _Validation)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    foreach (var _obj in _Validation)
                    {
                        ValidateUnitRegistry _m = new ValidateUnitRegistry();
                        _m.Reason = _obj.Reason;
                        _m.Notes = _obj.Notes;
                        _m.FundClientPK = _obj.FundClientPK;

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {


                            cmd.CommandText = @"
                                                declare @maxPK int

                                                select @maxPK = max(HighRiskMonitoringPK) from HighRiskMonitoring

                                                set @maxPK = isnull(@maxPK,0) + 1 

                                                insert into HighRiskMonitoring( HighRiskMonitoringPK,HistoryPK,status,Selected,FundClientPK,Date,Reason,Description,EntryUsersID,LastUpdate,EntryTime)
	                                            Select @maxPK,1,1,0,@FundClientPK,cast(@ValueDate as date),@Reason,@Notes,@UsersID,@LastUpdate,@LastUpdate
                                          ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@Reason", _m.Reason);
                            cmd.Parameters.AddWithValue("@Notes", _m.Notes);
                            cmd.Parameters.AddWithValue("@FundClientPK", _m.FundClientPK);
                            cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                            cmd.Parameters.AddWithValue("@UsersID", _userID);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();

                        }
                    }


                    return "";
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public HighRiskMonitoring InsertHighRiskMonitoring_ExposureFromOMSTimeDeposit(string _usersID, string _ValueDate)
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
                            

                        
--declare @valuedate date
--declare @UsersID nvarchar(200)
--declare @lastupdate datetime

--set @valuedate = '2020-05-20'
--set @UsersID = 'admin'
--set @lastupdate = getdate()

DECLARE @Reason nvarchar(1000)
DECLARE @Alert nvarchar(50)
DECLARE @PK int
DECLARE @InvestmentPK int
Declare @Exposure int
Declare @ExposureID nvarchar(50)
Declare @Parameter nvarchar(50)
Declare @AlertExposure int
Declare @ExposurePercent numeric(22,4)
DECLARE @r varchar(50)

IF EXISTS(select * from ZTEMP_FUNDEXPOSURE_IMPORT_DEPOSITO where AlertExposure in (2,4,6,8))
BEGIN

	Declare A Cursor For
		select Exposure,ExposureID,ParameterDesc,AlertExposure,ExposurePercent,InvestmentPK from ZTEMP_FUNDEXPOSURE_IMPORT_DEPOSITO where AlertExposure in (2,4,6,8)
	Open A
	Fetch Next From A
	INTO @Exposure,@ExposureID,@Parameter,@AlertExposure,@ExposurePercent,@InvestmentPK
	While @@FETCH_STATUS = 0  
	BEGIN
		select @Alert = case when @AlertExposure = 2 then 'Max Exposure % ' else case when @AlertExposure = 4 then 'Min Exposure % '
						else case when @AlertExposure = 6 then 'Max Value ' else 'Min Value ' end end end
	
		select @Reason = 'ExposureID : ' + @ExposureID + ', Parameter : ' + @Parameter + ', Exposure Percent : ' +cast(@ExposurePercent as nvarchar) + ' %'

		set @r = ''

        SELECT @r = coalesce(@r, '') + n
        FROM (SELECT top 50 
        CHAR(number) n FROM
        master..spt_values
        WHERE type = 'P' AND 
        (number between ascii(0) and ascii(9)
        or number between ascii('A') and ascii('Z')
        or number between ascii('a') and ascii('z'))
        ORDER BY newid()) a

		while exists (select * from HighRiskMonitoring where SecurityEmail = @r)
        begin
	        SELECT @r = coalesce(@r, '') + n
	        FROM (SELECT top 50 
	        CHAR(number) n FROM
	        master..spt_values
	        WHERE type = 'P' AND 
	        (number between ascii(0) and ascii(9)
	        or number between ascii('A') and ascii('Z')
	        or number between ascii('a') and ascii('z'))
	        ORDER BY newid()) a
        end
								

		select @PK = Max(highRiskMonitoringPK) + 1 from HighRiskMonitoring
		set @PK = isnull(@PK,1)

		insert into HighRiskMonitoring(HighRiskMonitoringPK,HistoryPK,status,HighRiskType,FundClientPK,Date,Reason,EntryUsersID,EntryTime,LastUpdate,Selected,SecurityEmail,EmailExpiredTime)
		Select @PK,1,1,2,@InvestmentPK,@valuedate,@Reason,@UsersID,@LastUpdate,@LastUpdate,0,@r,DATEADD(MINUTE," + Tools._EmailSessionTime + @",@lastUpdate)

	Fetch next From A   
	Into @Exposure,@ExposureID,@Parameter,@AlertExposure,@ExposurePercent,@InvestmentPK              
	End                  
	Close A                  
	Deallocate A
END 


                           ";

                        cmd.Parameters.AddWithValue("@ValueDate", _ValueDate);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.CommandTimeout = 0;

                        cmd.ExecuteNonQuery();
                    }
                    return null;
                }
            }

            catch (Exception err)
            {
                throw err;
            }

        }

        public string EmailFundExposureFromImportOMSTimeDeposit(string _usersID, string _sessionID, string _ValueDate)
        {
            try
            {
                string _approved, _reject;
                _approved = Tools._urlAPIHighRiskMonitoring + "/Radsoft/HighRiskMonitoring/ApprovedFromEmail/";
                _reject = Tools._urlAPIHighRiskMonitoring + "/Radsoft/HighRiskMonitoring/RejectFromEmail/";
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            declare @BodyMessage nvarchar(max)
                            declare @SecurityEmail nvarchar(50)
                            declare @ParameterDesc nvarchar(200)
                            declare @ExposurePercent numeric(22,4)
                            declare @MaxExposurePercent numeric(22,4)
                            declare @InvestmentPK int

                            set @BodyMessage = ''


							DECLARE A CURSOR FOR 
								select B.ParameterDesc,B.ExposurePercent,B.MaxExposurePercent,A.SecurityEmail,B.InvestmentPK from HighRiskMonitoring A
                                left join ZTEMP_FUNDEXPOSURE_IMPORT_DEPOSITO B on cast('ExposureID : ' + B.ExposureID + ', Parameter : ' + B.ParameterDesc + ', Exposure Percent : ' + cast(cast(B.ExposurePercent as numeric(22,4)) as nvarchar) + ' %' as nvarchar(max)) = A.Reason
                                where status = 1 and HighRiskType = 2 and Description is null and date = @date

							OPEN A;

							FETCH NEXT FROM A INTO @ParameterDesc,@ExposurePercent,@MaxExposurePercent,@SecurityEmail,@InvestmentPK

							WHILE @@FETCH_STATUS = 0
								BEGIN
                                    if (@ClientCode = '17')
                                    begin
                                        select @BodyMessage = @BodyMessage + @ParameterDesc + ' <br /> Trx Date&emsp;&emsp;&nbsp;&nbsp;: ' + convert(nvarchar,cast(@date as date),106) + ' <br /> Exposure&emsp;&emsp;&nbsp;: ' + cast(@ExposurePercent as nvarchar) 
                                        + ' % <br /> Max Exposure&nbsp;: ' +  cast(@MaxExposurePercent as nvarchar) + ' % <br /> Entry&emsp;&emsp;&emsp;&emsp;: 
                                        ' + @UsersID + '<br /><br />'
                                    end
                                    else
                                    begin
                                        select @BodyMessage = @BodyMessage + @ParameterDesc + ' <br /> Trx Date&emsp;&emsp;&nbsp;&nbsp;: ' + convert(nvarchar,cast(@date as date),106) + ' <br /> Exposure&emsp;&emsp;&nbsp;: ' + cast(@ExposurePercent as nvarchar) 
                                        + ' % <br /> Max Exposure&nbsp;: ' +  cast(@MaxExposurePercent as nvarchar) + ' % <br /> Entry&emsp;&emsp;&emsp;&emsp;: 
                                        ' + @UsersID + '<br /><br /> Approved <a href=" + _approved + @"'+ cast(@InvestmentPK as nvarchar) +'/' + @SecurityEmail +' target=_blank>Click Here</a> | Reject ' +
                                         '<a href=" + _reject + @"' + '/' + cast(@InvestmentPK as nvarchar) + '/' + @SecurityEmail + ' target=_blank>Click Here</a> <br /><br />'
                                    end

									


									FETCH NEXT FROM A INTO @ParameterDesc,@ExposurePercent,@MaxExposurePercent,@SecurityEmail,@InvestmentPK
								END;

							CLOSE A;

							DEALLOCATE A;
                            

                            Select @BodyMessage BodyMessage
                           ";

                        cmd.Parameters.AddWithValue("@date", _ValueDate);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.CommandTimeout = 0;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["BodyMessage"].ToString();
                            }
                            else
                            {
                                return "";
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