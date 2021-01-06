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
    public class FundClientPositionReps
    {
        Host _host = new Host();

        //2
        private FundClientPosition setFundClientPosition(SqlDataReader dr)
        {
            FundClientPosition M_FundClientPosition = new FundClientPosition();
            M_FundClientPosition.FundClientPositionPK = Convert.ToInt32(dr["FundClientPositionPK"]);
            M_FundClientPosition.Date = Convert.ToString(dr["Date"]);
            M_FundClientPosition.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_FundClientPosition.FundClientID = Convert.ToString(dr["FundClientID"]);
            M_FundClientPosition.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_FundClientPosition.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundClientPosition.FundID = Convert.ToString(dr["FundID"]);
            M_FundClientPosition.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
            M_FundClientPosition.SID = Convert.ToString(dr["SID"]);
            M_FundClientPosition.IFUA = Convert.ToString(dr["IFUA"]);
            M_FundClientPosition.AUM = Convert.ToDecimal(dr["AUM"]);
            M_FundClientPosition.AvgNAV = Convert.ToDecimal(dr["AvgNAV"]);
            M_FundClientPosition.CostValue = Convert.ToDecimal(dr["CostValue"]);
            M_FundClientPosition.Unrealized = Convert.ToDecimal(dr["Unrealized"]);
            M_FundClientPosition.DBUserID = dr["DBUserID"].ToString();
            M_FundClientPosition.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundClientPosition.LastUpdate = dr["LastUpdate"].ToString();
            M_FundClientPosition.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FundClientPosition;
        }



        public List<FundClientPosition> FundClientPosition_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientPosition> L_FundClientPosition = new List<FundClientPosition>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                              @"Select isnull(B.ID,'') FundClientID,isnull(B.Name,'') FundClientName,isnull(C.ID,'') FundID,isnull(B.SID,'') SID, isnull(B.IFUACode,'') IFUA,A.UnitAmount * isnull(G.Nav,0) AUM,(isnull(G.Nav,0) - isnull(A.AVGNav,0)) * A.UnitAmount Unrealized ,isnull(A.AVGNav,0) * A.UnitAmount CostValue,A.* from FundClientPosition A 
                                left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2) 
                                left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                left join CloseNAV G on A.FundPK = G.FundPK and G.status = 2 and G.Date = A.Date
                                where  A.Date between @DateFrom and @DateTo order By B.Name  ";
                        }
                        else
                        {
                            cmd.CommandText =
                              @"Select isnull(B.ID,'') FundClientID,isnull(B.Name,'') FundClientName,isnull(C.ID,'') FundID,isnull(B.SID,'') SID, isnull(B.IFUACode,'') IFUA,A.UnitAmount * isnull(G.Nav,0) AUM,(isnull(G.Nav,0) - isnull(A.AVGNav,0)) * A.UnitAmount Unrealized ,isnull(A.AVGNav,0) * A.UnitAmount CostValue,A.* from FundClientPosition A 
                                left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2) 
                                left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                left join CloseNAV G on A.FundPK = G.FundPK and G.status = 2 and G.Date = A.Date
                                where  A.Date between @DateFrom and @DateTo order By B.Name  ";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClientPosition.Add(setFundClientPosition(dr));
                                }
                            }
                            return L_FundClientPosition;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public decimal Validate_CashAmountByDate(int _fundPK, int _fundClientPK, DateTime _date)
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
                            select isnull(sum(B.Nav * A.UnitAmount),0) CashAmount from FundClientPosition A
                            left join 
                                (
                                select * from CloseNAV Where  FundPK = @FundPK and Date =
                                (
                                    Select max(Date) From CloseNAV where Date <= @Date  and FundPK = @FundPK and status = 2
                                ) and status = 2
                                )B on A.FundPK = B.FundPK  and B.Status = 2
                            where  A.Date = (select max(Date) From FundClientPosition where Date <= @date and A.FundPK = @FundPK and FundClientPK = @FundClientPK)
                            and A.FundPK = @FundPK and FundClientPK = @FundClientPK
                        ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToDecimal(dr["CashAmount"]);
                                }
                            }
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


        public string FundClientPosition_GenerateAverageFCP(DateTime _date)
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
                            Declare @CFundClientPK int,@CFundPK int
                            DECLARE @RunningAmount NUMERIC(22, 8) 
                            DECLARE @RunningBalance NUMERIC(22, 8) 
                            DECLARE @RunningAvgPrice NUMERIC(22, 8) 
                            DECLARE @COUNTER INT

                            DECLARE @PeriodPK INT 
                            DECLARE @BegPeriodPK INT 

                            SELECT @BegPeriodPK = periodpk 
                            FROM   period 
                            WHERE  Dateadd(yy, Datediff(yy, 0, Dateadd(year, -1, @Date)) + 1, -1) 
                                    BETWEEN datefrom 
                                        AND dateto 
                                    AND status = 2 

                            SELECT @PeriodPK = periodpk 
                            FROM   period 
                            WHERE  @Date BETWEEN datefrom AND dateto 
                                    AND status = 2 

                            DECLARE @Mode INT -- ntar mode Ambil dari Setup Accounting         
                            SET @Mode = 1 -- 1 = priority Buy, 2 = priority Sell         
                            set @COUNTER = 1

                            Create table #AVGRecalculation 
                                ( 
		                            IdentInt int null,
                                    [runningamount]   [NUMERIC](22, 8) NULL, 
                                    [runningbalance]  [NUMERIC](22, 8) NULL, 
                                    [runningavgprice] [NUMERIC](22, 8) NULL, 
		                            FundClientPK int,
		                            FundPK int,
                                    [valuedate]       [DATETIME] NULL, 
                                    [volume]          [NUMERIC](22, 8) NULL, 
                                    [amount]          [NUMERIC](22, 8) NULL, 
                                    [price]           [NUMERIC](22, 8) NULL, 
                                    [trxtype]         [INT] NULL
                                ) 
                            CREATE CLUSTERED INDEX indx_AVGRecalculation ON #AVGRecalculation (FundPK,FundClientPK);

                            Create table #AVGResultList
                            (
	                            Date Datetime,
	                            FundClientPK int,
	                            FundPK int
                            )
                            CREATE CLUSTERED INDEX indx_AVGResultList ON #AVGResultList (FundPK,FundClientPK);

                            Create table #AVGResult
                            (
	                            FundClientPK int,
	                            FundPK int,
	                            AVGNav numeric(22,8)
                            )
                            CREATE CLUSTERED INDEX indx_AVGResult ON #AVGResult (FundPK,FundClientPK);

	                            INSERT INTO #AVGRecalculation(runningamount,runningbalance,runningavgprice,FundClientPK,FundPK,valuedate,volume,amount,price,trxtype) 
                                  SELECT Cast(0 AS NUMERIC(22, 8)) RunningAmount, 
                                         Cast(0 AS NUMERIC(22, 8)) RunningBalance, 
                                         Cast(0 AS NUMERIC(22, 8)) RunningAvgPrice, 
                                         * 
                                  FROM   (SELECT FundClientPK,FundPK,'12/31/2000'        ValueDate, 
                                                 unitamount, 
                                                 unitamount * avgnav Amount, 
                                                 avgnav              Price, 
                                                 1                   TrxType 
                                          FROM   fundclientpositionendyear 
                                          WHERE   periodpk = @PeriodPK  and status = 2
                                          UNION ALL 
                                          SELECT FundClientPK,FundPK,valuedate, 
                                                 totalunitamount, 
                                                 totalcashamount, 
                                                 nav, 
                                                 1 
                                          FROM   clientsubscription 
                                          WHERE   posted = 1 
                                                 AND revised = 0 
                                                 AND status = 2 
                                                 AND Year(valuedate) = Year(@Date) 
					 			                             and isnull(NOTES,'') <> 'Cutoff'
                                          UNION ALL 
                                          SELECT FundClientPK,FundPK,valuedate, 
                                                 unitamount, 
                                                 cashamount, 
                                                 nav, 
                                                 2 
                                          FROM   clientredemption 
                                          WHERE   posted = 1 
                                                 AND revised = 0 
                                                 AND status = 2 
                                                 AND Year(valuedate) = Year(@Date) 
                                          UNION ALL 
                                          SELECT FundClientPK,FundPKTo,valuedate, 
                                                 totalunitamountfundto, 
                                                 totalcashamountfundto, 
                                                 navfundto NAV, 
                                                 1 
                                          FROM   clientswitching 
                                          WHERE   posted = 1 
                                                 AND revised = 0 
                                                 AND status = 2 
                                                 AND Year(valuedate) = Year(@Date) 
                                          UNION ALL 
                                          SELECT FundClientPK,FundPKFrom,valuedate, 
                                                 unitamount, 
                                                 unitamount * navfundfrom, 
                                                 navfundfrom NAV, 
                                                 2 
                                          FROM   clientswitching 
                                          WHERE   posted = 1 
                                                 AND revised = 0 
                                                 AND status = 2 
                                                 AND Year(valuedate) = Year(@Date)) dt 
					 

                            Declare A Cursor For
	                            Select Distinct  FundClientPK,FundPK from FundClientPosition where Date = @Date
                            Open A
                            Fetch Next From A
                            Into @CFundClientPK,@CFundPK

                            While @@FETCH_STATUS = 0  
                            BEGIN	
	                            SET @RunningAmount = 0 
                                SET @RunningBalance = 0 
                                SET @RunningAvgPrice = 0;
	                             WITH q 
                                             AS (SELECT TOP 1000000000 * 
                                                 FROM   #AVGRecalculation where FundPK = @CFundPK and FundCLientPK = @CFundClientPK
                                                 ORDER  BY valuedate,trxtype ASC) 


                                        UPDATE q 
                                        SET    @RunningBalance = runningbalance = @RunningBalance + ( 
                                                                                  volume * CASE 
                                                                                             WHEN 
                                                                                       trxtype = 1 THEN 1 
                                                                                             ELSE -1 
                                                                                           END ), 
                                               @RunningAmount = runningamount = @RunningAmount + CASE WHEN 
                                                                                trxtype 
                                                                                = 
                                                                                1 
                                                                                THEN 
                                                                                amount ELSE -volume 
                                                                                * 
                                                                                @RunningAvgPrice 
                                                                                END, 
                                               @RunningAvgPrice = runningavgprice = 
                                                                  CASE 
                                                                    WHEN trxtype = 1 THEN 
                                                                      CASE 
                                                                        WHEN @RunningBalance 
                                                                             = 0 
                                                                      THEN 
                                                                        0 
                                                                        ELSE @RunningAmount 
                                                                             / 
                                                                             @RunningBalance 
                                                                      END 
                                                                    ELSE @RunningAvgPrice 
                                                                  END,
				                               @COUNTER = IdentInt = @Counter + 1 
	


	                            Fetch Next From A 
	                            into @CFundClientPK,@CFundPK
                            End	
                            Close A
                            Deallocate A



                            insert into #AVGResultList
                            Select max(ValueDate),FundClientPK,FundPK From #AVGRecalculation 
                            where valuedate <= @Date
                            group by FundClientPK,FundPK


                            DECLARE 
	                            @XDate date,
                                @XFundPK int, 
                                @XFundClientPK int
 
                            DECLARE A CURSOR FOR 
	                            SELECT * from #AVGResultList
 
                            OPEN A
 
                            FETCH NEXT FROM A INTO 
                                @XDate,@XFundClientPK,@XFundPK
 
                            WHILE @@FETCH_STATUS = 0
                                BEGIN
        
                                    insert into #AVGResult
		                            Select top 1 A.FundClientPK,A.FundPK,isnull(A.runningavgprice,0) From #AVGRecalculation A
		                            where A.FundClientPK = @XFundClientPK and A.FundPK = @XFundPK and A.valuedate = @XDate
		                            order by IdentInt desc
		
		
		                            FETCH NEXT FROM A INTO @XDate,@XFundClientPK,@XFundPK 
                                END
 
                            CLOSE A
 
                            DEALLOCATE A

                            --Select FundPk,Fundclientpk From #AVGResult 
                            --group by fundpk,fundclientpk
                            --having count(*) > 1
                            --order by FundClientPK,FundPK

                            --PROSES KE TABLE FUNDCLIENTPOSITION

                            Update A Set A.AvgNAV =  isnull(B.AVGNav,0)  From FundClientPosition A
                            left join #AVGResult B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK 
                            where A.Date = @Date 


                            select 'Generate Average Success' Result
                        ";
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return dr["Result"].ToString();
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


        public int Update_UnitFundClientPosition(FundClientPosition _fundClientPosition)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        Update FundClientPosition set UnitAmount = @UnitAmount
                        where FundClientPositionPK = @FundClientPositionPK";

                        cmd.Parameters.AddWithValue("@FundClientPositionPK", _fundClientPosition.FundClientPositionPK);
                        cmd.Parameters.AddWithValue("@UnitAmount", _fundClientPosition.UnitAmount);


                        cmd.ExecuteNonQuery();
                    }
                    return 0;


                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public string FundClientPosition_UpdateAvgAfterPosting(DateTime _paramDateFrom, DateTime _paramDateTo)
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
                            
