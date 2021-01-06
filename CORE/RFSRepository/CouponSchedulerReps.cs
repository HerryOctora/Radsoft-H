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
    public class CouponSchedulerReps
    {
        Host _host = new Host();
       
        private CouponScheduler setCouponScheduler(SqlDataReader dr)
        {
            CouponScheduler M_CouponScheduler = new CouponScheduler();
            M_CouponScheduler.AutoNo = Convert.ToInt32(dr["AutoNo"]);
            M_CouponScheduler.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_CouponScheduler.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_CouponScheduler.InstrumentName = Convert.ToString(dr["InstrumentName"]);
            M_CouponScheduler.CouponFromDate = Convert.ToString(dr["CouponFromDate"]);
            M_CouponScheduler.CouponToDate = Convert.ToString(dr["CouponToDate"]);
            M_CouponScheduler.CouponRate = Convert.ToDecimal(dr["CouponRate"]);
            M_CouponScheduler.CouponDays = Convert.ToInt32(dr["CouponDays"]);
            M_CouponScheduler.Description = dr["Description"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Description"]);
            M_CouponScheduler.UsersID = dr["UsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UsersID"]);
            M_CouponScheduler.LastUpdate = dr["LastUpdate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdate"]);
            return M_CouponScheduler;
        }

        public List<CouponScheduler> CouponScheduler_Select(int _instrumentPK, string _usersID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CouponScheduler> L_CouponScheduler = new List<CouponScheduler>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                --declare
                                --	@InstrumentPK	int,
                                --	@UsersID		nvarchar(50),
                                --	@LastUpdate		datetime

                                --select
                                --	@InstrumentPK	= 32,
                                --	@UsersID		= 'AMINK',
                                --	@LastUpdate		= '12/13/17'

                                if exists (select * from Instrument where InstrumentPK = @InstrumentPK and [Status] = 2)
                                begin
	                                declare @CouponFromDate			date,
			                                @CouponToDate			date,
			                                @CouponRate				numeric(8,4),
			                                @CouponFrequently		int,
			                                @endDate				date,
			                                @startDate				date,
			                                @MaxCouponSchedulerPK	int,
			                                @BasisType				int

	                                select
		                                @CouponFrequently	= InterestPaymentType,
		                                @CouponFromDate		= FirstCouponDate,
		                                @CouponToDate		= MaturityDate,
		                                @CouponRate			= InterestPercent,
		                                @BasisType			= isnull(InterestDaysType, 0)
	                                from Instrument
	                                where InstrumentPK = @InstrumentPK and [Status] = 2

	                                if object_id('tempdb..#tempScheduler', 'u') is not null drop table #tempScheduler                       
	                                create table #tempScheduler
	                                (
		                                [AutoNo] [int] NOT NULL identity,
		                                [InstrumentPK] [int] NOT NULL,
		                                [CouponFromDate] [date] NOT NULL,
		                                [CouponToDate] [date] NOT NULL,
		                                [CouponRate] [decimal](12, 6) NOT NULL,
		                                [CouponDays] [int] NULL,
		                                [Description] [nvarchar](1000) NULL,
		                                [UsersID] [nvarchar](70) NOT NULL,
		                                [LastUpdate] [datetime] NOT NULL
	                                )
	
	                                while @CouponFromDate < @CouponToDate
	                                begin
		                                if @CouponFrequently in (7,8,9) --> Monthly / Per 1 Bulan Sekali
		                                begin
			                                set @endDate = dateadd(month, 1, @CouponFromDate)
			                                set @endDate = case when @endDate > @CouponToDate then @CouponToDate else @endDate end

			                                insert into #tempScheduler (
				                                InstrumentPK, CouponFromDate, CouponToDate, 
				                                CouponRate, CouponDays, [Description],
				                                UsersID, LastUpdate
			                                )
			                                select
				                                @InstrumentPK as InstrumentPK, @CouponFromDate as CouponFromDate, @endDate as CouponToDate, @CouponRate as CouponRate,
				                                case when @BasisType in (1,5,6,7,8,9) then [dbo].[FGetDateDIffCorporateBond] (@CouponFromDate, @endDate) else abs(datediff(day, @endDate, @CouponFromDate)) end as CouponDays,
				                                'Generate Monthly Coupon Scheduler' as [Description],
				                                @UsersID as UsersID, @LastUpdate as LastUpdate
			
		                                end

		                                if @CouponFrequently in (10,11,12,13,14,15) --> Quarterly / Per 3 Bulan Sekali
		                                begin
			                                set @endDate = dateadd(month, 3, @CouponFromDate)
			                                set @endDate = case when @endDate > @CouponToDate then @CouponToDate else @endDate end
			
			                                insert into #tempScheduler (
				                                InstrumentPK, CouponFromDate, CouponToDate, 
				                                CouponRate, CouponDays, [Description],
				                                UsersID, LastUpdate
			                                )
			                                select
				                                @InstrumentPK as InstrumentPK, @CouponFromDate as CouponFromDate, @endDate as CouponToDate, @CouponRate as CouponRate,
				                                case when @BasisType in (1,5,6,7,8,9) then [dbo].[FGetDateDIffCorporateBond] (@CouponFromDate, @endDate) else abs(datediff(day, @endDate, @CouponFromDate)) end as CouponDays,
				                                'Generate Quarterly Coupon Scheduler' as [Description],
				                                @UsersID as UsersID, @LastUpdate as LastUpdate
		                                end

		                                if @CouponFrequently in (16,17,18) --> Semi Annual / Per 6 Bulan Sekali
		                                begin
			                                set @endDate = dateadd(month, 6, @CouponFromDate)
			                                set @endDate = case when @endDate > @CouponToDate then @CouponToDate else @endDate end

			                                insert into #tempScheduler (
				                                InstrumentPK, CouponFromDate, CouponToDate, 
				                                CouponRate, CouponDays, [Description],
				                                UsersID, LastUpdate
			                                )
			                                select
				                                @InstrumentPK as InstrumentPK, @CouponFromDate as CouponFromDate, @endDate as CouponToDate, @CouponRate as CouponRate,
				                                case when @BasisType in (1,5,6,7,8,9) then [dbo].[FGetDateDIffCorporateBond] (@CouponFromDate, @endDate) else abs(datediff(day, @endDate, @CouponFromDate)) end as CouponDays,
				                                'Generate Semi Annual Coupon Scheduler' as [Description],
				                                @UsersID as UsersID, @LastUpdate as LastUpdate
		                                end

		                                if @CouponFrequently  in (19,20,21) --> Yearly / Per 1 Tahun Sekali
		                                begin
			                                set @endDate = dateadd(year, 1, @CouponFromDate)
			                                set @endDate = case when @endDate > @CouponToDate then @CouponToDate else @endDate end

			                                insert into #tempScheduler (
				                                InstrumentPK, CouponFromDate, CouponToDate, 
				                                CouponRate, CouponDays, [Description],
				                                UsersID, LastUpdate
			                                )
			                                select
				                                @InstrumentPK as InstrumentPK, @CouponFromDate as CouponFromDate, @endDate as CouponToDate, @CouponRate as CouponRate,
				                                case when @BasisType in (1,5,6,7,8,9) then [dbo].[FGetDateDIffCorporateBond] (@CouponFromDate, @endDate) else abs(datediff(day, @endDate, @CouponFromDate)) end as CouponDays,
				                                'Generate Annual Coupon Scheduler' as [Description],
				                                @UsersID as UsersID, @LastUpdate as LastUpdate
		                                end
		
		                                set @CouponFromDate = @endDate
	                                end

	                                -- Return
	                                select
		                                a.AutoNo, a.InstrumentPK, b.ID as InstrumentID, b.Name as InstrumentName,
		                                a.CouponFromDate, a.CouponToDate, a.CouponRate, a.CouponDays, a.[Description], a.UsersID, a.LastUpdate
	                                from #tempScheduler a
		                                left join Instrument b on a.InstrumentPK = b.InstrumentPK and b.[Status] = 2
	                                order by a.AutoNo asc
                                end
                            ";
                        cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CouponScheduler.Add(setCouponScheduler(dr));
                                }
                            }
                            return L_CouponScheduler;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<CouponScheduler> CouponScheduler_SelectByMaturityDate(int _instrumentPK, DateTime _valueDate, string _usersID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CouponScheduler> L_CouponScheduler = new List<CouponScheduler>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                --declare
                                --	@InstrumentPK	int,
                                --	@ValueDate		date,
                                --	@UsersID		nvarchar(50),
                                --	@LastUpdate		datetime

                                --select
                                --	@InstrumentPK	= 26,
                                --	@ValueDate		= '12/13/17',
                                --	@UsersID		= 'AMINK',
                                --	@LastUpdate		= '12/13/17'

                                if exists (select * from Instrument where InstrumentPK = @InstrumentPK and [Status] = 2)
                                begin
	                                declare @CouponFromDate			date,
			                                @CouponToDate			date,
			                                @CouponRate				numeric(8,4),
			                                @CouponFrequently		int,
			                                @endDate				date,
			                                @startDate				date,
			                                @MaxCouponSchedulerPK	int,
			                                @BasisType				int

	                                select
		                                @CouponFrequently	= InterestPaymentType,
		                                @CouponFromDate		= @ValueDate,
		                                @CouponToDate		= MaturityDate,
		                                @CouponRate			= InterestPercent,
		                                @BasisType			= isnull(InterestDaysType, 0)
	                                from Instrument
	                                where InstrumentPK = @InstrumentPK and [Status] = 2

	                                if object_id('tempdb..#tempScheduler', 'u') is not null drop table #tempScheduler                       
	                                create table #tempScheduler
	                                (
		                                [AutoNo] [int] NOT NULL identity,
		                                [InstrumentPK] [int] NOT NULL,
		                                [CouponFromDate] [date] NOT NULL,
		                                [CouponToDate] [date] NOT NULL,
		                                [CouponRate] [decimal](12, 6) NOT NULL,
		                                [CouponDays] [int] NULL,
		                                [Description] [nvarchar](1000) NULL,
		                                [UsersID] [nvarchar](70) NOT NULL,
		                                [LastUpdate] [datetime] NOT NULL
	                                )
	
	                                while @CouponFromDate < @CouponToDate
	                                begin
		                                if @CouponFrequently in (7,8,9) --> Monthly / Per 1 Bulan Sekali
		                                begin
			                                set @startDate = dateadd(month, -1, @CouponToDate)
			                                set @startDate = case when @startDate < @CouponFromDate then @CouponFromDate else @startDate end
			                                set @endDate = @CouponToDate

			                                insert into #tempScheduler (
				                                InstrumentPK, CouponFromDate, CouponToDate, 
				                                CouponRate, CouponDays, [Description],
				                                UsersID, LastUpdate
			                                )
			                                select
				                                @InstrumentPK as InstrumentPK, @startDate as CouponFromDate, @endDate as CouponToDate, @CouponRate as CouponRate, 
				                                case when @BasisType in (1,5,6,7,8,9) then [dbo].[FGetDateDIffCorporateBond] (@startDate, @endDate) else abs(datediff(day, @startDate, @endDate)) end as CouponDays,
				                                'Generate Monthly Coupon Scheduler' as [Description],
				                                @UsersID as UsersID, @LastUpdate as LastUpdate
			
		                                end

		                                if @CouponFrequently in (10,11,12,13,14,15) --> Quarterly / Per 3 Bulan Sekali
		                                begin
			                                set @startDate = dateadd(month, -3, @CouponToDate)
			                                set @startDate = case when @startDate < @CouponFromDate then @CouponFromDate else @startDate end
			                                set @endDate = @CouponToDate
			
			                                insert into #tempScheduler (
				                                InstrumentPK, CouponFromDate, CouponToDate, 
				                                CouponRate, CouponDays, [Description],
				                                UsersID, LastUpdate
			                                )
			                                select
				                                @InstrumentPK as InstrumentPK, @startDate as CouponFromDate, @endDate as CouponToDate, @CouponRate as CouponRate,
				                                case when @BasisType in (1,5,6,7,8,9) then [dbo].[FGetDateDIffCorporateBond] (@startDate, @endDate) else abs(datediff(day, @startDate, @endDate)) end as CouponDays,
				                                'Generate Quarterly Coupon Scheduler' as [Description],
				                                @UsersID as UsersID, @LastUpdate as LastUpdate
		                                end

		                                if @CouponFrequently in (16,17,18) --> Semi Annual / Per 6 Bulan Sekali
		                                begin
			                                set @startDate = dateadd(month, -6, @CouponToDate)
			                                set @startDate = case when @startDate < @CouponFromDate then @CouponFromDate else @startDate end
			                                set @endDate = @CouponToDate

			                                insert into #tempScheduler (
				                                InstrumentPK, CouponFromDate, CouponToDate, 
				                                CouponRate, CouponDays, [Description],
				                                UsersID, LastUpdate
			                                )
			                                select
				                                @InstrumentPK as InstrumentPK, @startDate as CouponFromDate, @endDate as CouponToDate, @CouponRate as CouponRate, 
				                                case when @BasisType in (1,5,6,7,8,9) then [dbo].[FGetDateDIffCorporateBond] (@startDate, @endDate) else abs(datediff(day, @startDate, @endDate)) end as CouponDays,
				                                'Generate Semi Annual Coupon Scheduler' as [Description],
				                                @UsersID as UsersID, @LastUpdate as LastUpdate
		                                end

		                                if @CouponFrequently  in (19,20,21) --> Yearly / Per 1 Tahun Sekali
		                                begin
			                                set @startDate = dateadd(year, -1, @CouponToDate)
			                                set @startDate = case when @startDate < @CouponFromDate then @CouponFromDate else @startDate end
			                                set @endDate = @CouponToDate

			                                insert into #tempScheduler (
				                                InstrumentPK, CouponFromDate, CouponToDate, 
				                                CouponRate, CouponDays, [Description],
				                                UsersID, LastUpdate
			                                )
			                                select
				                                @InstrumentPK as InstrumentPK, @startDate as CouponFromDate, @endDate as CouponToDate, @CouponRate as CouponRate,
				                                case when @BasisType in (1,5,6,7,8,9) then [dbo].[FGetDateDIffCorporateBond] (@startDate, @endDate) else abs(datediff(day, @startDate, @endDate)) end as CouponDays,
				                                'Generate Yearly Coupon Scheduler' as [Description],
				                                @UsersID as UsersID, @LastUpdate as LastUpdate
		                                end
		
		                                set @CouponToDate = @startDate
	                                end

	                                -- Return
	                                select
		                                a.AutoNo, a.InstrumentPK, b.ID as InstrumentID, b.Name as InstrumentName,
		                                a.CouponFromDate, a.CouponToDate, a.CouponRate, a.CouponDays, a.[Description], a.UsersID, a.LastUpdate
	                                from #tempScheduler a
		                                left join Instrument b on a.InstrumentPK = b.InstrumentPK and b.[Status] = 2
	                                order by a.AutoNo desc
                                end
                            ";
                        cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CouponScheduler.Add(setCouponScheduler(dr));
                                }
                            }
                            return L_CouponScheduler;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public bool CouponScheduler_CheckExistingFirstCouponDate(int _instrumentPK)
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
                                if exists (select * from Instrument where InstrumentPK = @InstrumentPK and [Status] = 2)
                                begin
                                    declare @FirstCouponDate date

                                    select @FirstCouponDate = isnull(FirstCouponDate, '') 
                                    from Instrument 
                                    where InstrumentPK = @InstrumentPK and [Status] = 2

                                    if isnull(@FirstCouponDate, '') <> '' or @FirstCouponDate is not null
                                    begin
                                        select 1 as BitMsg
                                    end
                                    else
                                    begin
                                        select 0 as BitMsg
                                    end
                                end
                            ";
                        cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return false;
                            }
                            else
                            {
                                return Convert.ToBoolean(dr["BitMsg"]);
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