using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;



namespace RFSRepository
{
    public class RetrieveMKBDDailyReps
    {
        Host _host = new Host();

        private RetrieveMKBDDaily setRetrieveMKBDDaily(SqlDataReader dr)
        {
            RetrieveMKBDDaily M_RetrieveMKBDDaily = new RetrieveMKBDDaily();
            M_RetrieveMKBDDaily.RetrieveMKBDDailyPK = Convert.ToInt32(dr["RetrieveMKBDDailyPK"]);
            M_RetrieveMKBDDaily.ValueDate = Convert.ToString(dr["ValueDate"]);
            M_RetrieveMKBDDaily.CheckAUM = Convert.ToString(dr["CheckAUM"]);
            M_RetrieveMKBDDaily.CheckClosePrice = Convert.ToString(dr["CheckClosePrice"]);
            M_RetrieveMKBDDaily.CheckRevaluation = Convert.ToString(dr["CheckRevaluation"]);
            M_RetrieveMKBDDaily.CheckManagementFee = Convert.ToString(dr["CheckManagementFee"]);
            return M_RetrieveMKBDDaily;
        }

        public string Validate_CheckAUM(DateTime _date)
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
                        create table #Temp 
                        (
                        Result nvarchar(MAX)
                        )


                        IF (dbo.CheckTodayIsHoliday(@date) = 1)
                        BEGIN
	                        SELECT 'Today is Holiday' as Result 
                        END
                        ELSE
	                        BEGIN
		                        IF EXISTS(Select * from AUM where date = @Date and status in (1,2))
	                        BEGIN
		                        Insert Into #Temp(Result)
		                        SELECT 'DONE' Result
	                        END
                            ELSE IF NOT EXISTS(Select * from CloseNAV where date = @date and status = 2)
	                        BEGIN
		                        Insert Into #Temp(Result)
		                        SELECT 'Not Exists Close NAV Today' Result
	                        END
	                        ELSE
	                        BEGIN
		                        Insert Into #Temp(Result)
		                        select 'Ready For Retrieve AUM' Result
	                        END

	                        DECLARE @combinedString VARCHAR(MAX)
	                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Result
	                        FROM #Temp
	                        BEGIN
		                        SELECT @combinedString as Result 
	                        END 
                        END  ";

                        cmd.Parameters.AddWithValue("@Date", _date);

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

        public string Validate_CheckClosePrice(DateTime _date)
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
                        --list dormant fund
                        declare @ListFundDormant table
                        (
	                        FundPK int,
	                        DormantDate date,
	                        ActivateDate date,
	                        StatusDormant int
                        )

                        insert into @ListFundDormant(FundPK)
                        select distinct fundpk from DormantFundTrails where status = 2

                        update A set A.DormantDate = B.DormantDate, A.ActivateDate = isnull(C.ActivateDate,'1900-01-01'), A.StatusDormant = case when isnull(C.ActivateDate,'1900-01-01') = '1900-01-01' then 1 
                        when B.DormantDate > isnull(C.ActivateDate,'1900-01-01') then 1 else 0 end

                        from @ListFundDormant A
                        left join (
                            select FundPK, Max(DormantDate) DormantDate from DormantFundTrails where status = 2 and BitDormant = 1
                            group by FundPK
                        ) B on A.FundPK = B.FundPK
                        left join (
                            select FundPK, Max(ActivateDate) ActivateDate from DormantFundTrails where status = 2 and BitDormant = 0
                            group by FundPK
                        ) C on A.FundPK = B.FundPK
                        --end dormant fund

                        create table #Temp 
                        (
                        Result nvarchar(MAX) COLLATE DATABASE_DEFAULT
                        )

                        create table #A(InstrumentID nvarchar(50) COLLATE DATABASE_DEFAULT)
                        create table #B(InstrumentID nvarchar(50) COLLATE DATABASE_DEFAULT)
                        
                        Insert into #A(InstrumentID)
                        select B.ID from TrxPortfolio A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        where Valuedate <= @date and A.status = 2 and posted = 1 and Revised = 0 and A.InstrumentTypePK = 4
                        Group By B.ID
                        having sum(case when TrxType = 1 then Volume else Volume * -1 end) > 1

                        Insert into #B(InstrumentID)
                        Select InstrumentID from #A where InstrumentID not in
                        (
                        select C.ID from CloseNAV A 
                        left join Fund B on A.FundPK = B.FundPK and B.Status = 2 
                        left join Instrument C on B.ID = C.ID and C.Status = 2 
                        where Date = @date and A.status in (2) and B.MaturityDate > @Date and A.FundPK not in (select FundPK from @ListFundDormant where @date between DormantDate and case when isnull(ActivateDate,'1900-01-01') = '1900-01-01' then '2098-12-31' else ActivateDate end) )

                        IF (dbo.CheckTodayIsHoliday(@date) = 1)
                        BEGIN
	                        SELECT 'Today is Holiday' as Result 
                        END
                        ELSE
                        BEGIN

	                        IF EXISTS(Select InstrumentID from #A where InstrumentID in
	                        (
	                        select B.ID from ClosePrice A 
	                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 
	                        where Date = @Date and A.status in (1,2))
	                        )
	                        BEGIN
	                        Insert Into #Temp(Result)
	                            SELECT 'DONE' Result 
	                        END
	                        ELSE IF EXISTS(Select InstrumentID from #B)
	                        BEGIN
	                        DECLARE @String VARCHAR(MAX)
	                        SELECT @String = COALESCE(@String + ', ', '') + InstrumentID
	                        FROM #B
		                        Insert Into #Temp(Result)
		                        SELECT 'Not Exists Close NAV For Portfolio : ' + @String as Result 
	                        END
	                        ELSE IF NOT EXISTS(Select InstrumentID from #A
	                        )
	                        BEGIN
		                        Insert Into #Temp(Result)
		                        select 'DONE, No Portfolio Reksadana' Result
	                        END
	                        ELSE
	                        BEGIN
		                        Insert Into #Temp(Result)
		                        select 'Ready For Retrieve Close Price Reksadana' Result
	                        END
	                        DECLARE @combinedString VARCHAR(MAX)
	                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Result
	                        FROM #Temp
	                        BEGIN
		                        SELECT @combinedString as Result 
	                        END
                        END     ";
                        cmd.Parameters.AddWithValue("@Date", _date);
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

        public string Validate_CheckRevaluation(DateTime _date, string _param)
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
                        create table #Temp 
                        (
                        Result nvarchar(MAX) COLLATE DATABASE_DEFAULT
                        )

                        create table #TempA 
                        (
                        Result nvarchar(MAX) COLLATE DATABASE_DEFAULT
                        )

                        create table #A(InstrumentID nvarchar(50) COLLATE DATABASE_DEFAULT)
                        create table #B(InstrumentID nvarchar(50) COLLATE DATABASE_DEFAULT)

                        Declare @Description nvarchar(50)
                        select @Description = dbo.SpaceBeforeCap(@Param)

                        Create Table #Journal
                        (Reference nvarchar(50))
                        
                        Insert Into #Journal(Reference)
                        select Reference from Journal where ValueDate = @Date and status = 2 and Posted = 1 and Reversed = 0 and Description = @Description

                        Insert into #A(InstrumentID)
                        select B.ID from TrxPortfolio A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        where Valuedate <= @date and A.status = 2 and posted = 1 and Revised = 0 and A.InstrumentTypePK in (1,4)
                        Group By B.ID
                        having sum(case when TrxType = 1 then Volume else Volume * -1 end) > 1
                        union all
                        select B.ID from TrxPortfolio A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        where Valuedate <= @date and A.status = 2 and posted = 1 and Revised = 0 and A.InstrumentTypePK in (2) and A.MaturityDate >= @date
                        Group By B.ID
                        having sum(case when TrxType = 1 then Volume else Volume * -1 end) > 1

                        Insert into #B(InstrumentID)
                        Select InstrumentID from #A where InstrumentID not in
                        (
                        select B.ID from ClosePrice A 
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 
                        where Date = @date and A.status in (2))

                        IF (dbo.CheckTodayIsHoliday(@date) = 1)
                        BEGIN
	                        SELECT 'Today is Holiday' as Result 
                        END
                        ELSE
	                        BEGIN
                                IF EXISTS(Select * from TrxPortfolio where Valuedate = @date and status in (1,2) and Posted = 0 and Revised = 0)
                                BEGIN
                                    Insert Into #Temp(Result)
                                    SELECT 'TrxPortfolio Not Posted' Result
                                END
                                ELSE
                                BEGIN
                                    Insert Into #TempA(Result)
                                    SELECT 'TrxPortfolio OK' Result
                                END



		                        IF EXISTS(select JournalPK from Journal where ValueDate = @Date and status = 2 and Posted = 1 and Description = @Description)
                                BEGIN
                                    Insert Into #Temp(Result)
                                    SELECT 'DONE' Result
                                END
		                        ELSE IF EXISTS(Select InstrumentID from #B
                                )
                                BEGIN
                                DECLARE @String VARCHAR(MAX)
                                SELECT @String = COALESCE(@String + ', ', '') + InstrumentID
                                FROM #B
                                Insert Into #Temp(Result)
                                SELECT 'Not Exists Close Price For Portfolio : ' + @String as Result 
                                END
		                        ELSE IF NOT EXISTS(Select InstrumentID from #A
		                        )
		                        BEGIN
			                        Insert Into #Temp(Result)
			                        select 'DONE, No Portfolio' Result
		                        END
                                ELSE
                                BEGIN
                                    Insert Into #TempA(Result)
                                    SELECT 'Close Price OK' Result
                                END

		                        DECLARE @combinedStringA VARCHAR(MAX)
                                SELECT @combinedStringA = COALESCE(@combinedStringA + ', ', '') + Result
                                FROM #TempA
                                IF (@combinedStringA = 'TrxPortfolio OK, Close Price OK')
                                BEGIN
                                    SELECT 'Ready For Generate Revaluation' as Result
                                END
		                        ELSE
		                        BEGIN
                                DECLARE @combinedString VARCHAR(MAX)
                                SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Result
                                FROM #Temp
			                        BEGIN
				                        SELECT @combinedString as Result 
			                        END 
		                        END
                        END";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@Param", _param);

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

        public string Retrieve_AUM(DateTime _date, string _usersID)
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
                        --list dormant fund
                        declare @ListFundDormant table
                        (
	                        FundPK int,
	                        DormantDate date,
	                        ActivateDate date,
	                        StatusDormant int
                        )

                        insert into @ListFundDormant(FundPK)
                        select distinct fundpk from DormantFundTrails where status = 2

                        update A set A.DormantDate = B.DormantDate, A.ActivateDate = isnull(C.ActivateDate,'1900-01-01'), A.StatusDormant = case when isnull(C.ActivateDate,'1900-01-01') = '1900-01-01' then 1 
                        when B.DormantDate > isnull(C.ActivateDate,'1900-01-01') then 1 else 0 end

                        from @ListFundDormant A
                        left join (
                            select FundPK, Max(DormantDate) DormantDate from DormantFundTrails where status = 2 and BitDormant = 1
                            group by FundPK
                        ) B on A.FundPK = B.FundPK
                        left join (
                            select FundPK, Max(ActivateDate) ActivateDate from DormantFundTrails where status = 2 and BitDormant = 0
                            group by FundPK
                        ) C on A.FundPK = B.FundPK
                        --end dormant fund

                        declare @AUM numeric (32,4) 
                        select @AUM = sum(case when B.CurrencyPK <> 1 then isnull(C.Rate,0) * AUM else AUM end) from closenav A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        left join CurrencyRate C on B.CurrencyPK = C.CurrencyPK and C.status in (1,2) and A.Date = C.Date
                        where A.Date = @Date and A.status = 2 and A.FundPK not in (select FundPK from @ListFundDormant where @date between DormantDate and case when isnull(ActivateDate,'1900-01-01') = '1900-01-01' then '2098-12-31' else ActivateDate end) 

                        declare @AUMPK int
                        Select @AUMPK = isnull(max(AUMPK),0) + 1 from AUM
                        insert into AUM (AUMPK,HistoryPK,status,Date,Amount,EntryUsersID,EntryTime,LastUpdate)
                        select @AUMPK,1,2,@Date,@AUM,@UsersID,@TimeNow,@TimeNow