--drop table #AVGRecalculation
--drop table #AVGResultList
--drop table #AVGResult

Declare @CFundClientPK int
DECLARE @CFundPK int
DECLARE @RunningAmount NUMERIC(32, 12) 
DECLARE @RunningBalance NUMERIC(32, 12) 
DECLARE @RunningAvgPrice NUMERIC(32, 12) 
DECLARE @COUNTER INT
DECLARE @PeriodPK INT 
DECLARE @BegPeriodPK INT 
DECLARE @Mode INT 
DECLARE @XDate date
DECLARE @XFundPK int
DECLARE @XFundClientPK int
declare @Datefrom date
declare @dateto date

Create table #AVGRecalculation 
    ( 
		IdentInt int null,
        [runningamount]   [NUMERIC](32, 12) NULL, 
        [runningbalance]  [NUMERIC](32, 12) NULL, 
        [runningavgprice] [NUMERIC](32, 12) NULL, 
		FundClientPK int,
		FundPK int,
        [valuedate]       [DATETIME] NULL, 
        [volume]          [NUMERIC](32, 12) NULL, 
        [amount]          [NUMERIC](32, 12) NULL, 
        [price]           [NUMERIC](32, 12) NULL, 
        [trxtype]         [INT] NULL
    ) 
CREATE CLUSTERED INDEX indx_AVGRecalculation ON #AVGRecalculation (FundPK,FundClientPK);

Create table #AVGResultList
(
	Date Datetime,
	FundClientPK int,
	FundPK int
)
CREATE CLUSTERED INDEX indx_AVGResultList ON #AVGResultList (FundPK,FundClientPK);

