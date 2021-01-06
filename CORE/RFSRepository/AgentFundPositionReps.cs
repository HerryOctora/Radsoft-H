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
using System.Text;
using System.Threading;
using System.Data;
using System.Data.OleDb;

namespace RFSRepository
{
    public class AgentFundPositionReps
    {
        Host _host = new Host();

        //2
        private AgentFundPosition setAgentFundPosition(SqlDataReader dr)
        {
            AgentFundPosition M_AgentFundPosition = new AgentFundPosition();
            M_AgentFundPosition.AgentFundPositionPK = Convert.ToInt32(dr["AgentFundPositionPK"]);
            M_AgentFundPosition.Date = Convert.ToString(dr["Date"]);
            M_AgentFundPosition.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_AgentFundPosition.AgentID = Convert.ToString(dr["AgentID"]);
            M_AgentFundPosition.AgentName = Convert.ToString(dr["AgentName"]);
            M_AgentFundPosition.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_AgentFundPosition.FundID = Convert.ToString(dr["FundID"]);
            M_AgentFundPosition.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
            M_AgentFundPosition.DBUserID = dr["DBUserID"].ToString();
            M_AgentFundPosition.DBTerminalID = dr["DBTerminalID"].ToString();
            M_AgentFundPosition.LastUpdate = dr["LastUpdate"].ToString();
            M_AgentFundPosition.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_AgentFundPosition;
        }



        public List<AgentFundPosition> AgentFundPosition_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AgentFundPosition> L_AgentFundPosition = new List<AgentFundPosition>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                              "Select B.ID AgentID,B.Name AgentName,C.ID FundID, A.* from AgentFundPosition A left join " +
                              "Agent B on A.AgentPK = B.AgentPK and B.status in (1,2) left join " +
                              "Fund C on A.FundPK = C.FundPK and C.status in (1,2) " +
                              "where Date between @DateFrom and @DateTo order By B.Name ";
                        }
                        else
                        {
                            cmd.CommandText =
                              "Select B.ID AgentID,B.Name AgentName,C.ID FundID, A.* from AgentFundPosition A left join " +
                              "Agent B on A.AgentPK = B.AgentPK and B.status in (1,2) left join " +
                              "Fund C on A.FundPK = C.FundPK and C.status in (1,2) " +
                              "where Date between @DateFrom and @DateTo order By B.Name ";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AgentFundPosition.Add(setAgentFundPosition(dr));
                                }
                            }
                            return L_AgentFundPosition;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Generate_AgentPosition(AgentFundPosition _agentFundPosition)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _paramFund = "";
                        string _paramAgent = "";
                        string _paramFundText = "";
                        string _paramAgentText = "";

                        if (!_host.findString(_agentFundPosition.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_agentFundPosition.FundFrom))
                        {
                            _paramFund = " And A.FundPK in ( " + _agentFundPosition.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }




                        if (!_host.findString(_agentFundPosition.AgentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_agentFundPosition.AgentFrom))
                        {
                            _paramAgent = " And A.AgentPK in ( " + _agentFundPosition.AgentFrom + " ) ";
                        }
                        else
                        {
                            _paramAgent = "";
                        }


                        cmd.CommandText = @"
    

DECLARE @DateMinOne DATETIME
SET @DateMinOne = dbo.FWorkingDay(@datefrom,-1)

DECLARE @CAMDate datetime

SET @CAMDate = '07/11/19'


DELETE A From AgentRedemption A where NavDate >= @DateFrom " + _paramFund + _paramAgent + @"
delete A From agentFundPosition A WHERE date >= @DateFrom " + _paramFund + _paramAgent + @"


DECLARE @CounterDate datetime



Create TABLE #TableAgentRedemption 
(
	TrxInPK INT,
	TrxOutPK INT,
	TypeIn NVARCHAR(100),
	TypeOut NVARCHAR(100),
	FundPK INT,
	FundClientPK INT,
	AgentPK INT,
	UnitAmount NUMERIC(22,4),
	NAVDate Datetime
)
CREATE CLUSTERED INDEX indx_TableAgentRedemption  ON #TableAgentRedemption (TrxInPK,TrxOutPK,FundPK,FundClientPK,AgentPK);

Create TABLE #TableAgentRedemptionMovement 
(
	TrxInPK INT,
	TrxOutPK INT,
	TypeIn NVARCHAR(100),
	TypeOut NVARCHAR(100),
	FundPK INT,
	FundClientPK INT,
	AgentPK INT,
	UnitAmount NUMERIC(22,4),
	NAVDate Datetime
)
CREATE CLUSTERED INDEX indx_TableAgentRedemptionMovement  ON #TableAgentRedemptionMovement (TrxInPK,TrxOutPK,FundPK,FundClientPK,AgentPK);


DECLARE @CTrxOutPK INT
DECLARE @CType NVARCHAR(100)
DECLARE @CFundPK INT
DECLARE @CFundClientPK INT
DECLARE @CUnitRedemp NUMERIC(22,4)
DECLARE @CDate DATETIME

DECLARE @CounterUnit NUMERIC(22,4)
DECLARE @UnitBefore NUMERIC(22,4)

DECLARE @CTrxInPK INT
DECLARE @CBTotalUnitAmount NUMERIC(22,4)
DECLARE @CBType NVARCHAR(100)


Create TABLE #OutTrx 
(
	TrxOutPK INT,
	TYPE nvarchar(100),
	FundPK INT,
	FundClientPK INT,
	UnitAmount NUMERIC(22,4),
	NAVDate datetime
)
CREATE CLUSTERED INDEX indx_OutTrx   ON #OutTrx (TrxOutPK,FundPK,FundClientPK);


Create TABLE #InTrx 
(
	TrxInPK INT,
	TYPE nvarchar(100),
	FundPK INT,
	FundClientPK INT,
	UnitAmount NUMERIC(22,4),
	NAVDate datetime
)
CREATE CLUSTERED INDEX indx_InTrx  ON #InTrx (TrxInPK,FundPK,FundClientPK);


INSERT INTO #OutTrx
        ( TrxOutPK ,
          TYPE ,
          FundPK ,
          FundClientPK ,
          UnitAmount ,
          NAVDate
        )

SELECT A.* FROM (
SELECT ClientRedemptionPK TxOut,'RED' Type,FundPK,FundClientPK,UnitAmount,NAVDate
	FROM dbo.ClientRedemption WHERE posted = 1 AND status <> 3 AND Revised = 0
	AND NAVDate > @CAMDate
	AND NAVDate BETWEEN @datefrom AND @dateto
UNION ALL

SELECT ClientSwitchingPK TxOut,'SWI OUT' Type,FundPKFrom FundPK,FundClientPK,UnitAmount,NAVDate
	FROM dbo.ClientSwitching WHERE posted = 1 AND status <> 3 AND Revised = 0 	AND NAVDate > @CAMDate
	AND NAVDate BETWEEN @datefrom AND @dateto
	) A ORDER BY A.NAVDate asc

	
INSERT INTO #InTrx
        ( TrxInPK ,
          TYPE ,
          FundPK ,
          FundClientPK ,
          UnitAmount ,
          NAVDate
        )
SELECT A.* FROM (
		SELECT A.ClientSubscriptionPK TxPK,'SUB' Type,FundPK,FundClientPK,A.TotalUnitAmount,A.NAVDate NAVDate FROM dbo.ClientSubscription A 
		WHERE Posted = 1 AND Revised = 0 AND status <> 3 AND A.ClientSubscriptionPK IN
		(
			SELECT DISTINCT ClientSubscriptionPK FROM dbo.AgentSubscription
		)
		UNION ALL
		SELECT ClientSwitchingPK TxOut,'SWI IN' Type,FundPKTo FundPK,FundClientPK,TotalUnitAmountFundTo UnitAmount,NAVDate
		FROM dbo.ClientSwitching WHERE posted = 1 AND status <> 3 AND Revised = 0 AND ClientSwitchingPK IN
		(
			SELECT DISTINCT ClientSwitchingPK FROM dbo.AgentSwitching
		)
)A ORDER BY A.NAVDate asc

 

INSERT INTO #TableAgentRedemption
        ( TrxInPK ,
          TrxOutPK ,
          TypeIn ,
          TypeOut ,
          FundPK ,
		  FundClientPK,
          AgentPK ,
          UnitAmount ,
          NAVDate
        )

SELECT TrxInPK ,
          TrxOutPK ,
          TypeIn ,
          TypeOut ,
          FundPK ,
		  FundClientPK,
          AgentPK ,
          UnitAmount ,
          NAVDate FROM dbo.AgentRedemption





