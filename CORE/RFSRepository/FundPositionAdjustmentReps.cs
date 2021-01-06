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
    public class FundPositionAdjustmentReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundPositionAdjustment] " +
                            "([FundPositionAdjustmentPK],[HistoryPK],[Status],[FundPK],[InstrumentPK],[Date],[Balance],[Price],[Description],[AcqDate],[PeriodPK],";

        string _paramaterCommand = "@FundPK,@InstrumentPK,@Date,@Balance,@Price,@Description,@AcqDate,@PeriodPK,";

        //2
        private FundPositionAdjustment setFundPositionAdjustment(SqlDataReader dr)
        {
            FundPositionAdjustment M_FundPositionAdjustment = new FundPositionAdjustment();
            M_FundPositionAdjustment.FundPositionAdjustmentPK = Convert.ToInt32(dr["FundPositionAdjustmentPK"]);
            M_FundPositionAdjustment.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundPositionAdjustment.Status = Convert.ToInt32(dr["Status"]);
            M_FundPositionAdjustment.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundPositionAdjustment.Notes = Convert.ToString(dr["Notes"]);
            M_FundPositionAdjustment.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundPositionAdjustment.FundID = Convert.ToString(dr["FundID"]);
            M_FundPositionAdjustment.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_FundPositionAdjustment.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_FundPositionAdjustment.Date = Convert.ToString(dr["Date"]);
            M_FundPositionAdjustment.AcqDate = Convert.ToString(dr["AcqDate"]);
            M_FundPositionAdjustment.Balance = Convert.ToDecimal(dr["Balance"]);
            M_FundPositionAdjustment.Price = Convert.ToDecimal(dr["Price"]);
            M_FundPositionAdjustment.Description = Convert.ToString(dr["Description"]);
            M_FundPositionAdjustment.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_FundPositionAdjustment.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_FundPositionAdjustment.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundPositionAdjustment.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundPositionAdjustment.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundPositionAdjustment.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundPositionAdjustment.EntryTime = dr["EntryTime"].ToString();
            M_FundPositionAdjustment.UpdateTime = dr["UpdateTime"].ToString();
            M_FundPositionAdjustment.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundPositionAdjustment.VoidTime = dr["VoidTime"].ToString();
            M_FundPositionAdjustment.DBUserID = dr["DBUserID"].ToString();
            M_FundPositionAdjustment.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundPositionAdjustment.LastUpdate = dr["LastUpdate"].ToString();
            M_FundPositionAdjustment.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FundPositionAdjustment;
        }

        public List<FundPositionAdjustment> FundPositionAdjustment_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundPositionAdjustment> L_FundPositionAdjustment = new List<FundPositionAdjustment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"

                                Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                B.ID FundID,C.ID InstrumentID,D.ID PeriodID,* from FundPositionAdjustment A
                                left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                                left join Period D on A.PeriodPK = D.PeriodPK and D.status in (1,2)                            
                                where A.status = @status 
                                                     
                               ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"
                                Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                B.ID FundID,C.ID InstrumentID,D.ID PeriodID,* from FundPositionAdjustment A
                                left join Fund B on A.FundPK = B.FundPK and B.status in (1,2) 
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                                left join Period D on A.PeriodPK = D.PeriodPK and D.status in (1,2)  
                        ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundPositionAdjustment.Add(setFundPositionAdjustment(dr));
                                }
                            }
                            return L_FundPositionAdjustment;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundPositionAdjustment_Add(FundPositionAdjustment _FundPositionAdjustment, bool _havePrivillege)
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
                                 "Select isnull(max(FundPositionAdjustmentPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from FundPositionAdjustment";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundPositionAdjustment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(FundPositionAdjustmentPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from FundPositionAdjustment";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@FundPK", _FundPositionAdjustment.FundPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _FundPositionAdjustment.InstrumentPK);
                        cmd.Parameters.AddWithValue("@Date", _FundPositionAdjustment.Date);
                        cmd.Parameters.AddWithValue("@AcqDate", _FundPositionAdjustment.AcqDate);
                        cmd.Parameters.AddWithValue("@Balance", _FundPositionAdjustment.Balance);
                        cmd.Parameters.AddWithValue("@Price", _FundPositionAdjustment.Price);
                        cmd.Parameters.AddWithValue("@Description", _FundPositionAdjustment.Description);
                        cmd.Parameters.AddWithValue("@PeriodPK", _FundPositionAdjustment.PeriodPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundPositionAdjustment.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FundPositionAdjustment");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundPositionAdjustment_Update(FundPositionAdjustment _FundPositionAdjustment, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_FundPositionAdjustment.FundPositionAdjustmentPK, _FundPositionAdjustment.HistoryPK, "FundPositionAdjustment");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"if (@ClientCode = '03' or @ClientCode = '20' or @ClientCode = '21' or @ClientCode = '22') and (select status from FundPositionAdjustment where FundPositionAdjustmentPK = @PK and historyPK = @HistoryPK) = 2
                                            begin
                                                declare @InstrumentPK1 int
                                                declare @DoneVolume numeric(22,4)
                                                declare @Counter int
                                                declare @maxFifoDate date
                                                declare @Volume numeric(22,4)
                                                declare @FundPK1 int
                                                declare @RemainingVolume numeric(22,4)
                                                declare @AcqPriceInv numeric(22,4)
                                                declare @AcqVolumeInv numeric(22,4)
                                                declare @AcqDateInv date
							
								
	                                                if object_id('tempdb..#tableFifoSelect', 'u') is not null drop table #tableFifoSelect 
	                                                create table #tableFifoSelect
	                                                (
		                                                FifoBondPositionPK int,
		                                                FundPositionAdjustmentPK int,
		                                                AcqDate date,
		                                                AcqVolume numeric(22,4),
		                                                RemainingVolume numeric(22,4),
		                                                AcqPrice numeric(22,4)
	                                                )
	                                                CREATE CLUSTERED INDEX indx_tableFifoSelectn ON #tableFifoSelect (FifoBondPositionPK,AcqDate);


	                                                if object_id('tempdb..#tableInvest', 'u') is not null drop table #tableInvest 
	                                                create table #tableInvest
	                                                (
		                                                FifoBondPositionPK int,
		                                                InvesmentBuyPK int,
		                                                InvesmentSellPK int,
		                                                AcqDate date,
		                                                AcqVolume numeric(22,4),
		                                                AcqPrice numeric(22,4),
		                                                RemainingVolume numeric(22,4)
	                                                )
	                                                CREATE CLUSTERED INDEX indx_tableInvest ON #tableInvest (FifoBondPositionPK,InvesmentBuyPK);

	                                                select @InstrumentPK1 = InstrumentPK, @FundPK1 = FundPK, @volume = Balance, @DoneVolume = Balance,@AcqDateInv = AcqDate, @AcqPriceInv = Price, @AcqVolumeInv = Balance from FundPositionAdjustment where FundPositionAdjustmentPK = @PK and Status= 2 
	                                                insert into #tableFifoSelect(FifoBondPositionPK,FundPositionAdjustmentPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume)
	                                                select A.FifoBondPositionPK,A.FundPositionAdjustmentPK, A.AcqDate, A.DoneVolume,A.AcqPrice,RemainingVolume from (
			                                                select FifoBondPositionPK,FundPositionAdjustmentPK,AcqDate,AcqVolume DoneVolume,AcqPrice,RemainingVolume from FifoBondPosition 
			                                                where FundPK = @FundPK1 and InstrumentPk = @InstrumentPK1 and status in (1,2) and FundPositionAdjustmentPK = @PK
			                                                union all
			                                                select 0,FundPositionAdjustmentPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume from FifoBondPositionTemp
			                                                where FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 and FundPositionAdjustmentPK = @PK
	                                                )A 
	                                                order by A.AcqDate					

	                                                select * from #tableFifoSelect

	                                                set @Counter = 0

	                                                declare @FifobondPositionPK int
	                                                declare @Query nvarchar(500)
	                                                declare @InvestmentSellPK int
	                                                declare @AcqPrice numeric(22,4)
	                                                declare @AcqVolume numeric(22,4)
	                                                declare @AcqDate1 date


	                                                DECLARE AB CURSOR FOR   
		                                                select FifoBondPositionPK,FundPositionAdjustmentPK,AcqVolume,AcqPrice,AcqDate,RemainingVolume from #tableFifoSelect where FundPositionAdjustmentPK = @PK order by AcqDate desc
	                                                OPEN AB  
	                                                FETCH NEXT FROM AB INTO @FifoBondPositionPK,@PK,@AcqVolume,@AcqPrice,@AcqDate1,@RemainingVolume
  
	                                                WHILE @@FETCH_STATUS = 0  
	                                                BEGIN  

		                                                if (@AcqDate1 = @AcqDateInv and @AcqPrice = @AcqPriceInv)
			                                                if @FifobondPositionPK != 0
				                                                --select * from FifoBondPosition where fifobondpositionpk = @FifoBondPositionPK and FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1
				                                                update FifoBondPosition set RemainingVolume = @RemainingVolume - @AcqVolumeInv,FundPositionAdjustmentPK = 0 where fifobondpositionpk = @FifoBondPositionPK and FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 
			                                                else
				                                                --select * from FifoBondPositionTemp where FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 
				                                                update FifoBondPositionTemp set RemainingVolume = @RemainingVolume - @AcqVolumeInv,FundPositionAdjustmentPK = 0 where FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 
		                                                --else
		                                                --	update FifoBondPosition set RemainingVolume = @AcqVolume,FundPositionAdjustmentPK = 0 where FundPositionAdjustmentPK = @PK and AcqDate = @AcqDate1 and AcqPrice = @AcqPrice and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1
		                            
		
	                                                    FETCH NEXT FROM AB INTO @FifoBondPositionPK,@InvestmentSellPK,@AcqVolume,@AcqPrice,@AcqDate1,@RemainingVolume
	                                                end
	                                                CLOSE AB  
	                                                DEALLOCATE AB
                                            end
                                            

                                    Update FundPositionAdjustment set status=2,Notes=@Notes,FundPK=@FundPK,InstrumentPK=@InstrumentPK,Date=@Date,Balance=@Balance,Price=@Price,Description=@Description,AcqDate=@AcqDate,PeriodPK=@PeriodPK,
                                    ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate
                                    where FundPositionAdjustmentPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _FundPositionAdjustment.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundPositionAdjustment.FundPositionAdjustmentPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundPositionAdjustment.Notes);
                            cmd.Parameters.AddWithValue("@FundPK", _FundPositionAdjustment.FundPK);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _FundPositionAdjustment.InstrumentPK);
                            cmd.Parameters.AddWithValue("@Date", _FundPositionAdjustment.Date);
                            cmd.Parameters.AddWithValue("@AcqDate", _FundPositionAdjustment.AcqDate);
                            cmd.Parameters.AddWithValue("@Balance", _FundPositionAdjustment.Balance);
                            cmd.Parameters.AddWithValue("@Price", _FundPositionAdjustment.Price);
                            cmd.Parameters.AddWithValue("@Description", _FundPositionAdjustment.Description);
                            cmd.Parameters.AddWithValue("@PeriodPK", _FundPositionAdjustment.PeriodPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundPositionAdjustment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundPositionAdjustment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundPositionAdjustment set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundPositionAdjustmentPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundPositionAdjustment.FundPositionAdjustmentPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundPositionAdjustment.EntryUsersID);
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
                                cmd.CommandText = @"if (@ClientCode = '03' or @ClientCode = '20' or @ClientCode = '21' or @ClientCode = '22') and (select status from FundPositionAdjustment where FundPositionAdjustmentPK = @PK and historyPK = @HistoryPK) = 2
                                            begin
                                                declare @InstrumentPK1 int
                                                declare @DoneVolume numeric(22,4)
                                                declare @Counter int
                                                declare @maxFifoDate date
                                                declare @Volume numeric(22,4)
                                                declare @FundPK1 int
                                                declare @RemainingVolume numeric(22,4)
                                                declare @AcqPriceInv numeric(22,4)
                                                declare @AcqVolumeInv numeric(22,4)
                                                declare @AcqDateInv date
							
								
	                                                if object_id('tempdb..#tableFifoSelect', 'u') is not null drop table #tableFifoSelect 
	                                                create table #tableFifoSelect
	                                                (
		                                                FifoBondPositionPK int,
		                                                FundPositionAdjustmentPK int,
		                                                AcqDate date,
		                                                AcqVolume numeric(22,4),
		                                                RemainingVolume numeric(22,4),
		                                                AcqPrice numeric(22,4)
	                                                )
	                                                CREATE CLUSTERED INDEX indx_tableFifoSelectn ON #tableFifoSelect (FifoBondPositionPK,AcqDate);


	                                                if object_id('tempdb..#tableInvest', 'u') is not null drop table #tableInvest 
	                                                create table #tableInvest
	                                                (
		                                                FifoBondPositionPK int,
		                                                InvesmentBuyPK int,
		                                                InvesmentSellPK int,
		                                                AcqDate date,
		                                                AcqVolume numeric(22,4),
		                                                AcqPrice numeric(22,4),
		                                                RemainingVolume numeric(22,4)
	                                                )
	                                                CREATE CLUSTERED INDEX indx_tableInvest ON #tableInvest (FifoBondPositionPK,InvesmentBuyPK);

	                                                select @InstrumentPK1 = InstrumentPK, @FundPK1 = FundPK, @volume = Balance, @DoneVolume = Balance,@AcqDateInv = AcqDate, @AcqPriceInv = Price, @AcqVolumeInv = Balance from FundPositionAdjustment where FundPositionAdjustmentPK = @PK and Status= 2 
	                                                insert into #tableFifoSelect(FifoBondPositionPK,FundPositionAdjustmentPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume)
	                                                select A.FifoBondPositionPK,A.FundPositionAdjustmentPK, A.AcqDate, A.DoneVolume,A.AcqPrice,RemainingVolume from (
			                                                select FifoBondPositionPK,FundPositionAdjustmentPK,AcqDate,AcqVolume DoneVolume,AcqPrice,RemainingVolume from FifoBondPosition 
			                                                where FundPK = @FundPK1 and InstrumentPk = @InstrumentPK1 and status in (1,2) and FundPositionAdjustmentPK = @PK
			                                                union all
			                                                select 0,FundPositionAdjustmentPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume from FifoBondPositionTemp
			                                                where FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 and FundPositionAdjustmentPK = @PK
	                                                )A 
	                                                order by A.AcqDate					

	                                                select * from #tableFifoSelect

	                                                set @Counter = 0

	                                                declare @FifobondPositionPK int
	                                                declare @Query nvarchar(500)
	                                                declare @InvestmentSellPK int
	                                                declare @AcqPrice numeric(22,4)
	                                                declare @AcqVolume numeric(22,4)
	                                                declare @AcqDate1 date


	                                                DECLARE AB CURSOR FOR   
		                                                select FifoBondPositionPK,FundPositionAdjustmentPK,AcqVolume,AcqPrice,AcqDate,RemainingVolume from #tableFifoSelect where FundPositionAdjustmentPK = @PK order by AcqDate desc
	                                                OPEN AB  
	                                                FETCH NEXT FROM AB INTO @FifoBondPositionPK,@PK,@AcqVolume,@AcqPrice,@AcqDate1,@RemainingVolume
  
	                                                WHILE @@FETCH_STATUS = 0  
	                                                BEGIN  

		                                                if (@AcqDate1 = @AcqDateInv and @AcqPrice = @AcqPriceInv)
			                                                if @FifobondPositionPK != 0
				                                                --select * from FifoBondPosition where fifobondpositionpk = @FifoBondPositionPK and FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1
				                                                update FifoBondPosition set RemainingVolume = @RemainingVolume - @AcqVolumeInv,FundPositionAdjustmentPK = 0 where fifobondpositionpk = @FifoBondPositionPK and FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 
			                                                else
				                                                --select * from FifoBondPositionTemp where FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 
				                                                update FifoBondPositionTemp set RemainingVolume = @RemainingVolume - @AcqVolumeInv,FundPositionAdjustmentPK = 0 where FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 
		                                                --else
		                                                --	update FifoBondPosition set RemainingVolume = @AcqVolume,FundPositionAdjustmentPK = 0 where FundPositionAdjustmentPK = @PK and AcqDate = @AcqDate1 and AcqPrice = @AcqPrice and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1
		                            
		
	                                                    FETCH NEXT FROM AB INTO @FifoBondPositionPK,@InvestmentSellPK,@AcqVolume,@AcqPrice,@AcqDate1,@RemainingVolume
	                                                end
	                                                CLOSE AB  
	                                                DEALLOCATE AB
                                            end

                                    Update FundPositionAdjustment set Notes=@Notes,FundPK=@FundPK,InstrumentPK=@InstrumentPK,Date=@Date,Balance=@Balance,Price=@Price,Description=@Description,AcqDate=@AcqDate,PeriodPK=@PeriodPK,
                                    UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate 
                                    where FundPositionAdjustmentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundPositionAdjustment.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundPositionAdjustment.FundPositionAdjustmentPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundPositionAdjustment.Notes);
                                cmd.Parameters.AddWithValue("@FundPK", _FundPositionAdjustment.FundPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _FundPositionAdjustment.InstrumentPK);
                                cmd.Parameters.AddWithValue("@Date", _FundPositionAdjustment.Date);
                                cmd.Parameters.AddWithValue("@AcqDate", _FundPositionAdjustment.AcqDate);
                                cmd.Parameters.AddWithValue("@Balance", _FundPositionAdjustment.Balance);
                                cmd.Parameters.AddWithValue("@Price", _FundPositionAdjustment.Price);
                                cmd.Parameters.AddWithValue("@Description", _FundPositionAdjustment.Description);
                                cmd.Parameters.AddWithValue("@PeriodPK", _FundPositionAdjustment.PeriodPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundPositionAdjustment.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_FundPositionAdjustment.FundPositionAdjustmentPK, "FundPositionAdjustment");
                                cmd.CommandText = @"if (@ClientCode = '03' or @ClientCode = '20' or @ClientCode = '21' or @ClientCode = '22') and (select status from FundPositionAdjustment where FundPositionAdjustmentPK = @PK and historyPK = @HistoryPK) = 2
                                            begin
                                                declare @InstrumentPK1 int
                                                declare @DoneVolume numeric(22,4)
                                                declare @Counter int
                                                declare @maxFifoDate date
                                                declare @Volume numeric(22,4)
                                                declare @FundPK1 int
                                                declare @RemainingVolume numeric(22,4)
                                                declare @AcqPriceInv numeric(22,4)
                                                declare @AcqVolumeInv numeric(22,4)
                                                declare @AcqDateInv date
							
								
	                                                if object_id('tempdb..#tableFifoSelect', 'u') is not null drop table #tableFifoSelect 
	                                                create table #tableFifoSelect
	                                                (
		                                                FifoBondPositionPK int,
		                                                FundPositionAdjustmentPK int,
		                                                AcqDate date,
		                                                AcqVolume numeric(22,4),
		                                                RemainingVolume numeric(22,4),
		                                                AcqPrice numeric(22,4)
	                                                )
	                                                CREATE CLUSTERED INDEX indx_tableFifoSelectn ON #tableFifoSelect (FifoBondPositionPK,AcqDate);


	                                                if object_id('tempdb..#tableInvest', 'u') is not null drop table #tableInvest 
	                                                create table #tableInvest
	                                                (
		                                                FifoBondPositionPK int,
		                                                InvesmentBuyPK int,
		                                                InvesmentSellPK int,
		                                                AcqDate date,
		                                                AcqVolume numeric(22,4),
		                                                AcqPrice numeric(22,4),
		                                                RemainingVolume numeric(22,4)
	                                                )
	                                                CREATE CLUSTERED INDEX indx_tableInvest ON #tableInvest (FifoBondPositionPK,InvesmentBuyPK);

	                                                select @InstrumentPK1 = InstrumentPK, @FundPK1 = FundPK, @volume = Balance, @DoneVolume = Balance,@AcqDateInv = AcqDate, @AcqPriceInv = Price, @AcqVolumeInv = Balance from FundPositionAdjustment where FundPositionAdjustmentPK = @PK and Status= 2 
	                                                insert into #tableFifoSelect(FifoBondPositionPK,FundPositionAdjustmentPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume)
	                                                select A.FifoBondPositionPK,A.FundPositionAdjustmentPK, A.AcqDate, A.DoneVolume,A.AcqPrice,RemainingVolume from (
			                                                select FifoBondPositionPK,FundPositionAdjustmentPK,AcqDate,AcqVolume DoneVolume,AcqPrice,RemainingVolume from FifoBondPosition 
			                                                where FundPK = @FundPK1 and InstrumentPk = @InstrumentPK1 and status in (1,2) and FundPositionAdjustmentPK = @PK
			                                                union all
			                                                select 0,FundPositionAdjustmentPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume from FifoBondPositionTemp
			                                                where FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 and FundPositionAdjustmentPK = @PK
	                                                )A 
	                                                order by A.AcqDate					

	                                                select * from #tableFifoSelect

	                                                set @Counter = 0

	                                                declare @FifobondPositionPK int
	                                                declare @Query nvarchar(500)
	                                                declare @InvestmentSellPK int
	                                                declare @AcqPrice numeric(22,4)
	                                                declare @AcqVolume numeric(22,4)
	                                                declare @AcqDate1 date


	                                                DECLARE AB CURSOR FOR   
		                                                select FifoBondPositionPK,FundPositionAdjustmentPK,AcqVolume,AcqPrice,AcqDate,RemainingVolume from #tableFifoSelect where FundPositionAdjustmentPK = @PK order by AcqDate desc
	                                                OPEN AB  
	                                                FETCH NEXT FROM AB INTO @FifoBondPositionPK,@PK,@AcqVolume,@AcqPrice,@AcqDate1,@RemainingVolume
  
	                                                WHILE @@FETCH_STATUS = 0  
	                                                BEGIN  

		                                                if (@AcqDate1 = @AcqDateInv and @AcqPrice = @AcqPriceInv)
			                                                if @FifobondPositionPK != 0
				                                                --select * from FifoBondPosition where fifobondpositionpk = @FifoBondPositionPK and FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1
				                                                update FifoBondPosition set RemainingVolume = @RemainingVolume - @AcqVolumeInv,FundPositionAdjustmentPK = 0 where fifobondpositionpk = @FifoBondPositionPK and FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 
			                                                else
				                                                --select * from FifoBondPositionTemp where FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 
				                                                update FifoBondPositionTemp set RemainingVolume = @RemainingVolume - @AcqVolumeInv,FundPositionAdjustmentPK = 0 where FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 
		                                                --else
		                                                --	update FifoBondPosition set RemainingVolume = @AcqVolume,FundPositionAdjustmentPK = 0 where FundPositionAdjustmentPK = @PK and AcqDate = @AcqDate1 and AcqPrice = @AcqPrice and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1
		                            
		
	                                                    FETCH NEXT FROM AB INTO @FifoBondPositionPK,@InvestmentSellPK,@AcqVolume,@AcqPrice,@AcqDate1,@RemainingVolume
	                                                end
	                                                CLOSE AB  
	                                                DEALLOCATE AB
                                            end

                                " + _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundPositionAdjustment where FundPositionAdjustmentPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundPositionAdjustment.FundPositionAdjustmentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundPositionAdjustment.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@FundPK", _FundPositionAdjustment.FundPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _FundPositionAdjustment.InstrumentPK);
                                cmd.Parameters.AddWithValue("@Date", _FundPositionAdjustment.Date);
                                cmd.Parameters.AddWithValue("@AcqDate", _FundPositionAdjustment.AcqDate);
                                cmd.Parameters.AddWithValue("@Balance", _FundPositionAdjustment.Balance);
                                cmd.Parameters.AddWithValue("@Price", _FundPositionAdjustment.Price);
                                cmd.Parameters.AddWithValue("@Description", _FundPositionAdjustment.Description);
                                cmd.Parameters.AddWithValue("@PeriodPK", _FundPositionAdjustment.PeriodPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundPositionAdjustment.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = @"Update FundPositionAdjustment set status= 4,Notes=@Notes, 
                                    LastUpdate=@lastupdate where FundPositionAdjustmentPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundPositionAdjustment.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundPositionAdjustment.FundPositionAdjustmentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundPositionAdjustment.HistoryPK);
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

        public void FundPositionAdjustment_Approved(FundPositionAdjustment _FundPositionAdjustment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"if (@ClientCode = '03' or @ClientCode = '20' or @ClientCode = '21' or @ClientCode = '22')
                                            begin
                                                declare @instrumentpk int
                                                declare @Price numeric(22,4)
                                                declare @FifoPrice numeric(22,4)
                                                declare @DoneVolume numeric(22,4)
                                                declare @Counter int
                                                declare @maxFifoDate date
                                                declare @fifoDate date
                                                declare @FundPK int
                                                declare @Volume numeric(22,4)
							

                                                if object_id('tempdb..#tableFifoSelect', 'u') is not null drop table #tableFifoSelect 
                                                create table #tableFifoSelect
                                                (
	                                                FifoBondPositionPK int,
	                                                FundPositionAdjustmentPK int,
	                                                AcqDate date,
	                                                AcqVolume numeric(22,4),
	                                                AcqPrice numeric(22,4)
                                                )
                                                CREATE CLUSTERED INDEX indx_tableFifoSelectn ON #tableFifoSelect (FifoBondPositionPK,AcqDate);

                                                if object_id('tempdb..#tableInvest', 'u') is not null drop table #tableInvest 
                                                create table #tableInvest
                                                (
	                                                FifoBondPositionPK int,
	                                                InvesmentBuyPK int,
	                                                InvesmentSellPK int,
	                                                FundPositionAdjustmentPK int,
	                                                AcqDate date,
	                                                AcqVolume numeric(22,4),
	                                                AcqPrice numeric(22,4),
	                                                RemainingVolume numeric(22,4)
                                                )
                                                CREATE CLUSTERED INDEX indx_tableInvest ON #tableInvest (FifoBondPositionPK,InvesmentBuyPK);

                                                select @instrumentpk = InstrumentPK, @FundPK = FundPK, @volume = Balance, @DoneVolume = Balance, @fifoDate = AcqDate, @FifoPrice = Price from FundPositionAdjustment where FundPositionAdjustmentPK = @PK and Status = 1 


                                                if exists (select * from FiFoBondPosition where status in (1,2) and FundPK = @fundpk and InstrumentPK = @instrumentpk )
                                                begin
	                                                select @maxFifoDate = isnull(max(CutOffDate),case when @ClientCode = '03' then '2019-10-18' when @ClientCode = '20' then '2020-06-19' when @ClientCode = '21' then '2020-08-05' when @ClientCode = '22' then '2020-08-28' else '1900-01-01' end) from FiFoBondPosition where status in (1,2) and FundPK = @fundpk and InstrumentPK = @instrumentpk

	                                                insert into #tableFifoSelect(FifoBondPositionPK,FundPositionAdjustmentPK,AcqDate,AcqVolume,AcqPrice)
	                                                select A.FifoBondPositionPK,FundPositionAdjustmentPK, A.AcqDate, A.DoneVolume,A.AcqPrice from (
	                                                select FifoBondPositionPK,@PK FundPositionAdjustmentPK,AcqDate,RemainingVolume DoneVolume,AcqPrice from FifoBondPosition 
			                                                where FundPK = @fundpk and InstrumentPk = @instrumentpk and (RemainingVolume is null or RemainingVolume != 0) and status in (1,2)  and AcqPrice = @FifoPrice and AcqDate = @fifoDate
			                                                union all
			                                                select 0,InvestmentBuyPK,AcqDate,RemainingVolume,AcqPrice from FifoBondPositionTemp
			                                                where FundPK = @fundpk and InstrumentPK = @instrumentpk and (RemainingVolume is null or RemainingVolume != 0) and AcqPrice = @FifoPrice and AcqDate = @fifoDate
	                                                )A 
	                                                order by A.AcqDate,A.FundPositionAdjustmentPK
                                                end
                                                else
                                                begin
	                                                insert into #tableFifoSelect(FifoBondPositionPK,FundPositionAdjustmentPK,AcqDate,AcqVolume,AcqPrice)
	                                                select A.A,A.FundPositionAdjustmentPK, A.AcqDate, A.DoneVolume,A.AcqPrice from (
	                                                select 0 A,InvestmentBuyPK FundPositionAdjustmentPK,AcqDate,case when InvestmentSellPK is null or InvestmentSellPK = 0  then AcqVolume else RemainingVolume end DoneVolume,AcqPrice from FifoBondPositionTemp
			                                                where FundPK = @fundpk and InstrumentPK = @instrumentpk and (RemainingVolume is null or RemainingVolume != 0) and AcqPrice = @FifoPrice
	                                                )A 
	                                                order by A.AcqDate,A.FundPositionAdjustmentPK
                                                end
							

                                                set @Counter = 0

                                                declare @FifobondPositionPK int
                                                declare @AcqDate date
                                                declare @AcqVolume numeric(22,4)
                                                declare @AcqPrice numeric(22,4)
                                                declare @Query nvarchar(500)
                                                declare @InvestmentBuyPK int
                                                declare @FundPositionAdjustmentPK int

                                                DECLARE AB CURSOR FOR   
	                                                select FifoBondPositionPK,FundPositionAdjustmentPK,AcqVolume,AcqPrice,AcqDate from #tableFifoSelect order by AcqDate
                                                OPEN AB  
                                                FETCH NEXT FROM AB INTO @FifoBondPositionPK,@InvestmentBuyPK,@AcqVolume,@AcqPrice,@AcqDate
  
                                                WHILE @@FETCH_STATUS = 0  
                                                BEGIN  

	                                                insert into #tableInvest(FifoBondPositionPK,FundPositionAdjustmentPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume)
	                                                select @FifobondPositionPK,@InvestmentBuyPK,@AcqDate,@AcqVolume,@AcqPrice,@AcqVolume + @DoneVolume 
													
		
	                                                if @FifobondPositionPK != 0
		                                                update FiFoBondPosition set RemainingVolume = @AcqVolume + @DoneVolume, FundPositionAdjustmentPK = @PK where FiFoBondPositionPK = @FifobondPositionPK and status in (1,2)
														
	                                                else if @InvestmentBuyPK !=0 and @Counter = 0
	                                                begin
		                                                if exists(select * from FiFoBondPositionTemp where InvestmentBuyPK = @InvestmentBuyPK)
			                                                update FifoBondPositionTemp set RemainingVolume = @AcqVolume + @DoneVolume, FundPositionAdjustmentPK = @PK where InvestmentBuyPK = @InvestmentBuyPK
		                                                else
			                                                insert into FifoBondPositionTemp(InvestmentBuyPK,FundPositionAdjustmentPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume,FundPK,InstrumentPK)
			                                                select @InvestmentBuyPK,@PK,@AcqDate,@AcqVolume + @DoneVolume,@AcqPrice,@AcqVolume,@FundPK,@instrumentpk
														   
	                                                end

													set @Counter = @Counter + 1
		
                                                FETCH NEXT FROM AB INTO @FifoBondPositionPK,@InvestmentBuyPK,@AcqVolume,@AcqPrice,@AcqDate
                                                END  
  
                                                CLOSE AB  
                                                DEALLOCATE AB 
                                            end

                                            update FundPositionAdjustment set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate
                                            where FundPositionAdjustmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundPositionAdjustment.FundPositionAdjustmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundPositionAdjustment.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundPositionAdjustment.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundPositionAdjustment set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundPositionAdjustmentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundPositionAdjustment.FundPositionAdjustmentPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundPositionAdjustment.ApprovedUsersID);
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

        public void FundPositionAdjustment_Reject(FundPositionAdjustment _FundPositionAdjustment)
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
                                        
                                            update FundPositionAdjustment set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate 
                                            where FundPositionAdjustmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundPositionAdjustment.FundPositionAdjustmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundPositionAdjustment.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundPositionAdjustment.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundPositionAdjustment set status= 2,lastupdate=@lastupdate where FundPositionAdjustmentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundPositionAdjustment.FundPositionAdjustmentPK);
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

        public void FundPositionAdjustment_Void(FundPositionAdjustment _FundPositionAdjustment)
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
                                            if (@ClientCode = '03' or @ClientCode = '20' or @ClientCode = '21'  or @ClientCode = '22')
                                            begin
                                                declare @InstrumentPK1 int
                                                declare @DoneVolume numeric(22,4)
                                                declare @Counter int
                                                declare @maxFifoDate date
                                                declare @Volume numeric(22,4)
                                                declare @FundPK1 int
                                                declare @RemainingVolume numeric(22,4)
                                                declare @AcqPriceInv numeric(22,4)
                                                declare @AcqVolumeInv numeric(22,4)
                                                declare @AcqDateInv date
							
								
	                                                if object_id('tempdb..#tableFifoSelect', 'u') is not null drop table #tableFifoSelect 
	                                                create table #tableFifoSelect
	                                                (
		                                                FifoBondPositionPK int,
		                                                FundPositionAdjustmentPK int,
		                                                AcqDate date,
		                                                AcqVolume numeric(22,4),
		                                                RemainingVolume numeric(22,4),
		                                                AcqPrice numeric(22,4)
	                                                )
	                                                CREATE CLUSTERED INDEX indx_tableFifoSelectn ON #tableFifoSelect (FifoBondPositionPK,AcqDate);


	                                                if object_id('tempdb..#tableInvest', 'u') is not null drop table #tableInvest 
	                                                create table #tableInvest
	                                                (
		                                                FifoBondPositionPK int,
		                                                InvesmentBuyPK int,
		                                                InvesmentSellPK int,
		                                                AcqDate date,
		                                                AcqVolume numeric(22,4),
		                                                AcqPrice numeric(22,4),
		                                                RemainingVolume numeric(22,4)
	                                                )
	                                                CREATE CLUSTERED INDEX indx_tableInvest ON #tableInvest (FifoBondPositionPK,InvesmentBuyPK);

	                                                select @InstrumentPK1 = InstrumentPK, @FundPK1 = FundPK, @volume = Balance, @DoneVolume = Balance,@AcqDateInv = AcqDate, @AcqPriceInv = Price, @AcqVolumeInv = Balance from FundPositionAdjustment where FundPositionAdjustmentPK = @PK and Status= 2 
	                                                insert into #tableFifoSelect(FifoBondPositionPK,FundPositionAdjustmentPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume)
	                                                select A.FifoBondPositionPK,A.FundPositionAdjustmentPK, A.AcqDate, A.DoneVolume,A.AcqPrice,RemainingVolume from (
			                                                select FifoBondPositionPK,FundPositionAdjustmentPK,AcqDate,AcqVolume DoneVolume,AcqPrice,RemainingVolume from FifoBondPosition 
			                                                where FundPK = @FundPK1 and InstrumentPk = @InstrumentPK1 and status in (1,2) and FundPositionAdjustmentPK = @PK
			                                                union all
			                                                select 0,FundPositionAdjustmentPK,AcqDate,AcqVolume,AcqPrice,RemainingVolume from FifoBondPositionTemp
			                                                where FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 and FundPositionAdjustmentPK = @PK
	                                                )A 
	                                                order by A.AcqDate					

	                                                select * from #tableFifoSelect

	                                                set @Counter = 0

	                                                declare @FifobondPositionPK int
	                                                declare @Query nvarchar(500)
	                                                declare @InvestmentSellPK int
	                                                declare @AcqPrice numeric(22,4)
	                                                declare @AcqVolume numeric(22,4)
	                                                declare @AcqDate1 date


	                                                DECLARE AB CURSOR FOR   
		                                                select FifoBondPositionPK,FundPositionAdjustmentPK,AcqVolume,AcqPrice,AcqDate,RemainingVolume from #tableFifoSelect where FundPositionAdjustmentPK = @PK order by AcqDate desc
	                                                OPEN AB  
	                                                FETCH NEXT FROM AB INTO @FifoBondPositionPK,@PK,@AcqVolume,@AcqPrice,@AcqDate1,@RemainingVolume
  
	                                                WHILE @@FETCH_STATUS = 0  
	                                                BEGIN  

		                                                if (@AcqDate1 = @AcqDateInv and @AcqPrice = @AcqPriceInv)
			                                                if @FifobondPositionPK != 0
				                                                --select * from FifoBondPosition where fifobondpositionpk = @FifoBondPositionPK and FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1
				                                                update FifoBondPosition set RemainingVolume = @RemainingVolume - @AcqVolumeInv,FundPositionAdjustmentPK = 0 where fifobondpositionpk = @FifoBondPositionPK and FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 
			                                                else
				                                                --select * from FifoBondPositionTemp where FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 
				                                                update FifoBondPositionTemp set RemainingVolume = @RemainingVolume - @AcqVolumeInv,FundPositionAdjustmentPK = 0 where FundPositionAdjustmentPK = @PK and AcqDate = @AcqDateInv and AcqPrice = @AcqPriceInv and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1 
		                                                --else
		                                                --	update FifoBondPosition set RemainingVolume = @AcqVolume,FundPositionAdjustmentPK = 0 where FundPositionAdjustmentPK = @PK and AcqDate = @AcqDate1 and AcqPrice = @AcqPrice and FundPK = @FundPK1 and InstrumentPK = @InstrumentPK1
		                            
		
	                                                    FETCH NEXT FROM AB INTO @FifoBondPositionPK,@InvestmentSellPK,@AcqVolume,@AcqPrice,@AcqDate1,@RemainingVolume
	                                                end
	                                                CLOSE AB  
	                                                DEALLOCATE AB
                                            end
                                            

                                            update FundPositionAdjustment set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate 
                                            where FundPositionAdjustmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundPositionAdjustment.FundPositionAdjustmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundPositionAdjustment.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundPositionAdjustment.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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

        public string Validate_CheckFifoBalance(FundPositionAdjustment _FundPositionAdjustment)
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
                        declare @maxFifoDate date
							

                        if object_id('tempdb..#tableFifoSelect', 'u') is not null drop table #tableFifoSelect 
                        create table #tableFifoSelect
                        (
	                        FifoBondPositionPK int,
	                        FundPositionAdjustmentPK int,
	                        AcqDate date,
	                        AcqVolume numeric(22,4),
	                        AcqPrice numeric(22,4)
                        )
                        CREATE CLUSTERED INDEX indx_tableFifoSelectn ON #tableFifoSelect (FifoBondPositionPK,AcqDate);

                        --select @instrumentpk = InstrumentPK, @FundPK = FundPK, @volume = Balance, @DoneVolume = Balance, @AcqDate = AcqDate, @Price = Price from FundPositionAdjustment where FundPositionAdjustmentPK = @PK and Status = 1 


                        if exists (select * from FiFoBondPosition where status in (1,2) and FundPK = @fundpk and InstrumentPK = @instrumentpk )
                        begin
	                        select @maxFifoDate = isnull(max(CutOffDate),case when @ClientCode = '03' then '2019-10-18' when @ClientCode = '20' then '2020-06-19' when @ClientCode = '21' then '2020-09-30' when @ClientCode = '22' then '2020-08-28' else '1900-01-01' end) from FiFoBondPosition where status in (1,2) and FundPK = @fundpk and InstrumentPK = @instrumentpk

	                        insert into #tableFifoSelect(FifoBondPositionPK,FundPositionAdjustmentPK,AcqDate,AcqVolume,AcqPrice)
	                        select A.FifoBondPositionPK,FundPositionAdjustmentPK, A.AcqDate, A.DoneVolume,A.AcqPrice from (
	                        select FifoBondPositionPK,0 FundPositionAdjustmentPK,AcqDate,case when InvestmentPK is null or InvestmentPK = 0 then AcqVolume else RemainingVolume end DoneVolume,AcqPrice from FifoBondPosition 
			                        where FundPK = @fundpk and InstrumentPk = @instrumentpk and (RemainingVolume is null or RemainingVolume != 0) and status in (1,2)  and AcqPrice = @Price
			                        union all
			                        select 0,InvestmentBuyPK,AcqDate,case when InvestmentSellPK is null or InvestmentSellPK = 0  then AcqVolume else RemainingVolume end,AcqPrice from FifoBondPositionTemp
			                        where FundPK = @fundpk and InstrumentPK = @instrumentpk and (RemainingVolume is null or RemainingVolume != 0) and AcqPrice = @Price
	                        )A 
	                        order by A.AcqDate,A.FundPositionAdjustmentPK
                        end
                        else
                        begin
	                        insert into #tableFifoSelect(FifoBondPositionPK,FundPositionAdjustmentPK,AcqDate,AcqVolume,AcqPrice)
	                        select A.A,A.FundPositionAdjustmentPK, A.AcqDate, A.DoneVolume,A.AcqPrice from (
	                        select 0 A,InvestmentBuyPK FundPositionAdjustmentPK,AcqDate,case when InvestmentSellPK is null or InvestmentSellPK = 0  then AcqVolume else RemainingVolume end DoneVolume,AcqPrice from FifoBondPositionTemp
			                        where FundPK = @fundpk and InstrumentPK = @instrumentpk and (RemainingVolume is null or RemainingVolume != 0) and AcqPrice = @Price
	                        )A 
	                        order by A.AcqDate,A.FundPositionAdjustmentPK
                        end
							

                        if @Balance + ( select top 1 AcqVolume from #tableFifoSelect ) < 0
	                        select 'ERROR : Max balance input = ' + ( select top 1 cast(AcqVolume as nvarchar) from #tableFifoSelect ) Result
                        else
	                        select '' Result


                        ";

                        cmd.Parameters.AddWithValue("@FundPK", _FundPositionAdjustment.FundPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _FundPositionAdjustment.InstrumentPK);
                        cmd.Parameters.AddWithValue("@AcqDate", _FundPositionAdjustment.AcqDate);
                        cmd.Parameters.AddWithValue("@Balance", _FundPositionAdjustment.Balance);
                        cmd.Parameters.AddWithValue("@Price", _FundPositionAdjustment.Price);
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

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

    }
}