Create table #AVGResult
(
	FundClientPK int,
	FundPK int,
	AVGNav numeric(22,8)
)
CREATE CLUSTERED INDEX indx_AVGResult ON #AVGResult (FundPK,FundClientPK);

SET @Datefrom = dbo.FWorkingDay(@paramDateFrom,-1)
set @dateto = dbo.FWorkingDay(getdate(),1)

DECLARE @List TABLE
(
	FundClientPK int
)

INSERT INTO @List
SELECT DISTINCT FundClientPK FROM ClientSubscription A
Where A.status = 2 and Posted = 1 and ValueDate between @paramDateFrom and @paramDateto
AND (( isnull(A.NAV,0) > 0 and A.Type not in (3,6) ) or A.Type in (3,6)) and A.TotalUnitAmount > 0
UNION ALL
SELECT DISTINCT FundClientPK FROM dbo.ClientSwitching A
Where A.status = 2 and Posted = 1 and ValueDate between @paramDateFrom and @paramDateto
AND A.FeeType = 'OUT' AND isnull(A.NAVFundFrom,0) > 0 and A.UnitAmount > 0 and A.TotalUnitAmountFundTo > 0
 


WHILE (@Datefrom <= @dateto)
BEGIN
	delete #AVGRecalculation
	delete #AVGResultList
	delete #AVGResult

	SELECT @BegPeriodPK = periodpk 
    FROM   period 
    WHERE  Dateadd(yy, Datediff(yy, 0, Dateadd(year, -1, @datefrom)) + 1, -1) 
            BETWEEN datefrom 
                AND dateto 
            AND status = 2 

    SELECT @PeriodPK = periodpk 
    FROM   period 
    WHERE  @datefrom BETWEEN datefrom AND dateto 
            AND status = 2 

                                 
    SET @Mode = 1 -- 1 = priority Buy, 2 = priority Sell         
    set @COUNTER = 1

    

	    INSERT INTO #AVGRecalculation(runningamount,runningbalance,runningavgprice,FundClientPK,FundPK,valuedate,volume,amount,price,trxtype) 
            SELECT Cast(0 AS NUMERIC(32, 12)) RunningAmount, 
                    Cast(0 AS NUMERIC(32, 12)) RunningBalance, 
                    Cast(0 AS NUMERIC(32, 12)) RunningAvgPrice, 
                    * 
            FROM   (SELECT FundClientPK,FundPK,'12/31/2000'        ValueDate, 
                            unitamount, 
                            unitamount * avgnav Amount, 
                            avgnav              Price, 
                            1                   TrxType 
                    FROM   fundclientpositionendyear 
                    WHERE   periodpk = @PeriodPK  and status = 2
					AND FundClientPK IN(SELECT FundClientPK FROM @List)
                    UNION ALL 
                    SELECT FundClientPK,FundPK,valuedate, 
                            totalunitamount, 
                            totalcashamount, 
                            nav, 
                            1 
                    FROM   clientsubscription 
                    WHERE   posted = 1 
                            AND revised = 0 
                            AND status = 2 
                            AND Year(valuedate) = Year(@datefrom) 
					 			        and isnull(NOTES,'') <> 'Cutoff'
										AND FundClientPK IN(SELECT FundClientPK FROM @List)
                    UNION ALL 
                    SELECT FundClientPK,FundPK,valuedate, 
                            unitamount, 
                            cashamount, 
                            nav, 
                            2 
                    FROM   clientredemption 
                    WHERE   posted = 1 
                            AND revised = 0 
                            AND status = 2 
                            AND Year(valuedate) = Year(@datefrom) 
							AND FundClientPK IN(SELECT FundClientPK FROM @List)
                    UNION ALL 
                    SELECT FundClientPK,FundPKTo,valuedate, 
                            totalunitamountfundto, 
                            totalcashamountfundto, 
                            navfundto NAV, 
                            1 
                    FROM   clientswitching 
                    WHERE   posted = 1 
                            AND revised = 0 
                            AND status = 2 
                            AND Year(valuedate) = Year(@datefrom) 
							AND FundClientPK IN(SELECT FundClientPK FROM @List)
                    UNION ALL 
                    SELECT FundClientPK,FundPKFrom,valuedate, 
                            unitamount, 
                            unitamount * navfundfrom, 
                            navfundfrom NAV, 
                            2 
                    FROM   clientswitching 
                    WHERE   posted = 1 
                            AND revised = 0 
                            AND status = 2 
                            AND Year(valuedate) = Year(@datefrom)
							AND FundClientPK IN(SELECT FundClientPK FROM @List)
							) dt 
					 

    Declare A Cursor For
	    Select Distinct  FundClientPK,FundPK from FundClientPosition where Date = @datefrom AND FundClientPK IN(SELECT FundClientPK FROM @List)
    Open A
    Fetch Next From A
    Into @CFundClientPK,@CFundPK

    While @@FETCH_STATUS = 0  
    BEGIN	
	    SET @RunningAmount = 0 
        SET @RunningBalance = 0 
        SET @RunningAvgPrice = 0;
	        WITH q 
                        AS (SELECT TOP 1000000000 * 
                            FROM   #AVGRecalculation where FundPK = @CFundPK and FundCLientPK = @CFundClientPK
                            ORDER  BY valuedate,trxtype ASC) 


                UPDATE q 
                SET    @RunningBalance = runningbalance = @RunningBalance + ( 
                                                            volume * CASE 
                                                                        WHEN 
                                                                trxtype = 1 THEN 1 
                                                                        ELSE -1 
                                                                    END ), 
                        @RunningAmount = runningamount = @RunningAmount + CASE WHEN 
                                                        trxtype 
                                                        = 
                                                        1 
                                                        THEN 
                                                        amount ELSE -volume 
                                                        * 
                                                        @RunningAvgPrice 
                                                        END, 
                        @RunningAvgPrice = runningavgprice = 
                                            CASE 
                                            WHEN trxtype = 1 THEN 
                                                CASE 
                                                WHEN @RunningBalance 
                                                        = 0 
                                                THEN 
                                                0 
                                                ELSE @RunningAmount 
                                                        / 
                                                        @RunningBalance 
                                                END 
                                            ELSE @RunningAvgPrice 
                                            END,
				        @COUNTER = IdentInt = @Counter + 1 
	


	    Fetch Next From A 
	    into @CFundClientPK,@CFundPK
    End	
    Close A
    Deallocate A



    insert into #AVGResultList
    Select max(ValueDate),FundClientPK,FundPK From #AVGRecalculation 
    where valuedate <= @datefrom
    group by FundClientPK,FundPK


                           
    DECLARE A CURSOR FOR 
	    SELECT * from #AVGResultList
 
    OPEN A
 
    FETCH NEXT FROM A INTO 
        @XDate,@XFundClientPK,@XFundPK
 
    WHILE @@FETCH_STATUS = 0
        BEGIN
        
            insert into #AVGResult
		    Select top 1 A.FundClientPK,A.FundPK,isnull(A.runningavgprice,0) From #AVGRecalculation A
		    where A.FundClientPK = @XFundClientPK and A.FundPK = @XFundPK and A.valuedate = @XDate
		    order by IdentInt desc
		
		
		    FETCH NEXT FROM A INTO @XDate,@XFundClientPK,@XFundPK 
        END
 
    CLOSE A
 
    DEALLOCATE A

    --PROSES KE TABLE FUNDCLIENTPOSITION

    Update A Set A.AvgNAV =  isnull(B.AVGNav,0)  From FundClientPosition A
    inner join #AVGResult B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK 
    where A.Date = @datefrom 


	SET @Datefrom = dbo.FWorkingDay(@Datefrom,1)