                        SELECT 'Retrieve AUM Success !' as Result
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

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

        public string Retrieve_ClosePriceReksadana(DateTime _date, string _usersID)
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
                        --list dormant fund
                        declare @ListFundDormant table
                        (
	                        FundPK int,
	                        DormantDate date,
	                        ActivateDate date,
	                        StatusDormant int
                        )

                        insert into @ListFundDormant(FundPK)
                        select distinct fundpk from DormantFundTrails where status = 2

                        update A set A.DormantDate = B.DormantDate, A.ActivateDate = isnull(C.ActivateDate,'1900-01-01'), A.StatusDormant = case when isnull(C.ActivateDate,'1900-01-01') = '1900-01-01' then 1 
                        when B.DormantDate > isnull(C.ActivateDate,'1900-01-01') then 1 else 0 end

                        from @ListFundDormant A
                        left join (
                            select FundPK, Max(DormantDate) DormantDate from DormantFundTrails where status = 2 and BitDormant = 1
                            group by FundPK
                        ) B on A.FundPK = B.FundPK
                        left join (
                            select FundPK, Max(ActivateDate) ActivateDate from DormantFundTrails where status = 2 and BitDormant = 0
                            group by FundPK
                        ) C on A.FundPK = B.FundPK
                        --end dormant fund
    
                        declare @InstrumentPK int
                        declare @Nav numeric (22,4)
                        declare @AUM numeric (22,4)


                        DECLARE A CURSOR FOR  
                        select C.InstrumentPK,A.Nav,D.AUM from CloseNav A 
                        left join Fund B on A.FundPK = B.FundPK and B.status = 2
                        left join Instrument C on B.ID = C.ID and C.Status = 2 
                        left join CloseNav D on A.FundPK = D.FundPK and A.Date = D.Date and D.status = 2 
                        where A.Date = @Date and A.status = 2 and InstrumentPK is not null and A.FundPK not in (select FundPK from @ListFundDormant where @date between DormantDate and case when isnull(ActivateDate,'1900-01-01') = '1900-01-01' then '2098-12-31' else ActivateDate end) 


                        OPEN A 
                        FETCH NEXT FROM A 
                        INTO @InstrumentPK,@Nav,@AUM

                        While @@Fetch_Status  = 0 
                        BEGIN
                        declare @ClosePricePK int
                        Select @ClosePricePK = isnull(max(ClosePricePK),0) + 1 from ClosePrice
                        insert into ClosePrice (ClosePricePK,HistoryPK,status,Date,InstrumentPK,ClosePriceValue,TotalNAVReksadana,EntryUsersID,EntryTime,LastUpdate)
                        select @ClosePricePK,1,2,@Date,@InstrumentPK,@Nav,isnull(@AUM,0),@UsersID,@TimeNow,@TimeNow
                        FETCH NEXT FROM A 
                        INTO @InstrumentPK,@Nav,@AUM
                        END 
                        CLOSE A  
                        DEALLOCATE A


                        
                        SELECT 'Retrieve Close Price Reksadana Success !' as Result
                        
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

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

        public string Validate_CheckManagementFee(DateTime _date, string _param)
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
                        
                        --list dormant fund
                        declare @ListFundDormant table
                        (
	                        FundPK int,
	                        DormantDate date,
	                        ActivateDate date,
	                        StatusDormant int
                        )

                        insert into @ListFundDormant(FundPK)
                        select distinct fundpk from DormantFundTrails where status = 2

                        update A set A.DormantDate = B.DormantDate, A.ActivateDate = isnull(C.ActivateDate,'1900-01-01'), A.StatusDormant = case when isnull(C.ActivateDate,'1900-01-01') = '1900-01-01' then 1 
                        when B.DormantDate > isnull(C.ActivateDate,'1900-01-01') then 1 else 0 end

                        from @ListFundDormant A
                        left join (
                            select FundPK, Max(DormantDate) DormantDate from DormantFundTrails where status = 2 and BitDormant = 1
                            group by FundPK
                        ) B on A.FundPK = B.FundPK
                        left join (
                            select FundPK, Max(ActivateDate) ActivateDate from DormantFundTrails where status = 2 and BitDormant = 0
                            group by FundPK
                        ) C on A.FundPK = B.FundPK
                        --end dormant fund


						create table #Temp 
                        (
                        Result nvarchar(MAX)
                        )

                        create table #A(InstrumentID nvarchar(50))
                        create table #B(FundID nvarchar(50))

                        Declare @Description nvarchar(50)
                        select @Description = dbo.SpaceBeforeCap(@Param)

                        Create Table #Journal
                        (Reference nvarchar(50))
                        
                        Insert Into #Journal(Reference)
                        select Reference from Journal where ValueDate = @Date and status = 2 and Posted = 1 and Reversed = 0 and Description = @Description

                        Insert into #B(FundID)
                        Select ID from Fund where FundPK not in
                        (
                        select FundPK from FundDailyFee
                        where Date = @date) and status = 2  and @date between IssueDate and MaturityDate and FundPK not in (select FundPK from @ListFundDormant where @date between DormantDate and case when isnull(ActivateDate,'1900-01-01') = '1900-01-01' then '2098-12-31' else ActivateDate end)