Create table #InforCursorA 
(
	TrxInPK int,
	Type Nvarchar(200),
	FundPK int,
	FundClientPK int,
	AgentPK int,
	AgentUnit numeric(22,4),
	AgentTrxPercent numeric(18,8)
)
CREATE CLUSTERED INDEX indx_InforCursorA  ON #InforCursorA (TrxInPK,FundPK,FundClientPK,AgentPK);


		insert into #InforCursorA
		SELECT  A.TrxInPK
		--,@CTrxOutPK
		,A.Type
		--,@CType
		,A.FundPK,A.FundClientPK
		,CASE WHEN A.Type = 'SUB' THEN B.AgentPK ELSE C.AgentPK END
		,A.UnitAmount AgentUnit
		,CASE WHEN A.Type = 'SUB' THEN B.AgentTrxPercent ELSE C.AgentTrxPercent END
		--,@CDate
		FROM #InTrx A
		LEFT JOIN dbo.AgentSubscription B ON A.TrxInPK = B.ClientSubscriptionPK AND A.Type = 'SUB' AND B.status IN (1,2)
		LEFT JOIN dbo.AgentSwitching C ON A.TrxInPK = C.ClientSwitchingPK AND A.Type = 'SWI IN'	AND C.status IN (1,2)


Declare @CTotalFromAgentRedemption numeric(22,4)

Declare A Cursor FOR
	SELECT A.TrxOutPK,A.TYPE,A.FundPK,A.FundClientPK,A.UnitAmount,A.NAVDate FROM #OutTrx A
	ORDER BY A.NAVDate asc
Open A
Fetch Next From A
INTO @CTrxOutPK,@CType,@CFundPK,@CFundClientPK,@CUnitRedemp,@CDate
While @@FETCH_STATUS = 0  
Begin
	SET @CounterUnit = 0
	SET @CounterUnit = @CUnitRedemp

	Declare B Cursor For
			
	SELECT A.TrxInPK, UnitAmount - ISNULL(B.Unit,0) ,A.TYPE FROM #InTrx A
	LEFT JOIN (
		SELECT FundPK,FundClientPK,TrxInPK,TypeIn,SUM(isnull(UnitAmount,0)) Unit FROM #TableAgentRedemption
		where FundPK = @CFundPK and FundClientPK = @CFundClientPK
		GROUP BY FundPK,TrxInPK,TypeIn,FundClientPK
	)B ON A.TrxInPK = B.TrxInPK  AND A.Type = B.TypeIn AND A.FundClientPK = B.FundClientPK AND A.FundPK = B.FundPK
	AND NAVDate < @CDate
	WHERE A.FUndPK = @CFundPK AND A.FundClientPK = @CFundClientPK
	ORDER BY NAVDate ASC

	Open B
	Fetch Next From B
	INTO @CTrxInPK,@CBTotalUnitAmount,@CBType
	
	While @@FETCH_STATUS = 0  
	BEGIN

	set @CTotalFromAgentRedemption = 0


	SELECT @CTotalFromAgentRedemption =  SUM(UnitAmount)  FROM #TableAgentRedemption A
	WHERE  A.TrxInPK = @CTrxInPK AND A.TypeIn = @CBType and A.FundPK = @CFundPK AND A.FundClientPK = @CFundClientPK
	GROUP BY FundPK,TrxInPK,TypeIn,FundClientPK

	set @CTotalFromAgentRedemption = isnull(@CTotalFromAgentRedemption,0)

	--	IF(@CTrxOutPK = 5)
	--BEGIN
	--	SELECT @CounterUnit CounterUnit,@UnitBefore UnitBefore,@CBTotalUnitAmount TOtalUNit,@CUnitRedemp unitRedemp,@CTrxInPK TrxINPK,@CBType TypeIn,@CFundPK FundPK
	--END


	   SET @UnitBefore = @CounterUnit
		SET @CounterUnit = @CounterUnit - @CBTotalUnitAmount
		
		--SELECT @UnitBefore unitBefore,@CounterUnit CounterUnit,@CBTotalUnitAmount TotalUnitSubs



		IF @CounterUnit >= 0 
		BEGIN

				INSERT INTO #TableAgentRedemptionMovement
			        ( TrxInPK ,
			          TrxOutPK ,
			          TypeIn ,
			          TypeOut ,
			          FundPK ,
			          FundClientPK ,
			          AgentPK ,
			          UnitAmount ,
			          NAVDate
			        )
			SELECT  @CTrxInPK,@CTrxOutPK,@CBType,@CType,A.FundPK,A.FundClientPK
			,A.AgentPK
			,A.AgentTrxPercent / 100 * (A.AgentUnit -  ISNULL(@CTotalFromAgentRedemption,0))
			,@CDate
			FROM #InforCursorA  A
			WHERE  A.TrxInPK = @CTrxInPK AND A.Type = @CBType and A.FundPK = @CFundPK AND A.FundClientPK = @CFundClientPK


			INSERT INTO #TableAgentRedemption
			        ( TrxInPK ,
			          TrxOutPK ,
			          TypeIn ,
					  TypeOut,
			          FundPK ,
					  FUndClientPK,
			          AgentPK ,
			          UnitAmount ,
			          NAVDate
			        )
		
				SELECT  @CTrxInPK,@CTrxOutPK,@CBType,@CType,A.FundPK,A.FundClientPK
			,A.AgentPK
			,A.AgentTrxPercent / 100 * (A.AgentUnit -  ISNULL(@CTotalFromAgentRedemption,0))
			,@CDate
			FROM #InforCursorA  A
			WHERE  A.TrxInPK = @CTrxInPK AND A.Type = @CBType and A.FundPK = @CFundPK AND A.FundClientPK = @CFundClientPK


	
		END
		ELSE
		BEGIN
		 IF @UnitBefore > 0 
		 BEGIN
			
			 INSERT INTO #TableAgentRedemptionMovement
			        ( TrxInPK ,
			          TrxOutPK ,
			          TypeIn ,
					  TypeOut,
			          FundPK ,
					  FundClientPK,
			          AgentPK ,
			          UnitAmount ,
			          NAVDate
			        )

			SELECT  @CTrxInPK,@CTrxOutPK,@CBType,@CType,A.FundPK,A.FundClientPK
			,A.AgentPK
			,A.AgentTrxPercent * @UnitBefore / 100
			,@CDate
			FROM #InforCursorA  A
			WHERE  A.TrxInPK = @CTrxInPK AND A.Type = @CBType and A.FundPK = @CFundPK AND A.FundClientPK = @CFundClientPK


		   INSERT INTO #TableAgentRedemption
			        ( TrxInPK ,
			          TrxOutPK ,
			          TypeIn ,
					  TypeOut,
			          FundPK ,
					  FundClientPK,
			          AgentPK ,
			          UnitAmount ,
			          NAVDate
			        )
			SELECT  @CTrxInPK,@CTrxOutPK,@CBType,@CType,A.FundPK,A.FundClientPK
			,A.AgentPK
			,A.AgentTrxPercent * @UnitBefore / 100
			,@CDate
			FROM #InforCursorA  A
			WHERE  A.TrxInPK = @CTrxInPK AND A.Type = @CBType and A.FundPK = @CFundPK AND A.FundClientPK = @CFundClientPK

			
		 END
		END

		Fetch Next From B
		INTO @CTrxInPK,@CBTotalUnitAmount,@CBType
	End	
	Close B
	Deallocate B

	Fetch Next From A 
	INTO @CTrxOutPK,@CType,@CFundPK,@CFundClientPK,@CUnitRedemp,@CDate
End	
Close A
Deallocate A



--select 'AgentRedemption' Query,* from #TableAgentRedemptionMovement

INSERT INTO dbo.AgentRedemption
        ( TrxInPK ,
          TrxOutPK ,
          TypeIn ,
          TypeOut ,
          FundPK ,
		  FundClientPK,
          AgentPK ,
          UnitAmount ,
          NAVDate
        )

SELECT * FROM #TableAgentRedemptionMovement


--SELECT * FROM @TableAgentRedemptionMovement where fundPK = 30 and agentPK = 9 order by navdate desc


Create TABLE #AgentPosition 
(
	Date DATETIME,
	FundPK INT,
	AgentPK INT,
	UnitAmount NUMERIC(22,4)
)
CREATE CLUSTERED INDEX indx_AgentPosition  ON #AgentPosition (FundPK,AgentPK);

