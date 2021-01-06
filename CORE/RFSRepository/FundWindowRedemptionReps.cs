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
using System.Data;


namespace RFSRepository
{
    public class FundWindowRedemptionReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundWindowRedemption] " +
                            "([FundWindowRedemptionPK],[HistoryPK],[Status],[FirstRedemptionDate],[FirstDivDate],[FundPK],[VariableDate],[PaymentPeriod],[PaymentDate],[Description],";
        string _paramaterCommand = "@FirstRedemptionDate,@FirstDivDate,@FundPK,@VariableDate,@PaymentPeriod,@PaymentDate,@Description,";

        //2
        private FundWindowRedemption setFundWindowRedemption(SqlDataReader dr)
        {
            FundWindowRedemption M_FundWindowRedemption = new FundWindowRedemption();
            M_FundWindowRedemption.FundWindowRedemptionPK = Convert.ToInt32(dr["FundWindowRedemptionPK"]);
            M_FundWindowRedemption.Selected = Convert.ToBoolean(dr["Selected"]);
            M_FundWindowRedemption.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundWindowRedemption.Status = Convert.ToInt32(dr["Status"]);
            M_FundWindowRedemption.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundWindowRedemption.Notes = Convert.ToString(dr["Notes"]);
            M_FundWindowRedemption.FirstRedemptionDate = dr["FirstRedemptionDate"].ToString();
            M_FundWindowRedemption.FirstDivDate = dr["FirstDivDate"].ToString();
            M_FundWindowRedemption.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundWindowRedemption.FundID = Convert.ToString(dr["FundID"]);
            M_FundWindowRedemption.VariableDate = Convert.ToInt32(dr["VariableDate"]);
            M_FundWindowRedemption.PaymentDate = Convert.ToInt32(dr["PaymentDate"]);
            M_FundWindowRedemption.Description = Convert.ToString(dr["Description"]);
            M_FundWindowRedemption.PaymentPeriod = Convert.ToInt32(dr["PaymentPeriod"]);
            M_FundWindowRedemption.PaymentPeriodDesc = Convert.ToString(dr["PaymentPeriodDesc"]);
            M_FundWindowRedemption.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundWindowRedemption.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundWindowRedemption.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundWindowRedemption.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundWindowRedemption.EntryTime = dr["EntryTime"].ToString();
            M_FundWindowRedemption.UpdateTime = dr["UpdateTime"].ToString();
            M_FundWindowRedemption.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundWindowRedemption.VoidTime = dr["VoidTime"].ToString();
            M_FundWindowRedemption.DBUserID = dr["DBUserID"].ToString();
            M_FundWindowRedemption.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundWindowRedemption.LastUpdate = dr["LastUpdate"].ToString();
            M_FundWindowRedemption.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);

            return M_FundWindowRedemption;
        }

        public List<FundWindowRedemption> FundWindowRedemption_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundWindowRedemption> L_FundWindowRedemption = new List<FundWindowRedemption>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.ID FundID,C.DescOne PaymentPeriodDesc,* from FundWindowRedemption  A left join
                            Fund B on A.FundPK = B.FundPK and B.Status in(1,2) left join
                            MasterValue C on A.PaymentPeriod = C.Code and  C.ID = 'FundWindowRedemptionPeriod' and C.Status in (1,2)
                            where a.status = @status order by FundWindowRedemptionPK";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.ID FundID,C.DescOne PaymentPeriodDesc,* from FundWindowRedemption  A left join
                            Fund B on A.FundPK = B.FundPK and B.Status in(1,2) left join
                            MasterValue C on A.PaymentPeriod = C.Code and  C.ID = 'FundWindowRedemptionPeriod' and C.Status in (1,2) order by FundWindowRedemptionPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundWindowRedemption.Add(setFundWindowRedemption(dr));
                                }
                            }
                            return L_FundWindowRedemption;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundWindowRedemption_Add(FundWindowRedemption _FundWindowRedemption, bool _havePrivillege)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(FundWindowRedemptionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From FundWindowRedemption";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundWindowRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(FundWindowRedemptionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From FundWindowRedemption";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FirstRedemptionDate", _FundWindowRedemption.FirstRedemptionDate);
                        cmd.Parameters.AddWithValue("@FirstDivDate", _FundWindowRedemption.FirstDivDate);
                        cmd.Parameters.AddWithValue("@FundPK", _FundWindowRedemption.FundPK);
                        cmd.Parameters.AddWithValue("@VariableDate", _FundWindowRedemption.VariableDate);
                        cmd.Parameters.AddWithValue("@PaymentPeriod", _FundWindowRedemption.PaymentPeriod);
                        cmd.Parameters.AddWithValue("@PaymentDate", _FundWindowRedemption.PaymentDate);
                        cmd.Parameters.AddWithValue("@Description", _FundWindowRedemption.Description);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundWindowRedemption.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "FundWindowRedemption");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundWindowRedemption_Update(FundWindowRedemption _FundWindowRedemption, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FundWindowRedemption.FundWindowRedemptionPK, _FundWindowRedemption.HistoryPK, "FundWindowRedemption");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update FundWindowRedemption set status=2, Notes=@Notes,FirstRedemptionDate=@FirstRedemptionDate,FirstDivDate=@FirstDivDate,FundPK=@FundPK,VariableDate=@VariableDate,PaymentPeriod=@PaymentPeriod,Description=@Description,
                            ApprovedUsersID=@ApprovedUsersID, 
                            ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate 
                            where FundWindowRedemptionPK = @PK and historyPK = @HistoryPK
                            
                            delete FundWindowRedemptionDetail where FundWindowRedemptionPK = @PK
                            ";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FundWindowRedemption.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundWindowRedemption.FundWindowRedemptionPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundWindowRedemption.Notes);
                            cmd.Parameters.AddWithValue("@FirstRedemptionDate", _FundWindowRedemption.FirstRedemptionDate);
                            cmd.Parameters.AddWithValue("@FirstDivDate", _FundWindowRedemption.FirstDivDate);
                            cmd.Parameters.AddWithValue("@FundPK", _FundWindowRedemption.FundPK);
                            cmd.Parameters.AddWithValue("@VariableDate", _FundWindowRedemption.VariableDate);
                            cmd.Parameters.AddWithValue("@PaymentPeriod", _FundWindowRedemption.PaymentPeriod);
                            cmd.Parameters.AddWithValue("@PaymentDate", _FundWindowRedemption.PaymentDate);
                            cmd.Parameters.AddWithValue("@Description", _FundWindowRedemption.Description);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundWindowRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundWindowRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"
                            Update FundWindowRedemption set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundWindowRedemptionPK = @PK and status = 4
  
                            ";
                            cmd.Parameters.AddWithValue("@PK", _FundWindowRedemption.FundWindowRedemptionPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundWindowRedemption.EntryUsersID);
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
                                cmd.CommandText = @"Update FundWindowRedemption set Notes=@Notes,FirstRedemptionDate=@FirstRedemptionDate,FirstDivDate=@FirstDivDate,FundPK=@FundPK,VariableDate=@VariableDate,PaymentPeriod=@PaymentPeriod,Description=@Description,
                                    UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate 
                                    where FundWindowRedemptionPK = @PK and historyPK = @HistoryPK

                                    delete FundWindowRedemptionDetail where FundWindowRedemptionPK = @PK
                                    ";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundWindowRedemption.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundWindowRedemption.FundWindowRedemptionPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundWindowRedemption.Notes);
                                cmd.Parameters.AddWithValue("@FirstRedemptionDate", _FundWindowRedemption.FirstRedemptionDate);
                                cmd.Parameters.AddWithValue("@FirstDivDate", _FundWindowRedemption.FirstDivDate);
                                cmd.Parameters.AddWithValue("@FundPK", _FundWindowRedemption.FundPK);
                                cmd.Parameters.AddWithValue("@VariableDate", _FundWindowRedemption.VariableDate);
                                cmd.Parameters.AddWithValue("@PaymentPeriod", _FundWindowRedemption.PaymentPeriod);
                                cmd.Parameters.AddWithValue("@PaymentDate", _FundWindowRedemption.PaymentDate);
                                cmd.Parameters.AddWithValue("@Description", _FundWindowRedemption.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundWindowRedemption.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FundWindowRedemption.FundWindowRedemptionPK, "FundWindowRedemption");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundWindowRedemption where FundWindowRedemptionPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundWindowRedemption.FundWindowRedemptionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundWindowRedemption.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FirstRedemptionDate", _FundWindowRedemption.FirstRedemptionDate);
                                cmd.Parameters.AddWithValue("@FirstDivDate", _FundWindowRedemption.FirstDivDate);
                                cmd.Parameters.AddWithValue("@FundPK", _FundWindowRedemption.FundPK);
                                cmd.Parameters.AddWithValue("@VariableDate", _FundWindowRedemption.VariableDate);
                                cmd.Parameters.AddWithValue("@PaymentPeriod", _FundWindowRedemption.PaymentPeriod);
                                cmd.Parameters.AddWithValue("@PaymentDate", _FundWindowRedemption.PaymentDate);
                                cmd.Parameters.AddWithValue("@Description", _FundWindowRedemption.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundWindowRedemption.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundWindowRedemption set status= 4,Notes=@Notes," +
                                    "LastUpdate=LastUpdate where FundWindowRedemptionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundWindowRedemption.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundWindowRedemption.FundWindowRedemptionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundWindowRedemption.HistoryPK);
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

        public void FundWindowRedemption_Approved(FundWindowRedemption _FundWindowRedemption)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundWindowRedemption set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where FundWindowRedemptionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundWindowRedemption.FundWindowRedemptionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundWindowRedemption.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundWindowRedemption.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundWindowRedemption set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundWindowRedemptionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundWindowRedemption.FundWindowRedemptionPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundWindowRedemption.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
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

        public void FundWindowRedemption_Reject(FundWindowRedemption _FundWindowRedemption)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundWindowRedemption set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundWindowRedemptionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundWindowRedemption.FundWindowRedemptionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundWindowRedemption.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundWindowRedemption.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundWindowRedemption set status= 2,LastUpdate=@LastUpdate where FundWindowRedemptionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundWindowRedemption.FundWindowRedemptionPK);
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

        public void FundWindowRedemption_Void(FundWindowRedemption _FundWindowRedemption)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundWindowRedemption set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundWindowRedemptionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundWindowRedemption.FundWindowRedemptionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundWindowRedemption.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundWindowRedemption.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
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

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public bool CheckHassAdd(int _pk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        declare @FundPK int
                        select @FundPK = FundPK from FundWindowRedemption where FundWindowRedemptionPK = @PK and status in (1,2)
                        
                        
                        select * from FundWindowRedemption where FundPK = @FundPK and Status = 2";
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

        public void Generate_WindowRedemption(FundWindowRedemption _FundWindowRedemption)
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
--declare @dateto date
--declare @userid nvarchar(100)
--declare @TimeNow datetime

--set @dateto = '2020-11-03'
--set @userid = 'admin'
--set @TimeNow = getdate()


DECLARE @FundWindowRedemptionDetail TABLE
(
 FundWindowRedemptionPK int,
 MaxRedempDate datetime,
 RedemptionDate datetime,
 DividenDate datetime,
 PaymentDate datetime,
 EntryUsersID nvarchar(50),
 LastUpdate datetime
)

Declare @FundWindowRedemptionPK INT
Declare @FirstDividenDate DATETIME
Declare @FirstRedemptionDate datetime
Declare @MaturityDate datetime
declare @MonthVar int
Declare @MaxRedempDateVar int
Declare @PaymentDateVar INT

DECLARE @dayFirstdate INT

DECLARE @DateCounter datetime
DECLARE @FlagDate datetime
DECLARE @FlagCounter BIT

SET @FlagCounter = 0

DECLARE A CURSOR FOR 
	select FundWindowRedemptionPK,FirstDivDate,FirstRedemptionDate,B.MaturityDate,isnull(C.Priority,0) MonthVar
	,case when VariableDate > 0 then isnull(VariableDate * -1,0) else isnull(VariableDate,0) end MaxRedempDateVar
	,isnull(PaymentDate,0) PaymentDateVar
	from FundWindowRedemption A
	left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
	left join MasterValue C on A.PaymentPeriod = C.Code and C.id = 'FundWindowRedemptionPeriod' and C.status in (1,2)
	where  A.status = 2 and A.PaymentPeriod <> 5
	--AND A.FundWindowRedemptionPK in (22,23)
OPEN A             
FETCH NEXT FROM A INTO @FundWindowRedemptionPK,@FirstDividenDate,@FirstRedemptionDate,@MaturityDate,@MonthVar,@MaxRedempDateVar,@PaymentDateVar
WHILE @@FETCH_STATUS = 0              
BEGIN    
	DELETE FundWindowRedemptionDetail where FundWindowRedemptionPK = @FundWindowRedemptionPK
	DELETE @FundWindowRedemptionDetail
	SET @DateCounter = NULL
	set @FlagCounter = null
	
	IF (@FirstDividenDate <= @FirstRedemptionDate OR @FirstRedemptionDate = '1900-01-01 00:00:00.000')
	BEGIN
		SET @dayFirstdate = 0
		SET @dayFirstdate = DAY(@FirstDividenDate)
		SET @DateCounter = @FirstDividenDate
		WHILE @DateCounter <= @DateTo AND @DateCounter <= @MaturityDate
		BEGIN

					SET @FlagDate = @DateCounter
					SET @FlagCounter = case when dbo.CheckTodayIsHoliday(@DateCounter) = 1 then 1 else 0 END
			
				set @DateCounter = case when dbo.CheckTodayIsHoliday(@DateCounter) = 1 then dbo.FWorkingDay(@DateCounter,1) else @DateCounter END
				
				INSERT INTO @FundWindowRedemptionDetail
			        ( FundWindowRedemptionPK ,
			          MaxRedempDate ,
			          RedemptionDate ,
			          DividenDate ,
			          PaymentDate ,
			          EntryUsersID ,
			          LastUpdate
			        )
					SELECT @FundWindowRedemptionPK
					,CASE WHEN @DateCounter >= @FirstRedemptionDate and @FirstRedemptionDate <> '1900-01-01 00:00:00.000' THEN dbo.FWorkingDay(@DateCounter,@MaxRedempDateVar) ELSE NULL END
					,CASE WHEN @DateCounter >= @FirstRedemptionDate and @FirstRedemptionDate <> '1900-01-01 00:00:00.000' THEN @DateCounter ELSE NULL END
					,@DateCounter
					,dbo.FWorkingDay(@DateCounter,@PaymentDateVar)
					,@userID
					,@TimeNow
					
					

					SET @DateCounter = DATEADD(MONTH,@MonthVar,@DateCounter)

					IF(@FlagCounter = 1)
					BEGIN
                    SET @DateCounter = DATEADD(MONTH,@MonthVar,@FlagDate)
					END

				

					IF(@dayFirstdate > DAY(@DateCounter) AND DAY(EOMONTH(@DateCounter)) >= @dayFirstdate)
					BEGIN
						SET @DateCounter = DATEADD(DAY,@dayFirstdate - DAY(@DateCounter),@DateCounter)
					END 
					
					IF(@dayFirstdate < DAY(@DateCounter))
					BEGIN
						SET @DateCounter = DATEADD(DAY, (@dayFirstdate - DAY(@DateCounter)) ,@DateCounter)
					END
					
				--		select @DateCounter DateCounterAwal

					
				--SELECT @FlagDate,@FlagCounter
		END
	END
	ELSE
	BEGIN
		IF @FirstRedemptionDate <> '1900-01-01 00:00:00.000'
		BEGIN
		SET @dayFirstdate = 0
		SET @dayFirstdate = DAY(@FirstRedemptionDate)
		SET @DateCounter = @FirstRedemptionDate

		WHILE @DateCounter <= @DateTo AND @DateCounter <= @MaturityDate
		BEGIN

		
				INSERT INTO @FundWindowRedemptionDetail
			        ( FundWindowRedemptionPK ,
			          MaxRedempDate ,
			          RedemptionDate ,
			          DividenDate ,
			          PaymentDate ,
			          EntryUsersID ,
			          LastUpdate
			        )
					SELECT @FundWindowRedemptionPK
					,CASE WHEN @DateCounter >= @FirstRedemptionDate THEN dbo.FWorkingDay(@DateCounter,@MaxRedempDateVar) ELSE NULL END
					,CASE WHEN @DateCounter >= @FirstRedemptionDate THEN @DateCounter ELSE NULL END
					,case when @DateCounter >= @FirstDividenDate then @DateCounter else null end
					,case when @DateCounter >= @FirstDividenDate then dbo.FWorkingDay(@DateCounter,@PaymentDateVar) else null end 
					,@userID
					,@TimeNow

					SET @DateCounter = DATEADD(MONTH,@MonthVar,@DateCounter)
					IF(@dayFirstdate > DAY(@DateCounter) AND DAY(EOMONTH(@DateCounter)) >= @dayFirstdate)
					BEGIN
						SET @DateCounter = DATEADD(DAY,@dayFirstdate - DAY(@DateCounter),@DateCounter)
					END 
					IF(@dayFirstdate < DAY(@DateCounter))
					BEGIN
						SET @DateCounter = DATEADD(DAY, (@dayFirstdate - DAY(@DateCounter)) ,@DateCounter)
					END

					set @DateCounter = case when dbo.CheckTodayIsHoliday(@DateCounter) = 1 then dbo.FWorkingDay(@DateCounter,1) else @DateCounter end
					
		END
		END
		
	END
	
insert into FundWindowRedemptionDetail
SELECT * FROM @FundWindowRedemptionDetail ORDER BY DividenDate asc


FETCH NEXT FROM A INTO @FundWindowRedemptionPK,@FirstDividenDate,@FirstRedemptionDate,@MaturityDate,@MonthVar,@MaxRedempDateVar,@PaymentDateVar
END          
CLOSE A       
DEALLOCATE A  

--SELECT * FROM @FundWindowRedemptionDetail ORDER BY DividenDate asc

                        ";
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.AddWithValue("@DateTo", _FundWindowRedemption.ParamDateTo);
                        cmd.Parameters.AddWithValue("@userID", _FundWindowRedemption.EntryUsersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);
                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<SetWindowRedemptionDetail> WindowRedemptionDetail(int _pk)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SetWindowRedemptionDetail> L_SetWindowRedemptionDetail = new List<SetWindowRedemptionDetail>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select * from FundWindowRedemptionDetail  where FundWindowRedemptionPK  = @PK
                               ";
                        cmd.Parameters.AddWithValue("@PK", _pk);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_SetWindowRedemptionDetail.Add(SetWindowRedemptionDetail(dr));
                                }
                            }
                        }
                        return L_SetWindowRedemptionDetail;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private SetWindowRedemptionDetail SetWindowRedemptionDetail(SqlDataReader dr)
        {
            SetWindowRedemptionDetail M_FundWindowRedemptionDetail = new SetWindowRedemptionDetail();
            M_FundWindowRedemptionDetail.FundWindowRedemptionPK = Convert.ToInt32(dr["FundWindowRedemptionPK"]);
            M_FundWindowRedemptionDetail.MaxRedemptionDate = Convert.ToString(dr["MaxRedemptionDate"]);
            M_FundWindowRedemptionDetail.DividenDate = Convert.ToString(dr["DividenDate"]);
            M_FundWindowRedemptionDetail.PaymentDate = Convert.ToString(dr["PaymentDate"]);
            M_FundWindowRedemptionDetail.EntryUsersID = Convert.ToString(dr["EntryUsersID"]);
            M_FundWindowRedemptionDetail.LastUpdate = Convert.ToString(dr["LastUpdate"]);
            return M_FundWindowRedemptionDetail;
        }

        public bool CheckHassAddFund(int _pk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from FundWindowRedemption where FundPK = @PK and Status in (1,2)";
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

        public Boolean GenerateToExcelDetail(string _userID, FundWindowRedemption _FundWindowRedemption)
        {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {


                            cmd.CommandText =

                            @"
                               select * from fundwindowredemptiondetail where fundwindowredemptionpk = @FundWindowRedemptionPK

                            "
                            ;

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@FundWindowRedemptionPK", _FundWindowRedemption.FundWindowRedemptionPK);
                            cmd.ExecuteNonQuery();

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "FundWindowRedemptionDetail" + "_" + _userID + ".xlsx";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundWindowRedemption";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("FUND WINDOW REDEMPTION DETAIL");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<SetWindowRedemptionDetail> rList = new List<SetWindowRedemptionDetail>();
                                        while (dr0.Read())
                                        {
                                            SetWindowRedemptionDetail rSingle = new SetWindowRedemptionDetail();
                                            rSingle.MaxRedemptionDate = Convert.ToString(dr0["MaxRedemptionDate"]);
                                            rSingle.DividenDate = Convert.ToString(dr0["DividenDate"]);
                                            rSingle.PaymentDate = Convert.ToString(dr0["PaymentDate"]);
                                            rSingle.EntryUsersID = Convert.ToString(dr0["EntryUsersID"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByCategory =
                                         from r in rList
                                         group r by new {} into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;

                                        int _startRowDetail = 0;
                                        int _endRowDetail = 0;
                                        foreach (var rsHeader in GroupByCategory)
                                        {
                                            incRowExcel++;


                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 2].Value = "Max Redemption Date";
                                            worksheet.Cells[incRowExcel, 3].Value = "Dividen Date";
                                            worksheet.Cells[incRowExcel, 4].Value = "Payment Date";
                                            worksheet.Cells[incRowExcel, 5].Value = "Entry UsersID";

                                            incRowExcel++;

                                            _startRowDetail = incRowExcel;
                                            _endRowDetail = incRowExcel;
                                            //end area header
                                            int _no = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                //area detail
                                                
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(rsDetail.MaxRedemptionDate).ToString("dd/MMM/yyyy");
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(rsDetail.DividenDate).ToString("dd/MMM/yyyy");
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 4].Value = Convert.ToDateTime(rsDetail.PaymentDate).ToString("dd/MMM/yyyy");
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.EntryUsersID;
                                                worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;
                                                _no++;

                                            }

                                            worksheet.Cells["A" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + _endRowDetail + ":E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Row(incRowExcel).PageBreak = true;

                                        }

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 5];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 30;
                                        worksheet.Column(3).Width = 30;
                                        worksheet.Column(4).Width = 30;
                                        worksheet.Column(5).Width = 20;

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 FUND WINDOW REDEMPTION DETAIL";


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        return true;
                                    }
                                }
                            }

                        }

                    }
                }
                catch (Exception err)
                {
                    return false;
                    throw err;
                }
        }

        public string ImportWindowRedemption(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    //delete data yang lama
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = "truncate table WindowRedemptionTemp";
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.WindowRedemptionTemp";
                        bulkCopy.WriteToServer(CreateDataTableFromWindowRedemptionTempExcelFile(_fileSource));
                        //_msg = "Import Window Redemption Success";
                        //return _msg;
                    }

                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = @"