                        IF (dbo.CheckTodayIsHoliday(@date) = 1)
                        BEGIN
	                        SELECT 'Today is Holiday' as Result 
                        END
                        ELSE
                        BEGIN
	                        IF exists(select JournalPK from Journal where ValueDate = @Date and status = 2 and Posted = 1 and Description = @Description)
	                        BEGIN
		                        Insert Into #Temp(Result)
		                        SELECT 'DONE' Result
	                        END
	                        ELSE IF EXISTS(Select FundID from #B)
	                        BEGIN
	                        DECLARE @String VARCHAR(MAX)
	                        SELECT @String = COALESCE(@String + ', ', '') + FundID
	                        FROM #B
		                        Insert Into #Temp(Result)
		                        SELECT 'Not Exists Fund Daily Fee For : ' + @String as Result 
	                        END
	                        ELSE
	                        BEGIN
		                        Insert Into #Temp(Result)
		                        select 'Ready For Generate Management Fee' Result
	                        END


	                        DECLARE @combinedString VARCHAR(MAX)
	                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Result
	                        FROM #Temp
	                        BEGIN
		                        SELECT @combinedString as Result 
	                        END 
                        END   ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@Param", _param);

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

        public string Generate_PortfolioRevaluation(DateTime _date, string _usersID)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //                       cmd.CommandTimeout = 0;
                        //                       cmd.CommandText = @"

                        //                       Declare @InstrumentPK int 
                        //                       Declare @LastVolume numeric(19,4) 
                        //                       Declare @InstrumentTypePK int 
                        //                       Declare @CadanganAccountPK int 
                        //                       Declare @UnrealisedAccountPK int 
                        //                       Declare @MarketValue Numeric(19,4) 
                        //                       Declare @PortfolioValue Numeric(19,4) 
                        //                       Declare @CadanganValue Numeric(19,4)  
                        //                       Declare @MarginValue Numeric(19,4) 
                        //                       Declare @Amount numeric(19,4) 
                        //                       Declare @PrevRevaluationAmount numeric(19,4) 
                        //                       Declare @TrxAmount numeric(19,4) 
                        //                       Declare @SellVolume numeric(19,4) 
                        //                       Declare @SellAmount numeric(19,4) 
                        //                       Declare @Sell int

                        //                       create table #ReferenceTemp
                        //                       (Reference nvarchar(50))

                        //                       Declare @JourHeader int  
                        //                       set @JourHeader = 0    
                        //                       Declare @JournalPK int 
                        //                       Declare @PeriodPK int 
                        //                       Declare @Reference nvarchar(50)    
                        //                       Select @PeriodPK = PeriodPK From Period Where DateFrom <= @Date and Dateto >= @Date and Status = 2  
                        //                       Select @JournalPK = isnull(Max(JournalPK),0) from Journal    


                        //                       insert into #ReferenceTemp (Reference)
                        //                       exec getJournalReference @Date,'ADJ',@Reference out  

                        //                       set @JournalPK = @JournalPK + 1   
                        //                       INSERT INTO [Journal]  
                        //                       ([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate]  
                        //                       ,[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy]  
                        //                       ,[PostedTime],[EntryUsersID],[EntryTime]  
                        //                       ,[ApprovedUsersID],[ApprovedTime],[DBUserID],LastUpdate)      
                        //                       SELECT @JournalPK,1,2,'Portfolio Revaluation',@PeriodPK, @Date
                        //                       ,0,'',@Reference,1,'Portfolio Revaluation',1,@UsersID
                        //                       ,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow 

                        //                       Declare @AutoNo int 
                        //                       set @AutoNo = 0   
                        //                       DECLARE A CURSOR FOR 

                        //                       Select InstrumentPK,LastVolume ,InstrumentTypePK,Sell
                        //                       from (
                        //                       Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume ,A.InstrumentTypePK, sum(Sell) Sell    
                        //                       from (
                        //                       select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume,InstrumentTypePK,0 Sell from trxPortfolio 
                        //                       where ValueDate <= @Date and Posted = 1 and trxType = 1  and Revised = 0 and status = 2 and InstrumentTypePK in (1,4)
                        //                       Group By InstrumentPK,InstrumentTypePK  
                        //                       UNION ALL  
                        //                       select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume,InstrumentTypePK,0 Sell from trxPortfolio 
                        //                       where ValueDate <= @Date and Posted = 1 and trxType = 1  and Revised = 0 and status = 2 and InstrumentTypePK <> 1 and MaturityDate >= @Date
                        //                       Group By InstrumentPK,InstrumentTypePK  
                        //                       UNION ALL    
                        //                       select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume,InstrumentTypePK,0 Sell from trxPortfolio 
                        //                       where ValueDate < @Date and Posted = 1  and trxType = 2  and Revised = 0 and status = 2
                        //                       Group By InstrumentPK,InstrumentTypePK  
                        //                       UNION ALL   
                        //                       select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume,InstrumentTypePK,1 Sell from trxPortfolio 
                        //                       where ValueDate = @Date and Posted = 1  and trxType = 2  and Revised = 0 and status = 2
                        //                       Group By InstrumentPK,InstrumentTypePK  
                        //                       UNION ALL   
                        //                       select A.InstrumentPK,sum(Balance) BuyVolume,0 SellVolume,1 InstrumentTypePK,0 from CorporateActionResult A
                        //                       left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                        //                       where Date <= @Date and FundPK = 9999 
                        //                       Group By A.InstrumentPK
                        //                       )A 
                        //                       Group By A.InstrumentPK,A.InstrumentTypePK
                        //                       )B
                        //                       where (LastVolume <> 0) or (LastVolume = 0 and Sell = 1)
                        //                       OPEN A 
                        //                       FETCH NEXT FROM A 
                        //                       INTO @InstrumentPk,@LastVolume,@InstrumentTypePK,@Sell  
                        //                       WHILE @@FETCH_STATUS = 0  
                        //                       BEGIN 
                        //                       set @MarketValue = 0 
                        //                       If @InstrumentTypePK in (1,2,4) 
                        //                       BEGIN 
                        //                       IF (@InstrumentTypePK = 1)
                        //                       BEGIN
                        //                       Select @CadanganAccountPK = CadanganEquity ,@UnrealisedAccountPK = UnrealisedEquity From AccountingSetup 
                        //                       Where Status = 2

                        //                       IF (@Sell = 1 and @LastVolume = 0)   
                        //                       BEGIN
                        //                        select @Amount = isnull(dbo.[FGetAccountBalanceJournalByDateByInstrumentPK](@Date,@CadanganAccountPK,@InstrumentPK),0) *-1

                        //                       END 
                        //                       ELSE 
                        //                       BEGIN
                        //                        set @MarketValue =  @LastVolume * dbo.FGetLastClosePriceForFundPosition(@Date,@InstrumentPK) 
                        //                        set @PortfolioValue =  dbo.FGetLastAvgFromInvestment_Acc(@Date,@InstrumentPK) * @LastVolume              
                        //                        set @PrevRevaluationAmount = isnull(dbo.[FGetAccountBalanceJournalByDateByInstrumentPK](@Date,@CadanganAccountPK,@InstrumentPK),0)    
                        //                        select @Amount = @MarketValue - (@PortfolioValue + @PrevRevaluationAmount)
                        //                       END   

                        //                       END
                        //                       ELSE IF (@InstrumentTypePK = 2)
                        //                       BEGIN
                        //                       Select @CadanganAccountPK = CadanganBond ,@UnrealisedAccountPK = UnrealisedBond From AccountingSetup 
                        //                       Where Status = 2

                        //                       IF (@Sell = 1 and @LastVolume = 0)   
                        //                       BEGIN
                        //                        select @Amount = isnull(dbo.[FGetAccountBalanceJournalByDateByInstrumentPK](@Date,@CadanganAccountPK,@InstrumentPK),0) *-1

                        //                       END 
                        //                       ELSE 
                        //                       BEGIN
                        //                        set @MarketValue =  @LastVolume * dbo.FGetLastClosePriceForFundPosition(@Date,@InstrumentPK)/100 
                        //                        set @PortfolioValue =  dbo.FGetLastAvgFromInvestment_Acc(@Date,@InstrumentPK)/100 * @LastVolume              
                        //                        set @PrevRevaluationAmount = isnull(dbo.[FGetAccountBalanceJournalByDateByInstrumentPK](@Date,@CadanganAccountPK,@InstrumentPK),0)    
                        //                        select @Amount = @MarketValue - (@PortfolioValue + @PrevRevaluationAmount)
                        //                       END    

                        //                       END
                        //                       ELSE IF (@InstrumentTypePK = 4)
                        //                       BEGIN
                        //                       Select @CadanganAccountPK = CadanganReksadana ,@UnrealisedAccountPK = UnrealisedReksadana From AccountingSetup 
                        //                       Where Status = 2

                        //                       IF (@Sell = 1 and @LastVolume = 0)   
                        //                       BEGIN
                        //                        select @Amount = isnull(dbo.[FGetAccountBalanceJournalByDateByInstrumentPK](@Date,@CadanganAccountPK,@InstrumentPK),0) *-1

                        //                       END 
                        //                       ELSE 
                        //                       BEGIN
                        //                        set @MarketValue =  @LastVolume * dbo.FGetLastClosePriceForFundPosition(@Date,@InstrumentPK) 
                        //                        set @PortfolioValue =  dbo.FGetLastAvgFromInvestment_Acc(@Date,@InstrumentPK) * @LastVolume              
                        //                        set @PrevRevaluationAmount = isnull(dbo.[FGetAccountBalanceJournalByDateByInstrumentPK](@Date,@CadanganAccountPK,@InstrumentPK),0)    
                        //                        select @Amount = @MarketValue - (@PortfolioValue + @PrevRevaluationAmount)
                        //                       END   



                        //   IF (@Buy = 1) -- (LastPrice - PrevPrice) * VolumeYesterday
                        //   BEGIN
                        //    set @MarketValue =  dbo.FGetLastVolumeAccByCompanyAccountTradingPK(dbo.Fworkingday(@Date,-1),@InstrumentPK,@CompanyAccountTradingPK)   * dbo.FGetLastClosePriceForFundPosition(@Date,@InstrumentPK)            
                        //    set @PrevMarketValue =  dbo.FGetLastVolumeAccByCompanyAccountTradingPK(dbo.Fworkingday(@Date,-1),@InstrumentPK,@CompanyAccountTradingPK) * dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date,-1),@InstrumentPK) 
                        //    select @Amount = @MarketValue - @PrevMarketValue  
                        //   END
                        //ELSE IF (@Sell = 1)
                        //   BEGIN
                        //    set @MarketValue =  @LastVolume   * (dbo.FGetLastClosePriceForFundPosition(@Date,@InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(@date,@InstrumentPK))         
                        //    set @PrevMarketValue =  dbo.FGetLastVolumeAccByCompanyAccountTradingPK(dbo.Fworkingday(@Date,-1),@InstrumentPK,@CompanyAccountTradingPK) * (dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date,-1),@InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(dbo.Fworkingday(@Date,-1),@InstrumentPK)) 
                        //    select @Amount = @MarketValue - @PrevMarketValue 

                        //   END
                        //   ELSE
                        //   BEGIN
                        //    set @MarketValue =  @LastVolume   * dbo.FGetLastClosePriceForFundPosition(@Date,@InstrumentPK)            
                        //    set @PrevMarketValue = @LastVolume * dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date,-1),@InstrumentPK) 
                        //    select @Amount = @MarketValue - @PrevMarketValue  
                        //   END



                        //                       END


                        //                       Declare @AllocDepartmentPK int 
                        //                       Declare @AllocPercent numeric(18,8) 
                        //                       declare @AfterAllocateAmount numeric(18,4) 
                        //                       Declare @CounterAmount numeric(18,4) 
                        //                       Declare @Count Int 
                        //                       Declare @Inc int 
                        //                       set @Inc = 0 set @CounterAmount = 0 
                        //                       set @AfterAllocateAmount = 0   
                        //                       Declare @RoundAmount numeric(19,4) 
                        //                       Declare @RoundingDepartmentPK int 
                        //                       Declare @LastAmount Numeric(19,4)  
                        //                       Declare @FinalAmountAfterRounding numeric(19,4)  
                        //                       If @Amount < 0 BEGIN       
                        //                       --Mulai dari sini
                        //                       Select @Count = isnull(Count(DepartmentPK) ,0) From AccountAllocateByCostCenterSetup 
                        //                       where AccountPK =  @UnrealisedAccountPK and status = 2  
                        //                       if @Count = 0 
                        //                       begin 
                        //                       Set @AutoNo = @AutoNo + 1   

                        //                       INSERT INTO [JournalDetail]  
                        //                       ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        //                       ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        //                       ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        //                       ,[BaseCredit],[LastUsersID],LastUpdate)  
                        //                       Select @JournalPK,@AutoNo,1,2,@UnrealisedAccountPK,1,0,isnull(@AllocDepartmentPk,0),0,0,@InstrumentPK,0,'','','D',Abs(@Amount),Abs(@Amount),0,1,Abs(@Amount),0,@UsersID,@TimeNow 
                        //                       end   
                        //                       else 
                        //                       begin 
                        //                       Declare B Cursor For 
                        //                       Select departmentPK,AllocationPercentage From AccountAllocateByCostCenterSetup 
                        //                       where AccountPK =  @UnrealisedAccountPK and status = 2 
                        //                       Open B   
                        //                       Fetch Next From B 
                        //                       Into @AllocDepartmentPk,@AllocPercent  
                        //                       While @@Fetch_Status  = 0 
                        //                       Begin 	
                        //                       Set @AfterAllocateAmount = abs(@amount) * @AllocPercent/100  
                        //                       Set @CounterAmount =  @CounterAmount + @AfterAllocateAmount 
                        //                       set @AutoNo = @AutoNo + 1   

                        //                       INSERT INTO [JournalDetail]  
                        //                       ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        //                       ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        //                       ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        //                       ,[BaseCredit],[LastUsersID],LastUpdate) 
                        //                       Select @JournalPK,@AutoNo,1,2,@UnrealisedAccountPK,1,0,isnull(@AllocDepartmentPk,0),0,0,@InstrumentPK,0,'','','D',isnull(@AfterAllocateAmount,0),   
                        //                       isnull(@AfterAllocateAmount,0),0,1,isnull(@AfterAllocateAmount,0),0,@UsersID,@TimeNow  
                        //                       set @Inc = @Inc + 1 
                        //                       IF @Inc = @count 
                        //                       begin Set @RoundAmount = @CounterAmount - abs(@Amount) 
                        //                       if  @RoundAmount <> 0 
                        //                       begin 
                        //                       select top 1 @roundingDepartmentPK = DepartmentPK From AccountAllocateByCostCenterSetup  
                        //                       where AccountPK = @UnrealisedAccountPK and status = 2 and BitRounding = 1  
                        //                       Select @LastAmount =  Amount From JournalDetail 
                        //                       where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2 
                        //                       set @FinalAmountAfterRounding = @lastAmount +  @RoundAmount  
                        //                       Update JournalDetail Set Amount = @FinalAmountAfterRounding,Debit = @FinalAmountAfterRounding,baseDebit = @FinalAmountAfterRounding * CurrencyRate 
                        //                       where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2  end   end  
                        //                       FETCH NEXT FROM B 
                        //                       INTO  @AllocDepartmentPk,@AllocPercent  
                        //                       End  
                        //                       Close B 
                        //                       DEALLOCATE  B  
                        //                       end   
                        //                       set @AutoNo = @AutoNo + 1   

                        //                       INSERT INTO [JournalDetail]  
                        //                       ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        //                       ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        //                       ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        //                       ,[BaseCredit],[LastUsersID],LastUpdate) 
                        //                       Select @JournalPK,@AutoNo,1,2,@CadanganAccountPK,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@amount), 0,abs(@amount),1,0,abs(@amount),@UsersID,@TimeNow 
                        //                       END   
                        //                       If @Amount > 0 
                        //                       Begin   
                        //                       set @AutoNo = @AutoNo + 1   

                        //                       INSERT INTO [JournalDetail]  
                        //                       ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        //                       ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        //                       ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        //                       ,[BaseCredit],[LastUsersID],LastUpdate) 
                        //                       Select @JournalPK,@AutoNo,1,2,@CadanganAccountPK,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@amount),abs(@amount),0,1,abs(@amount),0,@UsersID,@TimeNow   
                        //                       set @AutoNo = @AutoNo + 1   

                        //                       Select @Count = isnull(Count(DepartmentPK) ,0) From AccountAllocateByCostCenterSetup 
                        //                       where AccountPK =  @UnrealisedAccountPK and status = 2  
                        //                       if @Count = 0 
                        //                       begin   

                        //                       INSERT INTO [JournalDetail]  
                        //                       ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        //                       ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        //                       ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        //                       ,[BaseCredit],[LastUsersID],LastUpdate)   
                        //                       Select @JournalPK,@AutoNo,1,2,@UnrealisedAccountPK,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@amount), 0,abs(@amount),1,0,abs(@amount),@UsersID,@TimeNow 
                        //                       end  
                        //                       else 
                        //                       begin 
                        //                       Declare C Cursor For 
                        //                       Select departmentPK,AllocationPercentage From AccountAllocateByCostCenterSetup 
                        //                       where AccountPK =  @UnrealisedAccountPK and status = 2 
                        //                       Open C   
                        //                       Fetch Next From C 
                        //                       Into @AllocDepartmentPk,@AllocPercent  
                        //                       While @@Fetch_Status  = 0 
                        //                       Begin 	
                        //                       Set @AfterAllocateAmount = abs(@amount) * @AllocPercent/100  
                        //                       Set @CounterAmount =  @CounterAmount + @AfterAllocateAmount 
                        //                       set @AutoNo = @AutoNo + 1   

                        //                       INSERT INTO [JournalDetail]  
                        //                       ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        //                       ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        //                       ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        //                       ,[BaseCredit],[LastUsersID],LastUpdate) 
                        //                       Select @JournalPK,@AutoNo,1,2,@UnrealisedAccountPK,1,0,isnull(@AllocDepartmentPk,0),0,0,@InstrumentPK,0,'','','C',isnull(@AfterAllocateAmount,0),   
                        //                       0,isnull(@AfterAllocateAmount,0),1,0,isnull(@AfterAllocateAmount,0),@UsersID,@TimeNow  
                        //                       set @Inc = @Inc + 1 
                        //                       IF @Inc = @count 
                        //                       begin 
                        //                       Set @RoundAmount = @CounterAmount - abs(@Amount) 
                        //                       if  @RoundAmount <> 0 
                        //                       begin 
                        //                       select top 1 @roundingDepartmentPK = DepartmentPK From AccountAllocateByCostCenterSetup  
                        //                       where AccountPK = @UnrealisedAccountPK and status = 2 and BitRounding = 1  
                        //                       Select @LastAmount =  Amount From JournalDetail 
                        //                       where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2 
                        //                       set @FinalAmountAfterRounding = @lastAmount  + @RoundAmount  
                        //                       Update JournalDetail Set Amount = @FinalAmountAfterRounding,Debit = @FinalAmountAfterRounding,baseDebit = @FinalAmountAfterRounding * CurrencyRate 
                        //                       where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2  end   
                        //                       end  
                        //                       FETCH NEXT FROM C 
                        //                       INTO  @AllocDepartmentPk,@AllocPercent  
                        //                       End  
                        //                       Close C 
                        //                       DEALLOCATE  C  
                        //                       end end end   

                        //                       FETCH NEXT FROM A 
                        //                       INTO @InstrumentPk,@LastVolume,@InstrumentTypePK,@Sell 
                        //                       END 
                        //                       CLOSE A  
                        //                       DEALLOCATE A

                        //                       DECLARE @combinedString VARCHAR(MAX)
                        //                       SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Reference
                        //                       FROM #ReferenceTemp
                        //                       IF (@combinedString is null)
                        //                       BEGIN
                        //                           SELECT 'No Data Revaluation, Please Check Close Price' as Result
                        //                       END
                        //                       ELSE
                        //                       BEGIN
                        //                           SELECT 'Portfolio Revaluation Success ! Reference is : ' + @combinedString as Result
                        //                       END

                        //                       ";


                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        
Declare @InstrumentPK int
Declare @LastVolume numeric(19, 4) 
Declare @InstrumentTypePK int
Declare @CadanganAccountPK int
Declare @UnrealisedAccountPK int
Declare @MarketValue Numeric(19, 4) 
Declare @PortfolioValue Numeric(19, 4)
Declare @CadanganValue Numeric(19, 4)
Declare @MarginValue Numeric(19, 4)
Declare @Amount numeric(19, 4)
Declare @PrevMarketValue numeric(19, 4)
Declare @TrxAmount numeric(19, 4)
Declare @SellVolume numeric(19, 4)
Declare @SellAmount numeric(19, 4)
Declare @Sell int
Declare @Buy int

Declare @SellPrice numeric(18,4)
Declare @FlagRealised int
Declare @RealisedAmount numeric(18,4)

create table #ReferenceTemp
(Reference nvarchar(50))

Declare @JourHeader int
set @JourHeader = 0
Declare @JournalPK int
Declare @PeriodPK int
Declare @Reference nvarchar(50)
Select @PeriodPK = PeriodPK From Period Where DateFrom <= @Date and Dateto >= @Date and Status = 2
Select @JournalPK = isnull(Max(JournalPK), 0) from Journal

IF(@ClientCode = '09')    
BEGIN
    insert into #ReferenceTemp (Reference)
    exec GetJournalReference_09 @Date,'ADJ',@Reference out
END
ELSE
BEGIN
    insert into #ReferenceTemp (Reference)
    exec getJournalReference @Date, 'GJ', @Reference out
END


set @JournalPK = @JournalPK + 1
INSERT INTO[Journal]
([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate]
,[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy]
,[PostedTime],[EntryUsersID],[EntryTime]
,[ApprovedUsersID],[ApprovedTime],[DBUserID], LastUpdate)      
SELECT @JournalPK,1,2,'Portfolio Revaluation',@PeriodPK, @Date
,0,'',@Reference,1,'Portfolio Revaluation',1,@UsersID
,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow

Declare @AutoNo int
set @AutoNo = 0
DECLARE A CURSOR FOR

Select InstrumentPK, LastVolume, InstrumentTypePK, Buy, Sell
from(
Select A.InstrumentPK, isnull(sum(A.BuyVolume) - sum(A.SellVolume), 0) LastVolume, A.InstrumentTypePK, sum(Buy) Buy, sum(Sell) Sell
from(
select InstrumentPK, sum(Volume) BuyVolume, 0 SellVolume, InstrumentTypePK, 0 Buy, 0 Sell from trxPortfolio
where ValueDate < @Date and Posted = 1 and trxType = 1  and Revised = 0 and InstrumentTypePK <> 2 and status = 2
Group By InstrumentPK, InstrumentTypePK
UNION ALL
select InstrumentPK, sum(Volume) BuyVolume, 0 SellVolume, InstrumentTypePK, 1 Buy, 0 Sell from trxPortfolio
where ValueDate = @Date and Posted = 1 and trxType = 1  and Revised = 0 and InstrumentTypePK <> 2 and status = 2
Group By InstrumentPK, InstrumentTypePK
UNION ALL
select InstrumentPK, 0 BuyVolume, sum(Volume) SellVolume, InstrumentTypePK, 0 Buy, 0 Sell from trxPortfolio
where ValueDate < @Date and Posted = 1  and trxType = 2  and Revised = 0 and InstrumentTypePK <> 2 and status = 2
Group By InstrumentPK, InstrumentTypePK
UNION ALL
select InstrumentPK, 0 BuyVolume, sum(Volume) SellVolume, InstrumentTypePK, 0 Buy, 1 Sell from trxPortfolio
where ValueDate = @Date and Posted = 1  and trxType = 2  and Revised = 0 and InstrumentTypePK <> 2 and status = 2
Group By InstrumentPK, InstrumentTypePK


-- BOND
UNION ALL
select InstrumentPK, sum(Volume) BuyVolume, 0 SellVolume, InstrumentTypePK, 0 Buy, 0 Sell from trxPortfolio
where SettledDate < @Date and Posted = 1 and trxType = 1  and Revised = 0 and InstrumentTypePK = 2 and status = 2
Group By InstrumentPK, InstrumentTypePK
UNION ALL
select InstrumentPK, sum(Volume) BuyVolume, 0 SellVolume, InstrumentTypePK, 1 Buy, 0 Sell from trxPortfolio
where dbo.fworkingday(SettledDate, 1) = @Date and Posted = 1 and trxType = 1  and Revised = 0 and InstrumentTypePK = 2 and status = 2
Group By InstrumentPK, InstrumentTypePK
UNION ALL
select InstrumentPK, 0 BuyVolume, sum(Volume) SellVolume, InstrumentTypePK, 0 Buy, 0 Sell from trxPortfolio
where SettledDate < @Date and Posted = 1  and trxType = 2  and Revised = 0 and InstrumentTypePK = 2 and status = 2
Group By InstrumentPK, InstrumentTypePK
UNION ALL
select InstrumentPK, 0 BuyVolume, sum(Volume) SellVolume, InstrumentTypePK, 0 Buy, 1 Sell from trxPortfolio
where SettledDate = @Date and Posted = 1  and trxType = 2  and Revised = 0 and InstrumentTypePK = 2 and status = 2
Group By InstrumentPK, InstrumentTypePK
UNION ALL
select A.InstrumentPK, sum(Balance) BuyVolume, 0 SellVolume, 1 InstrumentTypePK, 0 Buy, 0 Sell from CorporateActionResult A
     left
                                                                                               join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1, 2)
where Date = @Date and FundPK = 9999
Group By A.InstrumentPK

)A
Group By A.InstrumentPK,A.InstrumentTypePK
)B
where(LastVolume <> 0) or(LastVolume = 0 and Sell = 1)

OPEN A
FETCH NEXT FROM A
INTO @InstrumentPk, @LastVolume, @InstrumentTypePK, @Buy, @Sell
WHILE @@FETCH_STATUS = 0
BEGIN
set @MarketValue = 0
If @InstrumentTypePK in (1,2,4) 
BEGIN
IF(@InstrumentTypePK = 1)
BEGIN