SET @CounterDate = @DateFrom
WHILE @CounterDate <= @DateTo
BEGIN
	INSERT INTO #AgentPosition
	        ( Date ,
	          FundPK ,
	          AgentPK ,
	          UnitAmount
	        )
	SELECT A.Date,A.FundPK,A.AgentPK,SUM(ISNULL(A.AgentUnit,0)) FROM (
		SELECT @CounterDate Date,A.FundPK,B.AgentPK, SUM(ISNULL(B.AgentTrxPercent / 100 * A.TotalUnitAmount,0)) AgentUnit 
		FROM dbo.ClientSubscription A
		LEFT JOIN dbo.AgentSubscription B ON A.ClientSubscriptionPK = B.ClientSubscriptionPK AND B.status IN (1,2)
		WHERE A.ValueDate <= @CounterDate and A.Posted = 1
		AND A.Status <> 3 AND A.Revised = 0
		AND A.ClientSubscriptionPK IN
		(
			SELECT DISTINCT A.ClientSubscriptionPK FROM dbo.AgentSubscription A
		)
		GROUP BY B.AgentPK,A.FundPK

		UNION ALL

		SELECT @CounterDate Date,A.FundPKTo FundPk,B.AgentPK, SUM(ISNULL(B.AgentTrxPercent / 100 * A.TotalUnitAmountFundTo,0)) AgentUnit 
		FROM dbo.ClientSwitching A
		LEFT JOIN dbo.AgentSwitching B ON A.ClientSwitchingPK = B.ClientSwitchingPK AND B.status IN (1,2)
		WHERE A.ValueDate <= @CounterDate and A.Posted = 1
		AND A.Status <> 3 AND A.Revised = 0
		AND A.ClientswitchingPK IN
		(
			SELECT DISTINCT A.ClientSwitchingPK FROM dbo.AgentSwitching A
		)
		GROUP BY B.AgentPK,A.FundPKTo
	)A 
	GROUP BY A.Date,A.FundPK,A.AgentPK


	INSERT INTO #AgentPosition
	        ( Date, FundPK, AgentPK, UnitAmount )
	SELECT @CounterDate,A.FundPK,B.AgentPK,SUM(B.UnitAmount) * -1 FROM dbo.ClientRedemption A
	LEFT JOIN AgentRedemption B ON A.FundPK = B.FundPK AND A.ClientRedemptionPK = B.TrxOutPK AND B.TypeOut = 'RED'
	AND A.FundClientPK = B.FundClientPK 
	WHERE A.ValueDate <= @CounterDate and A.Posted = 1
	AND A.Status <> 3 AND A.Revised = 0
	AND A.NAVDate > @CAMDate
	GROUP BY B.AgentPk,A.FundPK

	INSERT INTO #AgentPosition
	( Date, FundPK, AgentPK, UnitAmount )
	SELECT @CounterDate,A.FundPKFrom,B.AgentPK,SUM(B.UnitAmount) * -1 FROM dbo.ClientSwitching A
	LEFT JOIN AgentRedemption B ON A.FundPKFrom = B.FundPK AND A.ClientSwitchingPK = B.TrxOutPK AND B.TypeOut = 'SWI OUT'
	AND A.FundClientPK = B.FundClientPK 
	WHERE A.ValueDate <= @CounterDate and A.Posted = 1
	AND A.Status <> 3 AND A.Revised = 0
		AND A.NAVDate > @CAMDate
	GROUP BY B.AgentPk,A.FundPKFrom

	SET @CounterDate = DATEADD(DAY,1,@CounterDate)
END


Insert Into AgentFundPosition(Date,AgentPK,FundPK,UnitAmount,DbUserID,LastUpdate)
SELECT Date,A.AgentPK,A.FundPK,SUM(A.UnitAmount)  + isnull(B.UnitAmount,0)  UnitAmount,@UsersID,@LastUpdate 
FROM #AgentPosition A 
left join AgentFundPositionAdjustment B on A.AgentPK = B.AgentPK and A.FundPK = B.FundPK
where 1 = 1 AND A.agentPK IS NOT null " + _paramFund + _paramAgent + @"

GROUP BY A.FundPK,A.AgentPK,Date,B.UnitAmount

";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@DateFrom", _agentFundPosition.ValueDateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _agentFundPosition.ValueDateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _agentFundPosition.EntryUsersID);
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

        public void Generate_AgentFeeSummary(AgentFundPosition _agentFundPosition)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _paramFund = "";
                        string _paramAgent = "";
                        string _paramFundText = "";
                        string _paramAgentText = "";

                        if (!_host.findString(_agentFundPosition.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_agentFundPosition.FundFrom))
                        {
                            _paramFund = " And A.FundPK in ( " + _agentFundPosition.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }




                        if (!_host.findString(_agentFundPosition.AgentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_agentFundPosition.AgentFrom))
                        {
                            _paramAgent = " And A.AgentPK in ( " + _agentFundPosition.AgentFrom + " ) ";
                        }
                        else
                        {
                            _paramAgent = "";
                        }


                        cmd.CommandText = @"
    