declare @FundWindowRedemptionPK int

select @FundWindowRedemptionPK = C.FundWindowRedemptionPK From WindowRedemptionTemp A
Left Join Fund B  on A.FundID = B.ID and B.status in (1,2)
left Join FundWindowRedemption C on B.FundPK = C.FundPK and C.Status in (1,2) 



Delete FundWindowRedemptionDetail Where FundWindowRedemptionPK = @FundWindowRedemptionPK

insert into FundWindowRedemptionDetail(FundWindowRedemptionPK,MaxRedemptionDate,DividenDate,PaymentDate,EntryUsersID,LastUpdate)
select @FundWindowRedemptionPK,MaxRedemptionDate,A.DividenDate,A.PaymentDate,@UsersID,@LastUpdate from WindowRedemptionTemp A

Select 'success'

                                                "
                                ;
                            cmd1.Parameters.AddWithValue("@UsersID", _userID);
                            cmd1.Parameters.AddWithValue("@LastUpdate", _now);

                            using (SqlDataReader dr = cmd1.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    _msg = "Import Window Redemption Success"; //Convert.ToString(dr[""]);
                                    return _msg;
                                }
                                else
                                {
                                    _msg = "";
                                    return _msg;
                                }
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

        private DataTable CreateDataTableFromWindowRedemptionTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "WindowRedemptionPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "MaxRedemptionDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "DividenDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PaymentDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);



                    FileInfo excelFile = new FileInfo(_path);
                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        int i = 2;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        var end = worksheet.Dimension.End;
                        while (i <= end.Row)
                        {
                            dr = dt.NewRow();
                            if (worksheet.Cells[i, 1].Value == null)
                                dr["FundID"] = "";
                            else
                                dr["FundID"] = worksheet.Cells[i, 1].Value.ToString();

                            if (worksheet.Cells[i, 2].Value == null)
                                dr["MaxRedemptionDate"] = "";
                            else
                                dr["MaxRedemptionDate"] = worksheet.Cells[i, 2].Value.ToString();

                            if (worksheet.Cells[i, 3].Value == null)
                                dr["DividenDate"] = "";
                            else
                                dr["DividenDate"] = worksheet.Cells[i, 3].Value.ToString();

                            if (worksheet.Cells[i, 4].Value == null)
                                dr["PaymentDate"] = "";
                            else
                                dr["PaymentDate"] = worksheet.Cells[i, 4].Value.ToString();


                            if (dr["FundID"].Equals(null) != true ||
                                dr["MaxRedemptionDate"].Equals(null) != true ||
                                dr["DividenDate"].Equals(null) != true ||
                                dr["PaymentDate"].Equals(null) != true
                                )

                            { dt.Rows.Add(dr); }
                            i++;

                        }
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



    }
}