    Select @CadanganAccountPK = CadanganEquity ,@UnrealisedAccountPK = UnrealisedEquity From AccountingSetup

    Where Status = 2


	select @SellPrice = Price,@FlagRealised = case when Price - dbo.FGetLastAvgFromInvestment_Acc(@date,InstrumentPK) < 0 then 1 else 0 end  from TrxPortfolio 
	where status = 2 and Revised = 0 and Posted = 1 and ValueDate = @Date and InstrumentPK = @InstrumentPk

    IF(@Sell = 1)
    BEGIN
        IF (@ClientCode = 1)
        BEGIN
			if (@FlagRealised = 1) -- kalau jual Rugi
			BEGIN
				select @RealisedAmount = (@LastVolume * (dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK) - @SellPrice))
				- dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * (dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date, -1), @InstrumentPK) - @SellPrice)
		
			END

            set @MarketValue = dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * (dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(@date, @InstrumentPK))
            set @PrevMarketValue = dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * (dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date, -1), @InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(dbo.Fworkingday(@Date, -1), @InstrumentPK))
            
			select @Amount = @MarketValue - @PrevMarketValue + isnull(@RealisedAmount,0)
        END
        ELSE
        BEGIN
            set @MarketValue = @LastVolume * (dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(@date, @InstrumentPK))

            set @PrevMarketValue = dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * (dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date, -1), @InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(dbo.Fworkingday(@Date, -1), @InstrumentPK))

            select @Amount = @MarketValue - @PrevMarketValue
        END

    END
    ELSE
    BEGIN
        IF NOT EXISTS(select * from CorporateActionResult where FundPK = 9999 and Date = @Date and InstrumentPK = @InstrumentPK)
        BEGIN


            set @MarketValue = dbo.FGetLastVolumeAcc(@date, @InstrumentPK) * (dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(@Date, @InstrumentPK))
            set @PrevMarketValue = dbo.FGetLastVolumeAcc(dbo.fworkingday(@date, -1), @InstrumentPK) * (dbo.FGetLastClosePriceForFundPosition(dbo.fworkingday(@date, -1), @InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(dbo.fworkingday(@date, -1), @InstrumentPK))
            select @Amount = @MarketValue - @PrevMarketValue
        END
        ELSE
        BEGIN
            set @MarketValue = @LastVolume * dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK)
            set @PrevMarketValue = dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date, -1), @InstrumentPK)
            select @Amount = @MarketValue - @PrevMarketValue
        END
    END

END
ELSE IF(@InstrumentTypePK = 2)
BEGIN
Select @CadanganAccountPK = CadanganBond ,@UnrealisedAccountPK = UnrealisedBond From AccountingSetup
Where Status = 2


    IF(@Buy = 1)--(LastPrice - PrevPrice) * VolumeYesterday
    BEGIN
        set @MarketValue = dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK) / 100

        set @PrevMarketValue = dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date, -1), @InstrumentPK) / 100

        select @Amount = @MarketValue - @PrevMarketValue
    END
    ELSE IF(@Sell = 1)
    BEGIN
        IF (@ClientCode = 1)
        BEGIN
			if (@FlagRealised = 1) -- kalau jual Rugi
			BEGIN
				select @RealisedAmount = (@LastVolume * (dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK) - @SellPrice))
				- dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * (dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date, -1), @InstrumentPK) - @SellPrice)
			END

            set @MarketValue = dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * (dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(@date, @InstrumentPK))

            set @PrevMarketValue = dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * (dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date, -1), @InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(dbo.Fworkingday(@Date, -1), @InstrumentPK))

            select @Amount = @MarketValue - @PrevMarketValue + isnull(@RealisedAmount,0)
        END
        ELSE
        BEGIN
            set @MarketValue = @LastVolume * (dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(@date, @InstrumentPK))

            set @PrevMarketValue = dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * (dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date, -1), @InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(dbo.Fworkingday(@Date, -1), @InstrumentPK))

            select @Amount = @MarketValue - @PrevMarketValue
        END

    END
    ELSE
    BEGIN
        set @MarketValue = @LastVolume * dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK) / 100

        set @PrevMarketValue = @LastVolume * dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date, -1), @InstrumentPK) / 100

        select @Amount = @MarketValue - @PrevMarketValue
    END