DELETE A from [dbo].[DailyDataForAgentCommissionRpt] A
where A.Mfeedate between @dateFrom and @Dateto  " + _paramFund + _paramAgent + @"

	
	DECLARE @StartDate datetime
    Declare @EndDate DATETIME
    

    set @StartDate =  dbo.FWorkingDay(@dateFrom,-3)
    set @EndDate = dbo.fworkingday(@DateTo,-1)


	DECLARE @FFSetup TABLE
    (
		Date DATETIME,
		FundPK INT,
		MfeeType INT
	)

	DECLARE @FFSetupDetail TABLE
    (
		Date DATETIME,
		FundPK INT,
		MfeeType INT,
		FeePercent NUMERIC(18,8),
		RangeFrom Numeric(22,4),
		RangeTo NUMERIC(22,4),
		AmortizeDate DATETIME,
		AmortizeAmount NUMERIC(22,4),
		FirstAmortizeDate datetime

	)

	Declare @FeeSetupOptional Table
	(
		Date datetime,
		FundPK int,
		FeePercent numeric(18,8),
		Days int
	)

	DECLARE @FFFundPK INT
	DECLARE @FFDate DATETIME

	DECLARE A CURSOR FOR
		SELECT DISTINCT A.Date,FundPK FROM dbo.ZDT_WorkingDays A,Fund WHERE status = 2
		AND A.date BETWEEN @StartDate AND @DateTo
	OPEN A
	FETCH NEXT FROM A
	INTO @FFDate,@FFFundPK
	
	WHILE @@FETCH_STATUS = 0  
	BEGIN
		
		IF EXISTS(
			SELECT TOP 1 '1' FROM dbo.FundFeeSetup 
		WHERE Status = 2 AND Date =
		(
			SELECT MAX(Date) FROM dbo.FundFeeSetup WHERE status = 2 AND Date<= @FFDate AND FundPK = @FFFundPK
		)AND FundPK = @FFFundPK 
		)
		BEGIN
			INSERT INTO @FFSetup
		        ( Date, FundPK, MfeeType )
		
		SELECT TOP 1 @FFDate,FundPK,FeeType FROM dbo.FundFeeSetup 
		WHERE Status = 2 AND Date =
		(
			SELECT MAX(Date) FROM dbo.FundFeeSetup WHERE status = 2 AND Date<= @FFDate AND FundPK = @FFFundPK
		)AND FundPK = @FFFundPK 
		ORDER BY FundFeeSetupPK DESC
		END
		ELSE
		BEGIN
				INSERT INTO @FFSetup
		        ( Date, FundPK, MfeeType )
				SELECT @FFDate,@FFFundPK,1
		END
		
		IF EXISTS(
			SELECT TOP 1 '1' FROM dbo.FundFeeSetup 
			WHERE Status = 2 AND Date =
			(
				SELECT MAX(Date) FROM dbo.FundFeeSetup WHERE status = 2 AND Date<= @FFDate AND FundPK = @FFFundPK
			)AND FundPK = @FFFundPK 
		)
		BEGIN
				INSERT INTO @FFSetupDetail
		        ( Date ,
		          FundPK ,
		          MfeeType ,
		          FeePercent ,
		          RangeFrom ,
		          RangeTo ,
		          AmortizeDate ,
		          AmortizeAmount,
				  FirstAmortizeDate
		        )
				SELECT  @FFDate,FundPK,FeeType,MiFeePercent,RangeFrom,RangeTo,DateAmortize,MiFeeAmount,Date FROM dbo.FundFeeSetup 
				WHERE Status = 2 AND Date =
				(
					SELECT MAX(Date) FROM dbo.FundFeeSetup WHERE status = 2 AND Date<= @FFDate AND FundPK = @FFFundPK
				)AND FundPK = @FFFundPK 
		END
		ELSE
		BEGIN
			INSERT INTO @FFSetupDetail
		        ( Date ,
		          FundPK ,
		          MfeeType ,
		          FeePercent ,
		          RangeFrom ,
		          RangeTo ,
		          AmortizeDate ,
		          AmortizeAmount,
				  FirstAmortizeDate
		        )
				SELECT @FFDate,@FFFundPK,1,ISNULL(ManagementFeePercent,0)  
				,0,0,'',0,''
				FROM Fund WHERE status = 2 AND FundPK = @FFFundPK
		END
        
		insert into @FeeSetupOptional
		Select @FFDate,FundPK,FeePercent,Days From MFeeSetupOptional
		where status = 2 and FundPK = @FFFundPK
		and Date =
		(
			Select max(date) from MFeeSetupOptional where status = 2
			and FundPK = @FFFundPK and Date <= @FFDate
		)
	
		

		FETCH NEXT FROM A 
		INTO @FFDate,@FFFundPK
	END	
	CLOSE A
	DEALLOCATE A


	DECLARE @AFFundPK INT
	DECLARE @AFAgentPK INT
	DECLARE @AFDate DATETIME
	
	DECLARE @AFSetup TABLE
    (
		Date DATETIME,
		FundPK INT,
		AgentPK INT,
		FeeType INT
	)

	DECLARE @AFSetupDetail TABLE
    (
		Date DATETIME,
		FundPK INT,
		AgentPK INT,
		FeeType INT,
		FeePercent NUMERIC(18,8),
		RangeFrom Numeric(22,4),
		RangeTo NUMERIC(22,4),
		AmortizeDate DATETIME,
		AmortizeAmount NUMERIC(22,4),
		FirstAmortizeDate datetime

	)

	DECLARE @AFFlagAllDate DATETIME
	DECLARE @AFFlagFundDate datetime

	DECLARE A CURSOR FOR
		SELECT DISTINCT A.Date,AgentPK,FundPK FROM dbo.ZDT_WorkingDays A,dbo.AgentFeeSetup WHERE status = 2
		AND A.date BETWEEN @StartDate AND @DateTo
	OPEN A
	FETCH NEXT FROM A
	INTO @AFDate,@AFAgentPK,@AFFundPK
	WHILE @@FETCH_STATUS = 0  
	BEGIN
				SET @AFFlagAllDate = ''
				SET @AFFlagFundDate = ''

			
				SELECT @AFFlagFundDate = MAX(Date) FROM dbo.AgentFeeSetup WHERE Status = 2 AND Date <= @AFDate
					AND AgentPK = @AFAgentPK AND FundPK = @AFFundPK

				SELECT @AFFlagAllDate =  MAX(Date) FROM dbo.AgentFeeSetup WHERE Status = 2 AND Date <= @AFDate
					AND AgentPK = @AFAgentPK AND FundPK = 0

				
				    IF @AFFlagAllDate > ISNULL(@AFFlagFundDate,'01/01/1900')  and @AFFlagFundDate is null
                    --IF (ISNULL(@AFFlagFundDate,'01/01/1900') = '01/01/1900')				
                    BEGIN
					INSERT INTO @AFSetup
					( Date, FundPK, AgentPK, FeeType )
					SELECT  TOP 1 @AFDate Date,@AFFundPK FundPK,AgentPK,FeeType FROM dbo.AgentFeeSetup WHERE status = 2 AND Date =
					(
						SELECT MAX(Date) FROM dbo.AgentFeeSetup WHERE Status = 2 AND Date <= @AFDate
						AND AgentPK = @AFAgentPK AND FundPK = 0
						
					)AND FundPK = 0 AND AgentPK = @AFAgentPK

					INSERT INTO @AFSetupDetail
				        ( Date ,
				          FundPK ,
				          AgentPK ,
				          FeeType ,
				          FeePercent ,
				          RangeFrom ,
				          RangeTo ,
				          AmortizeDate ,
				          AmortizeAmount ,
				          FirstAmortizeDate
				        )
					SELECT TOP 1 @AFDate Date,@AFFundPK FundPK,AgentPK,FeeType,MiFeePercent
					,RangeFrom,RangeTo,DateAmortize,MiFeeAmount,Date
					FROM dbo.AgentFeeSetup WHERE status = 2 AND Date =
					(
						SELECT MAX(Date) FROM dbo.AgentFeeSetup WHERE Status = 2 AND Date <= @AFDate
						AND AgentPK = @AFAgentPK AND FundPK = 0

					)AND FundPK = 0 AND AgentPK = @AFAgentPK
				END
				ELSE
				BEGIN
					INSERT INTO @AFSetup
					( Date, FundPK, AgentPK, FeeType )

					SELECT @AFDate Date,FundPK,AgentPK,FeeType FROM dbo.AgentFeeSetup WHERE status = 2 AND Date =
					(
						SELECT MAX(Date) FROM dbo.AgentFeeSetup WHERE Status = 2 AND Date <= @AFDate
						AND AgentPK = @AFAgentPK AND FundPK = @AFFundPK

					)AND FundPK = @AFFundPK AND AgentPK = @AFAgentPK


						INSERT INTO @AFSetupDetail
				        ( Date ,
				          FundPK ,
				          AgentPK ,
				          FeeType ,
				          FeePercent ,
				          RangeFrom ,
				          RangeTo ,
				          AmortizeDate ,
				          AmortizeAmount ,
				          FirstAmortizeDate
				        )
						SELECT @AFDate Date,@AFFundPK FundPK,AgentPK,FeeType,MiFeePercent
						,RangeFrom,RangeTo,DateAmortize,MiFeeAmount,Date 
						FROM dbo.AgentFeeSetup WHERE status = 2 AND Date =
							(
								SELECT MAX(Date) FROM dbo.AgentFeeSetup WHERE Status = 2 AND Date <= @AFDate
								AND AgentPK = @AFAgentPK AND FundPK = @AFFundPK

							)AND FundPK = @AFFundPK AND AgentPK = @AFAgentPK
				END



				
				

		FETCH NEXT FROM A 
		INTO @AFDate,@AFAgentPK,@AFFundPK
	END	
	CLOSE A
	DEALLOCATE A


    CREATE table #FCP  
	    (
	        FundPK int,
		    Unit numeric(22,4),
		    UnitDate datetime,
		    NAVDate datetime,
		    MFeeDate DATETIME,
			MfeeMethod INT,
			CurrencyPK INT,
			SharingFeeCalculation INT,
			IsHoliday BIT,
			SellingAgentPK INT,
			MFeeType INT,
			SharingFeeType INT,
			IssueDate Datetime
	    )

    insert into #FCP
    Select A.FundPK,sum(isnull(UnitAmount,0)) Unit
	,CASE WHEN B.DT1 = C.IssueDate THEN B.DT1 ELSE  A.Date END UnitDate
	,B.DT1 NAVDate,B.DT2 MFeeDate
	,ISNULL(C.MFeeMethod,1)
	,ISNULL(C.CurrencyPK,1) 
	,ISNULL(C.SharingFeeCalculation,1) 
	,ISNULL(B.IsHoliday,0) 
	,ISNULL(A.AgentPK,0) 
	,ISNULL(F.MfeeType,1) 
	,ISNULL(G.FeeType,1) 
	,C.IssueDate
    from dbo.AgentFundPosition A
	LEFT JOIN ZDT_WorkingDays B ON A.Date = B.Date
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN @FFSetup F ON A.FundPK = F.FundPK AND B.DT2 = F.Date
	LEFT JOIN @AFSetup G ON A.FundPK = G.FundPK AND A.AgentPK = G.AgentPK AND B.DT2 = G.Date
	

    where A.Date between @StartDate and @EndDate
	AND B.IsHoliday = 0
	----------PARAM DISINI