END


                            select 'Generate Average Success' Result
                        ";
                        cmd.Parameters.AddWithValue("@paramDateFrom", _paramDateFrom);
                        cmd.Parameters.AddWithValue("@paramDateTo", _paramDateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return dr["Result"].ToString();
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

        public string SchedulerCheckingFCP(string _userID)
        {

            #region Scheduler Checking EMAIL

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        //string _paramFundFrom = "";

                        //if (!_host.findString(_CustodianRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_CustodianRpt.FundFrom))
                        //{
                        //    _paramFundFrom = "And A.FundPK  in ( " + _CustodianRpt.FundFrom + " ) ";
                        //}
                        //else
                        //{
                        //    _paramFundFrom = "";
                        //}



                        cmd.CommandText = @"
declare @Valuedate date

set @Valuedate = getdate()
--set @Valuedate = '2020-07-03'


--drop table #tempCheckFCP

declare @DateT1 date
declare @UnitT0 numeric(32,4)
declare @UnitT1 numeric(32,4)
declare @Yesterday date
Declare @unitBefore numeric(32,4)
Declare @Subs numeric(32,4)
Declare @Red numeric(32,4)
Declare @SwitchIn numeric(32,4)
Declare @SwitchOut numeric(32,4)
declare @UnitAmount numeric(32,4)
declare @BodyMsg nvarchar(max)

set @DateT1 = dbo.FWorkingDay(@Valuedate,1)
set @Yesterday = dbo.FWorkingDay(@Valuedate,-1)

create table #tempCheckFCP
(
	date date,
	FundPK int,
	FundName nvarchar(100),
	FundClientPK int,
	FundClientName nvarchar(100),
	Reason nvarchar(200),
	DataProblem int,
	DataProblemDesc nvarchar(100)
)

Declare @tableBegBalance table
(
	FundClientPK int,
	FundPK int,
	UnitAmount numeric(22,4)
)

Declare @NetTrx table
(
	FundClientPK int,
	FundPK int,
	UnitAmount numeric(22,4)
)

declare @TableCloseNAV table (
	FundPK int
)

declare @TableFCP table (
	FundPK int,
	FundClientPK int
)

declare @TableFCP2 table (
	FundPK int,
	FundClientPK int
)

declare @totalDataMasalah table
(
	DataProblem nvarchar(200),
	JumlahData int
)

--1. cek duplicate fundclient di t0 dan t1
if exists (
	SELECT COUNT(FundClientPositionPK) FundClientPositionPK,FundPK,fundclientPK,Date 
	FROM FundClientPosition where date = @Valuedate
	GROUP BY FundPK,fundclientPK,Date
	HAVING COUNT(FundClientPositionPK) > 1
)

insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
SELECT Date,A.FundPK,B.Name,A.fundclientPK,C.Name,'(1)Fund Client Position Duplicate' Reason,1,'(1)Duplicate Fund Client Position'
FROM FundClientPosition A
left join Fund B on A.FundPK = B.FundPK and B.Status = 2
left join FundClient C on A.FundClientPK = C.FundClientPK and C.Status = 2
where date = @Valuedate
GROUP BY A.FundPK,B.Name,A.fundclientPK,C.Name,A.Date
HAVING COUNT(FundClientPositionPK) > 1
Order by Date asc

--t+1
if exists (
	SELECT COUNT(FundClientPositionPK) FundClientPositionPK,FundPK,fundclientPK,Date 
	FROM FundClientPosition where date = @DateT1
	GROUP BY FundPK,fundclientPK,Date
	HAVING COUNT(FundClientPositionPK) > 1
)

insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
SELECT Date,A.FundPK,B.Name,A.fundclientPK,C.Name,'(1)Fund Client Position Duplicate' Reason,1,'(1)Duplicate Fund Client Position'
FROM FundClientPosition A
left join Fund B on A.FundPK = B.FundPK and B.Status = 2
left join FundClient C on A.FundClientPK = C.FundClientPK and C.Status = 2
where date = @DateT1
GROUP BY A.FundPK,B.Name,A.fundclientPK,C.Name,A.Date
HAVING COUNT(FundClientPositionPK) > 1
Order by Date asc

--2. cek apakah sum fcp t0 dan t+1 sama
select @UnitT0 = sum(unitamount) From fundclientposition where date = @Valuedate
select @UnitT1 = sum(unitamount) From fundclientposition where date = @DateT1

if @UnitT1 - @UnitT0 <> 0
begin
	insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
	select @Valuedate,0,'',0,'','Unit T0 : ' + cast(format(@UnitT0, '###,###.####') as nvarchar) + ', Unit T1 : ' + cast(format(@UnitT1, '###,###.####') as nvarchar) + ', Different Unit : ' +  cast(format(abs(@UnitT1 - @UnitT0), '###,###.####') as nvarchar),2,
	'(2)Total Fund Client Position T0 and T1 different'
end

--3. cek different detail fcp kemarin + transaksi hari ini bedanya > 1 atau < -1 gak
delete @tableBegBalance
delete @NetTrx

Insert into @tableBegBalance
Select FundclientPK,FundPK,UnitAmount From FundClientPosition 
where Date = @Yesterday

insert into @NetTrx

Select FundClientPK,FundPK,Sum(isnull(Trx,0)) FROM
(
	Select FundClientPK,FundPK,sum(isnull(TotalUnitAmount,0)) Trx From ClientSubscription where  ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 
	GROUP BY FundClientPK,FundPK
	UNION ALL
	Select FundClientPK,FundPK,sum(isnull(UnitAmount,0)) * -1 Trx From ClientRedemption where  ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 
	GROUP BY FundClientPK,FundPK
	UNION ALL
	Select FundClientPK,FundPKFrom FundPK,sum(isnull(UnitAmount,0)) * -1 Trx From ClientSwitching where  ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 
	GROUP BY FundClientPK,FundPKFrom
	UNION ALL
	Select FundClientPK,FundPKTo FundPK,sum(isnull(TotalUnitAmountFundTo,0))  Trx From ClientSwitching where  ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 
	GROUP BY FundClientPK,FundPKTo
)A
group By A.FundClientPK,A.FundPK

insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
Select @Valuedate,A.FundPK,D.Name,A.FundClientPK,E.Name,'Unit : ' + cast(format(abs(isnull(C.UnitAmount,0) - (isnull(A.UnitAmount,0) + isnull(B.UnitAmount,0))), '###,###.####') as nvarchar),3,
'(3)Different Unit T-1 + Transaction T0 and Unit T0'
From @tableBegBalance A
left join @NetTrx B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK
left join FundClientPosition C on A.FundClientPK = C.FundClientPK and A.FundPK = C.FundPK and C.Date = @ValueDate
left join Fund D on A.FundPK = D.FundPK and D.Status = 2
left join FundClient E on A.FundClientPK = E.FundClientPK and E.Status = 2
where isnull(C.UnitAmount,0) - (isnull(A.UnitAmount,0) + isnull(B.UnitAmount,0)) > 1 or isnull(C.UnitAmount,0) - (isnull(A.UnitAmount,0) + isnull(B.UnitAmount,0)) < -1


--4. cek total semua nasabah sama kek point 3
select @unitBefore = sum(unitamount)  From FundClientPosition where date = @Yesterday

select @Subs = sum(totalunitamount)  From ClientSubscription where ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 
select @Red = sum(UnitAmount)  From ClientRedemption where ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 

select @SwitchIn = sum(TotalUnitAmountFundTo)  From ClientSwitching where ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 
select @SwitchOut = sum(unitAmount)  From ClientSwitching where ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 

select @UnitAmount = sum(unitamount) From FundClientPosition where date = @ValueDate 

if (sum(@UnitAmount) - (isnull(@unitBefore,0) + isnull(@Subs,0) - isnull(@Red,0) + isnull(@SwitchIn,0) - isnull(@SwitchOut,0)) > 0 )
begin
	insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
	select @Valuedate,0,'',0,'','Unit : ' +  cast(format(abs(sum(@UnitAmount) - (isnull(@unitBefore,0) + isnull(@Subs,0) - isnull(@Red,0) + isnull(@SwitchIn,0) - isnull(@SwitchOut,0))), '###,###.####') as nvarchar),4,
	'(4)Different Total Unit T-1 + Transaction T0 and Unit T0'
end

--5. cek transaksi UR yg blm posting di t0
insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
select @Valuedate,0,'',0,'',case when Type = 1 then 'Sub SysNo : ' when Type = 2 then 'Red SysNo : ' else 'Swi SysNo : ' end + cast(SysNo as nvarchar),5,
'(5)Transaction Not Posted' from (
	select ClientSubscriptionPK SysNo,'1' Type from ClientSubscription where status = 2 and posted = 0 and ValueDate = @Valuedate
	union all
	select ClientRedemptionPK SysNo,'2' Type from ClientRedemption where status = 2 and posted = 0 and ValueDate = @Valuedate
	union all
	select ClientSwitchingPK SysNo, '3' from ClientSwitching where status = 2 and posted = 0 and ValueDate = @Valuedate
) A

--6. cek nav yg blm ada di t0 yg fund blm mature
insert into @TableCloseNAV
select FundPK from CloseNAV where status = 2 and date = @valuedate

insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
select @ValueDate,FundPK,Name,0,'','Nav not exist',6,'(6)Nav not exist' from Fund where status = 2 and MaturityDate > @Valuedate and fundpk not in (
select FundPK from @TableCloseNAV )

--7. cek avg FCP t1 = 0 yg unitamount > 0,avg t0 <> 0
insert into @TableFCP
select FundPK,FundClientPK from FundClientPosition where date = @DateT1 and AvgNAV = 0 and UnitAmount > 0

insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
select @Valuedate,A.FundPK,C.Name,A.FundClientPK,D.Name,'Avg T1 not yet calculated',7,'(7)Avg T1 not yet calculated' from FundClientPosition A
inner join @TableFCP B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK
left join Fund C on A.FundPK = C.FundPK and C.Status = 2
left join FundClient D on A.FundClientPK = D.FundClientPK and D.Status = 2
where date = @Valuedate and AvgNAV <> 0

--8. cek avg FCP t1 & t0 = 0 tapi ada transaksi sub & switch in (fund to)
insert into @TableFCP2
select A.FundPK,A.FundClientPK from FundClientPosition A
inner join @TableFCP B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK
where date = @Valuedate and AvgNAV = 0

insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
select @Valuedate,A.FundPK,B.Name,A.FundClientPK,C.Name, case when A.Type = 1 then 'Sub SysNo : ' when A.Type = 2 then 'Red SysNo : ' else 'Swi SysNo : ' end + cast(SysNo as nvarchar),8,
'(8)Avg not calculated but there is transaction' from (
select ClientSubscriptionPK SysNo, '1' Type,A.FundPK,B.FundClientPK from ClientSubscription A
inner join @TableFCP2 B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK
where ValueDate = @Valuedate and status = 2 and Posted = 1
union all
select ClientSwitchingPK SysNo, '3' Type,A.FundPKTo,B.FundClientPK from ClientSwitching A
inner join @TableFCP2 B on A.FundPKTo = B.FundPK and A.FundClientPK = B.FundClientPK
where ValueDate = @Valuedate and status = 2 and Posted = 1
) A
left join Fund B on A.FundPK = B.FundPK and B.Status = 2
left join FundClient C on A.FundClientPK = C.FundClientPK and C.Status = 2

select date,DataProblemDesc,FundPK,FundName,FundClientPK,FundClientName,Reason from #tempCheckFCP
order by DataProblemDesc

--set @BodyMsg = 'Scheduler checking from Radsoft'

--select @BodyMsg = @BodyMsg + '</br></br>' + DataProblemDesc + '</br>Total Data : ' + cast(count(*) as nvarchar) from #tempCheckFCP
--group by DataProblemDesc

--set @BodyMsg = @BodyMsg + '</br></br>Please refer to attachment to see detail data'

--select @BodyMsg BodyMsg



";
                        cmd.CommandTimeout = 0;


                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return "";
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "SchedulerChecking" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "SchedulerChecking" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "Scheduler Checking";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Scheduler Checking");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<SchedulerChecking> rList = new List<SchedulerChecking>();
                                    while (dr0.Read())
                                    {

                                        SchedulerChecking rSingle = new SchedulerChecking();
                                        rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                        rSingle.DataProblemDesc = Convert.ToString(dr0["DataProblemDesc"]);
                                        rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                        rSingle.FundClientName = Convert.ToString(dr0["FundClientName"]);
                                        rSingle.Reason = Convert.ToString(dr0["Reason"]);
                                        rSingle.FundPK = Convert.ToInt32(dr0["FundPK"]);
                                        rSingle.FundClientPK = Convert.ToInt32(dr0["FundClientPK"]);
                                        rList.Add(rSingle);

                                    }



                                    var GroupByReference =
                                            from r in rList
                                                //orderby r ascending
                                            group r by new { } into rGroup
                                            select rGroup;

                                    int incRowExcel = 0;

                                    incRowExcel++;

                                    int RowB = incRowExcel;
                                    int RowG = incRowExcel + 1;

                                    worksheet.Cells[incRowExcel, 1].Value = "Date";
                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                    worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 2].Value = "Reason";
                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                    worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                    worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 3].Value = "SysNo";
                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                    worksheet.Cells["C" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                    worksheet.Cells["C" + incRowExcel + ":C" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["C" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 4].Value = "Fund";
                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                    worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                    worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 5].Value = "SysNo";
                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                    worksheet.Cells["E" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                    worksheet.Cells["E" + incRowExcel + ":E" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["E" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 6].Value = "Client";
                                    worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                    worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                    worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 7].Value = "Description";
                                    worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                    worksheet.Cells["G" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                    worksheet.Cells["G" + incRowExcel + ":G" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    worksheet.Cells["G" + incRowExcel + ":G" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                    foreach (var rsHeader in GroupByReference)
                                    {

                                        incRowExcel++;

                                        int first = incRowExcel;

                                        int _no = 1;
                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;

                                        foreach (var rsDetail in rsHeader)
                                        {
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = rsDetail.Date;
                                            worksheet.Cells[incRowExcel, 1].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.DataProblemDesc;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundPK;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.FundName;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.FundClientPK;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.FundClientName;
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.Reason;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                            _no++;
                                            _endRowDetail = incRowExcel;
                                            incRowExcel++;
                                        }



                                    }



                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 7];
                                    worksheet.Column(1).AutoFit();
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(3).AutoFit();
                                    worksheet.Column(4).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();


                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Email";


                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                    worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                    worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                    worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                    //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();
                                    //if (_EmailRpt.DownloadMode == "PDF")
                                    //{
                                    //    Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                    //}


                                    return filePath;
                                }

                            }
                        }
                    }
                }
            }

            catch (Exception err)
            {
                return "";
                throw err;
            }


            #endregion

        }

        public string SchedulerEmail()
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSDepositoInstrumentDetailPerFundPerBank> L_model = new List<OMSDepositoInstrumentDetailPerFundPerBank>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        declare @Valuedate date