END
ELSE IF (@InstrumentTypePK = 4)
BEGIN


    Select @CadanganAccountPK = CadanganReksadana, @UnrealisedAccountPK = UnrealisedReksadana From AccountingSetup

    Where Status = 2




    IF(@Buy = 1)--(LastPrice - PrevPrice) * VolumeYesterday
    BEGIN

        set @MarketValue = dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK)

        set @PrevMarketValue = dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date, -1), @InstrumentPK)

        select @Amount = @MarketValue - @PrevMarketValue
    END

    ELSE IF(@Sell = 1)
    BEGIN
        IF (@ClientCode = 1)
        BEGIN
			if (@FlagRealised = 1) -- kalau jual Rugi
			BEGIN
				select @RealisedAmount = (@LastVolume * (dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK) - @SellPrice))
				- dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * (dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date, -1), @InstrumentPK) - @SellPrice)
			END

            set @MarketValue = dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * (dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(@date, @InstrumentPK))

            set @PrevMarketValue = dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * (dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date, -1), @InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(dbo.Fworkingday(@Date, -1), @InstrumentPK))

            select @Amount = @MarketValue - @PrevMarketValue + isnull(@RealisedAmount,0)
        END
        ELSE
        BEGIN
            set @MarketValue = @LastVolume * (dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(@date, @InstrumentPK))

            set @PrevMarketValue = dbo.FGetLastVolumeAcc(dbo.Fworkingday(@Date, -1), @InstrumentPK) * (dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date, -1), @InstrumentPK) - dbo.FGetLastAvgFromInvestment_Acc(dbo.Fworkingday(@Date, -1), @InstrumentPK))

            select @Amount = @MarketValue - @PrevMarketValue
        END

    END
    ELSE
    BEGIN

        set @MarketValue = @LastVolume * dbo.FGetLastClosePriceForFundPosition(@Date, @InstrumentPK)

        set @PrevMarketValue = @LastVolume * dbo.FGetLastClosePriceForFundPosition(dbo.Fworkingday(@Date, -1), @InstrumentPK)

        select @Amount = @MarketValue - @PrevMarketValue
    END




END



Declare @AllocDepartmentPK int
Declare @AllocPercent numeric(18, 8)
declare @AfterAllocateAmount numeric(18, 4)
Declare @CounterAmount numeric(18, 4)
Declare @Count Int
Declare @Inc int
set @Inc = 0 set @CounterAmount = 0
set @AfterAllocateAmount = 0
Declare @RoundAmount numeric(19, 4)
Declare @RoundingDepartmentPK int
Declare @LastAmount Numeric(19, 4)
Declare @FinalAmountAfterRounding numeric(19, 4)
If @Amount < 0 BEGIN
--Mulai dari sini
Select @Count = isnull(Count(DepartmentPK), 0) From AccountAllocateByCostCenterSetup
where AccountPK = @UnrealisedAccountPK and status = 2
if @Count = 0
begin
Set @AutoNo = @AutoNo + 1

INSERT INTO[JournalDetail]
([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
,[BaseCredit],[LastUsersID],LastUpdate)  
Select @JournalPK, @AutoNo,1,2, @UnrealisedAccountPK,1,0, isnull(@AllocDepartmentPk,3), 0, 0, @InstrumentPK, 0, '', '', 'D', Abs(@Amount), Abs(@Amount), 0, 1, Abs(@Amount), 0, @UsersID, @TimeNow
end   
else 
begin
Declare B Cursor For
Select departmentPK, AllocationPercentage From AccountAllocateByCostCenterSetup
where AccountPK = @UnrealisedAccountPK and status = 2
Open B
Fetch Next From B
Into @AllocDepartmentPk, @AllocPercent
While @@Fetch_Status = 0
Begin
Set @AfterAllocateAmount = abs(@amount) * @AllocPercent / 100
Set @CounterAmount = @CounterAmount + @AfterAllocateAmount
set @AutoNo = @AutoNo + 1

INSERT INTO[JournalDetail]
([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]
,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]
,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]
,[BaseCredit],[LastUsersID], LastUpdate) 
Select @JournalPK, @AutoNo,1,2, @UnrealisedAccountPK,1,0, isnull(@AllocDepartmentPk,3), 0, 0, @InstrumentPK, 0, '', '', 'D', isnull(@AfterAllocateAmount, 0),
isnull(@AfterAllocateAmount, 0), 0, 1, isnull(@AfterAllocateAmount, 0), 0, @UsersID, @TimeNow
set @Inc = @Inc + 1
IF @Inc = @count
begin Set @RoundAmount = @CounterAmount - abs(@Amount) 
if  @RoundAmount <> 0
begin
select top 1 @roundingDepartmentPK = DepartmentPK From AccountAllocateByCostCenterSetup
where AccountPK = @UnrealisedAccountPK and status = 2 and BitRounding = 1
Select @LastAmount = Amount From JournalDetail
where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2
set @FinalAmountAfterRounding = @lastAmount + @RoundAmount
Update JournalDetail Set Amount = @FinalAmountAfterRounding, Debit = @FinalAmountAfterRounding, baseDebit = @FinalAmountAfterRounding * CurrencyRate
where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2  end   end
FETCH NEXT FROM B
INTO  @AllocDepartmentPk, @AllocPercent
End
Close B
DEALLOCATE  B
end
set @AutoNo = @AutoNo + 1

INSERT INTO[JournalDetail]
([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]
,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]
,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]
,[BaseCredit],[LastUsersID], LastUpdate) 
Select @JournalPK, @AutoNo,1,2, @CadanganAccountPK,1,0,3,0,0, @InstrumentPK,0,'','','C', abs(@amount), 0, abs(@amount), 1, 0, abs(@amount), @UsersID, @TimeNow
END
If @Amount > 0
Begin
set @AutoNo = @AutoNo + 1

INSERT INTO[JournalDetail]
([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]
,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]
,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]
,[BaseCredit],[LastUsersID], LastUpdate) 
Select @JournalPK, @AutoNo,1,2, @CadanganAccountPK,1,0,3,0,0, @InstrumentPK,0,'','','D', abs(@amount), abs(@amount), 0, 1, abs(@amount), 0, @UsersID, @TimeNow
set @AutoNo = @AutoNo + 1

Select @Count = isnull(Count(DepartmentPK), 0) From AccountAllocateByCostCenterSetup
where AccountPK = @UnrealisedAccountPK and status = 2  
if @Count = 0
begin

INSERT INTO[JournalDetail]
([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]
,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]
,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]
,[BaseCredit],[LastUsersID], LastUpdate)   
Select @JournalPK, @AutoNo,1,2, @UnrealisedAccountPK,1,0,3,0,0, @InstrumentPK,0,'','','C', abs(@amount), 0, abs(@amount), 1, 0, abs(@amount), @UsersID, @TimeNow
end  
else 
begin
Declare C Cursor For
Select departmentPK, AllocationPercentage From AccountAllocateByCostCenterSetup
where AccountPK = @UnrealisedAccountPK and status = 2
Open C
Fetch Next From C
Into @AllocDepartmentPk, @AllocPercent
While @@Fetch_Status = 0
Begin
Set @AfterAllocateAmount = abs(@amount) * @AllocPercent / 100
Set @CounterAmount = @CounterAmount + @AfterAllocateAmount
set @AutoNo = @AutoNo + 1

INSERT INTO[JournalDetail]
([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]
,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]
,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]
,[BaseCredit],[LastUsersID], LastUpdate) 
Select @JournalPK, @AutoNo,1,2, @UnrealisedAccountPK,1,0, isnull(@AllocDepartmentPk,3), 0, 0, @InstrumentPK, 0, '', '', 'C', isnull(@AfterAllocateAmount, 0),
0, isnull(@AfterAllocateAmount, 0), 1, 0, isnull(@AfterAllocateAmount, 0), @UsersID, @TimeNow
set @Inc = @Inc + 1
IF @Inc = @count
begin
Set @RoundAmount = @CounterAmount - abs(@Amount) 
if  @RoundAmount <> 0
begin
select top 1 @roundingDepartmentPK = DepartmentPK From AccountAllocateByCostCenterSetup
where AccountPK = @UnrealisedAccountPK and status = 2 and BitRounding = 1
Select @LastAmount = Amount From JournalDetail
where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2
set @FinalAmountAfterRounding = @lastAmount + @RoundAmount
Update JournalDetail Set Amount = @FinalAmountAfterRounding, Debit = @FinalAmountAfterRounding, baseDebit = @FinalAmountAfterRounding * CurrencyRate
where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2  end
end
FETCH NEXT FROM C
INTO  @AllocDepartmentPk, @AllocPercent
End
Close C
DEALLOCATE  C
end end 

end

FETCH NEXT FROM A
INTO @InstrumentPk, @LastVolume, @InstrumentTypePK, @Buy, @Sell
END
CLOSE A
DEALLOCATE A

DECLARE @combinedString VARCHAR(MAX)
SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Reference
FROM #ReferenceTemp
IF(@combinedString is null)
BEGIN
    SELECT 'No Data Revaluation, Please Check Close Price' as Result
END
ELSE
BEGIN
    SELECT 'Portfolio Revaluation Success ! Reference is : ' + @combinedString as Result
END ";


                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);
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



        public string Retrieve_ManagementFee(DateTime _date, string _usersID)
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

                      Declare @FundPK int
Declare @InstrumentPK int 
    
Declare @ARManagementFee int 
Declare @TaxARManagementFee int 
Declare @ManagementFeeExpense int 
Declare @TaxManagementFeeExpense int 


declare @ManagementFeeAmount numeric(22,6)
declare @SubsFeeAmount numeric(22,6)
declare @RedempFeeAmount numeric(22,6)
declare @SwitchFeeAmount numeric(22,6)

declare @ARManagementFeeAmount numeric(22,6)
declare @TaxARManagementFeeAmount numeric(22,6)
declare @ManagementFeeExpenseAmount numeric(22,6)
declare @TaxManagementFeeExpenseAmount numeric(22,6)

-- Subs, Redemp, Switch
declare @ARSubscriptionFee int
declare @SubscriptionFeeIncome int
declare @ARRedemptionFee int
declare @RedemptionFeeIncome int
declare @ARSwitchingFee int
declare @SwitchingFeeIncome int


create table #ReferenceTemp
(Reference nvarchar(50))

Declare @JourHeader int  
set @JourHeader = 0    
Declare @JournalPK int 
Declare @PeriodPK int 
Declare @Reference nvarchar(50)    
Select @PeriodPK = PeriodPK From Period Where DateFrom <= @Date and Dateto >= @Date and Status = 2  
Select @JournalPK = isnull(Max(JournalPK),0) from Journal   

IF(@ClientCode = '09')    
BEGIN
    insert into #ReferenceTemp (Reference)
    exec GetJournalReference_09 @Date,'ADJ',@Reference out
END
ELSE IF(@ClientCode = '21')    
BEGIN
    insert into #ReferenceTemp (Reference)
    exec getJournalReference @Date,'GJ',@Reference out
END
ELSE
BEGIN
    insert into #ReferenceTemp (Reference)
    exec getJournalReference @Date,'ADJ',@Reference out
END


 
set @JournalPK = @JournalPK + 1   
INSERT INTO [Journal]  
([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate]  
,[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy]  
,[PostedTime],[EntryUsersID],[EntryTime]  
,[ApprovedUsersID],[ApprovedTime],[DBUserID],LastUpdate)     
SELECT @JournalPK,1,2,'Management Fee',@PeriodPK, @Date
,0,'',@Reference,1,'Management Fee',1,@UsersID
,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow 


Declare @AutoNo int 
set @AutoNo = 0   

DECLARE A CURSOR FOR 
select A.FundPK,case when B.CurrencyPK <> 1 then isnull(C.Rate,0) * ManagementFeeAmount else ManagementFeeAmount end,
case when B.CurrencyPK <> 1 then isnull(C.Rate,0) * isnull(SubscriptionFeeAmount,0) else isnull(SubscriptionFeeAmount,0) end,
case when B.CurrencyPK <> 1 then isnull(C.Rate,0) * isnull(RedemptionFeeAmount,0) else isnull(RedemptionFeeAmount,0) end,
case when B.CurrencyPK <> 1 then isnull(C.Rate,0) * isnull(SwitchingFeeAmount,0) else isnull(SwitchingFeeAmount,0) end from FundDailyFee A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
left join CurrencyRate C on B.CurrencyPK = C.CurrencyPK and C.status in (1,2) and A.Date = C.Date
where A.Date = @Date
OPEN A 
FETCH NEXT FROM A 
INTO @FundPK,@ManagementFeeAmount,@SubsFeeAmount,@RedempFeeAmount,@SwitchFeeAmount   
WHILE @@FETCH_STATUS = 0  
BEGIN 