" + _paramFund + _paramAgent + @"

    group by A.FundPK,A.Date,B.DT1,B.DT2,C.MFeeMethod,C.CurrencyPK,C.SharingFeeCalculation,B.IsHoliday
	,A.AgentPK,F.MfeeType,G.FeeType,C.IssueDate
    --having sum(isnull(UnitAmount,0)) > 1
    order by MfeeDate


	--CEK 1
	--Select '1' Cek,* from #FCP where SellingAgentPK = 19 and FundPK = 13
	--order by NAVDate asc



	
	
		DECLARE @FundFC TABLE
        (
			FundPK INT,
			AgentPK int
		)

		INSERT INTO @FundFC
		SELECT DISTINCT FundPK,SellingAgentPK FROM #FCP


	INSERT INTO #FCP
	        ( FundPK ,
	          Unit ,
	          UnitDate ,
	          NAVDate ,
	          MFeeDate ,
	          MfeeMethod ,
	          CurrencyPK ,
	          SharingFeeCalculation ,
	          IsHoliday ,
	          SellingAgentPK , 
	          MFeeType ,
	          SharingFeeType ,
	          IssueDate
	        )

		SELECT  
		Z.FundPK,H.TotalUnitAmount Unit
		,H.ValueDate UnitDate
		,H.ValueDate NAVDate
		,H.ValueDate MFeeDate	
		,ISNULL(B.MFeeMethod,1)
		,ISNULL(B.CurrencyPK,1) 
		,ISNULL(B.SharingFeeCalculation,1) 
		,ISNULL(C.IsHoliday,0) 
		,ISNULL(Z.AgentPK,0) 
		,ISNULL(F.MfeeType,1) 
		,ISNULL(G.FeeType,1) 
		,B.IssueDate
		FROM @FundFC Z
		INNER JOIN Fund B ON Z.FundPK = B.FundPK AND B.status  IN (1,2)
		INNER JOIN 
		(
			SELECT A.ValueDate,A.FundPK,B.AgentPK,SUM(ISNULL(A.TotalUnitAmount * B.AgentTrxPercent / 100,0)) TotalUnitAmount FROM dbo.ClientSubscription A
			LEFT JOIN dbo.AgentSubscription B ON A.ClientSubscriptionPK = B.ClientSubscriptionPK
			WHERE A.Posted = 1 AND A.status = 2 AND A.Revised = 0
			GROUP BY A.ValueDate,B.AgentPK,A.FundPK
		)H ON Z.AgentPK = H.AgentPK AND B.IssueDate = H.ValueDate AND Z.FundPK = H.FundPK
		INNER JOIN @FFSetup F ON Z.FundPK = F.FundPK AND B.IssueDate = F.Date
		INNER JOIN @AFSetup G ON Z.FundPK = G.FundPK AND Z.AgentPK	 = G.AgentPK AND B.IssueDate = G.Date
		INNER JOIN dbo.ZDT_WorkingDays C ON B.IssueDate = C.Date 
		WHERE ValueDate BETWEEN @DateFrom AND @DateTo 

		--Cek2
		--Select '2' Cek,* from #FCP where SellingAgentPK = 19 and FundPK = 13
		--order by NAVDate asc
		
	Declare @FlagDate datetime
    Declare @CFundPK int
    declare @CFundClientPK int
    Declare @UnitBal numeric(22,4)

    DECLARE @cDate TABLE
    (
		Date Datetime
	)

	INSERT INTO @cDate
	Select distinct MFeeDate From #FCP
	
	INSERT #FCP
	        ( FundPK ,
	          Unit ,
	          UnitDate ,
	          NAVDate ,
	          MFeeDate,
			  IsHoliday,
			  SellingAgentPK,
			  CurrencyPK,
			  MfeeMethod,
			  SharingFeeCalculation,
			  MFeeType,
			  SharingFeeType,
			  IssueDate
			  )
	        
	  
	SELECT   B.FundPK,
	CASE WHEN B.UnitDate < @StartDate THEN 0 ELSE  ISNULL(B.Unit,0) END
	,CASE WHEN D.IssueDate = A.DTM1 THEN A.DTM1 ELSE A.DTM2 END UnitDate
	,A.DTM1 NAVDate,A.Date
	--,ISNULL(B.UNIT,0)
	--,B.UnitDate
	--,B.NAVDate NAVDate,A.Date

	,A.IsHoliday,ISNULL(B.SellingAgentPK,0),ISNULL(D.CurrencyPK,0)
	,ISNULL(B.MFeeMethod,1)
	,ISNULL(B.SharingFeeCalculation,1) 
	,ISNULL(F.MfeeType,1) 
	,ISNULL(G.FeeType,1) 
	,B.IssueDate
	FROM dbo.ZDT_WorkingDays A
	LEFT JOIN #FCP B ON B.UnitDate = CASE WHEN B.IssueDate = A.DTM1 THEN A.DTM1 else A.DTM2 END AND B.MFeeDate <> B.IssueDate --AND B.Unit > 0
	--LEFT JOIN #FCP B ON B.MFeeDate = CASE WHEN B.IssueDate = A.DTM1 THEN A.DTM1 else A.DTM2 END --AND B.Unit > 0
	LEFT JOIN Fund D ON B.FundPK = D.FundPK AND D.status IN (1,2)
	LEFT JOIN @FFSetup F ON B.FundPK = F.FundPK AND A.Date = F.Date
	LEFT JOIN @AFSetup G ON B.FundPK = G.FundPK AND B.SellingAgentPK = G.AgentPK AND A.Date = G.Date
	WHERE A.Date NOT IN
		(
		SELECT Date FROM @cDate
		) AND A.Date BETWEEN @DateFrom AND @Dateto	
	AND B.FundPK IS NOT NULL
	AND A.Date BETWEEN @DateFrom AND @DateTo
	

	--cek3
		--Select '3' Cek,* from #FCP where SellingAgentPK = 19 and FundPK = 13
		--order by NAVDate asc

DECLARE @FPCFundAndDate TABLE
(
	FundPK INT,
	Date DATETIME
)

INSERT INTO @FPCFundAndDate
SELECT DISTINCT FundPK,MFeeDate FROM #FCP


DECLARE @LastFundInformation TABLE
(
	Date DATETIME,
	FundPK int,
	NAV NUMERIC(22,8),
	Days INT
)

INSERT INTO @LastFundInformation
SELECT Date
,FundPK
,ISNULL(dbo.FgetLastCloseNav(Date,FundPK),0) 
,ISNULL([dbo].[FgetManagementFeeDaysByDate](Date,FundPK),0)
FROM @FPCFundAndDate




DECLARE @ClientRedemption TABLE
(
	ValueDate DATETIME,
	FundPK INT,
	AgentPK INT,
	TotalUnitAmount NUMERIC(22,4),
	TotalCashAmount NUMERIC(22,4),
	RedemptionFee NUMERIC(22,4)
)

INSERT INTO @ClientRedemption
        ( ValueDate ,
          FundPK ,
          AgentPK ,
          TotalUnitAmount
        )
	SELECT NAVDate,FundPK,AgentPK, sum(ISNULL(UnitAmount,0)) FROM dbo.AgentRedemption A

	WHERE TypeOut = 'RED'
	AND NAVDate between @DateFrom and @DateTo
	GROUP BY FundPK,AgentPK,NAVDate


DECLARE @ClientSubscription TABLE
(
	ValueDate DATETIME,
	FundPK INT,
	AgentPK INT,
	TotalUnitAmount NUMERIC(22,4)
)

INSERT INTO @ClientSubscription
        ( ValueDate ,
          FundPK ,
          AgentPK ,
          TotalUnitAmount 
        )
	SELECT A.ValueDate,A.FundPK,B.AgentPK,SUM(ISNULL(A.TotalUnitAmount * B.AgentTrxPercent / 100,0)) TotalUniAmount 
	FROM dbo.ClientSubscription A
	LEFT JOIN dbo.AgentSubscription B ON A.ClientSubscriptionPK = B.ClientSubscriptionPK
	WHERE A.Posted = 1 AND A.status = 2 AND A.Revised = 0
	AND A.NAVDate BETWEEN @DateFrom AND @DateTo
	GROUP BY A.ValueDate,A.FundPK,B.AgentPK


DECLARE @ClientSwitchingFrom TABLE
(
	ValueDate DATETIME,
	FundPK INT,
	AgentPK INT,
	TotalUnitAmountFundFrom NUMERIC(22,4)
)
INSERT INTO @ClientSwitchingFrom
        ( ValueDate ,
          FundPK ,
          AgentPK ,
          TotalUnitAmountFundFrom 
        )
Select NAVDate,FundPK ,AgentPK, sum(ISNULL(UnitAmount,0)) TotalUnitAmountFundFrom
from dbo.AgentRedemption 
WHERE NAVDate between @DateFrom and @DateTo AND TypeOut = 'SWI OUT'
group by AgentPK,NAVDate,FundPK



DECLARE @ClientSwitching TABLE
(
	ValueDate DATETIME,
	FundPK INT,
	AgentPK INT,
	TotalUnitAmountFundTo NUMERIC(22,4)
)

INSERT INTO @ClientSwitching
        ( ValueDate ,
          FundPK ,
          AgentPK ,
          TotalUnitAmountFundTo 
        )
    SELECT A.ValueDate,A.FundPKTo,B.AgentPK,SUM(ISNULL(A.TotalUnitAmountFundTo * B.AgentTrxPercent / 100,0)) TotalUniAmount 
	FROM dbo.ClientSwitching A
	LEFT JOIN dbo.AgentSwitching B ON A.ClientSwitchingPK = B.ClientSwitchingPK
	WHERE A.Posted = 1 AND A.status = 2 AND A.Revised = 0
	AND A.NAVDate BETWEEN @DateFrom AND @DateTo
	GROUP BY A.ValueDate,A.FundPKTo,B.AgentPK

	 
CREATE table #MFeeInformation 
(
	MfeeDate DATETIME,
	AUMForMFee NUMERIC(22,4),
	MFeeType INT,
	AgentPK INT,
	FundPK INT,
	Days INT
	--MfeePercent NUMERIC(18,8),
	--MFeeAmount NUMERIC(22,4)
)

INSERT INTO #MFeeInformation
        ( MfeeDate, AUMForMFee, MFeeType,AgentPK,FundPK,days )

SELECT A.MFeeDate
, ISNULL(K.UnitAmount  *  case when A.MFeeMethod = 2 and A.CurrencyPK = 1 then 1000 else case when A.MFeeMethod = 2 and A.CurrencyPK <> 1 then 1
else  B.NAV 
end END,0)  AUMForMFee
,1
,A.SellingAgentPK
,A.FundPK
,ISNULL(C.Days,365)

FROM #FCP A
Left join @LastFundInformation B on A.FundPK = B.FundPK and A.NAVDate = B.Date
left join dbo.AgentFundPosition K on A.FundPK = K.FundPK and A.SellingAgentPK = K.AgentPK AND K.Date =  case when A.IsHoliday = 1 then A.NAVDate else A.NAVDate END
left join @FeeSetupOptional C on A.FundPK = C.FundPK and A.MFeeDate = C.Date
WHERE A.MFeeDate BETWEEN @dateFrom AND @DateTo
ORDER BY A.MFeeDate ASC