set @Valuedate = getdate()
--set @Valuedate = '2020-07-03'


--drop table #tempCheckFCP

declare @DateT1 date
declare @UnitT0 numeric(32,4)
declare @UnitT1 numeric(32,4)
declare @Yesterday date
Declare @unitBefore numeric(32,4)
Declare @Subs numeric(32,4)
Declare @Red numeric(32,4)
Declare @SwitchIn numeric(32,4)
Declare @SwitchOut numeric(32,4)
declare @UnitAmount numeric(32,4)
declare @BodyMsg nvarchar(max)

set @DateT1 = dbo.FWorkingDay(@Valuedate,1)
set @Yesterday = dbo.FWorkingDay(@Valuedate,-1)

create table #tempCheckFCP
(
	date date,
	FundPK int,
	FundName nvarchar(100),
	FundClientPK int,
	FundClientName nvarchar(100),
	Reason nvarchar(200),
	DataProblem int,
	DataProblemDesc nvarchar(100)
)

Declare @tableBegBalance table
(
	FundClientPK int,
	FundPK int,
	UnitAmount numeric(22,4)
)

Declare @NetTrx table
(
	FundClientPK int,
	FundPK int,
	UnitAmount numeric(22,4)
)

declare @TableCloseNAV table (
	FundPK int
)