select @InstrumentPK = InstrumentPK From Instrument A 
left join Fund B on A.ID = B.ID and B.Status = 2 where A.Status = 2 and B.FundPK = @FundPK         
 
Select @ARManagementFee = ARManagementFee,@TaxARManagementFee =  TaxARManagementFee,
@ManagementFeeExpense = ManagementFeeExpense, @TaxManagementFeeExpense = TaxManagementFeeExpense,
@ARSubscriptionFee = ARSubscriptionFee,@SubscriptionFeeIncome = SubscriptionFeeIncome,
@ARRedemptionFee = ARRedemptionFee,@RedemptionFeeIncome = RedemptionFeeIncome,
@ARSwitchingFee = ARSwitchingFee,@SwitchingFeeIncome = SwitchingFeeIncome

  
From AccountingSetup Where Status = 2

set @AutoNo = @AutoNo + 1   

set @ManagementFeeExpenseAmount = @ManagementFeeAmount/1.1
set @TaxManagementFeeExpenseAmount = 0.1 * @ManagementFeeExpenseAmount
set @TaxARManagementFeeAmount = 0.02 * @ManagementFeeExpenseAmount
set @ARManagementFeeAmount = @ManagementFeeAmount - @TaxARManagementFeeAmount

INSERT INTO [JournalDetail]  
([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
,[BaseCredit],[LastUsersID],LastUpdate) 
Select @JournalPK,@AutoNo,1,2,@ARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@ARManagementFeeAmount), abs(@ARManagementFeeAmount),0,1,abs(@ARManagementFeeAmount),0,@UsersID,@TimeNow 
               
			            
set @AutoNo = @AutoNo + 1   


INSERT INTO [JournalDetail]  
([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
,[BaseCredit],[LastUsersID],LastUpdate) 
Select @JournalPK,@AutoNo,1,2,@TaxARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@TaxARManagementFeeAmount), abs(@TaxARManagementFeeAmount),0,1,abs(@TaxARManagementFeeAmount),0,@UsersID,@TimeNow 
               
			            
set @AutoNo = @AutoNo + 1   

INSERT INTO [JournalDetail]  
([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
,[BaseCredit],[LastUsersID],LastUpdate) 
Select @JournalPK,@AutoNo,1,2,@ManagementFeeExpense,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@ManagementFeeExpenseAmount), 0,abs(@ManagementFeeExpenseAmount),1,0,abs(@ManagementFeeExpenseAmount),@UsersID,@TimeNow 

set @AutoNo = @AutoNo + 1   

INSERT INTO [JournalDetail]  
([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
,[BaseCredit],[LastUsersID],LastUpdate) 
Select @JournalPK,@AutoNo,1,2,@TaxManagementFeeExpense,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@TaxManagementFeeExpenseAmount), 0,abs(@TaxManagementFeeExpenseAmount),1,0,abs(@TaxManagementFeeExpenseAmount),@UsersID,@TimeNow 
     
                        
-- SUBS, REDEMP, SWITCH


IF (isnull(@SubsFeeAmount,0) <> 0)
BEGIN
    set @AutoNo = @AutoNo + 1   

    INSERT INTO [JournalDetail]  
    ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
    ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
    ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
    ,[BaseCredit],[LastUsersID],LastUpdate) 
    Select @JournalPK,@AutoNo,1,2,@ARSubscriptionFee,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@SubsFeeAmount), abs(@SubsFeeAmount),0,1,abs(@SubsFeeAmount),0,@UsersID,@TimeNow 
               
    set @AutoNo = @AutoNo + 1   

    INSERT INTO [JournalDetail]  
    ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
    ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
    ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
    ,[BaseCredit],[LastUsersID],LastUpdate) 
    Select @JournalPK,@AutoNo,1,2,@SubscriptionFeeIncome,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@SubsFeeAmount), 0,abs(@SubsFeeAmount),1,0,abs(@SubsFeeAmount),@UsersID,@TimeNow 
END 

IF (isnull(@RedempFeeAmount,0) <> 0)
BEGIN
    set @AutoNo = @AutoNo + 1   

    INSERT INTO [JournalDetail]  
    ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
    ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
    ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
    ,[BaseCredit],[LastUsersID],LastUpdate) 
    Select @JournalPK,@AutoNo,1,2,@ARRedemptionFee,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@RedempFeeAmount), abs(@RedempFeeAmount),0,1,abs(@RedempFeeAmount),0,@UsersID,@TimeNow 
               
    set @AutoNo = @AutoNo + 1   

    INSERT INTO [JournalDetail]  
    ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
    ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
    ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
    ,[BaseCredit],[LastUsersID],LastUpdate) 
    Select @JournalPK,@AutoNo,1,2,@RedemptionFeeIncome,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@RedempFeeAmount), 0,abs(@RedempFeeAmount),1,0,abs(@RedempFeeAmount),@UsersID,@TimeNow 
END

IF (isnull(@SwitchFeeAmount,0) <> 0)
BEGIN
    set @AutoNo = @AutoNo + 1   

    INSERT INTO [JournalDetail]  
    ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
    ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
    ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
    ,[BaseCredit],[LastUsersID],LastUpdate) 
    Select @JournalPK,@AutoNo,1,2,@ARSwitchingFee,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@SwitchFeeAmount), abs(@SwitchFeeAmount),0,1,abs(@SwitchFeeAmount),0,@UsersID,@TimeNow 
               
    set @AutoNo = @AutoNo + 1   

    INSERT INTO [JournalDetail]  
    ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
    ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
    ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
    ,[BaseCredit],[LastUsersID],LastUpdate) 
    Select @JournalPK,@AutoNo,1,2,@SwitchingFeeIncome,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@SwitchFeeAmount), 0,abs(@SwitchFeeAmount),1,0,abs(@SwitchFeeAmount),@UsersID,@TimeNow 
END

FETCH NEXT FROM A 
INTO @FundPK,@ManagementFeeAmount,@SubsFeeAmount,@RedempFeeAmount,@SwitchFeeAmount   
END 
CLOSE A  
DEALLOCATE A

DECLARE @combinedString NVARCHAR(500)
SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Reference
FROM #ReferenceTemp
IF (@combinedString is null)
BEGIN
    SELECT 'No Data Retrieve, Please Check Fund Daily Fee' as Result
END
ELSE
BEGIN
SELECT 'Retrieve Management Fee Success ! Reference is : ' + @combinedString as Result
END
                        
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);
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

        public string Void_AUM(DateTime _date, string _usersID)
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
                         update AUM set status = 3,VoidUsersID = @UsersID,VoidTime = @TimeNow,LastUpdate=@TimeNow where Date =@Date
                         SELECT 'Void AUM Success !' as Result
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

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

        public string Void_ClosePriceReksadana(DateTime _date, string _usersID)
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

    
                        declare @InstrumentPK int


                        DECLARE A CURSOR FOR  
                        select C.InstrumentPK from CloseNav A 
                        left join Fund B on A.FundPK = B.FundPK and B.status = 2
                        left join Instrument C on B.ID = C.ID and C.Status = 2 
                        left join CloseNav D on A.FundPK = D.FundPK and dbo.FWorkingDay(A.Date,-1) = D.Date and D.status = 2 
                        where A.Date = @Date and A.status = 2


                        OPEN A 
                        FETCH NEXT FROM A 
                        INTO @InstrumentPK

                        While @@Fetch_Status  = 0 
                        BEGIN

                        update ClosePrice set status = 3,VoidUsersID = @UsersID,VoidTime = @TimeNow,LastUpdate=@TimeNow where Date =@Date and InstrumentPK =  @InstrumentPK
                        SELECT 'Void ClosePrice Reksadana Success !' as Result

                        FETCH NEXT FROM A 
                        INTO @InstrumentPK
                        END 
                        CLOSE A  
                        DEALLOCATE A

                        
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

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

        public string Void_PortfolioRevaluation(DateTime _date, string _usersID)
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


                        update A set status = 3 from JournalDetail A
                        left join Journal B on A.JournalPK = B.JournalPK and B.status = 2 and B.Posted = 1 and B.Reversed = 0
                        where B.ValueDate = @Date and B.Description = 'Portfolio Revaluation'

                        update Journal set posted = 0, status = 3, VoidUsersID = @UsersID,VoidTime = @TimeNow,LastUpdate=@TimeNow
                        where status = 2 and Posted = 1 and Reversed = 0 and ValueDate = @Date and Description = 'Portfolio Revaluation'

                        SELECT 'Void Portfolio Revaluation Success !' as Result

                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

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

        public string Void_ManagementFee(DateTime _date, string _usersID)
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

                        update A set status = 3 from JournalDetail A
                        left join Journal B on A.JournalPK = B.JournalPK and B.status = 2 and B.Posted = 1 and B.Reversed = 0
                        where B.ValueDate = @Date and B.Description = 'Management Fee'

                        update Journal set posted = 0, status = 3, VoidUsersID = @UsersID,VoidTime = @TimeNow,LastUpdate=@TimeNow
                        where status = 2 and Posted = 1 and Reversed = 0 and ValueDate = @Date and Description = 'Management Fee'

                        SELECT 'Void Management Fee Success !' as Result
                        
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

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

        public string Validate_CheckAgentCommission(DateTime _date, string _param)
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
                        
                        create table #Temp 
                        (
                        Result nvarchar(MAX)
                        )

                        create table #A(InstrumentID nvarchar(50))

                        Declare @Description nvarchar(50)
                        Declare @DailyDataCommTime datetime

                        select @DailyDataCommTime = LastUpdate from DailyDataForCommissionAcc where date = @date 

                        select @Description = dbo.SpaceBeforeCap(@Param)

                        Create Table #Journal
                        (Reference nvarchar(50))
                        
                        Insert Into #Journal(Reference)
                        select Reference from Journal where ValueDate = @Date and status = 2 and Posted = 1 and Reversed = 0 and Description = @Description


                        IF (dbo.CheckTodayIsHoliday(@date) = 1)
                        BEGIN
	                        SELECT 'Today is Holiday' as Result 
                        END
                        ELSE
                        BEGIN
	                        IF exists(select JournalPK from Journal where ValueDate = @Date and status = 2 and Posted = 1 and Description = @Description
                                      and EntryTime >= @DailyDataCommTime
                            )
	                        BEGIN
		                        Insert Into #Temp(Result)
		                        SELECT 'DONE' Result
	                        END
	                        ELSE IF NOT EXISTS(Select top 1 FundPK from DailyDataForCommissionAcc where date = @date)
	                        BEGIN
		                        Insert Into #Temp(Result)
		                        SELECT 'Not Exists DailyDataForCommissionAcc' as Result 
	                        END
	                        ELSE
	                        BEGIN
		                        Insert Into #Temp(Result)
		                        select 'Ready For Posting Agent Commission' Result
	                        END


	                        DECLARE @combinedString VARCHAR(MAX)
	                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Result
	                        FROM #Temp
	                        BEGIN
		                        SELECT @combinedString as Result 
	                        END 
                        END   ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@Param", _param);

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

        public string Retrieve_AgentCommission(DateTime _date, string _usersID)
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

DECLARE @PPHCutOff NUMERIC(22,4)
declare @Counter int
SET @PPHCutOff = 0


select @datefrom = case when dbo.CheckIsYesterdayHoliday(@dateto) = 1 then dateadd(day,1,dbo.FWorkingDay(@dateto,-1)) else @dateto end





DECLARE @table TABLE
(
	Date datetime,
	Bulan INT,
	FundPK INT,
	AgentPK INT,
	CurrencyPK INT,
	TotalPrevLastMonth NUMERIC(22,4),
	TotalThisMonth NUMERIC(22,4),
	TotalKomisi NUMERIC(22,4)
)

DECLARE @tablePPH23 TABLE
(
	Date datetime,
	Bulan INT,
	FundPK INT,
	AgentPK INT,
	CurrencyPK INT,
	TotalMfee NUMERIC(22,4),
	PPH23 NUMERIC(22,4),
	PPN NUMERIC(22,4),
	TotalKomisi NUMERIC(22,4)
)