CREATE Table #MFeeCal 
(
	MFeeDate datetime,
	MFeeAmount numeric(22,4),
	MFeePercent numeric(18,8),
	AgentPK INT,
	FundPK INT
	)
	
INSERT INTO #MFeeCal
        ( MFeeDate, MFeeAmount, MFeePercent,AgentPK,FundPK )

SELECT 
A.MfeeDate

--MFeeAmount
,CASE WHEN A.MfeeDate >= ISNULL(D.DT1,'01/01/19') then ISNULL(CASE WHEN A.MFeeType = 1 THEN A.AUMForMFee * ((isnull(E.FeePercent,0) / 1.1)) * 1.1  / 100 / A.Days
WHEN A.MFeeType = 2 THEN 
	CASE WHEN A.AUMForMFee > B.RangeTo then
		(B.RangeTo - B.RangeFrom) * ((B.FeePercent / 1.1) - 0.045) * 1.1 / 100 / A.Days 
		ELSE CASE WHEN A.AUMForMFee > B.RangeFrom THEN (A.AUMForMFee - B.RangeFrom) * ((B.FeePercent / 1.1)) * 1.1 / 100 / A.Days ELSE 0 END END
WHEN A.MFeeType = 3 THEN
	CASE WHEN A.AUMForMfee BETWEEN B.RangeFrom AND B.RangeTo 
		THEN A.AUMForMFee * ((B.FeePercent / 1.1) - 0.045) * 1.1 / 100 / A.Days ELSE 0 END
WHEN A.MFeeType = 4 AND A.MfeeDate <= B.AmortizeDate THEN
	CASE WHEN DATEDIFF(DAY,B.FirstAmortizeDate,B.AmortizeDate) > 0 AND B.AmortizeAmount > 0 
		THEN B.AmortizeAmount / (DATEDIFF(DAY,B.FirstAmortizeDate,B.AmortizeDate) + 1) ELSE 0 END
END,0) ELSE 0 END
,ISNULL(((isnull(E.FeePercent,0) / 1.1)) * 1.1,0)
,A.AgentPK
,A.FundPK
FROM #MFeeInformation A
LEFT JOIN @FFSetupDetail B ON A.MfeeDate = B.Date AND A.MFeeType = B.MfeeType AND A.FundPK = B.FundPK
LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
LEFT JOIN dbo.ZDT_WorkingDays D ON C.IssueDate = D.Date 
left join @FeeSetupOptional E on A.FundPK = E.FundPK and A.MfeeDate = E.Date



--SELECT * FROM #MFeeCal WHERE agentPK = 52
--SELECT * FROM #MFeeInformation WHERE AgentPK = 52 ORDER BY FundPK,MfeeDate ASC
--SELECT * FROM @FFSetupDetail WHERE FundPK = 30

-- CHECK PERHITUNGAN FEE DETAIL DISINI
--SELECT * FROM #MFeeInformation ORDER BY MfeeDate
--SELECT * FROM @FFSetupDetail WHERE fundPK = 235 ORDER BY date asc
--SELECT * FROM #MFeeCal ORDER BY MFeeDate asc
---------
 




CREATE table #MFeeCalSummary 
(
	MFeeDate datetime,
	MFeeAmount numeric(22,4),
	MFeePercent numeric(18,8),
	AgentPK INT,
	FundPK INT
)

INSERT INTO #MFeeCalSummary

        ( MFeeDate ,
          MFeeAmount ,
          MFeePercent ,
          AgentPK ,
          FundPK
        )
SELECT MfeeDate,SUM(ISNULL(MFeeAmount,0)),AVG(MfeePercent),AgentPK,FundPK FROM #MFeeCal
GROUP BY MFeeDate,AgentPK,FundPK



 
CREATE table #MAgentFeeInformation 
(
	MfeeDate DATETIME,
	AgentFeeType INT,
	AgentPK INT,
	FundPK INT,
	AUMForMFee NUMERIC(22,4),
	MFeeAmount NUMERIC(22,4),
	Days INT,
	MFeePercent NUMERIC(18,8)
)

INSERT INTO #MAgentFeeInformation
SELECT A.MFeeDate
,ISNULL(A.SharingFeeType,1)
,A.SellingAgentPK
,A.FundPK

,isnull(Case when A.SharingFeeCalculation = 1 then K.UnitAmount 
*  case when A.MFeeMethod = 2 and A.CurrencyPK = 1 then 1000 else case when A.MFeeMethod = 2 and A.CurrencyPK <> 1 then 1
else  ISNULL(B.NAV,0) 
end end else  K.UnitAmount * 
    case when A.MFeeMethod = 2 and A.CurrencyPK = 1 then 1000 else case when A.MFeeMethod = 2 and A.CurrencyPK <> 1 then 1
ELSE case when A.IsHoliday = 1 THEN B.NAV ELSE B.NAV END
end end END,0)  AUMForMFee
,ISNULL(M.MFeeAmount,0) MFee

--,ISNULL(B.Days,365)
,365
,M.MFeePercent
FROM #FCP A
Left join @LastFundInformation B on A.FundPK = B.FundPK and A.NAVDate = B.Date
LEFT JOIN #MFeeCalSummary M ON A.FundPK = M.FundPK  AND A.SellingAgentPK = M.AgentPK AND A.MFeeDate = M.MFeeDate
left join dbo.AgentFundPosition K on A.FundPK = K.FundPK and A.SellingAgentPK = K.AgentPK AND K.Date =  case when A.IsHoliday = 1 then A.NAVDate else A.NAVDate END
Left join @LastFundInformation Y on A.FundPK = Y.FundPK and A.MFeeDate = Y.Date 

WHERE A.MFeeDate BETWEEN @dateFrom AND @DateTo
ORDER BY A.MFeeDate ASC

--SELECT * FROM #MAgentFeeInformation ORDER BY MfeeDate

CREATE TABLE #MAgentFeeCal 
(
	MFeeDate DATETIME,
	AgentFeeAmount NUMERIC(22,4),
	AgentFeePercent NUMERIC(18,8),
	FundPK INT,
	AgentPK INT
)

--1 FLAT
--2 Tiering By Aum
--3 Tiering By MFee
--4 Prog by Aum
--5 Prog by MFee
--6 Amortize

INSERT INTO #MAgentFeeCal
SELECT A.MfeeDate
,
CASE WHEN D.SharingfeeCalculation = 1  and A.MfeeDate >= E.DT2 then
ISNULL(CASE WHEN A.AgentFeeType = 1 THEN CASE WHEN A.MfeePercent > 0 THEN A.AUMForMFee * A.MFeePercent/100 / A.Days * ISNULL(B.FeePercent,C.Feepercent) / 100 
ELSE A.MFeeAmount * ISNULL(B.FeePercent,C.Feepercent) / 100 END
	 WHEN A.AgentFeeType = 2 THEN 
		CASE WHEN A.AUMForMFee > B.RangeTo THEN  	
		(B.RangeTo - B.RangeFrom) * ISNULL(B.FeePercent,C.Feepercent) / 100 / A.Days 
		 ELSE CASE WHEN A.AUMForMFee > B.RangeFrom THEN (A.AUMForMFee - B.RangeFrom) * ISNULL(B.FeePercent,C.Feepercent) / 100 / A.Days ELSE 0 END END
	WHEN A.AgentFeeType = 3 THEN          
		CASE WHEN A.MFeeAmount > B.RangeTo THEN  	
		(B.RangeTo - B.RangeFrom) * ISNULL(B.FeePercent,C.Feepercent) / 100 
		ELSE CASE WHEN A.MFeeAmount > B.RangeFrom THEN  (A.MFeeAmount - B.RangeFrom) * ISNULL(B.FeePercent,C.Feepercent) / 100 ELSE 0 END  END    
	WHEN A.AgentFeeType = 4 THEN
		CASE WHEN A.AUMForMfee BETWEEN B.RangeFrom AND B.RangeTo 
		THEN A.AUMForMFee * ISNULL(B.FeePercent,C.Feepercent) / 100 / A.Days ELSE 0 END
	WHEN A.AgentFeeType = 5 THEN
		CASE WHEN A.MFeeAmount BETWEEN B.RangeFrom AND B.RangeTo 
		THEN A.MFeeAmount * ISNULL(B.FeePercent,C.Feepercent) / 100  ELSE 0 END
	WHEN A.AgentFeeType = 6 AND A.MfeeDate <= B.AmortizeDate THEN
		CASE WHEN DATEDIFF(DAY,B.FirstAmortizeDate,B.AmortizeDate) > 0 AND B.AmortizeAmount > 0 
		THEN B.AmortizeAmount / (DATEDIFF(DAY,B.FirstAmortizeDate,B.AmortizeDate) + 1) ELSE 0 END
 END,0) 
 
 ELSE CASE WHEN D.SharingfeeCalculation = 2 AND A.MfeeDate >= E.DT1 THEN

 ISNULL(CASE WHEN A.AgentFeeType = 1 THEN CASE WHEN A.MfeePercent > 0 THEN A.AUMForMFee * A.MFeePercent/100 / A.Days * ISNULL(B.FeePercent,C.Feepercent) / 100 
ELSE A.MFeeAmount * ISNULL(B.FeePercent,C.Feepercent) / 100 END
	 WHEN A.AgentFeeType = 2 THEN 
		CASE WHEN A.AUMForMFee > B.RangeTo THEN  	
		(B.RangeTo - B.RangeFrom) * ISNULL(B.FeePercent,C.Feepercent) / 100 / A.Days 
		 ELSE CASE WHEN A.AUMForMFee > B.RangeFrom THEN (A.AUMForMFee - B.RangeFrom) * ISNULL(B.FeePercent,C.Feepercent) / 100 / A.Days ELSE 0 END END
	WHEN A.AgentFeeType = 3 THEN          
		CASE WHEN A.MFeeAmount > B.RangeTo THEN  	
		(B.RangeTo - B.RangeFrom) * ISNULL(B.FeePercent,C.Feepercent) / 100 
		ELSE CASE WHEN A.MFeeAmount > B.RangeFrom THEN  (A.MFeeAmount - B.RangeFrom) * ISNULL(B.FeePercent,C.Feepercent) / 100 ELSE 0 END  END    
	WHEN A.AgentFeeType = 4 THEN
		CASE WHEN A.AUMForMfee BETWEEN B.RangeFrom AND B.RangeTo 
		THEN A.AUMForMFee * ISNULL(B.FeePercent,C.Feepercent) / 100 / A.Days ELSE 0 END
	WHEN A.AgentFeeType = 5 THEN
		CASE WHEN A.MFeeAmount BETWEEN B.RangeFrom AND B.RangeTo 
		THEN A.MFeeAmount * ISNULL(B.FeePercent,C.Feepercent) / 100  ELSE 0 END
	WHEN A.AgentFeeType = 6 AND A.MfeeDate <= B.AmortizeDate THEN
		CASE WHEN DATEDIFF(DAY,B.FirstAmortizeDate,B.AmortizeDate) > 0 AND B.AmortizeAmount > 0 
		THEN B.AmortizeAmount / (DATEDIFF(DAY,B.FirstAmortizeDate,B.AmortizeDate) + 1) ELSE 0 END
 END,0) ELSE 0 END END
 


 ,CASE WHEN B.fundPK IS NOT NULL AND B.FeeType = 6 THEN 0     
    WHEN C.FundPK IS NOT NULL AND C.FeeType =6 THEN 0 ELSE
