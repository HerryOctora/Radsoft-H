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
    public class DormantAccountReps
    {
        Host _host = new Host();
        
        private DormantAccount setDormantAccount(SqlDataReader dr)
        {
            DormantAccount M_DormantAccount = new DormantAccount();
            M_DormantAccount.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_DormantAccount.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_DormantAccount.Status = Convert.ToInt32(dr["Status"]);
            M_DormantAccount.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_DormantAccount.FundClientID = Convert.ToString(dr["FundClientID"]);
            M_DormantAccount.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_DormantAccount.BitIsSuspend = Convert.ToBoolean(dr["BitIsSuspend"]);
            M_DormantAccount.LastHoldingDate = dr["LastHoldingDate"].ToString();
            if (_host.CheckColumnIsExist(dr, "CashAmount"))
            {
                M_DormantAccount.CashAmount = Convert.ToDecimal(dr["CashAmount"]);
            }
            if (_host.CheckColumnIsExist(dr, "IFUACode"))
            {
                M_DormantAccount.IFUACode = dr["IFUACode"].ToString();
            }
            if (_host.CheckColumnIsExist(dr, "FundName"))
            {
                M_DormantAccount.FundName = dr["FundName"].ToString();
            }
            if (_host.CheckColumnIsExist(dr, "UnitAmount"))
            {
                M_DormantAccount.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
            }
            if (_host.CheckColumnIsExist(dr, "Nav"))
            {
                M_DormantAccount.Nav = Convert.ToDecimal(dr["Nav"]);
            }
            if (_host.CheckColumnIsExist(dr, "LastNavDate"))
            {
                M_DormantAccount.LastNavDate = dr["LastNavDate"].ToString();
            }
         
            M_DormantAccount.EntryUsersID = dr["EntryUsersID"].ToString();
            M_DormantAccount.EntryTime = dr["EntryTime"].ToString();
            M_DormantAccount.LastUpdate = dr["LastUpdate"].Equals(DBNull.Value) == true ? "" : dr["LastUpdate"].ToString();
            return M_DormantAccount;
        }

        public List<DormantAccount> DormantAccount_SelectDormantAccountDate(int _status, int _month)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DormantAccount> L_DormantAccount = new List<DormantAccount>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                set @ParamMonth = @ParamMonth * -1
                                Select A.FundClientPK,A.HistoryPK,A.ID FundClientID,A.Status Status,a.BitIsSuspend,
                                case when A.Status=1 then 'PENDING' 
                                else case when A.Status = 2 then 'APPROVED' 
                                else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,
                                case when A.DormantDate is null then 
                                (select top 1 Date from FundClientPosition where FundClientPK = A.FundClientPK order by date desc)
                                else case when A.DormantDate < (select top 1 Date from FundClientPosition where FundClientPK = A.FundClientPK order by date desc) then
                                (select top 1 Date from FundClientPosition where FundClientPK = A.FundClientPK order by date desc) else A.DormantDate end end LastHoldingDate,
                                A.Name FundClientName,       
                                A.EntryUsersID EntryUsersID,A.EntryTime EntryTime,A.LastUpdate LastUpdate From FundClient A
                                where  A.FundClientPK not in (     
                                Select distinct FundClientPK From FundClientPosition 
                                where date between DATEADD(month,@ParamMonth,getDate()) and GETDATE()) and A.status = 2 and (select case when A.DormantDate is null then 
                                (select top 1 Date from FundClientPosition where FundClientPK = A.FundClientPK order by date desc)
                                else case when A.DormantDate < (select top 1 Date from FundClientPosition where FundClientPK = A.FundClientPK order by date desc) then
                                (select top 1 Date from FundClientPosition where FundClientPK = A.FundClientPK order by date desc) else A.DormantDate end end) is not null and
                                (select case when A.DormantDate is null then 
                                (select top 1 Date from FundClientPosition where FundClientPK = A.FundClientPK order by date desc)
                                else case when A.DormantDate < (select top 1 Date from FundClientPosition where FundClientPK = A.FundClientPK order by date desc) then
                                (select top 1 Date from FundClientPosition where FundClientPK = A.FundClientPK order by date desc) else A.DormantDate end end) != '1900-01-01')
                            ";
                        cmd.Parameters.AddWithValue("@ParamMonth", _month);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_DormantAccount.Add(setDormantAccount(dr));
                                }
                            }
                            return L_DormantAccount;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<DormantAccount> DormantAccount_SelectDormantAccountValueDate(int _status, DateTime _valueDate, decimal _paramAmount)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DormantAccount> L_DormantAccount = new List<DormantAccount>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        var _suspend = "";
                        if(Tools.ClientCode == "21")
                        {
                            _suspend = " and BitIsSuspend = 1 ";
                        }
                        else
                        {
                            _suspend = "";
                        }

                        if (Tools.ClientCode == "20")
                        {
                            cmd.CommandText =
                            @"
                                DECLARE @MaxDate datetime

                            select @MaxDate = Date from FundClientPosition where Date = (
	                            SELECT MAX(date) from FundClientPosition where Date <= @ValueDate
                            )


                            DECLARE @NAV TABLE
                            (
	                            LastDate DATETIME,
	                            FundPK INT,
	                            NAV NUMERIC(18,4)
                            )


                            DECLARE @NAVLastDate TABLE
                            (
	                            FundPK INT,
	                            LastDate datetime
                            )

                            INSERT INTO @NAVLastDate
                                    ( FundPK, LastDate )
                            SELECT FundPK,MAX(Date) FROM CloseNAV WHERE status  = 2 AND Date <= @ValueDate
                            GROUP BY FundPK


                            INSERT INTO @NAV
                                    ( LastDate, FundPK, NAV )
                            SELECT A.LastDate,A.FundPK,B.Nav FROM @NAVLastDate A
                            LEFT JOIN dbo.CloseNAV B ON A.FundPK = B.FundPK AND A.LastDate = B.Date AND B.status = 2
                            WHERE B.Date IN(
	                            SELECT DISTINCT lastDate FROM @NAVLastDate
                            )


                            Select B.FundClientPK,B.HistoryPK,B.Status,
                            case when B.[Status] = 1 then 'PENDING' 
		                        else 
			                        case when B.[Status] = 2 then 'APPROVED' 
				                        else 
                                            case when B.[Status] = 3 then 'VOID' 
						                        else 'WAITING' end end end as StatusDesc
                            ,B.ID FundClientID, B.Name FundClientName, isnull(B.BitIsSuspend, 0) BitIsSuspend,'' LastHoldingDate, sum(A.UnitAmount * D.NAV) CashAmount,B.EntryUsersID,B.EntryTime,getdate() LastUpdate
                            ,B.IFUACode,C.Name FundName,A.UnitAmount,D.NAV,D.LastDate LastNavDate
                            From FundClientPosition A   
                            Left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status = 2   
                            Left join Fund C on A.FundPK = C.FundPK and C.Status = 2
                            left join MasterValue mv on b.InvestorType = mv.Code  and mv.ID = 'InvestorType'  
                            left join MasterValue mv1 on b.Tipe = mv1.Code  and mv1.ID = 'CompanyType' 
                            left Join @NAV D on A.FundPK = D.FundPK  
                            left Join Currency E on C.CurrencyPK = E.CurrencyPK and E.status = 2
                            where A.Date = @MaxDate   and A.UnitAmount <> 0 
                            group by B.FundClientPK,B.HistoryPK,B.Status,B.ID,B.Name,B.BitIsSuspend,B.EntryUsersID,B.EntryTime,B.IFUACode,C.Name,A.UnitAmount,D.NAV,D.LastDate
                            having sum(A.UnitAmount * D.NAV)  < @ParamAmount 
                            order by CashAmount desc
                            ";

                            cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                            cmd.Parameters.AddWithValue("@ParamAmount", _paramAmount);
                        }
                        else
                        {
                            cmd.CommandText =
                            @"
                                --declare @ValueDate date
                                --	set @ValueDate = '01/15/18'
     
                                select fc.FundClientPK, fc.HistoryPK, fc.[Status],
	                                case when fc.[Status] = 1 
		                                then 'PENDING' 
		                                else 
			                                case when fc.[Status] = 2 
				                                then 'APPROVED' 
				                                else 
					                                case when fc.[Status] = 3 
						                                then 'VOID' 
						                                else 'WAITING' 
					                                end 
			                                end 
	                                end as StatusDesc, 
	                                fc.ID as FundClientID, fc.Name as FundClientName, isnull(fc.BitIsSuspend, 0) as BitIsSuspend,
	                                case when fc.DormantDate is null 
		                                then (
			                                select top 1 [Date] 
			                                from FundClientPosition 
			                                where FundClientPK = fc.FundClientPK 
			                                order by [Date] desc
		                                )
		                                else 
			                                case when fc.DormantDate < (
				                                select top 1 [Date] 
				                                from FundClientPosition 
				                                where FundClientPK = fc.FundClientPK 
				                                order by [Date] desc
			                                ) 
				                                then (
					                                select top 1 [Date]
					                                from FundClientPosition 
					                                where FundClientPK = fc.FundClientPK 
					                                order by [Date] desc
				                                ) 
				                                else fc.DormantDate
			                                end 
	                                end as LastHoldingDate,
	                                fc.EntryUsersID as EntryUsersID, fc.EntryTime as EntryTime, fc.LastUpdate as LastUpdate 
                                from FundClient fc
                                where fc.FundClientPK not in (     
	                                select distinct FundClientPK 
	                                from FundClientPosition 
	                                where [Date] <= @ValueDate and fc.[Status] = 2 
		                                and (
		                                select 
			                                case when fc.DormantDate is null 
				                                then (
					                                select top 1 [Date] 
					                                from FundClientPosition 
					                                where FundClientPK = fc.FundClientPK 
					                                order by [Date] desc
				                                )
				                                else 
					                                case when fc.DormantDate < (
						                                select top 1 [Date] 
						                                from FundClientPosition 
						                                where FundClientPK = fc.FundClientPK 
						                                order by [Date] desc
					                                ) 
						                                then (
							                                select top 1 [Date] 
							                                from FundClientPosition 
							                                where FundClientPK = fc.FundClientPK 
							                                order by [Date] desc
						                                ) 
						                                else fc.DormantDate 
					                                end 
			                                end) is not null and 
			                                (
				                                select 
					                                case when fc.DormantDate is null 
						                                then (
							                                select top 1 [Date] 
							                                from FundClientPosition 
							                                where FundClientPK = fc.FundClientPK 
							                                order by [Date] desc
						                                )
						                                else 
							                                case when fc.DormantDate < (
								                                select top 1 [Date] 
								                                from FundClientPosition 
								                                where FundClientPK = fc.FundClientPK 
								                                order by [Date] desc
							                                ) 
								                                then (
									                                select top 1 [Date] 
									                                from FundClientPosition 
									                                where FundClientPK = fc.FundClientPK 
									                                order by [Date] desc
								                                ) 
								                                else fc.DormantDate 
							                                end 
					                                end) <> '1900-01-01'
			                                ) " + _suspend;

                            cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        }

                        
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_DormantAccount.Add(setDormantAccount(dr));
                                }
                            }
                            return L_DormantAccount;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void DormantAccount_Suspend(DormantAccount _dormantAccount)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_dormantAccount.Status == 2)
                        {
                            cmd.CommandText = " INSERT INTO [FundClient] ([FundClientPK],[HistoryPK],[Status],[StatusFundClientSDI],[StatusFundClientBank],[StatusFundClientTradingSetup], " +
                            " [StatusFundClientTransactionAttributes],[StatusHighRiskMonitoring],[Notes],[ID],[Name],[FundClientCategoryPK],[FundClientType],[AccountFundClientCode], " +
                            " [BitIsGroup],[ParentPK],[SDIPK],[DepartmentPK],[OfficePK],[AgentPK],[BitIsSuspend],[EntryUsersID],[EntryTime], " +
                            " [LastUpdate] ) " +
                            " Select @PK,@NewHistoryPK,1,[StatusFundClientSDI],1,1, " +
                            " 1,[StatusHighRiskMonitoring],@NewNotes,[ID],[Name],[FundClientCategoryPK],[FundClientType],[AccountFundClientCode], " +
                            " [BitIsGroup],[ParentPK],[SDIPK],[DepartmentPK],[OfficePK],[AgentPK],1,@EntryUsersID,@EntryTime, " +
                            " @LastUpdate from FundClient where FundClientPK= @PK and historyPK = @HistoryPK " +
                            " Update FundClient set status = 4, Notes=@Notes, " +
                            " UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime, lastupdate=@lastupdate where FundClientPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@PK", _dormantAccount.FundClientPK);
                            cmd.Parameters.AddWithValue("@NewHistoryPK", _host.Get_NewHistoryPK(_dormantAccount.FundClientPK, "FundClient"));
                            cmd.Parameters.AddWithValue("@HistoryPK", _dormantAccount.HistoryPK);
                            cmd.Parameters.AddWithValue("@NewNotes", "Generate By Dormant Account (Status Suspend = Yes)");
                            cmd.Parameters.AddWithValue("@Notes", "Pending Data By Dormant Account");
                            cmd.Parameters.AddWithValue("@EntryUsersID", _dormantAccount.EntryUsersID);
                            cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _dormantAccount.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = " update FundClient set BitIsSuspend = 1,LastUpdate = @LastUpdate " +
                                              " where FundClientPK = @PK and historypk = @historyPK";
                            cmd.Parameters.AddWithValue("@PK", _dormantAccount.FundClientPK);
                            cmd.Parameters.AddWithValue("@historyPK", _dormantAccount.HistoryPK);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void DormantAccount_AutoSuspend(DormantAccount _dormantAccount)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_dormantAccount.Status == 2)
                        {
                            cmd.CommandText = 
                                " INSERT INTO [FundClient] ([FundClientPK],[HistoryPK],[Status],[StatusFundClientSDI],[StatusFundClientBank],[StatusFundClientTradingSetup], " +
                                " [StatusFundClientTransactionAttributes],[StatusHighRiskMonitoring],[Notes],[ID],[Name],[FundClientCategoryPK],[FundClientType],[AccountFundClientCode], " +
                                " [BitIsGroup],[ParentPK],[SDIPK],[DepartmentPK],[OfficePK],[AgentPK],[BitIsSuspend],[EntryUsersID],[EntryTime], " +
                                " [LastUpdate] ) " +
                                " Select @PK,@NewHistoryPK,1,[StatusFundClientSDI],1,1, " +
                                " 1,[StatusHighRiskMonitoring],@NewNotes,[ID],[Name],[FundClientCategoryPK],[FundClientType],[AccountFundClientCode], " +
                                " [BitIsGroup],[ParentPK],[SDIPK],[DepartmentPK],[OfficePK],[AgentPK],1,@EntryUsersID,@EntryTime, " +
                                " @LastUpdate from FundClient where FundClientPK= @PK and historyPK = @HistoryPK and " +
                                " Update FundClient set status = 4, Notes=@Notes, " +
                                " UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime, lastupdate=@lastupdate where FundClientPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@PK", _dormantAccount.FundClientPK);
                            cmd.Parameters.AddWithValue("@NewHistoryPK", _host.Get_NewHistoryPK(_dormantAccount.FundClientPK, "FundClient"));
                            cmd.Parameters.AddWithValue("@HistoryPK", _dormantAccount.HistoryPK);
                            cmd.Parameters.AddWithValue("@NewNotes", "Generate By Dormant Account (Status Suspend = Yes)");
                            cmd.Parameters.AddWithValue("@Notes", "Pending Data By Dormant Account");
                            cmd.Parameters.AddWithValue("@EntryUsersID", _dormantAccount.EntryUsersID);
                            cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _dormantAccount.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = " update FundClient set BitIsSuspend = 1,LastUpdate = @LastUpdate " +
                                              " where FundClientPK = @PK and historypk = @historyPK";
                            cmd.Parameters.AddWithValue("@PK", _dormantAccount.FundClientPK);
                            cmd.Parameters.AddWithValue("@historyPK", _dormantAccount.HistoryPK);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void DormantAccount_Activated(DormantAccount _dormantAccount)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = " update FundClient set status = 2,LastUpdate = @LastUpdate " +
                                          " where FundClientPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _dormantAccount.FundClientPK);
                        cmd.Parameters.AddWithValue("@historyPK", _dormantAccount.HistoryPK);
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


        public void DormantAccount_UnSuspend(DormantAccount _dormantAccount)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = " update FundClient set BitIsSuspend = 0,LastUpdate = @LastUpdate " +
                                          " where FundClientPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _dormantAccount.FundClientPK);
                        cmd.Parameters.AddWithValue("@historyPK", _dormantAccount.HistoryPK);
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

    }
}