declare @TableFCP table (
	FundPK int,
	FundClientPK int
)

declare @TableFCP2 table (
	FundPK int,
	FundClientPK int
)

declare @totalDataMasalah table
(
	DataProblem nvarchar(200),
	JumlahData int
)

--1. cek duplicate fundclient di t0 dan t1
if exists (
	SELECT COUNT(FundClientPositionPK) FundClientPositionPK,FundPK,fundclientPK,Date 
	FROM FundClientPosition where date = @Valuedate
	GROUP BY FundPK,fundclientPK,Date
	HAVING COUNT(FundClientPositionPK) > 1
)

insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
SELECT Date,A.FundPK,B.Name,A.fundclientPK,C.Name,'(1)Fund Client Position Duplicate' Reason,1,'(1)Duplicate Fund Client Position'
FROM FundClientPosition A
left join Fund B on A.FundPK = B.FundPK and B.Status = 2
left join FundClient C on A.FundClientPK = C.FundClientPK and C.Status = 2
where date = @Valuedate
GROUP BY A.FundPK,B.Name,A.fundclientPK,C.Name,A.Date
HAVING COUNT(FundClientPositionPK) > 1
Order by Date asc

--t+1
if exists (
	SELECT COUNT(FundClientPositionPK) FundClientPositionPK,FundPK,fundclientPK,Date 
	FROM FundClientPosition where date = @DateT1
	GROUP BY FundPK,fundclientPK,Date
	HAVING COUNT(FundClientPositionPK) > 1
)

insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
SELECT Date,A.FundPK,B.Name,A.fundclientPK,C.Name,'(1)Fund Client Position Duplicate' Reason,1,'(1)Duplicate Fund Client Position'
FROM FundClientPosition A
left join Fund B on A.FundPK = B.FundPK and B.Status = 2
left join FundClient C on A.FundClientPK = C.FundClientPK and C.Status = 2
where date = @DateT1
GROUP BY A.FundPK,B.Name,A.fundclientPK,C.Name,A.Date
HAVING COUNT(FundClientPositionPK) > 1
Order by Date asc

--2. cek apakah sum fcp t0 dan t+1 sama
select @UnitT0 = sum(unitamount) From fundclientposition where date = @Valuedate
select @UnitT1 = sum(unitamount) From fundclientposition where date = @DateT1

if @UnitT1 - @UnitT0 <> 0
begin
	insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
	select @Valuedate,0,'',0,'','Unit T0 : ' + cast(format(@UnitT0, '###,###.####') as nvarchar) + ', Unit T1 : ' + cast(format(@UnitT1, '###,###.####') as nvarchar) + ', Different Unit : ' +  cast(format(abs(@UnitT1 - @UnitT0), '###,###.####') as nvarchar),2,
	'(2)Total Fund Client Position T0 and T1 different'
end

--3. cek different detail fcp kemarin + transaksi hari ini bedanya > 1 atau < -1 gak
delete @tableBegBalance
delete @NetTrx

Insert into @tableBegBalance
Select FundclientPK,FundPK,UnitAmount From FundClientPosition 
where Date = @Yesterday

insert into @NetTrx

Select FundClientPK,FundPK,Sum(isnull(Trx,0)) FROM
(
	Select FundClientPK,FundPK,sum(isnull(TotalUnitAmount,0)) Trx From ClientSubscription where  ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 
	GROUP BY FundClientPK,FundPK
	UNION ALL
	Select FundClientPK,FundPK,sum(isnull(UnitAmount,0)) * -1 Trx From ClientRedemption where  ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 
	GROUP BY FundClientPK,FundPK
	UNION ALL
	Select FundClientPK,FundPKFrom FundPK,sum(isnull(UnitAmount,0)) * -1 Trx From ClientSwitching where  ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 
	GROUP BY FundClientPK,FundPKFrom
	UNION ALL
	Select FundClientPK,FundPKTo FundPK,sum(isnull(TotalUnitAmountFundTo,0))  Trx From ClientSwitching where  ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 
	GROUP BY FundClientPK,FundPKTo
)A
group By A.FundClientPK,A.FundPK

insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
Select @Valuedate,A.FundPK,D.Name,A.FundClientPK,E.Name,'Unit : ' + cast(format(abs(isnull(C.UnitAmount,0) - (isnull(A.UnitAmount,0) + isnull(B.UnitAmount,0))), '###,###.####') as nvarchar),3,
'(3)Different Unit T-1 + Transaction T0 and Unit T0'
From @tableBegBalance A
left join @NetTrx B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK
left join FundClientPosition C on A.FundClientPK = C.FundClientPK and A.FundPK = C.FundPK and C.Date = @ValueDate
left join Fund D on A.FundPK = D.FundPK and D.Status = 2
left join FundClient E on A.FundClientPK = E.FundClientPK and E.Status = 2
where isnull(C.UnitAmount,0) - (isnull(A.UnitAmount,0) + isnull(B.UnitAmount,0)) > 1 or isnull(C.UnitAmount,0) - (isnull(A.UnitAmount,0) + isnull(B.UnitAmount,0)) < -1


--4. cek total semua nasabah sama kek point 3
select @unitBefore = sum(unitamount)  From FundClientPosition where date = @Yesterday

select @Subs = sum(totalunitamount)  From ClientSubscription where ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 
select @Red = sum(UnitAmount)  From ClientRedemption where ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 

select @SwitchIn = sum(TotalUnitAmountFundTo)  From ClientSwitching where ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 
select @SwitchOut = sum(unitAmount)  From ClientSwitching where ValueDate = @ValueDate and status = 2 and Posted = 1 and Revised = 0 

select @UnitAmount = sum(unitamount) From FundClientPosition where date = @ValueDate 