CASE WHEN ISNULL(B.FeePercent,0) > 0  THEN B.FeePercent ELSE ISNULL(C.FeePercent,0) END END
 ,A.FundPK
 ,A.AgentPK
FROM #MAgentFeeInformation A
LEFT JOIN @AFSetupDetail B ON A.FundPK = B.FundPK AND A.AgentPK = B.AgentPK AND A.MfeeDate = B.Date
LEFT JOIN @AFSetupDetail C ON C.FundPK = 0 AND A.MfeeDate = C.Date AND A.AgentPK = C.AgentPK
LEFT JOIN Fund D ON A.FundPK = D.FundPK AND D.status IN (1,2)
LEFT JOIN dbo.ZDT_WorkingDays E ON D.IssueDate = E.Date



CREATE table #AgentFeeCalSummary 
(
	MFeeDate datetime,
	AgentFeeAmount numeric(22,4),
	AgentFeePercent numeric(18,8),
	FundPK INT,
	AgentPK INT
)
INSERT INTO #AgentFeeCalSummary
SELECT MFeeDate,SUM(ISNULL(AgentFeeAmount,0)),AVG(AgentFeePercent) 
,FundPK,AgentPK
FROM #MAgentFeeCal
GROUP BY MFeeDate,FundPK,AgentPK


-- BIKIN TABLE DailyDataForCommissionRptNEW

INSERT INTO DailyDataForAgentCommissionRpt
SELECT  A.UnitDate,A.NAVDate,A.MFeeDate,A.FundPK
,A.SellingAgentPK  
--NAV
,case when A.MFeeMethod = 2 and A.CurrencyPK = 1 then 1000 else case when A.MFeeMethod = 2 and A.CurrencyPK <> 1 then 1
else  ISNULL(Y.NAV,0)
END END NAV

,isnull(case when A.IsHoliday = 1 then isnull(J.UnitAmount,0)  else isnull(J.UnitAmount,0)  end 
* case when A.IsHoliday = 1 THEN ISNULL(B.NAV,0) ELSE ISNULL(Y.NAV,0) END,0) AUM


--,A.MFeeType

-- AUM FOR Mfee
,isnull(J.UnitAmount  *  case when A.MFeeMethod = 2 and A.CurrencyPK = 1 then 1000 else case when A.MFeeMethod = 2 and A.CurrencyPK <> 1 then 1
else  B.NAV 
end END,0)  AUMForMFee

--MFee
,ISNULL(M.MFeeAmount,0) MFee
,ISNULL(M.MFeePercent,0) MFeePercent
,A.MFeeType

--AgentFee
,ISNULL(P.AgentFeeAmount,0) AgentFee
,ISNULL(P.AgentFeePercent,0) AgentFeePercent
,A.SharingFeeType
, case when A.IsHoliday = 1 then isnull(J.UnitAmount,0)  else isnull(J.UnitAmount,0)  end UnitAmount

,isnull(A.CurrencyPK,'') Currency
,isnull(I.TotalUnitAmount,0) * ISNULL(Y.NAV,0) SubsAmount
,isnull(F.TotalUnitAmount,0) * ISNULL(Y.NAV,0) RedempAmount
,isnull(G.TotalUnitAmountFundTo,0) * ISNULL(Y.NAV,0) SwitchInAmount
,ISNULL(H.TotalUnitAmountFundFrom,0) * ISNULL(Y.NAV,0) SwitchOutAmount
,isnull(I.TotalUnitAmount,0) SubsUnit
,isnull(F.TotalUnitAmount,0) RedempUnit
,isnull(G.TotalUnitAmountFundTo,0) SwitchInUnit
,ISNULL(H.TotalUnitAmountFundFrom,0) SwitchOutUnit


FROM #FCP A
Left join @LastFundInformation B on A.FundPK = B.FundPK and A.NAVDate = B.Date
Left join @LastFundInformation Y on A.FundPK = Y.FundPK and A.MFeeDate = Y.Date 
left join @ClientRedemption F on A.FundPK = F.FundPK and A.SellingAgentPK = F.AgentPK AND A.MFeeDate = F.ValueDate
left JOIN @ClientSwitching G on A.FundPK = G.FundPK and A.SellingAgentPK = G.AgentPK and A.MFeeDate = G.ValueDate
left JOIN @ClientSwitchingFrom H on A.FundPK = H.FundPK and A.SellingAgentPK = H.AgentPK and A.MFeeDate = H.ValueDate
left join @ClientSubscription I on A.FundPK = I.FundPK and A.SellingAgentPK = I.AgentPK and A.MFeeDate = I.ValueDate
LEFT JOIN #MFeeCalSummary M ON A.FundPK = M.FundPK  AND A.SellingAgentPK = M.AgentPK AND A.MFeeDate = M.MFeeDate
left join dbo.AgentFundPosition L on A.FundPK = L.FundPK and A.SellingAgentPK = L.AgentPK AND L.Date =  A.UnitDate
left join AgentFundPosition J on A.FundPK = J.FundPK and A.SellingAgentPK = J.AgentPK and J.Date =  A.NAVDate
LEFT JOIN #AgentFeeCalSummary P ON A.SellingAgentPK = P.AgentPK AND A.MFeeDate = P.MFeeDate AND A.FundPK = P.FundPK 
WHERE A.MFeeDate BETWEEN @dateFrom AND @DateTo
ORDER BY A.MFeeDate ASC


CREATE TABLE #Subs
(
	AgentPK INT,
	FundPK INT,
	MinMfeeDate DATETIME,
	MfeePercent NUMERIC(8,4),
	AgentFeePercent NUMERIC(8,4)
)

INSERT INTO #Subs
        ( AgentPK, FundPK, MinMfeeDate )
SELECT AgentPK,FundPK, MIN(MFeeDate) FROM dbo.DailyDataForAgentCommissionRpt A
WHERE 1=1 

" + _paramFund + _paramAgent + @"


-- PARAM DISINI
 
GROUP BY AgentPK,FundPK



UPDATE A SET  A.MfeePercent = B.MFeePercent, A.AgentFeePercent = B.AgentFeePercent FROM #Subs A
LEFT JOIN dbo.DailyDataForAgentCommissionRpt B ON A.AgentPK = B.AgentPK AND A.FundPK = B.FundPK AND A.MinMfeeDate = B.MFeeDate