INSERT INTO @table
        ( Date,
		  Bulan ,
		  FundPK,
          AgentPK ,
		  CurrencyPK ,
          TotalPrevLastMonth ,
          TotalThisMonth,
		  TotalKomisi
        )

SELECT A.MFeeDate,MONTH(A.MFeeDate) Bulan
,A.FundPK
,A.SellingAgentPK,B.CurrencyPK
,@PPHCutOff * 0.5
,(SUM(ISNULL(A.AgentFee,0) * CASE WHEN B.CurrencyPK <> 1 THEN C.Rate ELSE 1 END ) / 1.1 + @PPHCutOff) * 0.5
,(SUM(ISNULL(A.AgentFee,0) * CASE WHEN B.CurrencyPK <> 1 THEN C.Rate ELSE 1 END ) / 1.1 + @PPHCutOff)
FROM dbo.DailyDataForCommissionRptNew A
LEFT JOIN Fund B ON A.fundPK = B.FundpK AND B.Status IN (1,2)
LEFT JOIN 
								(
									Select CurrencyPK,Rate From CurrencyRate where date = (
										Select max(Date) from CurrencyRate where status = 2 and date <= @dateTo
									) and status = 2
								) C ON B.CurrencyPK = C.CurrencyPK 
LEFT JOIN Agent D ON A.SellingAgentPK = D.AgentPK AND D.status IN (1,2)
WHERE  A.MFeeDate BETWEEN @Datefrom AND @DateTo
AND D.BitPPH21 = 1 
GROUP BY A.MFeeDate,A.FundPK,A.SellingAgentPK,B.CurrencyPK,MONTH(NAVDate)



INSERT INTO @tablePPH23
        ( Date,
		  Bulan ,
		  FundPK,
          AgentPK ,
		  CurrencyPK ,
		  TotalMfee,
		  PPH23,
		  PPN,
		  TotalKomisi
        )

SELECT A.MFeeDate,MONTH(A.MFeeDate) Bulan
,A.FundPK
,A.SellingAgentPK,B.CurrencyPK
,(SUM(ISNULL(A.AgentFee,0) * CASE WHEN B.CurrencyPK <> 1 THEN C.Rate ELSE 1 END ) / 1.1 )
,(SUM(ISNULL(A.AgentFee,0) * CASE WHEN B.CurrencyPK <> 1 THEN C.Rate ELSE 1 END ) / 1.1 ) * D.PPH23Percent / 100 
,CASE WHEN D.BitPPN = 1 THEN  (SUM(ISNULL(A.AgentFee,0) * CASE WHEN B.CurrencyPK <> 1 THEN C.Rate ELSE 1 END ) / 1.1 ) * 0.1 ELSE 0 END
,(SUM(ISNULL(A.AgentFee,0) * CASE WHEN B.CurrencyPK <> 1 THEN C.Rate ELSE 1 END ) / 1.1 )
  + CASE WHEN D.BitPPN = 1 THEN  (SUM(A.AgentFee * CASE WHEN B.CurrencyPK <> 1 THEN C.Rate ELSE 1 END ) / 1.1 ) * 0.1 ELSE 0 END
	- (SUM(ISNULL(A.AgentFee,0) * CASE WHEN B.CurrencyPK <> 1 THEN C.Rate ELSE 1 END ) / 1.1 ) * D.PPH23Percent / 100 
FROM dbo.DailyDataForCommissionRptNew A
LEFT JOIN Fund B ON A.fundPK = B.FundpK AND B.Status IN (1,2)
 LEFT JOIN 
								(
									Select CurrencyPK,Rate From CurrencyRate where date = (
										Select max(Date) from CurrencyRate where status = 2 and date <= @dateTo
									) and status = 2
								) C ON B.CurrencyPK = C.CurrencyPK 
LEFT JOIN Agent D ON A.SellingAgentPK = D.AgentPK AND D.status IN (1,2)
WHERE  A.MFeeDate BETWEEN @Datefrom AND @DateTo
AND D.BitPPH23 = 1 
GROUP BY  A.MFeeDate,A.FundPK,A.SellingAgentPK,MONTH(NAVDate),B.CurrencyPK,D.PPH23Percent,D.BitPPN

	SELECT 
		@Counter =  count(*) +1000
	FROM @table A
	LEFT JOIN
	(
		SELECT * FROM dbo.PPH21Setup WHERE status = 2
		AND date =
		(
			SELECT MAX(date) FROM dbo.PPH21Setup WHERE status = 2
		)
	)B ON A.TotalThisMonth BETWEEN B.RangeFrom AND B.RangeTo or A.TotalThisMonth > B.RangeTo
	LEFT JOIN Agent C ON A.AgentPK = C.AgentPK AND C.status IN (1,2)


delete DailyDataForCommissionAcc where Date between @datefrom and @dateto

INSERT INTO DailyDataForCommissionAcc
SELECT 
	A.Date,A.FundPK,A.AgentPK,A.CurrencyPK,C.BitPPH23,C.BitPPH21,C.BitPPN
	,ISNULL(A.TotalKomisi,0) MFee
	
	,CASE WHEN ISNULL(A.TotalKomisi,0) <> 0 THEN
		CASE WHEN A.TotalThisMonth BETWEEN B.RangeFrom AND B.RangeTo THEN (A.TotalThisMonth - B.RangeFrom + 1) 
	* (B.Percentage / 100 + CASE WHEN LEN(ISNULL(C.NPWPNo,'')) > 4 THEN 0 ELSE (B.Percentage / 100 * 0.2) END)
		ELSE B.RangeTo * B.Percentage / 100 END
			ELSE 0 
				 END PPH21
	,0 PPH23
	,0 Tax
	,ISNULL(A.TotalKomisi,0) - (CASE WHEN ISNULL(A.TotalKomisi,0) <> 0 THEN
		CASE WHEN A.TotalThisMonth BETWEEN B.RangeFrom AND B.RangeTo THEN (A.TotalThisMonth - B.RangeFrom + 1) 
	* (B.Percentage / 100 + CASE WHEN LEN(ISNULL(C.NPWPNo,'')) > 4 THEN 0 ELSE (B.Percentage / 100 * 0.2) END)
		ELSE B.RangeTo * B.Percentage / 100 END
			ELSE 0 
				 END) TotalKomisiSetelah, @TimeNow
	
	
FROM @table A
LEFT JOIN
(
	SELECT * FROM dbo.PPH21Setup WHERE status = 2
	AND date =
	(
		SELECT MAX(date) FROM dbo.PPH21Setup WHERE status = 2
	)
)B ON A.TotalThisMonth BETWEEN B.RangeFrom AND B.RangeTo or A.TotalThisMonth > B.RangeTo
LEFT JOIN Agent C ON A.AgentPK = C.AgentPK AND C.status IN (1,2)
--ORDER BY C.Name

union all

SELECT 
	A.Date,A.FundPK,A.AgentPK,A.CurrencyPK,C.BitPPH23,C.BitPPH21,C.BitPPN
	,ISNULL(A.TotalMfee,0) ManagementFee,0 pph21,ISNULL(A.PPH23,0) PPH23,ISNULL(A.PPN,0) PPN,ISNULL(A.TotalKomisi,0) TotalKomisi,@TimeNow
	
FROM @tablePPH23 A
LEFT JOIN Agent C ON A.AgentPK = C.AgentPK AND C.status IN (1,2)
--ORDER BY C.Name

union all

SELECT 
	A.MfeeDate,A.FundPK,A.SellingAgentPK,B.CurrencyPK,isnull(C.BitPPH23,0),isnull(C.BitPPH21,0),isnull(C.BitPPN,0)
	,ISNULL(A.AgentFee/1.1,0) AgentFee,0 pph21,0 PPH23,0 PPN,ISNULL(A.AgentFee/1.1,0) TotalKomisi,@TimeNow
	
FROM DailyDataForCommissionRptNew A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
LEFT JOIN Agent C ON A.SellingAgentPK = C.AgentPK AND C.status IN (1,2)
where A.AgentFee <> 0 and (C.BitPPH21 = 0 and C.BitPPH23 = 0) and A.MFeeDate between @DateFrom and @DateTo

update DailyDataForCommissionAcc 
set PPH21 = case when BitPPH21 = 1 then 0.025 * MFee else 0 end,
PPH23 = case when BitPPH23 = 1 then 0.02 * MFee else 0 end,
Tax = case when BitPPN = 1 then 0.1 * MFee else 0 end
where Date between @datefrom and @dateto





IF NOT EXISTS(select top 1 FundPK from DailyDataForCommissionAcc where Date between @datefrom and @dateto)
BEGIN
    SELECT 'No Data Retrieve, Please Check Generate Unit Fee Summary' as Result