if (sum(@UnitAmount) - (isnull(@unitBefore,0) + isnull(@Subs,0) - isnull(@Red,0) + isnull(@SwitchIn,0) - isnull(@SwitchOut,0)) > 0 )
begin
	insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
	select @Valuedate,0,'',0,'','Unit : ' +  cast(format(abs(sum(@UnitAmount) - (isnull(@unitBefore,0) + isnull(@Subs,0) - isnull(@Red,0) + isnull(@SwitchIn,0) - isnull(@SwitchOut,0))), '###,###.####') as nvarchar),4,
	'(4)Different Total Unit T-1 + Transaction T0 and Unit T0'
end

--5. cek transaksi UR yg blm posting di t0
insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
select @Valuedate,0,'',0,'',case when Type = 1 then 'Sub SysNo : ' when Type = 2 then 'Red SysNo : ' else 'Swi SysNo : ' end + cast(SysNo as nvarchar),5,
'(5)Transaction Not Posted' from (
	select ClientSubscriptionPK SysNo,'1' Type from ClientSubscription where status = 2 and posted = 0 and ValueDate = @Valuedate
	union all
	select ClientRedemptionPK SysNo,'2' Type from ClientRedemption where status = 2 and posted = 0 and ValueDate = @Valuedate
	union all
	select ClientSwitchingPK SysNo, '3' from ClientSwitching where status = 2 and posted = 0 and ValueDate = @Valuedate
) A

--6. cek nav yg blm ada di t0 yg fund blm mature
insert into @TableCloseNAV
select FundPK from CloseNAV where status = 2 and date = @valuedate

insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
select @ValueDate,FundPK,Name,0,'','Nav not exist',6,'(6)Nav not exist' from Fund where status = 2 and MaturityDate > @Valuedate and fundpk not in (
select FundPK from @TableCloseNAV )

--7. cek avg FCP t1 = 0 yg unitamount > 0,avg t0 <> 0
insert into @TableFCP
select FundPK,FundClientPK from FundClientPosition where date = @DateT1 and AvgNAV = 0 and UnitAmount > 0

insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
select @Valuedate,A.FundPK,C.Name,A.FundClientPK,D.Name,'Avg T1 not yet calculated',7,'(7)Avg T1 not yet calculated' from FundClientPosition A
inner join @TableFCP B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK
left join Fund C on A.FundPK = C.FundPK and C.Status = 2
left join FundClient D on A.FundClientPK = D.FundClientPK and D.Status = 2
where date = @Valuedate and AvgNAV <> 0

--8. cek avg FCP t1 & t0 = 0 tapi ada transaksi sub & switch in (fund to)
insert into @TableFCP2
select A.FundPK,A.FundClientPK from FundClientPosition A
inner join @TableFCP B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK
where date = @Valuedate and AvgNAV = 0

insert into #tempCheckFCP(date,FundPK,FundName,FundClientPK,FundClientName,Reason,DataProblem,DataProblemDesc)
select @Valuedate,A.FundPK,B.Name,A.FundClientPK,C.Name, case when A.Type = 1 then 'Sub SysNo : ' when A.Type = 2 then 'Red SysNo : ' else 'Swi SysNo : ' end + cast(SysNo as nvarchar),8,
'(8)Avg not calculated but there is transaction' from (
select ClientSubscriptionPK SysNo, '1' Type,A.FundPK,B.FundClientPK from ClientSubscription A
inner join @TableFCP2 B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK
where ValueDate = @Valuedate and status = 2 and Posted = 1
union all
select ClientSwitchingPK SysNo, '3' Type,A.FundPKTo,B.FundClientPK from ClientSwitching A
inner join @TableFCP2 B on A.FundPKTo = B.FundPK and A.FundClientPK = B.FundClientPK
where ValueDate = @Valuedate and status = 2 and Posted = 1
) A
left join Fund B on A.FundPK = B.FundPK and B.Status = 2
left join FundClient C on A.FundClientPK = C.FundClientPK and C.Status = 2

--select date,DataProblemDesc,FundPK,FundName,FundClientPK,FundClientName,Reason from #tempCheckFCP
--order by DataProblemDesc

if exists (
	select * from #tempCheckFCP
)
begin
set @BodyMsg = 'Scheduler checking from Radsoft'

select @BodyMsg = @BodyMsg + '<br /><br />' + DataProblemDesc + '<br />Total Data : ' + cast(count(*) as nvarchar) from #tempCheckFCP
group by DataProblemDesc

set @BodyMsg = @BodyMsg + '<br /><br />Please refer to attachment to see detail data'

end
else
	set @BodyMsg = ''

select @BodyMsg BodyMsg



                        ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["BodyMsg"].ToString();
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

        public string FundClientPosition_UpdateAvgAfterRevise(DateTime _paramDateFrom, DateTime _paramDateTo, int _Type, int _PK, int _HistoryPK)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                            

--drop table #AVGRecalculation
--drop table #AVGResultList
--drop table #AVGResult

Declare @CFundClientPK int
DECLARE @CFundPK int
DECLARE @RunningAmount NUMERIC(22, 8) 
DECLARE @RunningBalance NUMERIC(22, 8) 
DECLARE @RunningAvgPrice NUMERIC(22, 8) 
DECLARE @COUNTER INT
DECLARE @PeriodPK INT 
DECLARE @BegPeriodPK INT 
DECLARE @Mode INT 
DECLARE @XDate date
DECLARE @XFundPK int
DECLARE @XFundClientPK int
declare @Datefrom date
declare @dateto date

Create table #AVGRecalculation 
    ( 
		IdentInt int null,
        [runningamount]   [NUMERIC](22, 8) NULL, 
        [runningbalance]  [NUMERIC](22, 8) NULL, 
        [runningavgprice] [NUMERIC](22, 8) NULL, 
		FundClientPK int,
		FundPK int,
        [valuedate]       [DATETIME] NULL, 
        [volume]          [NUMERIC](22, 8) NULL, 
        [amount]          [NUMERIC](22, 8) NULL, 
        [price]           [NUMERIC](22, 8) NULL, 
        [trxtype]         [INT] NULL
    ) 
CREATE CLUSTERED INDEX indx_AVGRecalculation ON #AVGRecalculation (FundPK,FundClientPK);

Create table #AVGResultList
(
	Date Datetime,
	FundClientPK int,
	FundPK int
)
CREATE CLUSTERED INDEX indx_AVGResultList ON #AVGResultList (FundPK,FundClientPK);

Create table #AVGResult
(
	FundClientPK int,
	FundPK int,
	AVGNav numeric(22,8)
)
CREATE CLUSTERED INDEX indx_AVGResult ON #AVGResult (FundPK,FundClientPK);

SET @Datefrom = dbo.FWorkingDay(@paramDateFrom,-1)
set @dateto = dbo.FWorkingDay(getdate(),1)

DECLARE @List TABLE
(
	FundClientPK int
)

if @Type = 1
begin
    INSERT INTO @List
    SELECT DISTINCT FundClientPK FROM ClientSubscription A
    Where A.status = 3 and Posted = 1 and Revised = 1 and ValueDate between @paramDateFrom and @paramDateto
    AND (( isnull(A.NAV,0) > 0 and A.Type not in (3,6) ) or A.Type in (3,6)) and A.TotalUnitAmount > 0 and ClientSubscriptionPK = @PK and historypk = @historyPK