INSERT INTO dbo.DailyDataForAgentCommissionRpt
        ( UnitDate ,
          NAVDate ,
          MFeeDate ,
          FundPK ,
          AgentPK ,
          NAV ,
          AUM ,
          AUMForMFee ,
          MFee ,
          MFeePercent ,
          MFeeType ,
          AgentFee ,
          AgentFeePercent ,
          SharingFeeType ,
          UnitAmount ,
          Currency ,
          SubsAmount ,
          RedempAmount ,
          SwitchInAmount ,
          SwitchOutAmount ,
          SubsUnit ,
          RedempUnit ,
          SwitchInUnit ,
          SwitchOutUnit
        )
SELECT B.ValueDate,Valuedate,B.ValueDate,B.FundPK,A.AgentPK
,B.NAV,B.NAV * B.TotalUniAmount,B.NAV * B.TotalUniAmount,0
,isnull(A.MfeePercent,0),1,0,A.AgentFeePercent,1,B.TotalUniAmount,B.CurrencyPK,ISNULL(B.TotalUniAmount * B.NAV,0),0,0,0,B.TotalUniAmount,0,0,0
FROM #Subs A
LEFT JOIN
(
	SELECT A.ClientSubscriptionPK,A.ValueDate,A.FundPK,B.AgentPK,SUM(ISNULL(A.TotalUnitAmount * B.AgentTrxPercent / 100,0)) TotalUniAmount 
	,A.CurrencyPK,A.NAV
	FROM dbo.ClientSubscription A
	LEFT JOIN dbo.AgentSubscription B ON A.ClientSubscriptionPK = B.ClientSubscriptionPK
	WHERE A.Posted = 1 AND A.status = 2 AND A.Revised = 0
	AND A.NAVDate BETWEEN @DateFrom AND @DateTo
	GROUP BY A.ValueDate,A.FundPK,B.AgentPK,A.CurrencyPK,A.NAV,A.ClientSubscriptionPK
)B ON A.AgentPK = B.AgentPK AND A.FundPK = B.FundPK AND B.ValueDate < A.MinMfeeDate
LEFT JOIN @LastFundInformation C on A.FundPK = C.FundPK and B.ValueDate = C.Date
WHERE B.ClientSubscriptionPK IS NOT NULL and B.ValueDate between @datefrom and @Dateto



INSERT INTO dbo.DailyDataForAgentCommissionRpt
        ( UnitDate ,
          NAVDate ,
          MFeeDate ,
          FundPK ,
          AgentPK ,
          NAV ,
          AUM ,
          AUMForMFee ,
          MFee ,
          MFeePercent ,
          MFeeType ,
          AgentFee ,
          AgentFeePercent ,
          SharingFeeType ,
          UnitAmount ,
          Currency ,
          SubsAmount ,
          RedempAmount ,
          SwitchInAmount ,
          SwitchOutAmount ,
          SubsUnit ,
          RedempUnit ,
          SwitchInUnit ,
          SwitchOutUnit
        )
SELECT B.ValueDate,Valuedate,B.ValueDate,B.FundPKTo,A.AgentPK
,B.NAVFundTo,0,0,0,isnull(A.MfeePercent,0),1,0,A.AgentFeePercent,1,0,B.CurrencyPK,ISNULL(B.TotalUniAmount * B.NAVFundTo,0),0,0,0,B.TotalUniAmount,0,0,0
FROM #Subs A
LEFT JOIN
(
	SELECT A.ClientSwitchingPK,A.ValueDate,A.FundPKTo,B.AgentPK,SUM(ISNULL(A.TotalUnitAmountFundTo * B.AgentTrxPercent / 100,0)) TotalUniAmount 
	,A.CurrencyPK,A.NAVFundTo
	FROM dbo.ClientSwitching A
	LEFT JOIN dbo.AgentSwitching B ON A.ClientSwitchingPK = B.ClientSwitchingPK
	WHERE A.Posted = 1 AND A.status = 2 AND A.Revised = 0
	AND A.NAVDate BETWEEN @DateFrom AND @DateTo
	GROUP BY A.ValueDate,A.FundPKTo,B.AgentPK,A.ClientSwitchingPK,A.CurrencyPK,A.NAVFundTo

)B ON A.AgentPK = B.AgentPK AND A.FundPK = B.FundPKTo AND B.ValueDate < A.MinMfeeDate
WHERE B.ClientSwitchingPK IS NOT NULL  and B.ValueDate between @datefrom and @Dateto



DECLARE @ZCAgentPK INT
DECLARE @ZCFundPK INT
DECLARE @ZCMinMfeeDate Datetime
DECLARE @ZCMIFeePercent NUMERIC(18,4)
DECLARE @ZCMIAgentPercent NUMERIC(18,4)

Declare A Cursor For
	SELECT A.AgentPK,A.FundPK,A.MinMfeeDate,A.MfeePercent,A.AgentFeePercent FROM #Subs A
	WHERE MinMfeeDate > @DateFrom
Open A
Fetch Next From A
INTO @ZCAgentPK,@ZCFundPK,@ZCMinMfeeDate,@ZCMIFeePercent,@ZCMIAgentPercent
While @@FETCH_STATUS = 0  
Begin




INSERT INTO dbo.DailyDataForAgentCommissionRpt
        ( UnitDate ,
          NAVDate ,
          MFeeDate ,
          FundPK ,
          AgentPK ,
          NAV ,
          AUM ,
          AUMForMFee ,
          MFee ,
          MFeePercent ,
          MFeeType ,
          AgentFee ,
          AgentFeePercent ,
          SharingFeeType ,
          UnitAmount ,
          Currency ,
          SubsAmount ,
          RedempAmount ,
          SwitchInAmount ,
          SwitchOutAmount ,
          SubsUnit ,
          RedempUnit ,
          SwitchInUnit ,
          SwitchOutUnit
        )
	SELECT A.Date,A.Date,A.Date,@ZCFundPK,@ZCAgentPK
	,ISNULL(D.NAV,F.NAV),ISNULL(H.UnitAmount * F.Nav,0),ISNULL(H.UnitAmount * F.Nav,0),isnull(ISNULL(H.UnitAmount * F.Nav,0) * @ZCMIFeePercent / 100 / I.Days,0)
	,isnull(@ZCMIFeePercent,0),1,0,isnull(@ZCMIFeePercent,0),1,ISNULL(H.UnitAmount,0),E.CurrencyPK,0,0,0,0,0,0,0,0 
	FROM dbo.ZDT_WorkingDays A
	LEFT JOIN Agent B ON B.AgentPK = @ZCAgentPK AND B.status IN (1,2)
	LEFT JOIN CloseNAV D ON D.FundPK = @ZCFundPK AND D.Date = A.Date AND D.Status IN (1,2)
	LEFT JOIN Fund E ON E.FundPK = @ZCFundPK AND E.status IN (1,2) 
	LEFT JOIN CloseNAV F ON F.FundPK = @ZCFundPK AND F.Date = A.DTM1 AND F.Status IN (1,2)
	LEFT JOIN dbo.AgentFundPosition G ON A.DTM1 = G.Date AND G.AgentPK = @ZCAgentPK AND G.FundPK = @ZCFundPK
	LEFT JOIN dbo.AgentFundPosition H ON A.Date = H.Date AND H.AgentPK = @ZCAgentPK AND H.FundPK = @ZCFundPK
	LEFT JOIN @LastFundInformation I on I.FundPK = @ZCFundPK and I.Date = A.Date
	WHERE A.date NOT IN
    (
		SELECT MFeeDate FROM dbo.DailyDataForAgentCommissionRpt
		WHERE AgentPK = @ZCAgentPK AND FundPK = @ZCFundPK
		AND MFeeDate BETWEEN @DateFrom AND @ZCMinMfeeDate
	)
	AND A.Date >
    (
		SELECT MIN(A.Date) FROM (
			
			SELECT ISNULL(MIN(NAVDate),'01/01/2099') Date FROM dbo.ClientSubscription A
			LEFT JOIN dbo.AgentSubscription B ON A.ClientSubscriptionPK = B.ClientSubscriptionPK
			WHERE B.AgentPK = @ZCAgentPK AND A.FundPK = @ZCFundPK
			AND Posted = 1 AND A.status = 2 AND Revised = 0 AND NAVDate < @ZCMinMfeeDate
			
			UNION ALL
			
			Select ISNULL(MIN(NAVDate),'01/01/2099') Date
			from dbo.ClientSwitching A
			LEFT JOIN dbo.AgentSwitching B ON A.ClientSwitchingPK = B.ClientSwitchingPK
			WHERE B.AgentPK = @ZCAgentPK AND A.FUndPKTo = @ZCFundPK
			AND Posted = 1 AND A.status = 2 AND Revised = 0 AND NAVDate < @ZCMinMfeeDate
			) A
	)
	AND A.Date BETWEEN @DateFrom AND @ZCMinMfeeDate

	Fetch Next From A 
	INTO @ZCAgentPK,@ZCFundPK,@ZCMinMfeeDate,@ZCMIFeePercent,@ZCMIAgentPercent
End	
Close A
Deallocate A


";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@DateFrom", _agentFundPosition.ValueDateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _agentFundPosition.ValueDateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _agentFundPosition.EntryUsersID);
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