END
ELSE
BEGIN
SELECT 'Retrieve Agent Commission Success ! Please Check Daily Data For Commission Acc' as Result
END

                        
                        ";

                        cmd.Parameters.AddWithValue("@DateFrom", _date);
                        cmd.Parameters.AddWithValue("@DateTo", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

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

        public string Posting_AgentCommission(DateTime _date, string _usersID)
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

                       

Declare @InstrumentPK int
Declare @AgentPK int 
Declare @CurrencyPK int 
Declare @BitPPH23 bit
Declare @BitPPH21 bit
Declare @BitPPN bit

    
Declare @AgentCommExpense int 
Declare @AgentCommPayable int 
Declare @WHTPayablePPH23 int 
Declare @WHTPayablePPH21 int
Declare @VatIn int 
Declare @VatOut int 
Declare @AgentCommCash int 


declare @ManagementFeeAmount numeric(22,6)
declare @PPH21Amount numeric(22,6)
declare @PPH23Amount numeric(22,6)
declare @PPNAmount numeric(22,6)
declare @AmountAfterTax numeric(22,6)
declare @CashAmount numeric(22,6)

declare @startDate datetime

select @startDate = case when dbo.CheckIsYesterdayHoliday(@date) = 1 then dateadd(day,1,dbo.FWorkingDay(@date,-1)) else @date end


create table #ReferenceTemp
(Reference nvarchar(50))

Declare @JourHeader int  
set @JourHeader = 0    
Declare @JournalPK int 
Declare @PeriodPK int 
Declare @Reference nvarchar(50)    
Select @PeriodPK = PeriodPK From Period Where DateFrom <= @Date and Dateto >= @Date and Status = 2  
Select @JournalPK = isnull(Max(JournalPK),0) from Journal   

insert into #ReferenceTemp (Reference)
exec getJournalReference @Date,'ADJ',@Reference out  
 
set @JournalPK = @JournalPK + 1  
 
update A set status = 3 from JournalDetail A
left join Journal B on A.JournalPK = B.JournalPK and B.status = 2 and B.Posted = 1 and B.Reversed = 0
where B.ValueDate = @Date and B.Description = 'Agent Commission'

update Journal set posted = 0, status = 3, VoidUsersID = @UsersID,VoidTime = @TimeNow,LastUpdate=@TimeNow
where status = 2 and Posted = 1 and Reversed = 0 and ValueDate = @Date and Description = 'Agent Commission'


INSERT INTO [Journal]  
([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate]  
,[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy]  
,[PostedTime],[EntryUsersID],[EntryTime]  
,[ApprovedUsersID],[ApprovedTime],[DBUserID],LastUpdate)     
SELECT @JournalPK,1,2,'Agent Commission',@PeriodPK, @Date
,0,'',@Reference,1,'Agent Commission',1,@UsersID
,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow 


Declare @AutoNo int 
set @AutoNo = 0   

DECLARE A CURSOR FOR 
select C.InstrumentPK,AgentPK,A.CurrencyPK,BitPPH23,BitPPH21,BitPPN,sum(Mfee) Mfee,sum(PPH21) PPH21,sum(PPH23) PPH23,sum(Tax) Tax,sum(TaxKomisiSetelah) TaxKomisiSetelah from DailyDataForCommissionAcc A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
left join Instrument C on B.ID = C.ID and C.status in (1,2)
where Date between @startDate and @Date
group by C.InstrumentPK,AgentPK,A.CurrencyPK,BitPPH23,BitPPH21,BitPPN

OPEN A 
FETCH NEXT FROM A 
INTO @InstrumentPK,@AgentPK,@CurrencyPK,@BitPPH23,@BitPPH21,@BitPPN,@ManagementFeeAmount,@PPH21Amount,@PPH23Amount,@PPNAmount,@AmountAfterTax  
WHILE @@FETCH_STATUS = 0  
BEGIN 

 
Select @AgentCommExpense = AgentCommissionExpense,@AgentCommPayable =  AgentCommissionPayable,
@WHTPayablePPH23 = WHTPayablePPH23,@WHTPayablePPH21 = WHTPayablePPH21, @VatIn = VatIn,
@VatOut = VatOut,@AgentCommCash = AgentCommissionCash
From AccountingSetup Where Status = 2

set @AutoNo = @AutoNo + 1   



IF(@BitPPH23 = 1 AND @BitPPN = 1)
BEGIN

	--Comm Cost Company 1
	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,@AgentCommExpense,@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'PPH with PPN','','D',abs(@ManagementFeeAmount), abs(@ManagementFeeAmount),0,1,abs(@ManagementFeeAmount),0,@UsersID,@TimeNow 
               
			            
	set @AutoNo = @AutoNo + 1   

	--Account Payables 2
	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,@AgentCommPayable,@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'PPH with PPN','','C',abs(@ManagementFeeAmount - @PPH23Amount + @PPNAmount),0, abs(@ManagementFeeAmount - @PPH23Amount + @PPNAmount),1,0,abs(@ManagementFeeAmount - @PPH23Amount + @PPNAmount),@UsersID,@TimeNow 
               

	set @AutoNo = @AutoNo + 1   
	 
	--WHT Payables (PPh 23) 4
	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,@WHTPayablePPH23,@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'PPH with PPN','','C',abs(@PPH23Amount), 0,abs(@PPH23Amount),1,0,abs(@PPH23Amount),@UsersID,@TimeNow 
  
  
		      
    set @AutoNo = @AutoNo + 1   

	--VAT IN (Current Year) 9
	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,@VatIn,@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'PPH with PPN','','D',abs(@PPNAmount),abs(@PPNAmount),0,1,abs(@PPNAmount),0,@UsersID,@TimeNow 
 
	
END


ELSE IF(@BitPPH23 = 1 AND @BitPPN = 0)
BEGIN

	--Comm Cost Company 2
	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,@AgentCommExpense,@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'PPH No PPN','','D',abs(@ManagementFeeAmount), abs(@ManagementFeeAmount),0,1,abs(@ManagementFeeAmount),0,@UsersID,@TimeNow 
               
			            
	set @AutoNo = @AutoNo + 1   

	--Account Payables 2
	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,@AgentCommPayable,@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'PPH No PPN','','C',abs(@ManagementFeeAmount - @PPH23Amount),0, abs(@ManagementFeeAmount - @PPH23Amount),1,0,abs(@ManagementFeeAmount - @PPH23Amount),@UsersID,@TimeNow 
               

	set @AutoNo = @AutoNo + 1   

	--WHT Payables (PPh 23) 4
	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,@WHTPayablePPH23,@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'PPH No PPN','','C',abs(@PPH23Amount), 0,abs(@PPH23Amount),1,0,abs(@PPH23Amount),@UsersID,@TimeNow 
  
  
END


ELSE IF(@BitPPH21 = 1) --PPH21  = 1
BEGIN
	--Comm Cost Company 1
	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,@AgentCommExpense,@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'PPH21','','D',abs(@ManagementFeeAmount), abs(@ManagementFeeAmount),0,1,abs(@ManagementFeeAmount),0,@UsersID,@TimeNow 
               
			            
	set @AutoNo = @AutoNo + 1   

	--Account Payables 2
	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,@AgentCommPayable,@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'PPH21','','C',abs(@ManagementFeeAmount - @PPH21Amount),0, abs(@ManagementFeeAmount - @PPH21Amount),1,0,abs(@ManagementFeeAmount - @PPH21Amount),@UsersID,@TimeNow 
               
	            
	
	set @AutoNo = @AutoNo + 1   

	--WHT Payables (PPh 21) 4
	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,@WHTPayablePPH21,@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'PPH21','','C',abs(@PPH21Amount), 0,abs(@PPH21Amount),1,0,abs(@PPH21Amount),@UsersID,@TimeNow 
   


END


ELSE --PPH21  = 0 and --PPH23  = 0
BEGIN
	--Comm Cost Company 1
	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,@AgentCommExpense,@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'Non PPH21 and PPH23','','D',abs(@ManagementFeeAmount), abs(@ManagementFeeAmount),0,1,abs(@ManagementFeeAmount),0,@UsersID,@TimeNow 
               
			            
	set @AutoNo = @AutoNo + 1   

	--Account Payables 2
	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,@AgentCommPayable,@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'Non PPH21 and PPH23','','C',abs(@ManagementFeeAmount),0, abs(@ManagementFeeAmount),1,0,abs(@ManagementFeeAmount),@UsersID,@TimeNow 
               


END
                
FETCH NEXT FROM A 
INTO @InstrumentPK,@AgentPK,@CurrencyPK,@BitPPH23,@BitPPH21,@BitPPN,@ManagementFeeAmount,@PPH21Amount,@PPH23Amount,@PPNAmount,@AmountAfterTax
END 
CLOSE A  
DEALLOCATE A

DECLARE @combinedString NVARCHAR(500)
SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Reference
FROM #ReferenceTemp
IF (@combinedString is null)
BEGIN
    SELECT 'Posting Error, Please Check DailyDataForCommissionAcc' as Result
END
ELSE
BEGIN
SELECT 'Posting Agent Commission Success ! Reference is : ' + @combinedString as Result
END

                        
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

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

        public string Validate_CheckAgentCSR(DateTime _date, string _param)
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
                        
                        create table #Temp 
                        (
                        Result nvarchar(MAX)
                        )

                        create table #A(InstrumentID nvarchar(50))

                        Declare @Description nvarchar(50)

                        select @Description = dbo.SpaceBeforeCap(@Param)

                        Create Table #Journal
                        (Reference nvarchar(50))
                        
                        Insert Into #Journal(Reference)
                        select Reference from Journal where ValueDate = @Date and status = 2 and Posted = 1 and Reversed = 0 and Description = @Description


       
	                    IF exists(select JournalPK from Journal where ValueDate = @Date and status = 2 and Posted = 1 and Description = @Description
                        )
	                    BEGIN
		                    Insert Into #Temp(Result)
		                    SELECT 'DONE' Result
	                    END
	                    ELSE IF NOT EXISTS(Select top 1 FundPK from AgentCSRDataForCommissionRpt where date between DATEADD(m, DATEDIFF(m, 0, @date), 0) and @date)
	                    BEGIN
		                    Insert Into #Temp(Result)
		                    SELECT 'Not Exists Agent CSR Data For Commission' as Result 
	                    END
	                    ELSE
	                    BEGIN
		                    Insert Into #Temp(Result)
		                    select 'Ready For Posting Agent CSR' Result
	                    END


	                    DECLARE @combinedString VARCHAR(MAX)
	                    SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Result
	                    FROM #Temp
	                    BEGIN
		                    SELECT @combinedString as Result 
	                    END 
                          ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@Param", _param);

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

        public string Posting_AgentCSR(DateTime _date, string _usersID)
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
Declare @InstrumentPK int
Declare @AgentPK int 
Declare @CurrencyPK int 

Declare @AgentCSRExpense int 
Declare @AgentCSRPayable int 
Declare @WHTPayablePPH23 int 


declare @DanaProgram numeric(22,6)
declare @PPH23 numeric(22,6)
declare @NetDanaProgram numeric(22,6)
declare @AgentCSRExpenseAccountPK int


declare @startDate datetime

select @startDate = DATEADD(m, DATEDIFF(m, 0, @date), 0)


create table #ReferenceTemp
(Reference nvarchar(50))

Declare @JourHeader int  
set @JourHeader = 0    
Declare @JournalPK int 
Declare @PeriodPK int 
Declare @Reference nvarchar(50)    
Select @PeriodPK = PeriodPK From Period Where DateFrom <= @Date and Dateto >= @Date and Status = 2  
Select @JournalPK = isnull(Max(JournalPK),0) from Journal   

insert into #ReferenceTemp (Reference)
exec getJournalReference @Date,'ADJ',@Reference out  
 
set @JournalPK = @JournalPK + 1  
 
update A set status = 3 from JournalDetail A
left join Journal B on A.JournalPK = B.JournalPK and B.status = 2 and B.Posted = 1 and B.Reversed = 0
where B.ValueDate = @Date and B.Description = 'Agent Corporate Social Responsibilities'

update Journal set posted = 0, status = 3, VoidUsersID = @UsersID,VoidTime = @TimeNow,LastUpdate=@TimeNow
where status = 2 and Posted = 1 and Reversed = 0 and ValueDate = @Date and Description = 'Agent Corporate Social Responsibilities'


INSERT INTO [Journal]  
([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate]  
,[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy]  
,[PostedTime],[EntryUsersID],[EntryTime]  
,[ApprovedUsersID],[ApprovedTime],[DBUserID],LastUpdate)     
SELECT @JournalPK,1,2,'Agent Corporate Social Responsibilities',@PeriodPK, @Date
,0,'',@Reference,1,'Agent Corporate Social Responsibilities',1,@UsersID
,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow 


Declare @AutoNo int 
set @AutoNo = 0   

DECLARE A CURSOR FOR 
Select C.InstrumentPK,A.AgentPK,C.CurrencyPK,DanaProgram,PPH23,NetDanaProgram,isnull(D.AgentCSRExpenseAccountPK,0)   from AgentCSRDataForCommissionRpt A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
left join Instrument C on B.ID = C.ID and C.status in (1,2)
left join AgentCSRFund D on A.FundPK = D.FundPK and D.status in (1,2)
and D.Date = (
select max(Date) from AgentCSRFund E where D.FundPK = E.FundPK and E.Date <= A.Date and E.status = 2
)
where A.date = @date


OPEN A 
FETCH NEXT FROM A 
INTO @InstrumentPK,@AgentPK,@CurrencyPK,@DanaProgram,@PPH23,@NetDanaProgram,@AgentCSRExpenseAccountPK
WHILE @@FETCH_STATUS = 0  
BEGIN 

 
Select @AgentCSRExpense = AgentCSRExpense,@AgentCSRPayable =  AgentCSRPayable,
@WHTPayablePPH23 = WHTPayablePPH23
From AccountingSetup Where Status = 2

    set @AutoNo = @AutoNo + 1   


	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,isnull(@AgentCSRExpenseAccountPK,@AgentCSRExpense),@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'Dana Program','','D',@DanaProgram, @DanaProgram,0,1,@DanaProgram,0,@UsersID,@TimeNow 
               
			           
	set @AutoNo = @AutoNo + 1   
	 

	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,@WHTPayablePPH23,@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'PPH 23','','C',abs(@PPH23), 0,abs(@PPH23),1,0,abs(@PPH23),@UsersID,@TimeNow 

	set @AutoNo = @AutoNo + 1   

	INSERT INTO [JournalDetail]  
	([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	,[BaseCredit],[LastUsersID],LastUpdate) 
	Select @JournalPK,@AutoNo,1,2,@AgentCSRPayable,@CurrencyPK,0,0,@AgentPK,0,@InstrumentPK,0,'Net Dana Program','','C',@NetDanaProgram,0,@NetDanaProgram,1,0,@NetDanaProgram,@UsersID,@TimeNow 

                
FETCH NEXT FROM A 
INTO @InstrumentPK,@AgentPK,@CurrencyPK,@DanaProgram,@PPH23,@NetDanaProgram,@AgentCSRExpenseAccountPK
END 
CLOSE A  
DEALLOCATE A

DECLARE @combinedString NVARCHAR(500)
SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Reference
FROM #ReferenceTemp
IF (@combinedString is null)
BEGIN
    SELECT 'Posting Error, Please Check Agent CSR Data For Commission' as Result
END
ELSE
BEGIN
SELECT 'Posting Agent CSR Success ! Reference is : ' + @combinedString as Result
END
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

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