end
else if @Type = 3
begin
    INSERT INTO @List
    SELECT DISTINCT FundClientPK FROM dbo.ClientSwitching A
    Where A.status = 3 and Posted = 1 and Revised = 1 and ValueDate between @paramDateFrom and @paramDateto
    AND A.FeeType = 'OUT' AND isnull(A.NAVFundFrom,0) > 0 and A.UnitAmount > 0 and A.TotalUnitAmountFundTo > 0 and ClientSwitchingPK = @PK and historypk = @historyPK
end
 


WHILE (@Datefrom <= @dateto)
BEGIN
	delete #AVGRecalculation
	delete #AVGResultList
	delete #AVGResult

	SELECT @BegPeriodPK = periodpk 
    FROM   period 
    WHERE  Dateadd(yy, Datediff(yy, 0, Dateadd(year, -1, @datefrom)) + 1, -1) 
            BETWEEN datefrom 
                AND dateto 
            AND status = 2 

    SELECT @PeriodPK = periodpk 
    FROM   period 
    WHERE  @datefrom BETWEEN datefrom AND dateto 
            AND status = 2 

                                 
    SET @Mode = 1 -- 1 = priority Buy, 2 = priority Sell         
    set @COUNTER = 1

    

	    INSERT INTO #AVGRecalculation(runningamount,runningbalance,runningavgprice,FundClientPK,FundPK,valuedate,volume,amount,price,trxtype) 
            SELECT Cast(0 AS NUMERIC(22, 8)) RunningAmount, 
                    Cast(0 AS NUMERIC(22, 8)) RunningBalance, 
                    Cast(0 AS NUMERIC(22, 8)) RunningAvgPrice, 
                    * 
            FROM   (SELECT FundClientPK,FundPK,'12/31/2000'        ValueDate, 
                            unitamount, 
                            unitamount * avgnav Amount, 
                            avgnav              Price, 
                            1                   TrxType 
                    FROM   fundclientpositionendyear 
                    WHERE   periodpk = @PeriodPK  and status = 2
					AND FundClientPK IN(SELECT FundClientPK FROM @List)
                    UNION ALL 
                    SELECT FundClientPK,FundPK,valuedate, 
                            totalunitamount, 
                            totalcashamount, 
                            nav, 
                            1 
                    FROM   clientsubscription 
                    WHERE   posted = 1 
                            AND revised = 0 
                            AND status = 2 
                            AND Year(valuedate) = Year(@datefrom) 
					 			        and isnull(NOTES,'') <> 'Cutoff'
										AND FundClientPK IN(SELECT FundClientPK FROM @List)
                    UNION ALL 
                    SELECT FundClientPK,FundPK,valuedate, 
                            unitamount, 
                            cashamount, 
                            nav, 
                            2 
                    FROM   clientredemption 
                    WHERE   posted = 1 
                            AND revised = 0 
                            AND status = 2 
                            AND Year(valuedate) = Year(@datefrom) 
							AND FundClientPK IN(SELECT FundClientPK FROM @List)
                    UNION ALL 
                    SELECT FundClientPK,FundPKTo,valuedate, 
                            totalunitamountfundto, 
                            totalcashamountfundto, 
                            navfundto NAV, 
                            1 
                    FROM   clientswitching 
                    WHERE   posted = 1 
                            AND revised = 0 
                            AND status = 2 
                            AND Year(valuedate) = Year(@datefrom) 
							AND FundClientPK IN(SELECT FundClientPK FROM @List)
                    UNION ALL 
                    SELECT FundClientPK,FundPKFrom,valuedate, 
                            unitamount, 
                            unitamount * navfundfrom, 
                            navfundfrom NAV, 
                            2 
                    FROM   clientswitching 
                    WHERE   posted = 1 
                            AND revised = 0 
                            AND status = 2 
                            AND Year(valuedate) = Year(@datefrom)
							AND FundClientPK IN(SELECT FundClientPK FROM @List)
							) dt 
					 

    Declare A Cursor For
	    Select Distinct  FundClientPK,FundPK from FundClientPosition where Date = @datefrom AND FundClientPK IN(SELECT FundClientPK FROM @List)
    Open A
    Fetch Next From A
    Into @CFundClientPK,@CFundPK

    While @@FETCH_STATUS = 0  
    BEGIN	
	    SET @RunningAmount = 0 
        SET @RunningBalance = 0 
        SET @RunningAvgPrice = 0;
	        WITH q 
                        AS (SELECT TOP 1000000000 * 
                            FROM   #AVGRecalculation where FundPK = @CFundPK and FundCLientPK = @CFundClientPK
                            ORDER  BY valuedate,trxtype ASC) 


                UPDATE q 
                SET    @RunningBalance = runningbalance = @RunningBalance + ( 
                                                            volume * CASE 
                                                                        WHEN 
                                                                trxtype = 1 THEN 1 
                                                                        ELSE -1 
                                                                    END ), 
                        @RunningAmount = runningamount = @RunningAmount + CASE WHEN 
                                                        trxtype 
                                                        = 
                                                        1 
                                                        THEN 
                                                        amount ELSE -volume 
                                                        * 
                                                        @RunningAvgPrice 
                                                        END, 
                        @RunningAvgPrice = runningavgprice = 
                                            CASE 
                                            WHEN trxtype = 1 THEN 
                                                CASE 
                                                WHEN @RunningBalance 
                                                        = 0 
                                                THEN 
                                                0 
                                                ELSE @RunningAmount 
                                                        / 
                                                        @RunningBalance 
                                                END 
                                            ELSE @RunningAvgPrice 
                                            END,
				        @COUNTER = IdentInt = @Counter + 1 
	


	    Fetch Next From A 
	    into @CFundClientPK,@CFundPK
    End	
    Close A
    Deallocate A



    insert into #AVGResultList
    Select max(ValueDate),FundClientPK,FundPK From #AVGRecalculation 
    where valuedate <= @datefrom
    group by FundClientPK,FundPK


                           
    DECLARE A CURSOR FOR 
	    SELECT * from #AVGResultList
 
    OPEN A
 
    FETCH NEXT FROM A INTO 
        @XDate,@XFundClientPK,@XFundPK
 
    WHILE @@FETCH_STATUS = 0
        BEGIN
        
            insert into #AVGResult
		    Select top 1 A.FundClientPK,A.FundPK,isnull(A.runningavgprice,0) From #AVGRecalculation A
		    where A.FundClientPK = @XFundClientPK and A.FundPK = @XFundPK and A.valuedate = @XDate
		    order by IdentInt desc
		
		
		    FETCH NEXT FROM A INTO @XDate,@XFundClientPK,@XFundPK 
        END
 
    CLOSE A
 
    DEALLOCATE A

    --PROSES KE TABLE FUNDCLIENTPOSITION

    Update A Set A.AvgNAV =  isnull(B.AVGNav,0)  From FundClientPosition A
    inner join #AVGResult B on A.FundPK = B.FundPK and A.FundClientPK = B.FundClientPK 
    where A.Date = @datefrom 


	SET @Datefrom = dbo.FWorkingDay(@Datefrom,1)
END


                            select 'Generate Average Success' Result
                        ";
                        cmd.Parameters.AddWithValue("@paramDateFrom", _paramDateFrom);
                        cmd.Parameters.AddWithValue("@paramDateTo", _paramDateTo);
                        cmd.Parameters.AddWithValue("@Type", _Type);
                        cmd.Parameters.AddWithValue("@PK", _PK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _HistoryPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return dr["Result"].ToString();
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




    }
}