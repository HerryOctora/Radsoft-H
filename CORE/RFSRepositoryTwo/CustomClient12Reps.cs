using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using OfficeOpenXml.Drawing;
using System.Data.OleDb;using RFSRepository;
using OfficeOpenXml.Drawing.Chart;
//using Microsoft.Office.Interop.Excel;

namespace RFSRepositoryTwo
{
    public class CustomClient12Reps
    {
        Host _host = new Host();

        public class CustomerPortfolioAllFundClient
        {
            public int FundClientPK { get; set; }
            public int FundPK { get; set; }
            public string FundName { get; set; }
            public decimal Unit { get; set; }
            public decimal Nav { get; set; }
            public decimal CloseNav { get; set; }
            public string ClientID { get; set; }
            public string ClientName { get; set; }
            public string CIF { get; set; }
            public string CurrencyID { get; set; }
            public decimal UnitBalance { get; set; }
            public decimal CashBalance { get; set; }
            public decimal Balance { get; set; }
            public decimal Charge { get; set; }
            public string Description { get; set; }
            public decimal AvgNav { get; set; }
            public decimal FundValue { get; set; }
            public decimal MarketValue { get; set; }
            public decimal Unrealized { get; set; }
            public string Address { get; set; }
            public decimal UnrealizedPercent { get; set; }
            public DateTime ValueDate { get; set; }

        }

        private class UnitTrustReport
        {
            public string ClientName { get; set; }
            public string FundName { get; set; }
            public string FundID { get; set; }

            public string TrxDate { get; set; }
            public string PostedTime { get; set; }
            public string AccountNo { get; set; }
            public string Address { get; set; }
            public string CurrencyID { get; set; }
            public string TRXType { get; set; }
            public decimal Amount { get; set; }
            public decimal FeeAmount { get; set; }
            public decimal NAV { get; set; }
            public decimal AvgCost { get; set; }
            public decimal TotalUnitAmount { get; set; }
            public decimal BeginningBalance { get; set; }
            public decimal TotalCashAmount { get; set; }
            public decimal EndAVG { get; set; }
            public decimal EndNAV { get; set; }
            public decimal NetValue { get; set; } 

        }

        private class LaporanKeuanganRpt
        {
            public decimal ID { get; set; }
            public string Name { get; set; }
            public decimal PrevBalance { get; set; }
            public decimal CurrBalance { get; set; }
            public decimal Credit { get; set; }
            public decimal Debet { get; set; }

        }
        private class FundFactSheet
        {
            public string TanggalEfektif { get; set; }
            public string BankCustodian { get; set; }
            public string AccountName { get; set; }
            public string AccountNumber { get; set; }
            public string CustodyAccount { get; set; }
            public string AboutCompany { get; set; }
            public string AboutFund { get; set; }
            public string MarketBrief { get; set; }
            public string AboutCustodian { get; set; }
            public decimal JumlahUnit { get; set; }
            public decimal CustodyFee { get; set; }
            public decimal ManagementFee { get; set; }
            public decimal SubscriptionFee { get; set; }
            public decimal RedemptionFee { get; set; }
            public decimal SwitchingFee { get; set; }
            public string FundName { get; set; }

        }

        public NAWCDailyProcess Generate_NAWCDailyByDateFromTo(string _userID, DateTime _dateFrom, DateTime _dateTo)
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
                        
                        
                        Declare @Date datetime

                        Declare @NewMKBDTrailsPK int

                        Declare @BitValidate bit





                        declare @ssql nvarchar(4000)   

                        declare @field nvarchar(5)    

                        declare @value numeric(18,4)  

                        declare @TotalEquity float   



                        declare @CompanyID nvarchar(2)        

                        declare @CompanyName nvarchar(50)        

                        declare @DirectorName nvarchar(50)    

                        Declare @PeriodPK int



                        Declare @MsgSuccess nvarchar(max)



                        --drop table #date 
                        create table #date 
                        (
                        valuedate datetime
                        )

                        --drop table #tmpReksadana

                        Create Table #tmpReksadana

                        (  

                        [InstrumentPK] INTEGER NOT NULL,  

                        [LastVolume] [numeric](18, 4) NOT NULL,
                        
                        ValueDate Datetime

                        )

                        --drop table #tmpDeposito

                        Create Table #tmpDeposito

                        (  

                        [InstrumentPK] INTEGER NOT NULL,  

                        [LastAmount] [numeric](18, 4) NOT NULL,

                        [MaturityDate] Datetime NOT NULL,
                        
                        ValueDate Datetime


                        )

                        --drop table #tmpBond

                        Create Table #tmpBond

                        (  

                        [InstrumentPK] INTEGER NOT NULL,  

                        [Volume] [numeric](18,4) NOT NULL,
                     
                        ValueDate Datetime

                        )

                        --drop table #tmpEquity

                        Create Table #tmpEquity

                        (  

                        [InstrumentPK] INTEGER NOT NULL,  

                        [LastVolume] [numeric](18, 4) NOT NULL,
                        
                        ValueDate Datetime

                        )

                        --drop table #tmpSBN

                        Create Table #tmpSBN

                        (  

                        [InstrumentPK] INTEGER NOT NULL,  

                        [Volume] [numeric](18,4) NOT NULL,

                        MaturityDate Datetime,
                        
                        ValueDate Datetime

                        )

                        --drop table #IsUnderDueDateAmount
                        CREATE TABLE #IsUnderDueDateAmount (
                        [HistoryPK] [bigint] NOT NULL,
                        [Status] [int] NOT NULL,
                        [Notes] [nvarchar](1000) NULL,
                        [Date] [datetime] NOT NULL,
                        [MKBDTrailsPK] [int] NOT NULL,
                        [MKBD01] [int] NULL,
                        [MKBD02] [int] NULL,
                        [MKBD03] [int] NULL,
                        [MKBD09] [int] NULL,
                        [InstrumentPK] [int] NOT NULL,
                        [DepositoTypePK] [int] NOT NULL,
                        [IsLiquidated] [nchar](10) NOT NULL,
                        [DueDate] [datetime] NOT NULL,
                        [MarketValue] [numeric](18, 4) NOT NULL,
                        [IsUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [NotUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [QuaranteedAmount] [numeric](18, 4) NOT NULL,
                        [NotLiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [LiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [HaircutPercent] [numeric](18, 4) NOT NULL,
                        [HaircutAmount] [numeric](18, 4) NOT NULL,
                        [AfterHaircutAmount] [numeric](18, 4) NOT NULL,
                        [RankingLiabilities] [numeric](18, 4) NOT NULL,
                        [EntryUsersID] [nvarchar](70) NULL,
                        [EntryTime] [datetime] NULL,
                        [UpdateUsersID] [nvarchar](70) NULL,
                        [UpdateTime] [datetime] NULL,
                        [ApprovedUsersID] [nvarchar](70) NULL,
                        [ApprovedTime] [datetime] NULL,
                        [VoidUsersID] [nvarchar](70) NULL,
                        [VoidTime] [datetime] NULL,
                        [DBUserID] [nvarchar](50) NULL,
                        [DBTerminalID] [nvarchar](50) NULL,
                        [LastUpdate] [datetime] NULL,
                        [LastUpdateDB] [datetime] NULL
                        )

                        --drop table #QuaranteedAmount
                        CREATE TABLE #QuaranteedAmount (
                        [HistoryPK] [bigint] NOT NULL,
                        [Status] [int] NOT NULL,
                        [Notes] [nvarchar](1000) NULL,
                        [Date] [datetime] NOT NULL,
                        [MKBDTrailsPK] [int] NOT NULL,
                        [MKBD01] [int] NULL,
                        [MKBD02] [int] NULL,
                        [MKBD03] [int] NULL,
                        [MKBD09] [int] NULL,
                        [InstrumentPK] [int] NOT NULL,
                        [DepositoTypePK] [int] NOT NULL,
                        [IsLiquidated] [nchar](10) NOT NULL,
                        [DueDate] [datetime] NOT NULL,
                        [MarketValue] [numeric](18, 4) NOT NULL,
                        [IsUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [NotUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [QuaranteedAmount] [numeric](18, 4) NOT NULL,
                        [NotLiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [LiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [HaircutPercent] [numeric](18, 4) NOT NULL,
                        [HaircutAmount] [numeric](18, 4) NOT NULL,
                        [AfterHaircutAmount] [numeric](18, 4) NOT NULL,
                        [RankingLiabilities] [numeric](18, 4) NOT NULL,
                        [EntryUsersID] [nvarchar](70) NULL,
                        [EntryTime] [datetime] NULL,
                        [UpdateUsersID] [nvarchar](70) NULL,
                        [UpdateTime] [datetime] NULL,
                        [ApprovedUsersID] [nvarchar](70) NULL,
                        [ApprovedTime] [datetime] NULL,
                        [VoidUsersID] [nvarchar](70) NULL,
                        [VoidTime] [datetime] NULL,
                        [DBUserID] [nvarchar](50) NULL,
                        [DBTerminalID] [nvarchar](50) NULL,
                        [LastUpdate] [datetime] NULL,
                        [LastUpdateDB] [datetime] NULL
                        )

                        --drop table #NotLiquidatedAmount
                        CREATE TABLE #NotLiquidatedAmount (
                        [HistoryPK] [bigint] NOT NULL,
                        [Status] [int] NOT NULL,
                        [Notes] [nvarchar](1000) NULL,
                        [Date] [datetime] NOT NULL,
                        [MKBDTrailsPK] [int] NOT NULL,
                        [MKBD01] [int] NULL,
                        [MKBD02] [int] NULL,
                        [MKBD03] [int] NULL,
                        [MKBD09] [int] NULL,
                        [InstrumentPK] [int] NOT NULL,
                        [DepositoTypePK] [int] NOT NULL,
                        [IsLiquidated] [nchar](10) NOT NULL,
                        [DueDate] [datetime] NOT NULL,
                        [MarketValue] [numeric](18, 4) NOT NULL,
                        [IsUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [NotUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [QuaranteedAmount] [numeric](18, 4) NOT NULL,
                        [NotLiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [LiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [HaircutPercent] [numeric](18, 4) NOT NULL,
                        [HaircutAmount] [numeric](18, 4) NOT NULL,
                        [AfterHaircutAmount] [numeric](18, 4) NOT NULL,
                        [RankingLiabilities] [numeric](18, 4) NOT NULL,
                        [EntryUsersID] [nvarchar](70) NULL,
                        [EntryTime] [datetime] NULL,
                        [UpdateUsersID] [nvarchar](70) NULL,
                        [UpdateTime] [datetime] NULL,
                        [ApprovedUsersID] [nvarchar](70) NULL,
                        [ApprovedTime] [datetime] NULL,
                        [VoidUsersID] [nvarchar](70) NULL,
                        [VoidTime] [datetime] NULL,
                        [DBUserID] [nvarchar](50) NULL,
                        [DBTerminalID] [nvarchar](50) NULL,
                        [LastUpdate] [datetime] NULL,
                        [LastUpdateDB] [datetime] NULL
                        )

                        --drop table #LiquidatedAmount
                        CREATE TABLE #LiquidatedAmount (
                        [HistoryPK] [bigint] NOT NULL,
                        [Status] [int] NOT NULL,
                        [Notes] [nvarchar](1000) NULL,
                        [Date] [datetime] NOT NULL,
                        [MKBDTrailsPK] [int] NOT NULL,
                        [MKBD01] [int] NULL,
                        [MKBD02] [int] NULL,
                        [MKBD03] [int] NULL,
                        [MKBD09] [int] NULL,
                        [InstrumentPK] [int] NOT NULL,
                        [DepositoTypePK] [int] NOT NULL,
                        [IsLiquidated] [nchar](10) NOT NULL,
                        [DueDate] [datetime] NOT NULL,
                        [MarketValue] [numeric](18, 4) NOT NULL,
                        [IsUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [NotUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [QuaranteedAmount] [numeric](18, 4) NOT NULL,
                        [NotLiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [LiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [HaircutPercent] [numeric](18, 4) NOT NULL,
                        [HaircutAmount] [numeric](18, 4) NOT NULL,
                        [AfterHaircutAmount] [numeric](18, 4) NOT NULL,
                        [RankingLiabilities] [numeric](18, 4) NOT NULL,
                        [EntryUsersID] [nvarchar](70) NULL,
                        [EntryTime] [datetime] NULL,
                        [UpdateUsersID] [nvarchar](70) NULL,
                        [UpdateTime] [datetime] NULL,
                        [ApprovedUsersID] [nvarchar](70) NULL,
                        [ApprovedTime] [datetime] NULL,
                        [VoidUsersID] [nvarchar](70) NULL,
                        [VoidTime] [datetime] NULL,
                        [DBUserID] [nvarchar](50) NULL,
                        [DBTerminalID] [nvarchar](50) NULL,
                        [LastUpdate] [datetime] NULL,
                        [LastUpdateDB] [datetime] NULL
                        )

                        --drop table #MKBD04    

                        Create Table #MKBD04

                        (  

                        [MKBDTrailsPK] INTEGER NOT NULL,  

                        [Date] [datetime] NOT NULL,  

                        [CompanyID] [nvarchar](12) NOT NULL,  

                        [CompanyName] [nvarchar](50) NOT NULL,  

                        [DirectorName] [nvarchar](50) NOT NULL,  

                        [MKBDNo] [int] NOT NULL,  

                        [No] [int] IDENTITY(1,1) NOT NULL,  

                        [A] [nvarchar](50) NOT NULL,  

                        [B] [nvarchar](50) NOT NULL,  

                        [C] [nvarchar](50) NOT NULL,  

                        [D] [numeric](18, 4) NOT NULL,  

                        [E] [numeric](18, 4) NOT NULL,  

                        [F] [nvarchar](50) NOT NULL,  

                        [G] [numeric](18, 4) NOT NULL,  

                        [H] [numeric](18, 4) NOT NULL,  

                        LastUpdate Datetime  NOT NULL

                        )

                        insert into #date (valuedate)
                        SELECT  TOP (DATEDIFF(DAY, @datefrom, @dateto) + 1) Dates = DATEADD(DAY, ROW_NUMBER() OVER(ORDER BY a.object_id) - 1, @datefrom)
                        FROM sys.all_objects a CROSS JOIN sys.all_objects b

                        delete from #date where dbo.[CheckTodayIsHoliday](valuedate) = 1

                        DECLARE DATE CURSOR FOR  

                        select distinct valuedate from #date

                        OPEN DATE 
                        FETCH NEXT FROM DATE 
                        INTO @Date

                        While @@Fetch_Status  = 0 
                        BEGIN


                        if exists(Select * from MKBDTrails where status = 2 and ValueDate = @Date)

                        BEGIN

                        select 'NAWC Already Generated and Approved for this Date' Msg

                        return

                        END



                        if exists(select * from company where status = 2)

                        BEGIN

                        if exists(Select * from Company Where (ID ='' or ID is null or Name ='' or Name is null or DirectorOne ='' or DirectorOne is null) and status = 2)

                        BEGIN

                        select 'Master Company has no data in Field ID/Name/Director One,Please Check it' Msg

                        return

                        END

                        ELSE

                        BEGIN

                        Select @CompanyID = ID, @CompanyName = Name, @DirectorName = DirectorOne from Company (nolock) 

                        where status = 2  

                        Select @MsgSuccess = 'Company Check Success  <br/> '

                        END

                        END

                        ELSE

                        BEGIN

                        select 'Master Company has no Approved Data,Please Check it' Msg

                        return

                        END

  



                        select @PeriodPK = periodPK     

                        from Period (nolock) where @Date between Datefrom and Dateto and status = 2  




                        Declare @AccSetupTotalEkuitas int
                        select @AccSetupTotalEkuitas =  TotalEquity from AccountingSetup where status = 2


                        select @TotalEquity = [dbo].[FGetAccountBalanceByDateByParentForEquity] (@Date,   @AccSetupTotalEkuitas)   




                        --If @TotalEquity = 0 OR @TotalEquity Is null

                        --BEGIN

                        --	select 'Total Equity in MKBD02 Yesterday is 0 or Not Generate yet,Please Check it' Msg

                        --	return

                        --END



                        Select @MsgSuccess = @MsgSuccess + 'Total Equity Check Success <br/> '



                        Select @NewMKBDTrailsPK = isnull(Max(MKBDTrailsPK),0) + 1 From MKBDTrails



                        Insert into MKBDTrails(MKBDTrailsPK,historyPK,status,Notes,BitValidate,ValueDate,

                        LogMessages,LastUsersToText,ToTextTime,GenerateToTextCount,EntryUsersID,EntryTime)

                        Select @NewMKBDTrailsPK,1,@Status,'',0,@Date,'','',null,0,@UsersID,@timeNow



                        -- INIT MKBD 01,02,03,08,09



                        Insert into MKBD01(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate)

                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow



                        Insert into MKBD02(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate)

                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow



                        Insert into MKBD03(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate)

                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow



                        Insert into MKBD08(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate)

                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow



                        Insert into MKBD09(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate)

                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow



                        declare @msg nvarchar(1000)



                        -- Portfolio Checking REPO



                        --IF EXISTS(Select * from TrxRepo Where Posted = 1 and BuyBackDate <= @Date and TrxType = 1)

--                        BEGIN

--                        set @msg = ''

--                        Select distinct @msg = @msg + B.ID + ', ' from TrxRepo A

--                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

--                        where A.Posted = 1 and A.BuyBackDate <= @Date and TrxType = 1 and A.instrumentPK not in

--                        (

--                        Select InstrumentPK

--                        from ClosePrice 

--                        where status = 2 and Date = @Date

--                        )

--                        if @@Rowcount > 0

--                        BEGIN

--                        Select 'Cek Close Price untuk Data : ' + @msg Msg

--                        update MKBDTrails set LogMessages = 'Cek Close Price untuk Data : ' + @msg where MKBDTrailsPK = @NEWMKBDTrailsPK

--                        return

--                        END

--                        ELSE

--                        BEGIN

--                        Select @MsgSuccess = @MsgSuccess + 'Company has REPO <br/> '	

--                        Insert Into RL510ARepo (HistoryPK,Status,Notes,MKBDTrailsPK,Date,CounterPartPK,InstrumentPK,InstrumentTypePK,SellDate,

--                        BuyBackDate,SellAmount,Expense,BuyBackAmount,HaircutPercent,Volume,ClosePrice,MarketValue,RankingLiabilities,MKBD01,MKBD02,MKBD03,MKBD09)

--                        Select 1,2,'',@NewMKBDTrailsPK,@Date,CounterPartPK,A.InstrumentPK,B.InstrumentTypePK,SellDate,BuyBackDate,SellAmount,Expense,BuyBackAmount,

--                        Case when d.type = 1 then HcEquity.RGHaircutMKBD when d.type = 2 then HcObl.OBHaircutMKBD when d.type = 5 then HcSBN.SBNHaircutMKBD else 0 End

--                        ,Volume,

--                        C.ClosePriceValue,Volume * C.ClosePriceValue, .05 * BuyBackAmount

--                        ,Case when d.type = 1 then HcEquity.RGMKBD01 when d.type = 2 then HcObl.OBMKBD01 when d.type = 5 then HcSBN.SBNMKBD01 else 0 End

--                        ,Case when d.type = 1 then HcEquity.RGMKBD02 when d.type = 2 then HcObl.OBMKBD02 when d.type = 5 then HcSBN.SBNMKBD02 else 0 End

--                        ,Case when d.type = 1 then HcEquity.RGMKBD03 when d.type = 2 then HcObl.OBMKBD03 when d.type = 5 then HcSBN.SBNMKBD03 else 0 End

--                        ,Case when d.type = 1 then HcEquity.RGMKBD09 when d.type = 2 then HcObl.OBMKBD09 when d.type = 5 then HcSBN.SBNMKBD09 else 0 End

		

--                        from TrxRepo A

--                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

--                        left join ClosePrice C on A.InstrumentPk = C.InstrumentPK and C.Status = 2

--                        Left join InstrumentType D on B.InstrumentTypePK = D.Type and D.status = 2

		

--                        Left join 

--                        (

--                        select InstrumentTypePK, HaircutFrom, HaircutTo, HaircutMKBD as RGHaircutMKBD, ConcentrationRisk as RGConcentrationRisk,    

--                        MKBD01 as RGMKBD01, MKBD02 as RGMKBD02, MKBD03 as RGMKBD03, MKBD09 as RGMKBD09    

--                        from MKBDSetup (nolock) A

--                        where [Date] = 

--                        (    

--                        select Max([Date]) as MaxDate from MKBDSetup (nolock)    

--                        where [Date] <= @Date and rlName = 'Rl510A'    

--                        )and rlName = 'Rl510A' and InstrumentTypePK = 1 and status = 2

--                        ) as HcEquity ON D.Type = HcEquity.InstrumentTypePK and [dbo].[FGetLastNAWCHaircut](@Date,A.InstrumentPK) = HcEquity.RGHaircutMKBD  

		

--                        Left Join

--                        (            

--                        select InstrumentTypePK, BondRating, HaircutMKBD as OBHaircutMKBD, ConcentrationRisk as OBConcentrationRisk,            

--                        MKBD01 as OBMKBD01, MKBD02 as OBMKBD02, MKBD03 as OBMKBD03, MKBD09 as OBMKBD09            

--                        from MKBDSetup  (nolock)            

--                        where [Date] = (                

--                        select Max([Date]) as MaxDate from MKBDSetup (nolock)                

--                        where [Date] <= @Date and RLName = 'Rl510A'                

--                        )and RLName = 'Rl510A' and InstrumentTypePK = 2 and status = 2

--                        ) as HcObl     

--                        ON D.Type = HcObl.InstrumentTypePK and     

--                        dbo.[FGetLastOBRating](@Date, A.InstrumentPK) = HcObl.BondRating 

	    

--                        Left Join

--                        (

--                        select InstrumentTypePK, SBNTenorFrom, SBNTenorTo, HaircutMKBD as SBNHaircutMKBD, ConcentrationRisk as SBNConcentrationRisk,      

--                        MKBD01 as SBNMKBD01, MKBD02 as SBNMKBD02, MKBD03 as SBNMKBD03, MKBD09 as SBNMKBD09      

--                        from MKBDSetup (nolock)      

--                        where [Date] =       

--                        (      

--                        select Max([Date]) as MaxDate       

--                        from MKBDSetup (nolock)      

--                        where [Date] <= @Date and RLName = 'Rl510A'       

--                        ) and RLName = 'Rl510A' and InstrumentTypePK = 5  and status = 2

--                        ) as HcSBN ON --St.StockType = HcSBN.StockType and     

--                        cast(datediff(dd, @Date, isnull(B.MaturityDate,@Date)) as float)/365 between HcSBN.SBNTenorFrom and HcSBN.SBNTenorTo       

		

--                        Where Posted = 1 and BuyBackDate <= @Date and TrxType = 1

	



--                        -- RL510 A Update MKBD 01

--                        set @ssql = ''

--                        declare A cursor for    

--                        select cast(MKBD01 as varchar) + 'B', sum(MarketValue) as Value    

--                        from RL510aRepo (nolock) where date = @date and MKBD01 > 0 

--                        and MKBDTrailsPK = @NewMKBDTrailsPK

--                        group by MKBD01    



--                        union all    



--                        select cast(rlb.MKBD01 as varchar) + 'B', -sum(MarketValue) as Value    

--                        from RL510aRepo rla (nolock)    

--                        left join     

--                        (    

--                        select MKBD01, InstrumentPK from RL510cEquity (nolock) where date = @date and MKBD01 <> 0 	and MKBDTrailsPK = @NewMKBDTrailsPK    

--                        union all    

--                        select MKBD01, InstrumentPK from RL510cSBN (nolock) where date = @date and MKBD01 <> 0 	and MKBDTrailsPK = @NewMKBDTrailsPK   

--                        union all    

--                        select MKBD01, InstrumentPK from RL510cBond (nolock) where date = @date and MKBD01 <> 0 and MKBDTrailsPK = @NewMKBDTrailsPK

--                        ) rlb on rla.InstrumentPK = rlb.InstrumentPK     

--                        where date = @date and rlb.MKBD01 > 0 

--                        and MKBDTrailsPK = @NewMKBDTrailsPK

--                        group by rlb.MKBD01    



--                        open A    



--                        fetch next from A    

--                        into @field, @value    



--                        while @@FETCH_STATUS = 0    

--                        begin     

--                        set @ssql = 'update MKBD01 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''  + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)     

--                        exec (@ssql)    



--                        fetch next from A    

--                        into @field, @value    

--                        end    



--                        close A    

--                        deallocate A   

	

--                        -- RL510 A Update MKBD 02

--                        set @ssql = ''

--                        declare A cursor for    

--                        select cast(MKBD02 as varchar) + 'B', sum(MarketValue) as Value    

--                        from RL510aRepo (nolock) where date = @date and MKBD02 > 0 

--                        and MKBDTrailsPK = @NewMKBDTrailsPK

--                        group by MKBD02    



--                        open A    



--                        fetch next from A    

--                        into @field, @value    



--                        while @@FETCH_STATUS = 0    

--                        begin     

--                        set @ssql = 'update MKBD02 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + '''' + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)        

--                        exec (@ssql)    



--                        fetch next from A    

--                        into @field, @value    

--                        end    



--                        close A    

--                        deallocate A   

	

--                        -- RL510 A Update MKBD 03

--                        set @ssql = ''

--                        declare A cursor for  

--                        select cast(MKBD03 as varchar) + 'B', sum(RankingLiabilities) as Value  

--                        from RL510aRepo (nolock) where date = @date and MKBD03 > 0 

--                        and MKBDTrailsPK = @NewMKBDTrailsPK

--                        group by MKBD03  



--                        open A  



--                        fetch next from A  

--                        into @field, @value  



--                        while @@FETCH_STATUS = 0  

--                        begin   

--                        set @ssql = 'update MKBD03 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''  + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)        

--                        exec (@ssql)  



--                        fetch next from A  

--                        into @field, @value  

--                        end  



--                        close A  

--                        deallocate A   

	

--                        -- RL510 A Update MKBD 09

--                        set @ssql = ''

--                        declare A cursor for    

--                        select cast(MKBD09 as varchar) + 'E', sum(MarketValue) as Value    

--                        from RL510aRepo (nolock) where date = @date and MKBD09 > 0 

--                        and MKBDTrailsPK = @NewMKBDTrailsPK

--                        group by MKBD09    

--                        union all  

--                        select cast(MKBD09 as varchar) + 'G', sum(MarketValue * haircutpercent) as Value    

--                        from RL510aRepo (nolock) where date = @date and MKBD09 > 0 

--                        and MKBDTrailsPK = @NewMKBDTrailsPK

--                        group by MKBD09   

--                        open A    



--                        fetch next from A    

--                        into @field, @value    



--                        while @@FETCH_STATUS = 0    

--                        begin     

--                        set @ssql = 'update MKBD09 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)   

--                        exec (@ssql)    



--                        fetch next from A    

--                        into @field, @value    

--                        end    



--                        close A    

--                        deallocate A   

	

--                        END

--                        END



--                        -- Portfolio Checking REVERSE REPO



--                        IF EXISTS(Select * from TrxRepo Where Posted = 1 and SellBackDate <= @Date and TrxType = 2)

--                        BEGIN

--                        set @msg = ''

--                        Select distinct @msg = @msg + B.ID + ', ' from #TrxRepo A

--                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

--                        where A.Posted = 1 and A.SellBackDate <= @Date and TrxType = 2 and A.instrumentPK not in

--                        (

--                        Select InstrumentPK

--                        from ClosePrice 

--                        where status = 2 and Date = @Date

--                        )

--                        if @@Rowcount > 0

--                        BEGIN

--                        Select 'Cek Close Price untuk Data : ' + @msg Msg

--                        update MKBDTrails set LogMessages = 'Cek Close Price untuk Data : ' + @msg where MKBDTrailsPK = @NEWMKBDTrailsPK

--                        return

--                        END

--                        ELSE

--                        BEGIN

--                        select * from RL510BReverseRepo

--                        Select @MsgSuccess = @MsgSuccess + 'Company has REVERSE REPO <br/> '	

--                        Insert Into RL510BReverseRepo (HistoryPK,Status,Notes,MKBDTrailsPK,Date,CounterPartPK,InstrumentPK,InstrumentTypePK,BuyDate,

--                        SellBackDate,BuyAmount,Income,SellBackAmount,HaircutPercent,Volume,ClosePrice,MarketValue,AmountAfterHaircut,SellBackAmountAdjusted

--                        ,RankingLiabilities,MKBD01,MKBD02,MKBD03,MKBD09)

--                        Select 1,2,'',@NewMKBDTrailsPK,@Date,CounterPartPK,A.InstrumentPK,B.InstrumentTypePK,BuyDate,SellBackDate,BuyAmount,Income,SellBackAmount,

--                        Case when d.type = 1 then HcEquity.RGHaircutMKBD when d.type = 2 then HcObl.OBHaircutMKBD when d.type = 5 then HcSBN.SBNHaircutMKBD else 0 End

--                        ,Volume,

--                        C.ClosePriceValue,Volume * C.ClosePriceValue,0,0,0

--                        ,Case when d.type = 1 then HcEquity.RGMKBD01 when d.type = 2 then HcObl.OBMKBD01 when d.type = 5 then HcSBN.SBNMKBD01 else 0 End

--                        ,Case when d.type = 1 then HcEquity.RGMKBD02 when d.type = 2 then HcObl.OBMKBD02 when d.type = 5 then HcSBN.SBNMKBD02 else 0 End

--                        ,Case when d.type = 1 then HcEquity.RGMKBD03 when d.type = 2 then HcObl.OBMKBD03 when d.type = 5 then HcSBN.SBNMKBD03 else 0 End

--                        ,Case when d.type = 1 then HcEquity.RGMKBD09 when d.type = 2 then HcObl.OBMKBD09 when d.type = 5 then HcSBN.SBNMKBD09 else 0 End

		

--                        from TrxRepo A

--                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

--                        left join ClosePrice C on A.InstrumentPk = C.InstrumentPK and C.Status = 2

--                        Left join InstrumentType D on B.InstrumentTypePK = D.Type and D.status = 2

		

--                        Left join 

--                        (

--                        select InstrumentTypePK, HaircutFrom, HaircutTo, HaircutMKBD as RGHaircutMKBD, ConcentrationRisk as RGConcentrationRisk,    

--                        MKBD01 as RGMKBD01, MKBD02 as RGMKBD02, MKBD03 as RGMKBD03, MKBD09 as RGMKBD09    

--                        from MKBDSetup (nolock) A

--                        where [Date] = 

--                        (    

--                        select Max([Date]) as MaxDate from MKBDSetup (nolock)    

--                        where [Date] <= @Date and rlName = 'Rl510B'    

--                        )and rlName = 'Rl510B' and InstrumentTypePK = 1 and status = 2

--                        ) as HcEquity ON D.Type = HcEquity.InstrumentTypePK and [dbo].[FGetLastNAWCHaircut](@Date,A.InstrumentPK) = HcEquity.RGHaircutMKBD  

		

--                        Left Join

--                        (            

--                        select InstrumentTypePK, BondRating, HaircutMKBD as OBHaircutMKBD, ConcentrationRisk as OBConcentrationRisk,            

--                        MKBD01 as OBMKBD01, MKBD02 as OBMKBD02, MKBD03 as OBMKBD03, MKBD09 as OBMKBD09            

--                        from MKBDSetup  (nolock)            

--                        where [Date] = (                

--                        select Max([Date]) as MaxDate from MKBDSetup (nolock)                

--                        where [Date] <= @Date and RLName = 'Rl510B'                

--                        )and RLName = 'Rl510B' and InstrumentTypePK = 2 and status = 2

--                        ) as HcObl     

--                        ON D.Type = HcObl.InstrumentTypePK and     

--                        dbo.[FGetLastOBRating](@Date, A.InstrumentPK) = HcObl.BondRating 

	    

--                        Left Join

--                        (

--                        select InstrumentTypePK, SBNTenorFrom, SBNTenorTo, HaircutMKBD as SBNHaircutMKBD, ConcentrationRisk as SBNConcentrationRisk,      

--                        MKBD01 as SBNMKBD01, MKBD02 as SBNMKBD02, MKBD03 as SBNMKBD03, MKBD09 as SBNMKBD09      

--                        from MKBDSetup (nolock)      

--                        where [Date] =       

--                        (      

--                        select Max([Date]) as MaxDate       

--                        from MKBDSetup (nolock)      

--                        where [Date] <= @Date and RLName = 'Rl510B'       

--                        ) and RLName = 'Rl510B' and InstrumentTypePK = 5  and status = 2

--                        ) as HcSBN ON --St.StockType = HcSBN.StockType and     

--                        cast(datediff(dd, @Date, isnull(B.MaturityDate,@Date)) as float)/365 between HcSBN.SBNTenorFrom and HcSBN.SBNTenorTo       

		

--                        Where Posted = 1 and SellBackDate <= @Date and TrxType = 2

		

--                        -- UPDATE RL

--                        Update A Set

--                        A.AmountAfterHaircut = A.MarketValue * (1 - A.HaircutPercent/100) ,

--                        A.SellBackAmountAdjusted = isnull(Case When B.Type = 1 Then 1.2 When B.Type = 2 then 1.1

--                        When B.Type = 5 then 1.05 else 0 END,0) * SellBackAmount,

--                        RankingLiabilities = CASE WHEN SellBackAmountAdjusted > AmountAfterHaircut      

--                        THEN SellBackAmountAdjusted - AmountAfterHaircut ELSE 0 END

--                        From RL510BReverseRepo A LEFT JOIN InstrumentType B on A.InstrumentTypePK = B.InstrumentTypePK

--                        and B.Status = 2

--                        WHERE A.MKBDTrailsPK = @NewMKBDTrailsPK

		

--                        -- RL510 B Update MKBD01

--                        Set @ssql = ''

--                        declare A cursor for      

--                        --select cast(MKBD01 as varchar) + 'B', sum(BuyAmount + Income) as Value  

--                        select cast(MKBD01 as varchar) + 'B', ([BuyAmount])  as Value       

--                        from RL510bReverseRepo (nolock) where date = @date and MKBD01 > 0 

--                        and MKBDTrailsPK = @NewMKBDTrailsPK 

--                        group by MKBD01, BuyAmount      

--                        open A      



--                        fetch next from A      

--                        into @field, @value      



--                        while @@FETCH_STATUS = 0      

--                        begin       

--                        set @ssql = 'update MKBD01 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)                  

--                        exec (@ssql)      



--                        fetch next from A      

--                        into @field, @value      

--                        end      



--                        close A      

--                        deallocate A   

		

--                        -- RL510 B Update MKBD03

--                        set @ssql = ''

--                        declare A cursor for  

--                        select cast(MKBD03 as varchar) + 'B', sum(RankingLiabilities) as Value  

--                        from RL510bReverseRepo (nolock) where date = @date and MKBD03 > 0 

--                        and MKBDTrailsPK = @NewMKBDTrailsPK 

--                        group by MKBD03  



--                        open A  



--                        fetch next from A  

--                        into @field, @value  



--                        while @@FETCH_STATUS = 0  

--                        begin   

--                        set @ssql = 'update MKBD03 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''  + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)                  

--                        exec (@ssql)  



--                        fetch next from A  

--                        into @field, @value  

--                        end  



--                        close A  

--                        deallocate A 

		

--                        END

--                        END

                        -- Portfolio Checking REKSADANA

                        -- itung volume reksadana terakhir klo lebih > 0 baru jalan bawah



                      

                        delete #tmpReksadana where ValueDate <> @Date

                        Insert into #tmpReksadana

                        Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume,@Date

                        from (

                        select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume from trxPortfolio

                        where ValueDate <= @Date and Posted = 1 and InstrumentTypePK = 4 and trxType = 1 and Revised = 0 and status = 2

                        Group By InstrumentPK

                        UNION ALL

                        select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume from trxPortfolio

                        where ValueDate <= @Date and Posted = 1 and InstrumentTypePK = 4 and trxType = 2 and Revised = 0 and status = 2

                        Group By InstrumentPK

                        )A

                        Group By A.InstrumentPK

                        having sum(A.BuyVolume) - sum(A.SellVolume) > 0



                        if Exists

                        (

                        select * from #tmpReksadana	

                        )

                        BEGIN

                        -- Cek Close NAV and TotalNAVReksadana Sudah ada apa belum di hari generate



                        set @msg = ''

                        Select distinct @msg = @msg + B.ID + ', ' from #tmpReksadana A

                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

                        where A.instrumentPK not in

                        (

                        Select InstrumentPK

                        from ClosePrice 

                        where status = 2 and Date = @Date and ClosePriceValue > 0  and TotalNAVReksadana > 0

                        )

                        if @@Rowcount > 0

                        BEGIN

                        Select 'Cek Close Price untuk Data : ' + @msg Msg

                        update MKBDTrails set LogMessages = 'Cek Close Price untuk Data : ' + @msg where MKBDTrailsPK = @NEWMKBDTrailsPK

                        return

                        END

                        ELSE

                        BEGIN

                        -- Insert Data TO RL504 Reksadana From Portfolio join with MKBDSetup

                        Select @MsgSuccess = @MsgSuccess + 'Company has Reksadana <br/> '		



                        Insert into RL504(HistoryPK,Status,Notes,MKBDTrailsPK,Date,InstrumentPK,

                        ReksadanaTypePK,Volume,CloseNAV,MarketValue,TotalNAVReksadana,HaircutPercent,HaircutAmount,

                        ConcentrationRisk,ConcentrationLimit,affiliated,MKBD01,MKBD02,MKBD03,MKBD09)


                        Select 1,2,'',@NewMKBDTrailsPK,@Date,A.InstrumentPK,B.ReksadanaTypePK,A.LastVolume,

                        C.ClosePriceValue,A.LastVolume * C.ClosePriceValue,C.TotalNAVReksadana,E.RDHaircutMKBD,

                        (C.ClosePriceValue * A.LastVolume * isnull(E.RDHaircutMKBD/100,0)),E.RDConcentrationRisk,

                        E.RDConcentrationRisk * C.TotalNAVReksadana,B.Affiliated,E.RDMKBD01,E.RDMKBD02,E.RDMKBD03,E.RDMKBD09

                        From #tmpReksadana A 

                        Left Join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

                        Left Join ClosePrice C on A.InstrumentPK = C.InstrumentPK and C.Status = 2 and c.Date = @Date

                        Left join ReksadanaType D on B.ReksadanaTypePK = D.ReksadanaTypePK and D.Status = 2

                        Left Join (              

                        SELECT  ReksadanaTypePK,InstrumentTypePK, HaircutMKBD AS RDHaircutMKBD, ConcentrationRisk AS RDConcentrationRisk,                  

                        MKBD01 AS RDMKBD01, MKBD02 AS RDMKBD02, MKBD03 AS RDMKBD03, MKBD09 AS RDMKBD09             

                        FROM MKBDSetup (NOLOCK)              

                        WHERE RLName = 'RL504' and Status = 2 and [Date] = (  

                        select Max([Date]) as MaxDate from MKBDSetup (nolock)      

                        where [Date] <= @date and RLName = 'RL504' and status = 2     

                        )

                        )AS E ON  D.ReksadanaTypePK = E.ReksadanaTypePK

		

                        -- UPDATE RL504 RANGKING LIABILITIES

                        update rl      

                        set rl.RankingLiabilities =         

                        case when MarketValue > ConcentrationLimit           

                        then MarketValue - ConcentrationLimit else 0    end           

                        from RL504 rl     

                        where rl.Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

		

                        -- INSERT TO MKBD04

                        Declare @No int


		                        

		

                        DECLARE A CURSOR FOR  

                        select MKBDNo  

                        from ReksadanaType (nolock)  

                        where status = 2 

                        order by mkbdno  



                        OPEN A  

		  

                        FETCH NEXT FROM A  

                        INTO @No   

		  

                        WHILE @@FETCH_STATUS = 0  

                        BEGIN  

		

                        truncate table #MKBD04

                        insert into #MKBD04  

                        (MKBDTrailsPK,Date, CompanyID, CompanyName, DirectorName, MKBDNo,   

                        A, B, C, D, E, F, G, H,[lastUpdate])  



                        select @NewMKBDTrailsPK, @Date, @CompanyID, @CompanyName, @DirectorName, Rd.MKBDNo, 

                        rd.Description, isnull(IT.ISIN,''),case when RL.Affiliated is null then ''   

                        when RL.Affiliated = 0 then 'Tidak Terafiliasi'  

                        when RL.Affiliated = 1 then 'Afiliasi' end,   

                        isnull(MarketValue, 0), isnull(rl.TotalNAVReksadana, 0),  

                        'Kelebihan atas ' + cast(cast(Hc.ConcentrationRisk * 100 as int) as varchar) + '% NAB',  

                        isnull(ConcentrationLimit, 0), isnull(RankingLiabilities, 0),@timeNow

                        from ReksadanaType Rd  

			 

                        left join RL504 rl on rl.ReksadanaTypePK = Rd.ReksadanaTypePK and rl.Date = @Date   

                        left join Instrument IT on rl.InstrumentPK = IT.InstrumentPK  

                        left join   

                        (  

                        select ReksadanaTypePK, ConcentrationRisk   

                        from MKBDSetup (nolock)  

                        where RLName = 'RL504'  and status = 2

                        ) Hc on Rd.ReksadanaTypePK = Hc.ReksadanaTypePK  

                        where Rd.MKBDNo = @No and Rd.status = 2 and rl.MKBDTrailsPK = @NewMKBDTrailsPK



                        insert into MKBD04   

                        (MKBDTrailsPK, Date, CompanyID, CompanyName, DirectorName, [No],   

                        A, B, C, D, E, F, G, H, [LastUpdate])  

                        select MKBDTrailsPK, Date, CompanyID, CompanyName, DirectorName, cast(MKBDNo as varchar) + '.' + cast(No as varchar),   

                        A, B, C, D, E, F, G, H, @timeNow

                        from #MKBD04  

		   

                        FETCH NEXT FROM A  

                        INTO @No   

                        END  

		  

                        CLOSE A  

                        DEALLOCATE A 

		

                        -- RL Update MKBD 01

                        set @ssql = ''

		

                        declare A cursor for    

                        select cast(MKBD01 as varchar) + 'B', sum(MarketValue) as Value    

                        from RL504 (nolock) where date = @date and MKBD01 > 0 

                        and status =  2 and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD01    



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD01 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + '''' + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)           

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A   

		

                        -- RL Update MKBD 03

                        set @ssql = ''

                        declare A cursor for  

                        select cast(MKBD03 as varchar) + 'B', sum(RankingLiabilities) as Value  

                        from RL504 (nolock) where date = @date and MKBD03 > 0

                        and MKBDTrailsPK = @NewMKBDTrailsPK 

                        group by MKBD03  



                        open A  



                        fetch next from A  

                        into @field, @value  



                        while @@FETCH_STATUS = 0  

                        begin   

                        set @ssql = 'update MKBD03 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''  + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)   

                        exec (@ssql)  



                        fetch next from A  

                        into @field, @value  

                        end  



                        close A  

                        deallocate A   

		

                        -- RL Update MKBD 09

                        set @ssql = ''

                        declare A cursor for    

                        select cast(MKBD09 as varchar) + 'E', sum(MarketValue) as Value    

                        from RL504 (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09    

                        Union all  

                        select cast(MKBD09 as varchar) + 'G', sum(HaircutAmount) as Value    

                        from RL504 (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09    

                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD09 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)         

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A   

				

                        END

                        END



                        -- Portfolio Checking DEPOSITO

                        delete #tmpDeposito where ValueDate <> @Date

                        Insert into #tmpDeposito

                        
                        Select A.InstrumentPK,isnull(sum(A.BuyAmount) - sum(A.SellAmount),0) LastAmount,MaturityDate,@Date from (

                        select InstrumentPK,sum(Amount) BuyAmount,0 SellAmount,MaturityDate from trxPortfolio

                        where ValueDate <= @Date and MaturityDate > @Date and Posted = 1 and InstrumentTypePK = 3 and trxType in (1,3) and Revised = 0 and status = 2

                        Group By InstrumentPK,MaturityDate

                        UNION ALL

                        select InstrumentPK,0 BuyAmount,sum(Amount) SellAmount,MaturityDate from trxPortfolio

                        where ValueDate <= @Date and MaturityDate > @Date and Posted = 1 and InstrumentTypePK = 3 and trxType = 2 and Revised = 0 and status = 2

                        Group By InstrumentPK,MaturityDate

                        )A

                        Group By A.InstrumentPK,MaturityDate

                        having sum(A.BuyAmount) - sum(A.SellAmount) > 0



                        if Exists

                        (

                        select * from #tmpDeposito	

                        )

                        BEGIN

                        Select @MsgSuccess = @MsgSuccess + 'Company has Deposito <br/> '

                        Insert into Rl510CDeposito(HistoryPK,Status,Notes,MKBDTrailsPK,Date,InstrumentPK,

                        DepositoTypePK,MarketValue,DueDate)

		

                        Select 1,2,'',@NewMKBDTrailsPK,@Date,A.InstrumentPK,B.DepositoTypePK,A.LastAmount,

                        A.MaturityDate From #tmpDeposito A

                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2

		

                        --RL510CDepositoUpdateData

                        update rl     

                        set isUnderDuedateAmount = case when datediff(dd, Date, DueDate) <= TenorLimit then MarketValue else 0 end,    

                        notUnderDuedateAmount = case when datediff(dd, Date, DueDate) <= TenorLimit then 0 else MarketValue end,    

                        quaranteedAmount =     

                        case when datediff(dd, Date, DueDate) <= TenorLimit then 0     

                        else     

                        case when MarketValue >= CollateralLimit then CollateralLimit     

                        else MarketValue end     

                        end     

                        from RL510cDeposito rl    

                        left join DepositoType DnT on rl.DepositoTypePK = DnT.DepositoTypePK   

                        where DnT.DEPType in ('DEP-BUM') and rl.Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK

	

                        update rl set   

                        NotLiquidatedAmount = case when isLiquidated = 0 then NotUnderDuedateAmount - QuaranteedAmount else 0 end,    

                        LiquidatedAmount = case when isLiquidated = 1 then NotUnderDuedateAmount - QuaranteedAmount else 0 end    

                        from RL510cDeposito rl    

                        left join DepositoType DnT on rl.DepositoTypePK = DnT.DepositoTypePK   

                        where DnT.DEPType in ('DEP-BUM') and rl.Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK


                        insert into #IsUnderDueDateAmount(
                        [HistoryPK],[Status],[Notes],[Date],[MKBDTrailsPK],[MKBD01],
                        [MKBD02],[MKBD03],[MKBD09],[InstrumentPK],[DepositoTypePK],[IsLiquidated],[DueDate],[MarketValue],[IsUnderDuedateAmount],
                        [NotUnderDuedateAmount],[QuaranteedAmount],[NotLiquidatedAmount],[LiquidatedAmount],[HaircutPercent],[HaircutAmount],
                        [AfterHaircutAmount],[RankingLiabilities],[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[ApprovedUsersID],
                        [ApprovedTime],[VoidUsersID],[VoidTime],[DBUserID],[DBTerminalID],[LastUpdate],[LastUpdateDB])
    
                        select A.HistoryPK,A.Status,A.Notes,Date,MKBDTrailsPK,MKBD01,
                        MKBD02,MKBD03,MKBD09,InstrumentPK,A.DepositoTypePK,IsLiquidated,DueDate,MarketValue,IsUnderDuedateAmount,
                        NotUnderDuedateAmount,QuaranteedAmount,NotLiquidatedAmount,LiquidatedAmount,HaircutPercent,HaircutAmount,
                        AfterHaircutAmount,RankingLiabilities,A.EntryUsersID,A.EntryTime,A.UpdateUsersID,A.UpdateTime,A.ApprovedUsersID,
                        A.ApprovedTime,A.VoidUsersID,A.VoidTime,A.DBUserID,A.DBTerminalID,A.LastUpdate,A.LastUpdateDB


                        from RL510cDeposito A 

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK  

                        where DEPType in ('DEP-BUM') and Date = @Date and IsUnderDuedateAmount > 0    

                        and MKBD01 = 0 and MKBD09 = 0    

		

                        update rl set   

                        HaircutPercent = DEPHaircutMKBD, MarketValue = IsUnderDuedateAmount,    

                        HaircutAmount = IsUnderDuedateAmount * (DEPHaircutMKBD/100), AfterHaircutAmount = IsUnderDuedateAmount * (1 - (DEPHaircutMKBD/100)),    

                        rl.MKBD01 = isnull(DepMKBD01, 0), rl.MKBD02 = isnull(DepMKBD02, 0),    

                        rl.MKBD03 = isnull(DepMKBD03, 0), rl.MKBD09 = isnull(DepMKBD09, 0),    

                        NotUnderDuedateAmount = 0, QuaranteedAmount = 0, NotLiquidatedAmount = 0, LiquidatedAmount = 0    

                        from #IsUnderDueDateAmount rl    

                        left join DepositoType DnT on rl.DepositoTypePK = DnT.DepositoTypePK  and Dnt.Status = 2 

                        left join 

                        (    

                        select A.DepositoTypePK ,HaircutMKBD as DEPHaircutMKBD,     

                        MKBD01 as DepMKBD01, MKBD02 as DepMKBD02, MKBD03 as DepMKBD03, MKBD09 as DepMKBD09    

                        from MKBDSetup A (nolock) 

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK and B.status = 2   

                        where [Date] = (  

                        select Max([Date]) as MaxDate from MKBDSetup (nolock)    

                        where [Date] <= @Date and RLName = 'RL510cDeposito'    

                        ) and RLName = 'RL510cDeposito' and DEPType in ('DEP-BUM') and BitDEPisDue = 1 and A.Status = 2   

                        ) as HcDep ON DnT.DepositoTypePK = HcDep.DepositoTypePK 

		

                        insert into #QuaranteedAmount(
                        [HistoryPK],[Status],[Notes],[Date],[MKBDTrailsPK],[MKBD01],
                        [MKBD02],[MKBD03],[MKBD09],[InstrumentPK],[DepositoTypePK],[IsLiquidated],[DueDate],[MarketValue],[IsUnderDuedateAmount],
                        [NotUnderDuedateAmount],[QuaranteedAmount],[NotLiquidatedAmount],[LiquidatedAmount],[HaircutPercent],[HaircutAmount],
                        [AfterHaircutAmount],[RankingLiabilities],[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[ApprovedUsersID],
                        [ApprovedTime],[VoidUsersID],[VoidTime],[DBUserID],[DBTerminalID],[LastUpdate],[LastUpdateDB])
    
                        select A.HistoryPK,A.Status,A.Notes,Date,MKBDTrailsPK,MKBD01,
                        MKBD02,MKBD03,MKBD09,InstrumentPK,A.DepositoTypePK,IsLiquidated,DueDate,MarketValue,IsUnderDuedateAmount,
                        NotUnderDuedateAmount,QuaranteedAmount,NotLiquidatedAmount,LiquidatedAmount,HaircutPercent,HaircutAmount,
                        AfterHaircutAmount,RankingLiabilities,A.EntryUsersID,A.EntryTime,A.UpdateUsersID,A.UpdateTime,A.ApprovedUsersID,
                        A.ApprovedTime,A.VoidUsersID,A.VoidTime,A.DBUserID,A.DBTerminalID,A.LastUpdate,A.LastUpdateDB


                        from RL510cDeposito  A   

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK  

                        where DEPType in ('DEP-BUM') and Date = @Date and QuaranteedAmount > 0    

                        and MKBD01 = 0 and MKBD09 = 0    



                        update rl set   

                        HaircutPercent = DEPHaircutMKBD, MarketValue = QuaranteedAmount,    

                        HaircutAmount = QuaranteedAmount * (DEPHaircutMKBD/100), AfterHaircutAmount = QuaranteedAmount * (1 - (DEPHaircutMKBD/100)),    

                        rl.MKBD01 = isnull(DepMKBD01, 0), rl.MKBD02 = isnull(DepMKBD02, 0),    

                        rl.MKBD03 = isnull(DepMKBD03, 0), rl.MKBD09 = isnull(DepMKBD09, 0),    

                        NotUnderDuedateAmount = QuaranteedAmount, IsUnderDuedateAmount = 0, NotLiquidatedAmount = 0, LiquidatedAmount = 0    

                        from #QuaranteedAmount rl    

                        left join DepositoType DnT on rl.DepositoTypePK = DnT.DepositoTypePK   

                        left join (    

                        select A.DepositoTypePK, HaircutMKBD as DEPHaircutMKBD, MKBD01 as DepMKBD01,   

                        MKBD02 as DepMKBD02, MKBD03 as DepMKBD03, MKBD09 as DepMKBD09    

                        from MKBDSetup A (nolock)  

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK and B.status = 2  

                        where [Date] = (  

                        select Max([Date]) as MaxDate from [MKBDSetup] (nolock)    

                        where [Date] <= @Date  and RLName = 'RL510cDeposito'    

                        )and RLName = 'RL510cDeposito'  and DEPType in ('DEP-BUM') and BitDEPisGuaranteed = 1    

                        ) as HcDep ON DnT.DepositoTypePK = HcDep.DepositoTypePK

		

                        insert into RL510cDeposito      

                        (HistoryPK,Status,Notes,MKBDTrailsPK,Date, MKBD01, MKBD02, MKBD03, MKBD09, InstrumentPK, DepositoTypePK, IsLiquidated, DueDate, MarketValue, IsUnderDuedateAmount,   

                        NotUnderDuedateAmount, QuaranteedAmount, NotLiquidatedAmount, LiquidatedAmount, HaircutPercent, HaircutAmount, AfterHaircutAmount, RankingLiabilities)      

                        select 1,2,'',@NewMKBDTrailsPK,Date, MKBD01, MKBD02, MKBD03, MKBD09, InstrumentPK, DepositoTypePK, IsLiquidated, DueDate, MarketValue, IsUnderDuedateAmount,   

                        NotUnderDuedateAmount, QuaranteedAmount, NotLiquidatedAmount, LiquidatedAmount, HaircutPercent, HaircutAmount, AfterHaircutAmount, RankingLiabilities         

                        from (   

                        select * from #IsUnderDueDateAmount    

                        union all    

                        select * from #QuaranteedAmount    

                        ) Dep 
                        --order by RL510CDepositoPK   





                        insert into #NotLiquidatedAmount(
                        [HistoryPK],[Status],[Notes],[Date],[MKBDTrailsPK],[MKBD01],
                        [MKBD02],[MKBD03],[MKBD09],[InstrumentPK],[DepositoTypePK],[IsLiquidated],[DueDate],[MarketValue],[IsUnderDuedateAmount],
                        [NotUnderDuedateAmount],[QuaranteedAmount],[NotLiquidatedAmount],[LiquidatedAmount],[HaircutPercent],[HaircutAmount],
                        [AfterHaircutAmount],[RankingLiabilities],[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[ApprovedUsersID],
                        [ApprovedTime],[VoidUsersID],[VoidTime],[DBUserID],[DBTerminalID],[LastUpdate],[LastUpdateDB])
    
                        select A.HistoryPK,A.Status,A.Notes,Date,MKBDTrailsPK,MKBD01,
                        MKBD02,MKBD03,MKBD09,InstrumentPK,A.DepositoTypePK,IsLiquidated,DueDate,MarketValue,IsUnderDuedateAmount,
                        NotUnderDuedateAmount,QuaranteedAmount,NotLiquidatedAmount,LiquidatedAmount,HaircutPercent,HaircutAmount,
                        AfterHaircutAmount,RankingLiabilities,A.EntryUsersID,A.EntryTime,A.UpdateUsersID,A.UpdateTime,A.ApprovedUsersID,
                        A.ApprovedTime,A.VoidUsersID,A.VoidTime,A.DBUserID,A.DBTerminalID,A.LastUpdate,A.LastUpdateDB


                        from RL510cDeposito A       

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK  and B.status = 2

                        where B.DEPType in ('DEP-BUM') and A.Date = @Date and A.NotLiquidatedAmount > 0      

                        and MKBD01 = 0 and MKBD09 = 0  



                        update rl set   

                        HaircutPercent = isnull(DEPHaircutMKBD,0), MarketValue = NotLiquidatedAmount,      

                        HaircutAmount = isnull(NotLiquidatedAmount * (DEPHaircutMKBD/100),0), AfterHaircutAmount = isnull(NotLiquidatedAmount * (1 - (DEPHaircutMKBD/100)),0),      

                        rl.MKBD01 = isnull(DepMKBD01, 0), rl.MKBD02 = isnull(DepMKBD02, 0),      

                        rl.MKBD03 = isnull(DepMKBD03, 0), rl.MKBD09 = isnull(DepMKBD09, 0),      

                        NotUnderDuedateAmount = NotLiquidatedAmount, IsUnderDuedateAmount = 0, QuaranteedAmount = 0, LiquidatedAmount = 0      

                        from #NotLiquidatedAmount rl      

                        left join DepositoType DnT on rl.DepositoTypePK = DnT.DepositoTypePK   and Dnt.Status = 2

                        left join

                        (      

                        select  A.DepositoTypePK , HaircutMKBD as DEPHaircutMKBD,       

                        MKBD01 as DepMKBD01, MKBD02 as DepMKBD02, MKBD03 as DepMKBD03, MKBD09 as DepMKBD09      

                        from MKBDSetup A (nolock) 

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK and B.status = 2

                        where RLName = 'RL510cDeposito' and B.DEPType in ('DEP-BUM') and BitDEPisGuaranteed = 0 and BitDEPisDue = 0 and BitDEPisPailit = 0      

                        and A.status = 2 and [Date] = (  

                        select Max([Date]) as MaxDate from MKBDSetup (nolock)      

                        where [Date] <= @Date and RLName = 'RL510cDeposito'  and status = 2     

                        )

                        ) as HcDep ON DnT.DepositoTypePK = HcDep.DepositoTypePK    

		

                        insert into #LiquidatedAmount(
                        [HistoryPK],[Status],[Notes],[Date],[MKBDTrailsPK],[MKBD01],
                        [MKBD02],[MKBD03],[MKBD09],[InstrumentPK],[DepositoTypePK],[IsLiquidated],[DueDate],[MarketValue],[IsUnderDuedateAmount],
                        [NotUnderDuedateAmount],[QuaranteedAmount],[NotLiquidatedAmount],[LiquidatedAmount],[HaircutPercent],[HaircutAmount],
                        [AfterHaircutAmount],[RankingLiabilities],[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[ApprovedUsersID],
                        [ApprovedTime],[VoidUsersID],[VoidTime],[DBUserID],[DBTerminalID],[LastUpdate],[LastUpdateDB])
    
                        select A.HistoryPK,A.Status,A.Notes,Date,MKBDTrailsPK,MKBD01,
                        MKBD02,MKBD03,MKBD09,InstrumentPK,A.DepositoTypePK,IsLiquidated,DueDate,MarketValue,IsUnderDuedateAmount,
                        NotUnderDuedateAmount,QuaranteedAmount,NotLiquidatedAmount,LiquidatedAmount,HaircutPercent,HaircutAmount,
                        AfterHaircutAmount,RankingLiabilities,A.EntryUsersID,A.EntryTime,A.UpdateUsersID,A.UpdateTime,A.ApprovedUsersID,
                        A.ApprovedTime,A.VoidUsersID,A.VoidTime,A.DBUserID,A.DBTerminalID,A.LastUpdate,A.LastUpdateDB
   

                        from RL510cDeposito A 

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK  and B.status = 2

                        where DEPType in ('DEP-BUM') and Date = @Date and LiquidatedAmount > 0      

                        and MKBD01 = 0 and MKBD09 = 0      



                        update rl set   

                        HaircutPercent = DEPHaircutMKBD, MarketValue = LiquidatedAmount,      

                        HaircutAmount = LiquidatedAmount * (DEPHaircutMKBD/100), AfterHaircutAmount = LiquidatedAmount * (1 - (DEPHaircutMKBD/100)),      

                        rl.MKBD01 = isnull(DepMKBD01, 0), rl.MKBD02 = isnull(DepMKBD02, 0),      

                        rl.MKBD03 = isnull(DepMKBD03, 0), rl.MKBD09 = isnull(DepMKBD09, 0),      

                        NotUnderDuedateAmount = LiquidatedAmount, IsUnderDuedateAmount = 0, QuaranteedAmount = 0, NotLiquidatedAmount = 0  

                        from #LiquidatedAmount rl      

                        left join DepositoType DnT on rl.DepositoTypePK = DnT.DepositoTypePK   and Dnt.Status = 2  

                        left join 

                        (      

                        select  A.DepositoTypePK , HaircutMKBD as DEPHaircutMKBD,       

                        MKBD01 as DepMKBD01, MKBD02 as DepMKBD02, MKBD03 as DepMKBD03, MKBD09 as DepMKBD09      

                        from MKBDSetup A (nolock) 

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK and B.status = 2

                        where RLName = 'RL510cDeposito' and B.DEPType in ('DEP-BUM') and BitDEPisPailit = 1      

                        and A.status = 2 and [Date] = (  

                        select Max([Date]) as MaxDate from MKBDSetup (nolock)      

                        where [Date] <= @Date and RLName = 'RL510cDeposito' and status = 2      

                        )

                        ) as HcDep ON DnT.DepositoTypePK = HcDep.DepositoTypePK

		

                        insert into RL510cDeposito      

                        (HistoryPK,Status,Notes,MKBDTrailsPK,Date, MKBD01, MKBD02, MKBD03, MKBD09, InstrumentPK, DepositoTypePK, IsLiquidated, DueDate, MarketValue, IsUnderDuedateAmount,   

                        NotUnderDuedateAmount, QuaranteedAmount, NotLiquidatedAmount, LiquidatedAmount, HaircutPercent, HaircutAmount, AfterHaircutAmount, RankingLiabilities)      

                        select 1,2,'',@NewMKBDTrailsPK,Date, MKBD01, MKBD02, MKBD03, MKBD09, InstrumentPK, DepositoTypePK, IsLiquidated, DueDate, MarketValue, IsUnderDuedateAmount,   

                        NotUnderDuedateAmount, QuaranteedAmount, NotLiquidatedAmount, LiquidatedAmount, HaircutPercent, HaircutAmount, AfterHaircutAmount, RankingLiabilities       

                        from (  

                        select * from #NotLiquidatedAmount      

                        union all      

                        select * from #LiquidatedAmount      

                        ) Dep 
                        --order by RL510CDepositoPK     



                        delete RL510cDeposito  

                        from RL510cDeposito       

                        left join DepositoType on RL510cDeposito.DepositoTypePK = DepositoType.DepositoTypePK  and  DepositoType.status = 2

                        where DEPType in ('DEP-BUM') and Date = @Date      

                        and MKBD01 = 0 and MKBD09 = 0   

		

                        --RL510CUpdateMKBD01

                        Set @ssql = ''

		

                        declare A cursor for    

                        select cast(MKBD01 as varchar) + 'B', sum(MarketValue) as Value    

                        from RL510cDeposito (nolock) where date = @date and MKBD01 > 0 

                        and status =  2 and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD01    



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD01 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)         

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end      

	

		

                        close A    

                        deallocate A   

		

                        --RL510CUpdateMKBD09

		

                        Set @ssql = ''

                        declare A cursor for    

                        select cast(MKBD09 as varchar) + 'E', sum(MarketValue) as Value    

                        from RL510cDeposito (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09   

                        union all  

                        select cast(MKBD09 as varchar) + 'G', sum(haircutamount) as Value    

                        from RL510cDeposito (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09   

                        open A    

                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD09 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)     

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A   



                        END		

                        -- Portfolio Checking BOND , Corporate Bond berhubungan dengan RATING

                        

                        delete #tmpBond where ValueDate <> @Date

                        Insert into #tmpBond ([InstrumentPK],[Volume],ValueDate)

                        Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) Volume,@Date from (

	                    select A.InstrumentPK,sum(Volume) BuyVolume,0 SellVolume from trxPortfolio A

	                    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2

	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.Status = 2

	                    where ValueDate <= @Date and Posted = 1 and c.InstrumentTypePK in (2,3,8,9,11,13,14,15) and trxType = 1 and Revised = 0 and A.status = 2 and A.MaturityDate >= @Date

	                    Group By A.InstrumentPK

	                    UNION ALL

	                    select A.InstrumentPK,0 BuyVolume,sum(Volume) SellVolume from trxPortfolio A

	                    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2

	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.Status = 2

	                    where ValueDate <= @Date and Posted = 1 and c.InstrumentTypePK  in (2,3,8,9,11,13,14,15)  and trxType = 2 and Revised = 0 and A.status = 2 and A.MaturityDate >= @Date

	                    Group By A.InstrumentPK

	                    )A 



	                    Group By A.InstrumentPK

	                    having sum(A.BuyVolume) - sum(A.SellVolume) > 0

                        if Exists

                        (

                        select * from #tmpBond	

                        )

                        BEGIN

                        -- Cek Close Price Bond and Bond Rating

                        set @msg = ''

                        Select distinct @msg = @msg + B.ID + ', ' from #tmpBond A

                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

                        where A.instrumentPK not in

                        (

                        Select InstrumentPK

                        from ClosePrice 

                        where status = 2 and Date = @Date and ClosePriceValue > 0 

                        )

                        if @@Rowcount > 0

                        BEGIN

                        Select 'Cek Close Price untuk Data : ' + @msg Msg

                        update MKBDTrails set LogMessages = 'Cek Close Price untuk Data : ' + @msg where MKBDTrailsPK = @NEWMKBDTrailsPK

                        return

                        END

                        ELSE

                        BEGIN

                        Select @MsgSuccess = @MsgSuccess + 'Company has Corporate Bond <br/> '

                        Insert into Rl510cBond(HistoryPK,Status,Notes,MKBDTrailsPK,Date,InstrumentPK,

                        InstrumentTypePK,HoldingPK,ObRating,Volume)

		

                        Select 1,2,'',@NewMKBDTrailsPK,@Date,A.InstrumentPK,

                        B.InstrumentTypePK,B.HoldingPK,B.BondRating,A.Volume

                        From #tmpBond A

                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2

		

                        --RL510CBondUpdateData

		 

                        update rl set       

                        rl.price = dbo.[FGetLastClosePriceForFundPosition](@Date,rl.InstrumentPK),    

                        rl.obrating =  CASE WHEN dbo.[FGetLastOBRating](@Date, rl.InstrumentPK) = '' Then st.BondRating Else dbo.[FGetLastOBRating](@Date, rl.InstrumentPK) END,    

                        rl.InstrumentTypePK = isnull(St.instrumentTypePK, 0),     

                        rl.MarketValue = rl.volume *  (dbo.[FGetLastClosePriceForFundPosition](@Date,rl.InstrumentPK))/100,    

                        rl.HoldingPK = isnull(St.HoldingPK,0),          

                         rl.HaircutPercent = isnull(case when St.CurrencyPK <> 1 then 90 else HcObl.OBHaircutMKBD end, 0),   

                        rl.ConcentrationRisk = isnull(HcObl.OBConcentrationRisk, 0),          

                        rl.MKBD01 = case when St.CurrencyPK <> 1 then 82 else  isnull(obMKBD01, 0) end,     

                        rl.MKBD02 = isnull(obMKBD02, 0),          

                        rl.MKBD03 = case when St.CurrencyPK <> 1 then 23 else  isnull(obMKBD03, 0) end,     

                        rl.MKBD09 = case when St.CurrencyPK <> 1 then 56 else  isnull(obMKBD09, 0) end       

                        from RL510CBond rl         

                        left join Instrument St on rl.InstrumentPK = St.InstrumentPK and st.status = 2

                        left join InstrumentType It on case when st.instrumentTypePK in (2,3,8,9,11,13,14,15) then 2 end = It.instrumentTypePK and It.status = 2

                        left join 

                        (            

                        select InstrumentTypePK, BondRating, HaircutMKBD as OBHaircutMKBD, ConcentrationRisk as OBConcentrationRisk,            

                        MKBD01 as OBMKBD01, MKBD02 as OBMKBD02, MKBD03 as OBMKBD03, MKBD09 as OBMKBD09            

                        from MKBDSetup  (nolock)            

                        where [Date] = (                

                        select Max([Date]) as MaxDate from MKBDSetup (nolock)                

                        where [Date] <= @date and RLName = 'RL510CBond'                

                        )and RLName = 'RL510CBond' and status = 2

                        ) as HcObl     

                        ON case when It.instrumentTypePK in (2,3,8,9,11,13,14,15) then 2 end = HcObl.InstrumentTypePK and     

                        dbo.[FGetLastOBRating](@Date, rl.InstrumentPK) = HcObl.BondRating 

                        where rl.Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

		 

		



                        update rl set   

                        rl.HaircutAmount = rl.MarketValue * (rl.HaircutPercent/100),     

                        rl.AfterHaircutAmount = rl.MarketValue * (1-(rl.HaircutPercent/100)),    

                        rl.TotalEquity = @TotalEquity  

                        from RL510cBond rl where rl.Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK    



                        update rl set   

                        rl.RankingLiabilities =    

                        case when MarketValue > TotalEquity * ConcentrationRisk    

                        then MarketValue - (TotalEquity * ConcentrationRisk)    

                        else 0 end     

                        from RL510cBond rl where rl.Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK     

		

                        -- BOND UPDATE MKBD 01 

                        set @ssql = ''

                        declare A cursor for    

                        select cast(MKBD01 as varchar) + 'B', sum(MarketValue) as Value    

                        from RL510cBond (nolock) where date = @date and MKBD01 > 0 

                        and status =  2 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD01    



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin  


                        set @ssql = 'update MKBD01 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)         

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A   

		

                        -- BOND UPDATE MKBD 03 

                        set @ssql = ''

                        declare A cursor for  

                        select cast(MKBD03 as varchar) + 'B', sum(RankingLiabilities) as Value  

                        from RL510cBond (nolock) where date = @date and MKBD03 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK 

                        group by MKBD03  

		

                        open A  



                        fetch next from A  

                        into @field, @value  



                        while @@FETCH_STATUS = 0  

                        begin   

                        set @ssql = 'update MKBD03 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''  + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)        

                        exec (@ssql)  



                        fetch next from A  

                        into @field, @value  

                        end  



                        close A  

                        deallocate A   

		

                        --BOND UPDATE MKBD09

                        declare A cursor for    

                        select cast(MKBD09 as varchar) + 'E', sum(MarketValue) as Value    

                        from RL510cBond (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09  

                        union all  

                        select cast(MKBD09 as varchar) + 'G', sum(HaircutAmount) as Value    

                        from RL510cBond (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09    



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD09 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + '''' +' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)        

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A    

		

                        END

                        END



                        -- Portfolio Checking EQUITY

                        delete #tmpEquity where ValueDate <> @Date

                        Insert into #tmpEquity



                        Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume,@Date from (

                        select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume from trxPortfolio

                        where ValueDate <= @Date and Posted = 1 and InstrumentTypePK = 1 and trxType = 1 and status = 2 and Revised = 0

                        Group By InstrumentPK

                        UNION ALL

                        select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume from trxPortfolio

                        where ValueDate <= @Date and Posted = 1 and InstrumentTypePK = 1 and trxType = 2 and status = 2 and Revised = 0

                        Group By InstrumentPK

                        )A

                        Group By A.InstrumentPK

                        having sum(A.BuyVolume) - sum(A.SellVolume) > 0

                        if Exists

                        (

                        select * from #tmpEquity	

                        )

                        BEGIN



                        -- Cek Close Price Equity

                        set @msg = ''

                        Select distinct @msg = @msg + B.ID + ', ' from #tmpEquity A

                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

                        where A.instrumentPK not in

                        (

                        Select InstrumentPK

                        from ClosePrice 

                        where status = 2 and Date = @Date and ClosePriceValue > 0 

                        )

                        if @@Rowcount > 0

                        BEGIN

                        Select 'Cek Close Price untuk Data : ' + @msg Msg

                        update MKBDTrails set LogMessages = 'Cek Close Price untuk Data : ' + @msg where MKBDTrailsPK = @NEWMKBDTrailsPK

                        return

                        END

                        ELSE

                        BEGIN

                        Select @MsgSuccess = @MsgSuccess + 'Company has Equity <br/> '

                        Insert into Rl510cEquity(HistoryPK,Status,Notes,MKBDTrailsPK,Date,InstrumentPK,

                        InstrumentTypePK,HoldingPK,Volume)

		

                        Select 1,2,'',@NewMKBDTrailsPK,@Date,A.InstrumentPK,

                        B.InstrumentTypePK,B.HoldingPK,A.LastVolume

                        From #tmpEquity A

                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2



                        --Update Rl510CEquity

                        update rl set   

                        rl.ClosePrice = isnull(CP.ClosePriceValue, 0), rl.MarketValue = isnull(CP.ClosePriceValue, 0) * rl.Volume,   

                        rl.HaircutPercent = isnull(hc.haircut,100), rl.ConcentrationRisk = isnull(RGConcentrationRisk, 0),    

                        rl.MKBD01 = isnull(RGMKBD01, 0), rl.MKBD02 = isnull(RGMKBD02, 0),    

                        rl.MKBD03 = isnull(RGMKBD03, 0), rl.MKBD09 = isnull(RGMKBD09, 0) 

                        from Rl510cEquity rl    

                        left join Instrument St on rl.InstrumentPK = St.InstrumentPK and st.status = 2 

                        left join InstrumentType It on st.InstrumentTypePK = It.InstrumentTypePK

                        left join HaircutMKBD HC on RL.InstrumentPK = HC.InstrumentPK and HC.Date  = (select Max(Date) from HaircutMKBD where Date <= @Date and Status = 2) and HC.status = 2

                        left join 

                        (    

                        select InstrumentPK, ClosePriceValue from [ClosePrice] (nolock)    

                        where Date = @Date  

                        ) as CP ON St.InstrumentPK = CP.InstrumentPK   

                        left join 

                        (  

                        select InstrumentTypePK, HaircutFrom, HaircutTo, HaircutMKBD as RGHaircutMKBD, ConcentrationRisk as RGConcentrationRisk,    

                        MKBD01 as RGMKBD01, MKBD02 as RGMKBD02, MKBD03 as RGMKBD03, MKBD09 as RGMKBD09    

                        from MKBDSetup (nolock) A

                        where [Date] = 

                        (    

                        select Max([Date]) as MaxDate from MKBDSetup (nolock)    

                        where [Date] <= @Date and rlName = 'RL510cEquity'    

                        )and rlName = 'RL510cEquity' 

                        ) as HcStock ON It.Type = HcStock.InstrumentTypePK and hc.haircut = HcStock.RGHaircutMKBD  

                        where rl.Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

		

                        --UPDATE Rl510cEquity RL

		

		

                        update rl set   

                        rl.HaircutAmount = rl.MarketValue * (rl.HaircutPercent/100),   

                        rl.AfterHaircutAmount = rl.MarketValue * (1-(rl.HaircutPercent/100)),  

                        rl.TotalEquity = @TotalEquity

                        from Rl510cEquity rl  

                        where rl.Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK



                        update rl set   

                        rl.RankingLiabilities =  

                        case when MarketValue > TotalEquity * ConcentrationRisk  

                        then MarketValue - (TotalEquity * ConcentrationRisk)  

                        else 0 end   

                        from Rl510cEquity rl  

                        where rl.Date = @Date   and MKBDTrailsPK = @NewMKBDTrailsPK

	
                        

                        --RL510cEquity Update 01

                        set @ssql = ''

                        declare A cursor for   



                        select cast(MKBD01 as varchar) + 'B', sum(MarketValue) as Value    

                        from RL510cEquity (nolock) where date = @date and MKBD01 > 0 

                        and status =  2 and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD01    



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD01 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)            

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A    

		

                        --RL510cEquity Update MKBD 03

                        set @ssql = ''

                        declare A cursor for  

                        select cast(MKBD03 as varchar) + 'B', sum(RankingLiabilities) as Value  

                        from RL510cEquity (nolock) where date = @date and MKBD03 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK 

                        group by MKBD03  



                        open A  



                        fetch next from A  

                        into @field, @value  



                        while @@FETCH_STATUS = 0  

                        begin   

                        set @ssql = 'update MKBD03 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''    + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)        

                        exec (@ssql)  



                        fetch next from A  

                        into @field, @value  

                        end  



                        close A  

                        deallocate A 

		

                        --RL510cEquity Update 09

                        set @ssql = ''

                        declare A cursor for    

                        select cast(MKBD09 as varchar) + 'E', sum(MarketValue) as Value    

                        from RL510cEquity (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09    

                        union all  

                        select cast(MKBD09 as varchar) + 'G', sum(HaircutAmount) as Value    

                        from RL510cEquity (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09    

                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD09 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''    + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)     

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A   



		

                        END

                        END



                        -- Portfolio Checking SBN / GOVERNMENT BOND

                        

                        Insert into #tmpSBN

                        Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) Volume,A.MaturityDate,@Date from (

                        select A.InstrumentPK,sum(Volume) BuyVolume,0 SellVolume,A.MaturityDate from trxPortfolio A

                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2

                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.Status = 2

                        where ValueDate <= @Date and Posted = 1 and c.Type = 5 and trxType = 1 and Revised = 0 and A.status = 2

                        Group By A.InstrumentPK,A.MaturityDate

                        UNION ALL

                        select A.InstrumentPK,0 BuyVolume,sum(Volume) SellVolume,A.MaturityDate from trxPortfolio A

                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2

                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.Status = 2

                        where ValueDate <= @Date and Posted = 1 and c.Type = 5 and trxType = 2 and Revised = 0 and A.status = 2

                        Group By A.InstrumentPK,A.MaturityDate

                        )A 

                        Group By A.InstrumentPK,A.MaturityDate

                        having sum(A.BuyVolume) - sum(A.SellVolume) > 0

                        if Exists

                        (

                        select * from #tmpSBN	

                        )

                        BEGIN

                        -- Cek Close Price Bond and Bond Rating

                        set @msg = ''

                        Select distinct @msg = @msg + B.ID + ', ' from #tmpSBN A

                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

                        where A.instrumentPK not in

                        (

                        Select InstrumentPK

                        from ClosePrice 

                        where status = 2 and Date = @Date and ClosePriceValue > 0 

                        )

                        if @@Rowcount > 0

                        BEGIN

                        Select 'Cek Close Price untuk Data : ' + @msg Msg

                        update MKBDTrails set LogMessages = 'Cek Close Price untuk Data : ' + @msg where MKBDTrailsPK = @NEWMKBDTrailsPK

                        return

                        END

                        ELSE

                        BEGIN

                        Select @MsgSuccess = @MsgSuccess + 'Company has Government Bond <br/> '

                        Insert into rl510cSbn(HistoryPK,Status,Notes,MKBDTrailsPK,Date,InstrumentPK,

                        InstrumentTypePK,HoldingPK,Volume,DueDate)

		

                        Select 1,2,'',@NewMKBDTrailsPK,@Date,A.InstrumentPK,

                        B.InstrumentTypePK,B.HoldingPK,A.Volume,A.MaturityDate

                        From #tmpSBN A

                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2

		

                        -- RL510cSBN Update Data

	

                        update rl       

                        set rl.Price = isnull(CP.ClosePriceValue, 0), rl.MarketValue = isnull(CP.ClosePriceValue, 0) * rl.Volume,   

                        rl.HaircutPercent = isnull(SBNHaircutMKBD, 0), rl.ConcentrationRisk = isnull(SBNConcentrationRisk,0),      

                        rl.MKBD01 = isnull(SBNMKBD01, 0), rl.MKBD02 = isnull(SBNMKBD02, 0),      

                        rl.MKBD03 = isnull(SBNMKBD03, 0), rl.MKBD09 = isnull(SBNMKBD09, 0)      

                        from RL510cSBN rl      

                        left join Instrument St on rl.InstrumentPK = St.InstrumentPK

                        left join 

                        (      

                        select [ClosePrice].InstrumentPK, [ClosePrice].ClosePriceValue

                        from [ClosePrice] (nolock)      

                        where [ClosePrice].Date = @Date      

                        ) as CP ON St.InstrumentPK = CP.InstrumentPK      

                        left join 

                        (      

                        select InstrumentTypePK, SBNTenorFrom, SBNTenorTo, HaircutMKBD as SBNHaircutMKBD, ConcentrationRisk as SBNConcentrationRisk,      

                        MKBD01 as SBNMKBD01, MKBD02 as SBNMKBD02, MKBD03 as SBNMKBD03, MKBD09 as SBNMKBD09      

                        from MKBDSetup (nolock)      

                        where [Date] =       

                        (      

                        select Max([Date]) as MaxDate       

                        from MKBDSetup (nolock)      

                        where [Date] <= @Date and RLName = 'RL510cSBN'       

                        ) and RLName = 'RL510cSBN'      

                        ) as HcSBN ON --St.StockType = HcSBN.StockType and     

                        cast(datediff(dd, @Date, isnull(Rl.DueDate,@Date)) as float)/365 between HcSBN.SBNTenorFrom and HcSBN.SBNTenorTo       

                        where rl.Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK

			

                        -- RL510cSBN Update RL

		

                        update rl   

                        set rl.HaircutAmount = rl.MarketValue * (rl.HaircutPercent/100),   

                        rl.AfterHaircutAmount = rl.MarketValue * (1-(rl.HaircutPercent/100)),  

                        rl.TotalEquity = @TotalEquity

                        from RL510cSBN rl  

                        where rl.Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK



                        update rl  

                        set rl.RankingLiabilities =  

                        case when MarketValue > TotalEquity * ConcentrationRisk  

                        then MarketValue - (TotalEquity * ConcentrationRisk)  

                        else 0  

                        end   

                        from RL510cSBN rl  

                        where rl.Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK       

		

                        -- RL510cSBN update 01

                        set @ssql = ''

                        declare A cursor for    

                        select cast(MKBD01 as varchar) + 'B', sum(MarketValue) as Value    

                        from RL510cSBN (nolock) where date = @date and MKBD01 > 0 

                        and status =  2 and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD01    



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD01 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)         

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A   

		

                        -- RL510cSBN update 03

                        set @ssql = ''

                        declare A cursor for  

                        select cast(MKBD03 as varchar) + 'B', sum(RankingLiabilities) as Value  

                        from RL510cSBN (nolock) where date = @date and MKBD03 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK 

                        group by MKBD03  



                        open A  



                        fetch next from A  

                        into @field, @value  



                        while @@FETCH_STATUS = 0  

                        begin   

                        set @ssql = 'update MKBD03 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + '''' + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)         

                        exec (@ssql)  



                        fetch next from A  

                        into @field, @value  

                        end  



                        close A  

                        deallocate A  

		 

                        -- RL510cSBN update 09

                        set @ssql = ''

                        declare A cursor for    

                        select cast(MKBD09 as varchar) + 'E', sum(MarketValue) as Value    

                        from RL510cSBN (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09   

                        union all  

                        select cast(MKBD09 as varchar) + 'G', sum(haircutamount) as Value    

                        from RL510cSBN (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09     



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD09 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + '''' + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)       

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A    

                        END

                        END

                        declare @MKBD06Bank int
                        declare @MKBD06PettyCash int
                        declare @InvestmentEquity int



                        select @MKBD06Bank = MKBD06Bank,@MKBD06PettyCash = MKBD06PettyCash,@InvestmentEquity = InvInEquity from AccountingSetup 
                        where status in (2)



                        Insert into MKBD06(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate,

                        [8B],[17B],[19B])

                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow

                        ,sum(dbo.[FGetAccountBalanceByDateByParent](@Date,@MKBD06PettyCash) + dbo.[FGetAccountBalanceByDateByParent](@Date,@MKBD06Bank)),dbo.[FGetAccountBalanceByDateByParent](@Date,@MKBD06PettyCash),

                        dbo.[FGetAccountBalanceByDateByParent](@Date,@MKBD06Bank)

                        ----------------------------------------------------------------------

                        Update MKBD06 set [15B] = [8B],[17C] = [17B],[19C] = [19B],[23B] = [8B] where MKBDTrailsPK = @NewMKBDTrailsPK


                        Insert into MKBD06Detail(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate,

                        [No],A,B,C,D,E,F)


                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow,ROW_NUMBER() OVER(ORDER BY A.ID ASC),isnull(E.RTGSCode,''),
                        'S' SendiriNasabah,A.BankAccountNo NoRek,'IDR' Currency,sum(BaseDebit - BaseCredit) Saldo, sum(BaseDebit - BaseCredit) SaldoRp from CashRef A 
                        left join JournalDetail B on A.AccountPK = B.AccountPK and B.status = 2
                        left join Journal C on B.JournalPK = C.JournalPK and C.status = 2
                        left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.status = 2
                        left join Bank E on D.BankPK = E.BankPK and E.status = 2
                        where A.Status in (2) and C.Posted = 1 and C.Reversed = 0 and ValueDate <= @Date and PeriodPK = @PeriodPK
                        and A.BankbranchPK <> 0 and A.CashRefType = 2
                        group by A.ID,A.BankAccountNo,E.RTGSCode



                        declare @TotalMKBD07 numeric (22,2)
                        select @TotalMKBD07 = sum(A.MarketValue) 
                        from (
                        select A.InstrumentTypePK,B.ID InstrumentID,A.Volume,dbo.[FGetLastAvgFromInvestment_Acc_12](@Date,A.InstrumentPK) AvgPrice,A.ClosePrice,A.MarketValue,A.RankingLiabilities from RL510CEquity A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 where Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK
                        union all
                        select A.InstrumentTypePK,B.ID InstrumentID,A.Volume,dbo.[FGetLastAvgFromInvestment_Acc_12](@Date,A.InstrumentPK) AvgPrice,A.ClosePrice,A.MarketValue,A.RankingLiabilities from RL510CBond A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 where Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK
                        ) A


                        -- INSERT MKBD 07 FROM JOURNAL

                        Insert into MKBD07(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate,

                        [9B])

                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow,

                        @TotalMKBD07


                        Update MKBD07 set [26B] = [9B],[31B] = [9B],[31C] = [9B],[36B] = [9B],[36C]=[9B],[63B] = [9B],[63C] = [9B] where MKBDTrailsPK = @NewMKBDTrailsPK







                        -- ACCOUNT UPDATE MKBD 01

                        set @ssql = ''

                        declare A cursor for    

                        select cast(dbo.[FVDNoByAccount](sj.AccountPK) as varchar) + 'B',     

                        sum

                        (    

                        case when A.Type = 1 then BaseDebit - BaseCredit    

                        else BaseCredit - BaseDebit end  

                        )    

                        from Journal J (nolock)    

                        left join JournalDetail Sj (nolock) on J.JournalPK = Sj.JournalPK      

                        left join Account A (nolock) on A.AccountPK = Sj.AccountPK and A.status = 2   

                        where J.Status  = 2 and j.Posted = 1 and J.ValueDate <= @Date and J.PeriodPK = @PeriodPK and A.Type = 1 and J.Reversed = 0

                        group by dbo.[FVDNoByAccount](sj.AccountPK)     

                        --group by A.[Type]      



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     



                        set @ssql = 'update MKBD01 set [' + @field + '] = [' + @field + '] + ' + cast(@value as varchar) +' Where MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)   

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A    



                        -- MKBD01 UPDATE TOTAL ASET LANCAR



                        set @ssql = ''

                        declare A cursor for    

                        select col.name from sysobjects obj    

                        left join syscolumns col on obj.id = col.id    

                        where obj.name = 'MKBD01'    

                        and col.name not in ('MKBD01PK','MKBDTrailsPK', 'Date', 'CompanyID', 'CompanyName', 'DirectorName',     

                        'LastUpdate','DBUserID','DBTerminalID','LastUpdateDB')     

                        and col.name like '%B' and cast(left(col.name, len(col.name)-1) as int) between 10 and 99    

     

                        open A    

    

                        fetch next from A    

                        into @field     

    

                        while @@FETCH_STATUS = 0    

                        begin     

                        select @ssql = 'update MKBD01 set [100B] = [100B] + ['+ @field + '] where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)           

                        exec (@ssql)    

      

                        fetch next from A    

                        into @field     

                        end    

    

                        close A    

                        deallocate A    



                        -- MKBD01 UPDATE TOTAL ASSET TETAP

                        set @ssql = ''

                        declare A cursor for    

                        select col.name from sysobjects obj    

                        left join syscolumns col on obj.id = col.id    

                        where obj.name = 'MKBD01'    

                        and col.name not in ('MKBD01PK','MKBDTrailsPK', 'Date', 'CompanyID', 'CompanyName', 'DirectorName',     

                        'LastUpdate','DBUserID','DBTerminalID','LastUpdateDB')     

                        and col.name like '%B' and cast(left(col.name, len(col.name)-1) as int) between 102 and 111    
                        --EMCO
                        --and col.name like '%B' and cast(left(col.name, len(col.name)-1) as int) between 108 and 111 
     

                        open A    

    

                        fetch next from A    

                        into @field     

    

                        while @@FETCH_STATUS = 0    

                        begin     

                        select @ssql = 'update MKBD01 set [112B] = [112B] + [' + @field + '] where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)               

                        exec (@ssql)    

      

                        fetch next from A    

                        into @field     

                        end    

    

                        close A    

                        deallocate A    



                        -- MKBD 01 UPDATE TOTAL



                        set @ssql =''

                        declare A cursor for      

                        select col.name from sysobjects obj      

                        left join syscolumns col on obj.id = col.id      

                        where obj.name = 'MKBD01'      

                        and col.name not in ('MKBD01PK','MKBDTrailsPK', 'Date', 'CompanyID', 'CompanyName', 'DirectorName',     

                        'LastUpdate','DBUserID','DBTerminalID','LastUpdateDB')     

                        and col.name like '%B' and cast(left(col.name, len(col.name)-1) as int) in (100,112)
                        --EMCO       
                        --and col.name like '%B' and cast(left(col.name, len(col.name)-1) as int) between 100 and 111 
       

                        open A      

      

                        fetch next from A      

                        into @field       

      

                        while @@FETCH_STATUS = 0      

                        begin       

                        select @ssql = 'update MKBD01 set [113B] = [113B] + [' + @field + '] where date = ''' +  convert(varchar(8),@date,112) + '''' + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)     

                        exec (@ssql)      

        

                        fetch next from A      

                        into @field       

                        end      

      

                        close A      

                        deallocate A      

      





                        -- ACCOUNT UPDATE MKBD 02

                        set @ssql = ''

                        declare A cursor for    

                        select cast(dbo.[FVDNoByAccount](sj.AccountPK) as varchar) + 'B',     

                        sum(    

                        case when A.Type = 1 then BaseDebit - BaseCredit    

                        else BaseCredit - BaseDebit    

                        end     

                        )    

                        from Journal J (nolock)    

                        left join JournalDetail Sj (nolock)     

                        on J.JournalPK = Sj.JournalPK      

                        left join Account A (nolock)    

                        on A.AccountPK = Sj.AccountPK  and A.status = 2    

                        where J.Status  = 2 and j.Posted = 1 and J.ValueDate <= @Date and J.PeriodPK = @PeriodPK and A.Type > 1  and J.Reversed = 0  

                        group by dbo.[FVDNoByAccount](sj.AccountPK)     

                        --group by A.[Type]      

       

                        open A    

    

                        fetch next from A    

                        into @field, @value    

    

                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD02 set [' + @field + '] = [' + @field + '] + ' + cast(@value as varchar) + ' where  MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)

                        exec (@ssql)    

     

                        fetch next from A    

                        into @field, @value    

                        end    

    

                        close A    

                        deallocate A    



                        -- MKBD02UpdateTotalLiabilities

                        set @ssql = ''



                        declare A cursor for    

                        select col.name from sysobjects obj    

                        left join syscolumns col on obj.id = col.id    

                        where obj.name = 'MKBD02'    

                        and col.name not in ('MKBD02PK','MKBDTrailsPK', 'Date', 'CompanyID', 'CompanyName', 'DirectorName',     

                        'LastUpdate','DBUserID','DBTerminalID','LastUpdateDB')  

                        and col.name like '%B' and cast(left(col.name, len(col.name)-1) as int) between 122 and 163    

     

                        open A    

    

                        fetch next from A    

                        into @field     

    

                        while @@FETCH_STATUS = 0    

                        begin      

                        set @ssql = 'update MKBD02 set [164B] = [164B] + [' + @field + '] where date = ''' +  convert(varchar(8),@date,112) + '''' + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)       

                        exec (@ssql)    

      

                        fetch next from A    

                        into @field     

                        end    

    

                        close A    

                        deallocate A   



  

                        -- MKBD02UpdateTotalEquities



                        set @ssql = ''

                        declare A cursor for    

                        select col.name from sysobjects obj    

                        left join syscolumns col on obj.id = col.id    

                        where obj.name = 'MKBD02'    

                        and col.name not in ('MKBD02PK','MKBDTrailsPK', 'Date', 'CompanyID', 'CompanyName', 'DirectorName',     

                        'LastUpdate','DBUserID','DBTerminalID','LastUpdateDB') 

                        and col.name like '%B' and cast(left(col.name, len(col.name)-1) as int) between 167 and 171    

     

                        open A    

    

                        fetch next from A    

                        into @field     

    

                        while @@FETCH_STATUS = 0    

                        begin     

                        select @ssql = 'update MKBD02 set [172B] = [172B] + [' + @field + '] where date = ''' +  convert(varchar(8),@date,112) + '''' + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)      

                        exec (@ssql)    

      

                        fetch next from A    

                        into @field     

                        end    

    

                        close A    

                        deallocate A    



                        -- MKBD02UpdateTotal

                        set @ssql = ''

                        declare A cursor for    

                        select col.name from sysobjects obj    

                        left join syscolumns col on obj.id = col.id    

                        where obj.name = 'MKBD02'    

                        and col.name not in ('MKBD02PK','MKBDTrailsPK', 'Date', 'CompanyID', 'CompanyName', 'DirectorName',     

                        'LastUpdate','DBUserID','DBTerminalID','LastUpdateDB') 

                        and col.name like '%B' and cast(left(col.name, len(col.name)-1) as int)  in (164, 172)    

     

                        open A    

    

                        fetch next from A    

                        into @field     

    

                        while @@FETCH_STATUS = 0    

                        begin     

                        select @ssql = 'update MKBD02 set [173B] = [173B] + [' + @field + '] where date = ''' +  convert(varchar(8),@date,112) + ''''  + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)        

                        exec (@ssql)    

      

                        fetch next from A    

                        into @field     

                        end    

    

                        close A    

                        deallocate A    




                        -- INSERT VD510C

                        INSERT INTO [dbo].[MKBD510C]
                        ([MKBDTrailsPK],[Date],[CompanyID],[CompanyName],[DirectorName],[A],[B],[C]
                        ,[D],[E],[F],[G],[H],[I],[J],[K])	


                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName, 
                        ROW_NUMBER() OVER(ORDER BY InstrumentTypePK,InstrumentID ASC) AS No,
                        case when InstrumentTypePK = 1 then 'saham' else 'obligasi' end B,A.InstrumentID C,'Tidak Terafiliasi' D, A.Volume E,AvgPrice F,A.ClosePrice G,A.MarketValue H,'' I,0 J,A.RankingLiabilities K 
                        from (
                        select A.InstrumentTypePK,B.ID InstrumentID,A.Volume,dbo.[FGetLastAvgFromInvestment_Acc_12](@Date,A.InstrumentPK) AvgPrice,A.ClosePrice,A.MarketValue,A.RankingLiabilities from RL510CEquity A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 where Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK
                        union all
                        select A.InstrumentTypePK,B.ID InstrumentID,A.Volume,dbo.[FGetLastAvgFromInvestment_Acc_12](@Date,A.InstrumentPK) AvgPrice,A.Price ClosePrice,A.MarketValue,A.RankingLiabilities from RL510CBond A
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 where Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK
                        ) A




                        declare @TotalMKBD01 numeric(18,4)      

                        declare @TotalMKBD02 numeric(18,4)      

                        declare @SelisihPembulatan numeric(18,4)      

      

                        select @SelisihPembulatan = 100   

                        select @TotalMKBD01 = isnull([113B],0) from MKBD01 (nolock) where Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK    

                        select @TotalMKBD02 = isnull([173B],0) from MKBD02 (nolock) where Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK    

      

                        if abs(@TotalMKBD01 - @TotalMKBD02) <= @SelisihPembulatan       

                        begin      

                        update MKBD02 set [170B] = [170B] + (@TotalMKBD01 - @TotalMKBD02),       

                        [172B] = [172B] + (@TotalMKBD01 - @TotalMKBD02), [173B] = [173B] + (@TotalMKBD01 - @TotalMKBD02)      

                        where Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK     

                        end 





                        -- MKBD 03 UPDATE TOTAL RL

                        set @ssql = ''

                        declare A cursor for    

                        select col.name from sysobjects obj    

                        left join syscolumns col on obj.id = col.id    

                        where obj.name = 'MKBD03'    

                        and col.name not in ('MKBD03PK','MKBDTrailsPK', 'Date', 'CompanyID', 'CompanyName', 'DirectorName',     

                        'LastUpdate','DBUserID','DBTerminalID','31B','LastUpdateDB') 

                        and col.name like '%B'   

     

                        open A    

                        fetch next from A  

                        into @field   

  

                        while @@FETCH_STATUS = 0  

                        begin   

                        set @ssql = 'update MKBD03 set [31B] = [31B] + [' + @field + '] where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)   

                        exec (@ssql)  

   

                        fetch next from A  

                        into @field   

                        end  

  

                        close A  

                        deallocate A   



                        -- MKBD08 UPDATE DATA

                        declare @PPEMinimalMKBD decimal(19,4)      

                        declare @MIMinimalMKBD decimal(19,4)       

                        declare @MITotalFund decimal(19,4)  



                        select @PPEMinimalMKBD = isnull(PPEMinimalMKBD,0), @MIMinimalMKBD = isnull(MIMinimalMKBD,0) from Company (nolock) 

                        where status = 2



                        select @MITotalFund = isnull(Amount,0) from AUM (nolock)  

                        where status = 2 and Date = 

                        (select MAX(Date) MaxDate from AUM (nolock) where Date <= @Date and status = 2)

  

                        update M1 set   

                        M1.[8E] = M2.[164B], M1.[11E] = M2.[163B], M1.[13E] = M2.[146B] ,M1.[14E] = M2.[147B],  

                        M1.[15E] = M2.[148B] ,M1.[18E] = @PPEMinimalMKBD ,M1.[22E] = @MIMinimalMKBD, M1.[23E] = isnull(@MITotalFund,0),  

                        M1.[9E] = M3.[31B]  

                        from MKBD08 M1        

                        left join MKBD02 M2 on M1.Date = M2.Date and M1.MKBDTrailsPK = M2.MKBDTrailsPK    

                        Left join MKBD03 M3 on M1.Date = M3.Date and M1.MKBDTrailsPK = M3.MKBDTrailsPK

                        where M1.Date = @Date        

                        and M1.MKBDTrailsPK = @NewMKBDTrailsPK

  

                        update MKBD08 set   

                        [10E] = [8E] + [9E]   

                        where [Date] = @Date

                        and MKBDTrailsPK = @NewMKBDTrailsPK

    

                        update MKBD08 set   

                        [16E] = [10E] - [11E]- [13E] - [14E] -[15E]   

                        where [Date] = @Date 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

   

                        update MKBD08 set [19E] = '0'

                        --EMCO  
                        --update MKBD08 set [19E] = 0.0625 * [16E]    

                        --where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

   

                        update MKBD08 set   

                        [20E] = case when [19E] > [18E] then [19E] else [18E] end   

                        where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

  

                        Update MKBD08 set   

                        [24E] = 0.001 * [23E]   

                        where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

   

                        update MKBD08 Set   

                        [25E] = [22E] + [24E]  

                        where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

   

                        update MKBD08 Set   

                        [26E] = [20E] + [25E]   

                        where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK



                        -- UPDATE MKBD 09

                        update M9 set   

                        M9.[9E] = M1.[100B], M9.[11E] = M2.[164B], M9.[12E] = M3.[31B],  

                        M9.[17E] = M2.[163B], M9.[24E] = M1.[16B],  

                        M9.[93E] = M1.[39B], M9.[94G] = M1.[44B], M9.[103G] = M8.[26E],   

                        M9.[96G] = Case when M6.[10B] >= M6.[20D] then M6.[10B] - M6.[20D] else 0 end,  

                        M9.[97G] = case when M6.[13B] >= M6.[21B] then M6.[13B] - M6.[21B] else 0 end,  

                        M9.[98G] = case when M7.[11B] >= M7.[36D] then M7.[11B] - M7.[36D] else 0 end,  

                        M9.[99G] = case when M7.[61E] >= M7.[13B] then M7.[61E] - M7.[13B] else 0 end  

                        from MKBD09 M9    

                        left join MKBD01 M1 on M9.Date = M1.Date and  M9.MKBDTrailsPK = M1.MKBDTrailsPK     

                        left join MKBD02 M2 on M9.Date = M2.Date and  M9.MKBDTrailsPK = M2.MKBDTrailsPK   

                        left join MKBD03 M3 on M9.Date = M3.Date and  M9.MKBDTrailsPK = M3.MKBDTrailsPK     

                        left join MKBD06 M6 on M9.Date = M6.Date and  M9.MKBDTrailsPK = M6.MKBDTrailsPK     

                        left join MKBD07 M7 on M9.Date = M7.Date and  M9.MKBDTrailsPK = M7.MKBDTrailsPK     

                        left join MKBD08 M8 on M9.Date = M8.Date and  M9.MKBDTrailsPK = M8.MKBDTrailsPK     

                        where M9.Date = @Date  and M9.MKBDTrailsPK = @NewMKBDTrailsPK  

  

                        update MKBD09 set   

                        [13E] = [9E] - [11E] - [12E] where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK   

                        update MKBD09 set   

                        [15E] = [13E] where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK  

                        update MKBD09 set   

                        [18E] = [15E] + [17E] where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK  

                        update MKBD09 set   

                        [20G] = [18E] where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK  

                        update MKBD09 set   

                        [102G] = [20G] + [101G] where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK  



                        set @ssql = ''    

                        declare A cursor for    

                        select col.name from sysobjects obj    

                        left join syscolumns col on obj.id = col.id    

                        where obj.name = 'MKBD09'    

                        and col.name not in ('MKBD09PK','MKBDTrailsPK', 'Date', 'CompanyID', 'CompanyName', 'DirectorName',     

                        'LastUpdate','DBUserID','DBTerminalID','LastUpdateDB')  

                        and col.name like '%G' and cast(left(col.name, len(col.name)-1) as int) between 24 and 99     

     

                        open A    

    

                        fetch next from A    

                        into @field     

    

                        while @@FETCH_STATUS = 0    

                        begin     

                        select @ssql = 'update MKBD09 set [102G] = [102G] - [' + @field + '] where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)    

                        exec (@ssql)    

      

                        fetch next from A    

                        into @field     

                        end    

    

                        close A    

                        deallocate A 



                        update MKBD09 set   

                        [104G] = [102G] - [103G] where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK  



                        --Select @MsgSuccess = @MsgSuccess + 'NAWC Process Done'

                        --select '' ErrMsg,@MsgSuccess Msg



                        update mkbdTrails set BitValidate = 1,logMEssages = @MsgSuccess where MKBDTrailsPK = @NewMKBDTrailsPK


                        FETCH NEXT FROM DATE 
                        INTO @Date
                        END 
                        CLOSE DATE  
                        DEALLOCATE DATE

                        DECLARE @combinedString NVARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + replace(convert(NVARCHAR, valuedate, 106), ' ', '/') from
                        (
                        select distinct  valuedate from #date

                        )A

                        SELECT 'Generate MKBD Success ! Valuedate : '  + @combinedString + ', Please Check Log' as msg
                        ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Status", 1);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _userID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                NAWCDailyProcess _NAWC = new NAWCDailyProcess();
                                _NAWC.Msg = Convert.ToString(dr["Msg"]);
                                return _NAWC;
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        
        public string FundClient_GenerateNewClientID(int _clientCategory, int _fundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //INSIGHT
                        cmd.CommandText = @" 							
                                declare @Prefix nvarchar(3)

select @Prefix = case when ClientCategory = 1 then 'IND' else 'INS' END
From Fundclient where fundclientPK = @FundClientPK and status = 1


Declare @NewClientID  nvarchar(100)    
Declare @MaxClientID  int
                                    
select @MaxClientID =   max(convert(int,right(ID,3)))  + 1 from FundClient where  status in (1,2) 
select @maxClientID = isnull(@MaxClientID,1)
							
declare @LENdigit int
select @LENdigit = LEN(@maxClientID) 
							
If @LENdigit = 1
BEGIN
	set @NewClientID = @Prefix + '00' + CAST(@MaxClientID as nvarchar) 
END
If @LENdigit = 2
BEGIN
	set @NewClientID = @Prefix + '0' + CAST(@MaxClientID as nvarchar) 
END
If @LENdigit = 3
BEGIN
	set @NewClientID =  @Prefix + CAST(@MaxClientID as nvarchar) 
END
Select @NewClientID NewClientID
                       ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["NewClientID"]);
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

        public Boolean GenerateReportUnitRegistry(string _userID, UnitRegistryRpt _unitRegistryRpt)
        {
            #region Unit Trust Report
            if (_unitRegistryRpt.ReportName.Equals("Unit Trust Report"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";
                            string _paramFundClient = "";
                            string _paramAgent = "";


                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = " And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = " And A.FundClientPK in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }

                            cmd.CommandText =

                            @"
                            DECLARE @DateFromBegBalance datetime
                            set @DateFromBegBalance = dbo.FWorkingDay(@Datefrom,-1)


	                        CREATE TABLE #resultUnitTrust
	                        (
		                        FundPK INT,
		                        FundID NVARCHAR(500),
		                        FundName NVARCHAR(500),
		                        FundClientName NVARCHAR(500),	
		                        AccountNo NVARCHAR(500),
		                        Address NVARCHAR(500),
		                        CurrencyID NVARCHAR(500),
		                        TrxDate NVARCHAR(500),
		                        PostedTime NVARCHAR(500),
		                        TrxType NVARCHAR(500),
		                        Amount numeric(32,4),
		                        FeeAmount numeric(32,4),
		                        NetValue numeric(32,4),
		                        TotalUnitAmount numeric(32,4),
		                        NAV numeric(32,4),
		                        AvgCost numeric(32,4),
		                        FundClientPK int,
		                        TotalCashAmount numeric(32,4),
		                        BegBalance numeric(32,4),
		                        EndAVG numeric(32,4),
		                        EndNAV numeric(32,4)
	                        )

	                        INSERT INTO #resultUnitTrust

                            Select A.FundPK,C.ID FundID,C.Name FundName,
                            B.Name FundClientName,B.ID AccountNo,Case when B.ClientCategory = 1 then B.AlamatInd1 else B.AlamatPerusahaan End Address,
                            D.ID CurrencyID,A.NAVDate TrxDate,A.PostedTime,'Subs' TrxType
                            ,A.CashAmount Amount,A.SubscriptionFeeAmount FeeAmount, TotalCashAmount NetValue,
	                        A.TotalUnitAmount,A.NAV,dbo.FGetAVGForFundClientPosition(A.NAVDAte,A.FundClientPK,A.FundPK) AvgCost
                            ,A.FundClientPK, TotalCashAmount, [dbo].[Get_UnitAmountByFundPKandFundClientPK](@DateFromBegBalance,A.FundClientPK,A.FundPK) BegBalance
                            ,[dbo].[FgetFirstNAV] (A.FundClientPK,A.FundPK)    EndAVG
                            ,dbo.FgetLastCloseNav(@DateTo,A.FundPK) EndNAV
                            from ClientSubscription A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                            left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                            left join Currency D on C.CurrencyPK = D.CurrencyPK and D.status in (1,2)
                            where posted = 1 and revised = 0 and A.status = 2 
                            and ValueDate between @DateFrom and @DateTo 
	                        "
                            + _paramFundClient + _paramFund +

                            @"
	
	                        UNION ALL

                            Select A.FundPK,C.ID FundID,C.Name FundName,
                            B.Name FundClientName,B.ID AccountNo,Case when B.ClientCategory = 1 then B.AlamatInd1 else B.AlamatPerusahaan End Address,
                            D.ID CurrencyID,A.NAVDate TrxDate,A.PostedTime,'Rdm' TrxType
                            ,A.CashAmount Amount,A.RedemptionFeeAmount FeeAmount, TotalCashAmount NetValue,A.TotalUnitAmount,A.NAV,dbo.FGetAVGForFundClientPosition(A.NAVDAte,A.FundClientPK,A.FundPK) AvgCost
                            ,A.FundClientPK, TotalCashAmount, [dbo].[Get_UnitAmountByFundPKandFundClientPK](@DateFromBegBalance,A.FundClientPK,A.FundPK) BegBalance
                            ,[dbo].[FgetFirstNAV] (A.FundClientPK,A.FundPK) EndAVG
                            ,dbo.FgetLastCloseNav(@DateTo,A.FundPK) EndNAV
                            from ClientRedemption A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                            left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                            left join Currency D on C.CurrencyPK = D.CurrencyPK and D.status in (1,2)
                            where posted = 1 and revised = 0 and A.status = 2 
                            and ValueDate between @DateFrom and @DateTo 
	                        "
                            + _paramFundClient + _paramFund +

                            @"
	
	                        UNION ALL

                            Select A.FundPKTo,C.ID FundID,C.Name FundName,
                            B.Name FundClientName,B.ID AccountNo,Case when B.ClientCategory = 1 then B.AlamatInd1 else B.AlamatPerusahaan End Address,
                            D.ID CurrencyID,A.NAVDate TrxDate,A.PostedTime,'Switch In' TrxType
                            ,A.CashAmount Amount,A.SwitchingFeeAmount FeeAmount, TotalCashAmountFundTo NetValue,A.TotalUnitAmountFundTo,A.NAVFundTo NAV,dbo.FGetAVGForFundClientPosition(A.NAVDAte,A.FundClientPK,A.FundPKto) AvgCost
                            ,A.FundClientPK, TotalCashAmountFundTo, [dbo].[Get_UnitAmountByFundPKandFundClientPK](@DateFromBegBalance,A.FundClientPK,A.FundPKTo) BegBalance
                            ,[dbo].[FgetFirstNAV] (A.FundClientPK,A.FundPKTo) EndAVG
                            ,dbo.FgetLastCloseNav(@DateTo,A.FundPKTo) EndNAV
                            from ClientSwitching A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                            left join Fund C on A.FundPKTo = C.FundPK and C.status in (1,2)
                            left join Currency D on C.CurrencyPK = D.CurrencyPK and D.status in (1,2)
                            where posted = 1 and revised = 0 and A.status = 2 
                            and ValueDate between @DateFrom and @DateTo 
	                        "
                            + _paramFundClient + _paramFund +

                            @"
	
	                        UNION ALL

                            Select A.FundPKFrom,C.ID FundID,C.Name FundName,
                            B.Name FundClientName,B.ID AccountNo,Case when B.ClientCategory = 1 then B.AlamatInd1 else B.AlamatPerusahaan End Address,
                            D.ID CurrencyID,A.NAVDate TrxDate,A.PostedTime,'Switch Out' TrxType
                            ,A.CashAmount Amount,A.SwitchingFeeAmount FeeAmount, TotalCashAmountFundFrom NetValue,A.TotalUnitAmountFundfrom,A.NAVFundTo NAV,dbo.FGetAVGForFundClientPosition(A.NAVDAte,A.FundClientPK,A.FundPKFrom) AvgCost
                            ,A.FundClientPK, TotalCashAmountFundFrom, [dbo].[Get_UnitAmountByFundPKandFundClientPK](@DateFromBegBalance,A.FundClientPK,A.FundPKFrom) BegBalance
                            ,[dbo].[FgetFirstNAV] (A.FundClientPK,A.FundPKFrom) EndAVG
                            ,dbo.FgetLastCloseNav(@DateTo,A.FundPKFrom) EndNAV
                            from ClientSwitching A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                            left join Fund C on A.FundPKTo = C.FundPK and C.status in (1,2)
                            left join Currency D on C.CurrencyPK = D.CurrencyPK and D.status in (1,2)
                            where posted = 1 and revised = 0 and A.status = 2 
                            and ValueDate between @DateFrom and @DateTo 
                            "
                            + _paramFundClient + _paramFund +

                            @"
	                        INSERT INTO #resultUnitTrust
	                        SELECT  
	                        A.FundPK,C.ID,C.Name
	                        ,D.Name,D.ID,Case when D.ClientCategory = 1 then D.AlamatInd1 else D.AlamatPerusahaan END
	                        ,E.ID,'','','',0,0,0,0,0
	                        ,dbo.FGetAVGForFundClientPosition(@DateTo,A.FundClientPK,A.FundPK)
	                        ,A.FundClientPK,0,[dbo].[Get_UnitAmountByFundPKandFundClientPK](@DateTo,A.FundClientPK,A.FundPK) BegBalance
	                        ,[dbo].[FgetFirstNAV] (A.FundClientPK,A.FundPK)
	                        ,dbo.FgetLastCloseNav(@DateTo,A.FundPK) 
	                        FROM dbo.FundClientPosition A 
	                        LEFT JOIN #resultUnitTrust B ON A.FundClientPK = B.FundClientPK AND A.FundPK = B.FundPK
	                        LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	                        LEFT JOIN FundClient D ON A.FundClientPK = D.FundClientPK AND D.status IN (1,2)
	                        LEFT JOIN Currency E ON C.CurrencyPK = E.CurrencyPK AND E.status IN (1,2)
	                        WHERE A.Date = @DateTo
                            "
                            + _paramFundClient + _paramFund +

                            @"
	                        AND B.FundPK IS NULL AND B.FundClientPK IS NULL
    

	                        SELECT * FROM #resultUnitTrust";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.ExecuteNonQuery();

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.UnitTrustPath + "UnitTrustReport" + "_" + _unitRegistryRpt.ClientName + ".xlsx";
                                    string pdfPath = Tools.UnitTrustPath + "UnitTrustReport" + "_" + _unitRegistryRpt.ClientName + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "UnitRegistryReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Unit Trust Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<UnitTrustReport> rList = new List<UnitTrustReport>();
                                        while (dr0.Read())
                                        {
                                            UnitTrustReport rSingle = new UnitTrustReport();
                                            rSingle.ClientName = Convert.ToString(dr0["FundClientName"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.AccountNo = Convert.ToString(dr0["AccountNo"]);
                                            rSingle.Address = Convert.ToString(dr0["Address"]);
                                            rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                            rSingle.TrxDate = Convert.ToString(dr0["TrxDate"]);
                                            rSingle.PostedTime = Convert.ToString(dr0["PostedTime"]);
                                            rSingle.TRXType = Convert.ToString(dr0["TrxType"]);
                                            rSingle.Amount = Convert.ToDecimal(dr0["Amount"]);
                                            rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                            rSingle.NetValue = Convert.ToDecimal(dr0["NetValue"]);
                                            rSingle.TotalUnitAmount = Convert.ToDecimal(dr0["TotalUnitAmount"]);
                                            rSingle.NAV = Convert.ToDecimal(dr0["NAV"]);
                                            rSingle.AvgCost = Convert.ToDecimal(dr0["AvgCost"]);
                                            rSingle.TotalCashAmount = Convert.ToDecimal(dr0["TotalCashAmount"]);
                                            rSingle.BeginningBalance = Convert.ToDecimal(dr0["BegBalance"]);
                                            rSingle.EndAVG = Convert.ToDecimal(dr0["EndAVG"]);
                                            rSingle.EndNAV = Convert.ToDecimal(dr0["EndNAV"]);


                                            rList.Add(rSingle);

                                        }

                                        var GroupByCategory =
                                         from r in rList
                                         group r by new { r.FundName, r.ClientName, r.AccountNo, r.Address, r.CurrencyID, r.BeginningBalance, r.EndAVG, r.EndNAV } into rGroup
                                         select rGroup;

                                        int incRowExcel = 0;
                                        //int _rowEndBalance = 0;

                                        foreach (var rsHeader in GroupByCategory)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "UNIT TRUST REPORT";
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.FundName;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;

                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1].Value = "Period : " + Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Unit Holder's Name ";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + rsHeader.Key.ClientName;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Account No ";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + rsHeader.Key.AccountNo;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Address ";
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            //worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + rsHeader.Key.Address;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(incRowExcel).Height = 60;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Currency ";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + rsHeader.Key.CurrencyID;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 9].Value = "Date : " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 9].Value = "Time : " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("hh:mm:ss");
                                            incRowExcel++;

                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "TRANSACTION";
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "TRX Date";
                                            worksheet.Cells[incRowExcel, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 2].Value = "TRX Type";
                                            worksheet.Cells[incRowExcel, 8].Value = "Unit Balance Quantity";
                                            worksheet.Cells[incRowExcel, 9].Value = "Unrealized";
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Font.Bold = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Post Date";
                                            worksheet.Cells[incRowExcel, 3].Value = "Subs Amount";
                                            worksheet.Cells[incRowExcel, 4].Value = "Fee/Charge";
                                            worksheet.Cells[incRowExcel, 5].Value = "Net Value";
                                            worksheet.Cells[incRowExcel, 7].Value = "Unit Quantity";
                                            worksheet.Cells[incRowExcel, 8].Value = "Average Cost";
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["C" + incRowExcel + ":H" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 7].Value = "NAV/Unit";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            incRowExcel = incRowExcel + 2;

                                            int _RowBorder = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = "Beginning Balance";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Value = rsHeader.Key.BeginningBalance;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;

                                            incRowExcel++;
                                            int _beg2 = incRowExcel;



                                            incRowExcel = incRowExcel + 2;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = incRowExcel;
                                            //end area header

                                            int _unrealized = 0;
                                            int _unrealized2 = 0;
                                            int count = 0;
                                            int _RowDetail = 0;
                                            int _rowTotal = 0;
                                            decimal _amount = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                _amount = rsDetail.Amount;
                                                if (rsDetail.Amount == 0)
                                                {
                                                    if (rsHeader.Key.BeginningBalance == 0)
                                                    {
                                                        worksheet.Cells[_beg2, 8].Value = 0;
                                                        worksheet.Cells[_beg2, 8].Style.Numberformat.Format = "#,##0.0000";
                                                        worksheet.Cells[_beg2, 8].Style.Font.Bold = true;
                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells[_beg2, 8].Value = rsDetail.AvgCost;
                                                        worksheet.Cells[_beg2, 8].Style.Numberformat.Format = "#,##0.0000";
                                                        worksheet.Cells[_beg2, 8].Style.Font.Bold = true;
                                                    }


                                                    //area detail
                                                    worksheet.Cells[_beg2, 1].Value = rsDetail.TrxDate;
                                                    worksheet.Cells[_beg2, 1].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[_beg2, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    worksheet.Cells[_beg2, 2].Value = rsDetail.TRXType;
                                                    worksheet.Cells[_beg2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[_beg2, 3].Value = rsDetail.Amount;
                                                    worksheet.Cells[_beg2, 3].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[_beg2, 4].Value = rsDetail.FeeAmount;
                                                    worksheet.Cells[_beg2, 4].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[_beg2, 5].Value = rsDetail.NetValue;
                                                    worksheet.Cells[_beg2, 5].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[_beg2, 7].Value = rsDetail.TotalUnitAmount;
                                                    worksheet.Cells[_beg2, 7].Style.Numberformat.Format = "#,##0.0000";

                                                    _unrealized = incRowExcel;

                                                    incRowExcel++;
                                                    _unrealized2 = incRowExcel;
                                                    //worksheet.Cells[incRowExcel, 1].Value = Convert.ToDateTime(rsDetail.PostedTime).ToString("dd-MMM-yyyy");
                                                    //worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                }
                                                else
                                                {
                                                    if (rsHeader.Key.BeginningBalance == 0)
                                                    {
                                                        worksheet.Cells[_beg2, 8].Value = 0;
                                                        worksheet.Cells[_beg2, 8].Style.Numberformat.Format = "#,##0.0000";
                                                        worksheet.Cells[_beg2, 8].Style.Font.Bold = true;
                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells[_beg2, 8].Value = rsDetail.AvgCost;
                                                        worksheet.Cells[_beg2, 8].Style.Numberformat.Format = "#,##0.0000";
                                                        worksheet.Cells[_beg2, 8].Style.Font.Bold = true;
                                                    }

                                                    //area detail
                                                    _RowDetail = incRowExcel;
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.TrxDate;
                                                    worksheet.Cells[incRowExcel, 1].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.TRXType;
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.FeeAmount;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.NetValue;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.TotalUnitAmount;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                    int incRow = incRowExcel - 2;
                                                    if (count == 0)
                                                    {
                                                        if (rsDetail.TRXType == "Subs" || rsDetail.TRXType == "Switch In")
                                                        {
                                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _RowBorder + "+G" + incRowExcel + ")";
                                                        }
                                                        else
                                                        {
                                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _RowBorder + "-G" + incRowExcel + ")";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (rsDetail.TRXType == "Subs" || rsDetail.TRXType == "Switch In")
                                                        {
                                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + incRow + "+G" + incRowExcel + ")";
                                                        }
                                                        else
                                                        {
                                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + incRow + "-G" + incRowExcel + ")";
                                                        }
                                                    }


                                                    worksheet.Cells[incRowExcel, 8].Calculate();
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";

                                                    _unrealized = incRowExcel;

                                                    incRowExcel++;
                                                    _unrealized2 = incRowExcel;
                                                    //worksheet.Cells[incRowExcel, 1].Value = Convert.ToDateTime(rsDetail.PostedTime).ToString("dd-MMM-yyyy");
                                                    //worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.NAV;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.AvgCost;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                    _rowTotal = incRowExcel;
                                                }






                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;
                                                count++;
                                            }


                                            worksheet.Cells[_unrealized, 9].Formula = "SUM(H" + _unrealized + "*G" + _unrealized2 + ") - (H" + _unrealized + "*H" + _beg2 + ") ";
                                            worksheet.Cells[_unrealized, 9].Calculate();
                                            worksheet.Cells[_unrealized, 9].Style.Numberformat.Format = "#,##0.00";


                                            int _endRowDetailTotal = incRowExcel;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            int _RowDetail2 = _RowDetail + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = "Total :";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            //worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _RowDetail + ":G" + _RowDetail2 + ")";
                                            //worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";

                                            if (_amount != 0)
                                            {
                                                worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 9].Calculate();
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                            }

                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            //worksheet.Cells[incRowExcel, 7].Calculate();
                                            //worksheet.Cells[incRowExcel, 8].Calculate();
                                            //worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            //worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            //worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";


                                            int _RowTotal1 = incRowExcel - 2;
                                            int _a = incRowExcel + 1;

                                            incRowExcel++;

                                            worksheet.Cells["A" + _RowBorder + ":I" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Dotted;
                                            worksheet.Cells["A" + _RowBorder + ":I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
                                            incRowExcel = incRowExcel + 2;
                                            int _rowSum = incRowExcel;
                                            worksheet.Cells[incRowExcel, 8].Value = "Ending Balance(Unit)";

                                            if (_amount == 0)
                                            {
                                                worksheet.Cells[incRowExcel, 9].Value = rsHeader.Key.BeginningBalance;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";

                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 9].Formula = "SUM(H" + _unrealized + ")";
                                                worksheet.Cells[incRowExcel, 9].Calculate();
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            }


                                            incRowExcel++;
                                            int _nav = incRowExcel;
                                            worksheet.Cells[incRowExcel, 8].Value = "NAV Unit Per " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                            worksheet.Cells[incRowExcel, 9].Value = rsHeader.Key.EndNAV;
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                            incRowExcel++;
                                            int _unit = incRowExcel;
                                            worksheet.Cells[incRowExcel, 8].Value = "Average Cost/Unit";
                                            worksheet.Cells[incRowExcel, 9].Value = rsHeader.Key.EndAVG;
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                            worksheet.Cells[incRowExcel, 1].Value = "Unless we receive any objectioms from you within 7 days of confirmation date, we shall assume that all details above are correct";

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 8].Value = "Unrealized Profit/(Loss)";

                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _rowSum + "*I" + _nav + ") - (I" + _rowSum + "*I" + _unit + ") ";
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;



                                            worksheet.Cells[incRowExcel, 1].Value = "This is a computer-generated advice and required no signature.";
                                            int _endRowSum = incRowExcel;

                                            if (_amount != 0)
                                            {
                                                worksheet.Cells[_unrealized, 9].Formula = "SUM(I" + incRowExcel + ")";
                                                worksheet.Cells[_unrealized, 9].Calculate();
                                            }

                                            worksheet.Cells[_endRowDetailTotal, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[_endRowDetailTotal, 9].Calculate();
                                            worksheet.Cells[_endRowDetailTotal, 9].Style.Numberformat.Format = "#,##0.00";

                                            worksheet.Cells["H" + _endRowSum + ":I" + _endRowSum].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["H" + _rowSum + ":I" + _rowSum].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["H" + _rowSum + ":H" + _endRowSum].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["I" + _rowSum + ":I" + _endRowSum].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            int _aa = _rowTotal + 1;
                                            if (_amount != 0)
                                            {
                                                worksheet.Cells[_rowTotal, 8].Formula = "SUM(E" + _aa + "/H" + _RowTotal1 + ")";
                                                worksheet.Cells[_rowTotal, 8].Calculate();
                                                worksheet.Cells[_rowTotal, 8].Style.Numberformat.Format = "#,##0.00";
                                            }


                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;

                                        }

                                        //string _rangeDetail = "A:G";

                                        //using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        //{
                                        //    //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                        //    r.Style.Font.Size = 12;
                                        //    r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        //}



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 9];
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).Width = 10;
                                        worksheet.Column(3).Width = 25;
                                        worksheet.Column(4).Width = 25;
                                        worksheet.Column(5).Width = 25;
                                        worksheet.Column(6).Width = 0;
                                        worksheet.Column(7).Width = 27;
                                        worksheet.Column(8).Width = 25;
                                        worksheet.Column(9).Width = 25;

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&14 UNIT TRUST REPORT";


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        Tools.ExportFromExcelToPDF(filePath, pdfPath);
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
            #endregion

            #region Customer Portfolio All Fund Client
            if (_unitRegistryRpt.ReportName.Equals("Customer Portfolio All Fund Client"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _paramFund = "";
                            string _paramFundFrom = "";
                            string _paramFundTo = "";
                            string _paramFundClient = "";
                            string _status = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = " And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFundFrom = " And A.FundPKFrom in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFundTo = " And A.FundPKTo in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundTo = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = " And A.FundClientPK in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }

                            if (_unitRegistryRpt.Status == 1)
                            {
                                _status = " and  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";

                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _status = " and A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";

                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _status = " and A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";

                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _status = " and A.Status = 1  ";

                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _status = " and A.Status = 3  ";

                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _status = " and (A.Status = 2 or A.Posted = 1) and A.Revised = 0  And A.status not in (3,4)";

                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _status = " and (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0 And A.status not in (3,4)";

                            }


                            cmd.CommandText =

                            @" 
                            declare @CutOffUnitAmount table
(
FundPK int,
FundClientPK int,
UnitAmount numeric(19,8),
date date
)

declare @BegUnitAmount table
(
FundPK int,
FundClientPK int,
UnitAmount numeric(19,8),
date date
)

insert into @CutOffUnitAmount
select FundPK,FundClientPK,isnull(UnitAmount,0),dbo.FWorkingDay(A.Date,1) from FundClientPosition A
where 1 = 1  " + _paramFund + _paramFundClient + @" and Date = '11/29/17'

insert into @BegUnitAmount
select FundPK,FundClientPK,isnull(UnitAmount,0),dbo.FWorkingDay(A.Date,1) from FundClientPosition A
where 1 = 1 " + _paramFund + _paramFundClient + @" and Date = 
(
select max(Date) from FundClientPosition where Date <= @DateFrom

)

if exists (
select * from @CutOffUnitAmount 
)
begin
	Select C.FundClientPK,isnull (C.ID,'') ClientCode,C.Name,
	Case when C.ClientCategory = 1 then ISNULL(C.AlamatInd1,'') else ISNULL(C.AlamatPerusahaan,'') end Address,
	Case when C.ClientCategory = 1 then ISNULL(c.TeleponSelular,'') + '-' + ISNULL(C.TeleponBisnis,'') 
	ELSE ISNULL(C.PhoneIns1,'') + '-' + ISNULL(C.TeleponBisnis,'') end Phone, 
	ISNULL(B.Name,'') FundName,ISNULL(B.FundPK,0) FundPK, 'BEGBALANCE' Description,'1900-01-01' ValueDate,0 TotalCashAmount,0 NAV,A.UnitAmount TotalUnitAmount,
A.UnitAmount Balance,A.UnitAmount Unit,
isnulL(D.AVGNav,0) AverageNAV,
isnull(E.Nav,0) ClosingNAV,0 FundValue,
	0 MarketValue,0 UnrealizedGainLoss,0 Percentage
	from @CutOffUnitAmount A
	LEFT JOIN Fund B on A.FundPK = B.FundPK and B.status in (1,2)
	LEFT JOIN FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
LEFT JOIN FundClientPosition D on A.FundPK = D.FundPK and A.FundClientPK = D.FundClientPK and D.Date = @dateto
LEFT JOIN CloseNAV E ON E.Date = @dateto AND B.FundPK = E.FundPK AND E.status IN (1,2)
	where 1 = 1
--A.FundPk = @FundPK and A.FundClientPK = @FundClientPK
" + _paramFund + _paramFundClient + @"

union all

SELECT * from (
Select C.FundClientPK,isnull (C.ID,'') ClientCode,C.Name,
Case when C.ClientCategory = 1 then ISNULL(C.AlamatInd1,'') else ISNULL(C.AlamatPerusahaan,'') end Address,
Case when C.ClientCategory = 1 then ISNULL(c.TeleponSelular,'') + '-' + ISNULL(C.TeleponBisnis,'') 
ELSE ISNULL(C.PhoneIns1,'') + '-' + ISNULL(C.TeleponBisnis,'') end Phone, 
ISNULL(B.Name,'') FundName,ISNULL(B.FundPK,0) FundPK, 'Subscribe' Description,A.ValueDate,A.TotalCashAmount,A.NAV,A.TotalUnitAmount,
case when isnull(F.UnitAmount,0) = 0 then isnull(G.UnitAmount,0) else isnull(F.UnitAmount,0) end Balance,isnull(D.UnitAmount,0) Unit,
dbo.FGetAVGForFundClientPosition(@DateTo,D.FundClientPK,D.FundPK) AverageNAV,
isnull(E.Nav,0) ClosingNAV,0 FundValue,
0 MarketValue,0 UnrealizedGainLoss,0 Percentage
from ClientSubscription A
LEFT JOIN Fund B on A.FundPK = B.FundPK and B.status in (1,2)
LEFT JOIN FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
LEFT JOIN FundClientPosition D on C.FundClientPK = D.FundClientPK and B.FundPK = D.FundPK 
LEFT JOIN CloseNAV E ON D.Date = E.Date AND B.FundPK = E.FundPK AND E.status IN (1,2)
LEFT JOIN @BegUnitAmount F on A.FundClientPK = F.FundClientPK and A.FundPK = F.FundPK 
LEFT JOIN @CutOffUnitAmount G on A.FundClientPK = G.FundClientPK and A.FundPK = G.FundPK 
where ValueDate between @DateFrom and @DateTo and D.Date = @DateTo 
and A.Type <> 3 
--and A.FundPk = @FundPK and A.FundClientPK = @FundClientPK

" + _paramFund + _paramFundClient + _status + @"

UNION ALL

Select C.FundClientPK,isnull (C.ID,'') ClientCode,C.Name,
Case when C.ClientCategory = 1 then ISNULL(C.AlamatInd1,'') else ISNULL(C.AlamatPerusahaan,'') end Address,
Case when C.ClientCategory = 1 then ISNULL(c.TeleponSelular,'') + '-' + ISNULL(C.TeleponBisnis,'') 
ELSE ISNULL(C.PhoneIns1,'') + '-' + ISNULL(C.TeleponBisnis,'') end Phone, 
ISNULL(B.Name,'') FundName,ISNULL(B.FundPK,0) FundPK, 'Redemption' Description,A.ValueDate,A.TotalCashAmount,A.NAV,A.TotalUnitAmount,
 case when isnull(F.UnitAmount,0) = 0 then isnull(G.UnitAmount,0) else isnull(F.UnitAmount,0) end Balance,isnull(D.UnitAmount,0) Unit,
dbo.FGetAVGForFundClientPosition(@DateTo,D.FundClientPK,D.FundPK) AverageNAV,
isnull(E.Nav,0) ClosingNAV,0 FundValue,
0 MarketValue,0 UnrealizedGainLoss,0 Percentage
from ClientRedemption A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
left join FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
LEFT JOIN FundClientPosition D on C.FundClientPK = D.FundClientPK and B.FundPK = D.FundPK 
LEFT JOIN CloseNAV E ON D.Date = E.Date AND B.FundPK = E.FundPK AND E.status IN (1,2)
LEFT JOIN @BegUnitAmount F on A.FundClientPK = F.FundClientPK and A.FundPK = F.FundPK 
LEFT JOIN @CutOffUnitAmount G on A.FundClientPK = G.FundClientPK and A.FundPK = G.FundPK 
where ValueDate between @DateFrom and @DateTo and D.Date = @DateTo 
and A.Type <> 3  
--and A.FundPk = @FundPK and A.FundClientPK = @FundClientPK
" + _paramFund + _paramFundClient + _status + @"

UNION ALL

Select C.FundClientPK,isnull (C.ID,'') ClientCode,C.Name,
Case when C.ClientCategory = 1 then ISNULL(C.AlamatInd1,'') else ISNULL(C.AlamatPerusahaan,'') end Address,
Case when C.ClientCategory = 1 then ISNULL(c.TeleponSelular,'') + '-' + ISNULL(C.TeleponBisnis,'') 
ELSE ISNULL(C.PhoneIns1,'') + '-' + ISNULL(C.TeleponBisnis,'') end Phone, 
ISNULL(B.Name,'') FundName,ISNULL(B.FundPK,0) FundPK, 'Switching OUT' Description,A.ValueDate,A.TotalCashAmountFundFrom,A.NAVFundFrom,A.TotalUnitAmountFundFrom,
case when isnull(F.UnitAmount,0) = 0 then isnull(G.UnitAmount,0) else isnull(F.UnitAmount,0) end Balance,isnull(D.UnitAmount,0) Unit,
dbo.FGetAVGForFundClientPosition(@DateTo,D.FundClientPK,D.FundPK) AverageNAV,
isnull(E.Nav,0) ClosingNAV,0 FundValue,
0 MarketValue,0 UnrealizedGainLoss,0 Percentage
from ClientSwitching A
left join Fund B on A.FundPKFrom = B.FundPK and B.status in (1,2)
left join FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
LEFT JOIN FundClientPosition D on C.FundClientPK = D.FundClientPK and B.FundPK = D.FundPK 
LEFT JOIN CloseNAV E ON D.Date = E.Date AND B.FundPK = E.FundPK AND E.status IN (1,2)
LEFT JOIN @BegUnitAmount F on A.FundClientPK = F.FundClientPK and A.FundPKFrom = F.FundPK 
LEFT JOIN @CutOffUnitAmount G on A.FundClientPK = G.FundClientPK and A.FundPKFrom = G.FundPK 
where ValueDate between @DateFrom and @DateTo and D.Date = @DateTo 
and A.Type <> 3    
--and A.FundPKFrom = @FundPK and A.FundClientPK = @FundClientPK
" + _paramFundFrom + _paramFundClient + _status + @"

UNION ALL

Select C.FundClientPK,isnull (C.ID,'') ClientCode,C.Name,
Case when C.ClientCategory = 1 then ISNULL(C.AlamatInd1,'') else ISNULL(C.AlamatPerusahaan,'') end Address,
Case when C.ClientCategory = 1 then ISNULL(c.TeleponSelular,'') + '-' + ISNULL(C.TeleponBisnis,'') 
ELSE ISNULL(C.PhoneIns1,'') + '-' + ISNULL(C.TeleponBisnis,'') end Phone, 
ISNULL(B.Name,'') FundName,ISNULL(B.FundPK,0) FundPK, 'Switching IN' Description,A.ValueDate,A.TotalCashAmountFundTo,A.NAVFundTo,A.TotalUnitAmountFundTo,
case when isnull(F.UnitAmount,0) = 0 then isnull(G.UnitAmount,0) else isnull(F.UnitAmount,0) end Balance,isnull(D.UnitAmount,0) Unit,
dbo.FGetAVGForFundClientPosition(@DateTo,D.FundClientPK,D.FundPK) AverageNAV,
isnull(E.Nav,0) ClosingNAV,0 FundValue,
0 MarketValue,0 UnrealizedGainLoss,0 Percentage
from ClientSwitching A
left join Fund B on A.FundPKTo = B.FundPK and B.status in (1,2)
left join FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
LEFT JOIN FundClientPosition D on C.FundClientPK = D.FundClientPK and B.FundPK = D.FundPK 
LEFT JOIN CloseNAV E ON D.Date = E.Date AND B.FundPK = E.FundPK AND E.status IN (1,2)
LEFT JOIN @BegUnitAmount F on A.FundClientPK = F.FundClientPK and A.FundPKTo = F.FundPK 
LEFT JOIN @CutOffUnitAmount G on A.FundClientPK = G.FundClientPK and A.FundPKTo = G.FundPK 
where ValueDate between @DateFrom and @DateTo and D.Date = @DateTo 
and A.Type <> 3    
--and A.FundPKTo = @FundPK and A.FundClientPK = @FundClientPK 
" + _paramFundTo + _paramFundClient + _status + @"

)A 
group by FundClientPK,ClientCode,Name,Address,Phone,FundName,FundPK,Description,ValueDate,TotalCashAmount,NAV,TotalUnitAmount,Balance,Unit,AverageNAV,ClosingNAV,FundValue,MarketValue,UnrealizedGainLoss,Percentage
--order by ClientCode,A.FundName,ValueDate,A.FundPK,name asc

end

else if exists (
select * from @BegUnitAmount
)
begin
Select C.FundClientPK,isnull (C.ID,'') ClientCode,C.Name,
	Case when C.ClientCategory = 1 then ISNULL(C.AlamatInd1,'') else ISNULL(C.AlamatPerusahaan,'') end Address,
	Case when C.ClientCategory = 1 then ISNULL(c.TeleponSelular,'') + '-' + ISNULL(C.TeleponBisnis,'') 
	ELSE ISNULL(C.PhoneIns1,'') + '-' + ISNULL(C.TeleponBisnis,'') end Phone, 
	ISNULL(B.Name,'') FundName,ISNULL(B.FundPK,0) FundPK, 'BEGBALANCE' Description,'1900-01-01' ValueDate,0 TotalCashAmount,0 NAV,A.UnitAmount TotalUnitAmount,
A.UnitAmount Balance,A.UnitAmount Unit,
isnulL(D.AVGNav,0) AverageNAV,
isnull(E.Nav,0) ClosingNAV,0 FundValue,
	0 MarketValue,0 UnrealizedGainLoss,0 Percentage
	from @BegUnitAmount A
	LEFT JOIN Fund B on A.FundPK = B.FundPK and B.status in (1,2)
	LEFT JOIN FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
LEFT JOIN FundClientPosition D on A.FundPK = D.FundPK and A.FundClientPK = D.FundClientPK and D.Date = @dateto
LEFT JOIN CloseNAV E ON E.Date = @dateto AND B.FundPK = E.FundPK AND E.status IN (1,2)
	where 1 = 1
--A.FundPk = @FundPK and A.FundClientPK = @FundClientPK
" + _paramFund + _paramFundClient + @"

union all

SELECT * from (
Select C.FundClientPK,isnull (C.ID,'') ClientCode,C.Name,
Case when C.ClientCategory = 1 then ISNULL(C.AlamatInd1,'') else ISNULL(C.AlamatPerusahaan,'') end Address,
Case when C.ClientCategory = 1 then ISNULL(c.TeleponSelular,'') + '-' + ISNULL(C.TeleponBisnis,'') 
ELSE ISNULL(C.PhoneIns1,'') + '-' + ISNULL(C.TeleponBisnis,'') end Phone, 
ISNULL(B.Name,'') FundName,ISNULL(B.FundPK,0) FundPK, 'Subscribe' Description,A.ValueDate,A.TotalCashAmount,A.NAV,A.TotalUnitAmount,
case when isnull(F.UnitAmount,0) = 0 then isnull(G.UnitAmount,0) else isnull(F.UnitAmount,0) end Balance,isnull(D.UnitAmount,0) Unit,
dbo.FGetAVGForFundClientPosition(@DateTo,D.FundClientPK,D.FundPK) AverageNAV,
isnull(E.Nav,0) ClosingNAV,0 FundValue,
0 MarketValue,0 UnrealizedGainLoss,0 Percentage
from ClientSubscription A
LEFT JOIN Fund B on A.FundPK = B.FundPK and B.status in (1,2)
LEFT JOIN FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
LEFT JOIN FundClientPosition D on C.FundClientPK = D.FundClientPK and B.FundPK = D.FundPK 
LEFT JOIN CloseNAV E ON D.Date = E.Date AND B.FundPK = E.FundPK AND E.status IN (1,2)
LEFT JOIN @BegUnitAmount F on A.FundClientPK = F.FundClientPK and A.FundPK = F.FundPK 
LEFT JOIN @CutOffUnitAmount G on A.FundClientPK = G.FundClientPK and A.FundPK = G.FundPK 
where ValueDate between @DateFrom and @DateTo and D.Date = @DateTo 
and A.Type <> 3 
--and A.FundPk = @FundPK and A.FundClientPK = @FundClientPK

" + _paramFund + _paramFundClient + _status + @"

UNION ALL

Select C.FundClientPK,isnull (C.ID,'') ClientCode,C.Name,
Case when C.ClientCategory = 1 then ISNULL(C.AlamatInd1,'') else ISNULL(C.AlamatPerusahaan,'') end Address,
Case when C.ClientCategory = 1 then ISNULL(c.TeleponSelular,'') + '-' + ISNULL(C.TeleponBisnis,'') 
ELSE ISNULL(C.PhoneIns1,'') + '-' + ISNULL(C.TeleponBisnis,'') end Phone, 
ISNULL(B.Name,'') FundName,ISNULL(B.FundPK,0) FundPK, 'Redemption' Description,A.ValueDate,A.TotalCashAmount,A.NAV,A.TotalUnitAmount,
 case when isnull(F.UnitAmount,0) = 0 then isnull(G.UnitAmount,0) else isnull(F.UnitAmount,0) end Balance,isnull(D.UnitAmount,0) Unit,
dbo.FGetAVGForFundClientPosition(@DateTo,D.FundClientPK,D.FundPK) AverageNAV,
isnull(E.Nav,0) ClosingNAV,0 FundValue,
0 MarketValue,0 UnrealizedGainLoss,0 Percentage
from ClientRedemption A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
left join FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
LEFT JOIN FundClientPosition D on C.FundClientPK = D.FundClientPK and B.FundPK = D.FundPK 
LEFT JOIN CloseNAV E ON D.Date = E.Date AND B.FundPK = E.FundPK AND E.status IN (1,2)
LEFT JOIN @BegUnitAmount F on A.FundClientPK = F.FundClientPK and A.FundPK = F.FundPK 
LEFT JOIN @CutOffUnitAmount G on A.FundClientPK = G.FundClientPK and A.FundPK = G.FundPK 
where ValueDate between @DateFrom and @DateTo and D.Date = @DateTo 
and A.Type <> 3  
--and A.FundPk = @FundPK and A.FundClientPK = @FundClientPK
" + _paramFund + _paramFundClient + _status + @"

UNION ALL

Select C.FundClientPK,isnull (C.ID,'') ClientCode,C.Name,
Case when C.ClientCategory = 1 then ISNULL(C.AlamatInd1,'') else ISNULL(C.AlamatPerusahaan,'') end Address,
Case when C.ClientCategory = 1 then ISNULL(c.TeleponSelular,'') + '-' + ISNULL(C.TeleponBisnis,'') 
ELSE ISNULL(C.PhoneIns1,'') + '-' + ISNULL(C.TeleponBisnis,'') end Phone, 
ISNULL(B.Name,'') FundName,ISNULL(B.FundPK,0) FundPK, 'Switching OUT' Description,A.ValueDate,A.TotalCashAmountFundFrom,A.NAVFundFrom,A.TotalUnitAmountFundFrom,
case when isnull(F.UnitAmount,0) = 0 then isnull(G.UnitAmount,0) else isnull(F.UnitAmount,0) end Balance,isnull(D.UnitAmount,0) Unit,
dbo.FGetAVGForFundClientPosition(@DateTo,D.FundClientPK,D.FundPK) AverageNAV,
isnull(E.Nav,0) ClosingNAV,0 FundValue,
0 MarketValue,0 UnrealizedGainLoss,0 Percentage
from ClientSwitching A
left join Fund B on A.FundPKFrom = B.FundPK and B.status in (1,2)
left join FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
LEFT JOIN FundClientPosition D on C.FundClientPK = D.FundClientPK and B.FundPK = D.FundPK 
LEFT JOIN CloseNAV E ON D.Date = E.Date AND B.FundPK = E.FundPK AND E.status IN (1,2)
LEFT JOIN @BegUnitAmount F on A.FundClientPK = F.FundClientPK and A.FundPKFrom = F.FundPK 
LEFT JOIN @CutOffUnitAmount G on A.FundClientPK = G.FundClientPK and A.FundPKFrom = G.FundPK 
where ValueDate between @DateFrom and @DateTo and D.Date = @DateTo 
and A.Type <> 3    
--and A.FundPKFrom = @FundPK and A.FundClientPK = @FundClientPK
" + _paramFundFrom + _paramFundClient + _status + @"

UNION ALL

Select C.FundClientPK,isnull (C.ID,'') ClientCode,C.Name,
Case when C.ClientCategory = 1 then ISNULL(C.AlamatInd1,'') else ISNULL(C.AlamatPerusahaan,'') end Address,
Case when C.ClientCategory = 1 then ISNULL(c.TeleponSelular,'') + '-' + ISNULL(C.TeleponBisnis,'') 
ELSE ISNULL(C.PhoneIns1,'') + '-' + ISNULL(C.TeleponBisnis,'') end Phone, 
ISNULL(B.Name,'') FundName,ISNULL(B.FundPK,0) FundPK, 'Switching IN' Description,A.ValueDate,A.TotalCashAmountFundTo,A.NAVFundTo,A.TotalUnitAmountFundTo,
case when isnull(F.UnitAmount,0) = 0 then isnull(G.UnitAmount,0) else isnull(F.UnitAmount,0) end Balance,isnull(D.UnitAmount,0) Unit,
dbo.FGetAVGForFundClientPosition(@DateTo,D.FundClientPK,D.FundPK) AverageNAV,
isnull(E.Nav,0) ClosingNAV,0 FundValue,
0 MarketValue,0 UnrealizedGainLoss,0 Percentage
from ClientSwitching A
left join Fund B on A.FundPKTo = B.FundPK and B.status in (1,2)
left join FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
LEFT JOIN FundClientPosition D on C.FundClientPK = D.FundClientPK and B.FundPK = D.FundPK 
LEFT JOIN CloseNAV E ON D.Date = E.Date AND B.FundPK = E.FundPK AND E.status IN (1,2)
LEFT JOIN @BegUnitAmount F on A.FundClientPK = F.FundClientPK and A.FundPKTo = F.FundPK 
LEFT JOIN @CutOffUnitAmount G on A.FundClientPK = G.FundClientPK and A.FundPKTo = G.FundPK 
where ValueDate between @DateFrom and @DateTo and D.Date = @DateTo 
and A.Type <> 3    
--and A.FundPKTo = @FundPK and A.FundClientPK = @FundClientPK 
" + _paramFundTo + _paramFundClient + _status + @"

)A 
group by FundClientPK,ClientCode,Name,Address,Phone,FundName,FundPK,Description,ValueDate,TotalCashAmount,NAV,TotalUnitAmount,Balance,Unit,AverageNAV,ClosingNAV,FundValue,MarketValue,UnrealizedGainLoss,Percentage
--order by ClientCode,A.FundName,ValueDate,A.FundPK,name asc

end
                            ";

                            cmd.CommandTimeout = 0;

                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@BegginingBalanceStatus", _unitRegistryRpt.BegDate);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                //if (!dr0.HasRows)
                                //{
                                //    return false;
                                //}
                                //else
                                //{
                                string filePath = Tools.ReportsPath + "CustomerPortfolioAllFundClient" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "CustomerPortfolioAllFundClient" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "UnitRegistryReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Customer Portfolio All Fund Client");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<CustomerPortfolioAllFundClient> rList = new List<CustomerPortfolioAllFundClient>();
                                    while (dr0.Read())
                                    {
                                        CustomerPortfolioAllFundClient rSingle = new CustomerPortfolioAllFundClient();
                                        rSingle.ClientID = Convert.ToString(dr0["ClientCode"]);
                                        rSingle.FundClientPK = Convert.ToInt32(dr0["FundClientPK"]);
                                        rSingle.ClientName = Convert.ToString(dr0["Name"]);
                                        rSingle.CIF = Convert.ToString(dr0["Address"]);
                                        rSingle.CurrencyID = Convert.ToString(dr0["Phone"]);
                                        rSingle.FundPK = Convert.ToInt32(dr0["FundPK"]);
                                        rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                        rSingle.Description = Convert.ToString(dr0["Description"]);
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.CashBalance = Convert.ToDecimal(dr0["TotalCashAmount"]);
                                        rSingle.Nav = Convert.ToDecimal(dr0["Nav"]);
                                        //rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitBalance"]);
                                        rSingle.Balance = Convert.ToDecimal(dr0["Balance"]);
                                        rSingle.Address = Convert.ToString(dr0["Address"]);
                                        rSingle.Unit = Convert.ToDecimal(dr0["Unit"]);
                                        rSingle.AvgNav = Convert.ToDecimal(dr0["AverageNAV"]);
                                        rSingle.CloseNav = Convert.ToDecimal(dr0["ClosingNAV"]);
                                        rSingle.FundValue = Convert.ToDecimal(dr0["FundValue"]);
                                        rSingle.MarketValue = Convert.ToDecimal(dr0["MarketValue"]);
                                        rSingle.Unrealized = Convert.ToDecimal(dr0["UnrealizedGainLoss"]);
                                        rSingle.UnrealizedPercent = Convert.ToDecimal(dr0["Percentage"]);
                                        rList.Add(rSingle);

                                    }

                                    var QueryByFundID =
                                        from r in rList
                                        orderby r.ClientID, r.ClientName, r.ValueDate ascending
                                        group r by new { r.FundClientPK, r.ClientID, r.ClientName, r.Address, r.FundPK, r.FundName, r.AvgNav, r.Unit, r.CloseNav, r.FundValue, r.MarketValue, r.Unrealized, r.UnrealizedPercent, r.Balance } into rGroup
                                        select rGroup;


                                    int incRowExcel = 0;
                                    int RowA = incRowExcel;
                                    int RowX = 0;
                                    DateTime d1 = Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom);
                                    d1 = d1.AddDays(-1);
                                    int count = 0;
                                    foreach (var rsHeader in QueryByFundID)
                                    {

                                        int RowZ = incRowExcel;
                                        incRowExcel++;

                                        worksheet.Row(incRowExcel).Height = 40;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                        worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                        worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;

                                        worksheet.Cells[incRowExcel, 8].Value = "CUSTOMER PORTFOLIO";
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Size = 18;
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 8].Value = "As of Date : " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("dd-MMM-yyyy"); ;
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Size = 18;
                                        worksheet.Row(incRowExcel).Height = 40;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyAddress();
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Style.WrapText = true;
                                        worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 18;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        incRowExcel = incRowExcel + 3;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Tel ";
                                        //worksheet.Cells[incRowExcel, 2].Value = ":";
                                        //worksheet.Cells[incRowExcel, 3].Value = _host.Get_CompanyPhone();
                                        //worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Font.Bold = true;

                                        //worksheet.Cells[incRowExcel, 4].Value = "Fax   : ";
                                        //worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        //worksheet.Cells[incRowExcel, 5].Value = _host.Get_CompanyFax();
                                        //worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;

                                        //incRowExcel = incRowExcel + 2;
                                        //worksheet.Cells[incRowExcel, 9].Value = "As of Date ";
                                        //worksheet.Cells[incRowExcel, 10].Value = ":";
                                        //worksheet.Cells[incRowExcel, 11].Value = Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                        //worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Style.Font.Bold = true;
                                        //worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                        //worksheet.Cells[incRowExcel, 9].Style.Font.Size = 18;

                                        //incRowExcel = incRowExcel + 1;

                                        //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1].Value = "A/C No ";
                                        //worksheet.Cells[incRowExcel, 2].Value = ":";
                                        //worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.ClientID;
                                        //worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        //worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        //worksheet.Cells["C" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        //worksheet.Cells[incRowExcel, 9].Value = "Date ";
                                        //worksheet.Cells[incRowExcel, 10].Value = ":";
                                        //worksheet.Cells[incRowExcel, 11].Value = Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                        //worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Style.Font.Bold = true;
                                        //worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Name ";
                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.ClientName;
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        worksheet.Cells["C" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        //worksheet.Cells[incRowExcel, 9].Value = "Time ";
                                        //worksheet.Cells[incRowExcel, 10].Value = ":";
                                        //worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 11].Value = DateTime.Now.ToString("H:mm:ss");
                                        //worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;

                                        incRowExcel++;
                                        worksheet.Row(incRowExcel).Height = 45;
                                        worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Address ";
                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                        worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Address;
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 4].Merge = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 4].Style.WrapText = true;


                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "Phone ";
                                        worksheet.Cells[incRowExcel, 2].Value = ":";
                                        worksheet.Cells[incRowExcel, 3].Value = _host.Get_CompanyPhone();
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        worksheet.Cells["C" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        //worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 9].Value = "Fax ";
                                        //worksheet.Cells[incRowExcel, 10].Value = ":";
                                        //worksheet.Cells[incRowExcel, 11].Value = _host.Get_CompanyFax();
                                        //worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        //worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        //worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;


                                        incRowExcel = incRowExcel + 2;
                                        //Row B = 3
                                        int RowBZ = incRowExcel;
                                        int RowGZ = incRowExcel + 1;

                                        worksheet.Row(incRowExcel).Height = 25;
                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Merge = true;
                                        worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 2].Value = "Fund";
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells["B" + RowBZ + ":D" + RowGZ].Merge = true;
                                        worksheet.Cells["B" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["B" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 5].Value = "Description";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Merge = true;
                                        worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 6].Value = "Trans. Date";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Merge = true;
                                        worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 7].Value = "Net Amount";
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Merge = true;
                                        worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 8].Value = "Nav";
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Merge = true;
                                        worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 9].Value = "Unit";
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells["I" + RowBZ + ":J" + RowGZ].Merge = true;
                                        worksheet.Cells["I" + RowBZ + ":J" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["I" + RowBZ + ":J" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[incRowExcel, 11].Value = "Balance";
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells["K" + RowBZ + ":L" + RowGZ].Merge = true;
                                        worksheet.Cells["K" + RowBZ + ":L" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["K" + RowBZ + ":L" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "Beginning Balance";
                                        worksheet.Cells[incRowExcel, 8].Value = Get_CloseNavByFundPK_12(rsHeader.Key.FundPK, d1);
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 9].Value = rsHeader.Key.Balance;
                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 11].Formula = "SUM(I" + incRowExcel + ")";
                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 11].Calculate();

                                        // Row C = 4
                                        int RowC = incRowExcel;

                                        incRowExcel++;
                                        //area header



                                        int _no = 1;
                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        decimal Balance = 0;
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            worksheet.Cells["A" + RowBZ + ":L" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowBZ + ":L" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            if (rsDetail.Description != "BEGBALANCE")
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;


                                                //ThickBox Border

                                                worksheet.Cells["A" + RowBZ + ":L" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowBZ + ":L" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.Description;

                                                worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsDetail.ValueDate).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.CashBalance;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Nav;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                if (rsDetail.Nav != 0)
                                                {
                                                    worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Formula = "SUM(G" + incRowExcel + "/H" + incRowExcel + ")";
                                                    worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Calculate();
                                                    worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                                }
                                                else
                                                {
                                                    worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Value = 0;
                                                    worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                                }

                                                if (rsDetail.Description == "Subscribe" || rsDetail.Description == "Switching IN")
                                                {
                                                    int incRowExcelA = incRowExcel - 1;
                                                    worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + incRowExcelA + "+I" + incRowExcel + ")";
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 11].Calculate();
                                                    worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                                    worksheet.Row(incRowExcel).Height = 15;
                                                }
                                                else if (rsDetail.Description == "Redemption" || rsDetail.Description == "Switching OUT")
                                                {
                                                    int incRowExcelA = incRowExcel - 1;
                                                    worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + incRowExcelA + "-I" + incRowExcel + ")";
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 11].Calculate();
                                                    worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                                    worksheet.Row(incRowExcel).Height = 15;
                                                }

                                                count++;
                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                RowX = incRowExcel;
                                                incRowExcel++;

                                            }

                                            _endRowDetail = incRowExcel - 1;
                                            RowX = incRowExcel - 1;

                                        }
                                        incRowExcel = incRowExcel + 2;
                                        int RowCZ = incRowExcel;
                                        int RowHZ = incRowExcel + 1;

                                        worksheet.Cells["A" + RowCZ + ":L" + RowHZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowCZ + ":L" + RowHZ].Style.Border.Bottom.Style = ExcelBorderStyle.Double;


                                        worksheet.Cells[incRowExcel, 1].Value = "Fund";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + RowCZ + ":C" + RowHZ].Merge = true;
                                        worksheet.Cells["A" + RowCZ + ":C" + RowHZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["A" + RowCZ + ":C" + RowHZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["A" + RowCZ + ":C" + RowHZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 4].Value = "Unit";
                                        worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                        worksheet.Cells["D" + RowCZ + ":D" + RowHZ].Merge = true;
                                        worksheet.Cells["D" + RowCZ + ":D" + RowHZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["D" + RowCZ + ":D" + RowHZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["D" + RowCZ + ":D" + RowHZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 5].Value = "Average NAV";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells["E" + RowCZ + ":E" + RowHZ].Merge = true;
                                        worksheet.Cells["E" + RowCZ + ":E" + RowHZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["E" + RowCZ + ":E" + RowHZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["E" + RowCZ + ":E" + RowHZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 6].Value = "Closing NAV";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells["F" + RowCZ + ":F" + RowHZ].Merge = true;
                                        worksheet.Cells["F" + RowCZ + ":F" + RowHZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["F" + RowCZ + ":F" + RowHZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["F" + RowCZ + ":F" + RowHZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 7].Value = "Fund Value";
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells["G" + RowCZ + ":G" + RowHZ].Merge = true;
                                        worksheet.Cells["G" + RowCZ + ":G" + RowHZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["G" + RowCZ + ":G" + RowHZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["G" + RowCZ + ":G" + RowHZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 8].Value = "Market Value";
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells["H" + RowCZ + ":H" + RowHZ].Merge = true;
                                        worksheet.Cells["H" + RowCZ + ":H" + RowHZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["H" + RowCZ + ":H" + RowHZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["H" + RowCZ + ":H" + RowHZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 9].Value = "Unrealized Gain/(Lost)";
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells["I" + RowCZ + ":J" + RowHZ].Merge = true;
                                        worksheet.Cells["I" + RowCZ + ":J" + RowHZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["I" + RowCZ + ":J" + RowHZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["I" + RowCZ + ":J" + RowHZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 11].Value = "%";
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells["K" + RowCZ + ":L" + RowHZ].Merge = true;
                                        worksheet.Cells["K" + RowCZ + ":L" + RowHZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["K" + RowCZ + ":L" + RowHZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["K" + RowCZ + ":L" + RowHZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                        incRowExcel++;

                                        int RowS = incRowExcel;

                                        incRowExcel++;
                                        //area header

                                        //int _no2 = 1;
                                        int _startRowDetail2 = incRowExcel;
                                        int _endRowDetail2 = 0;


                                        //ThickBox Border

                                        worksheet.Cells["A" + RowCZ + ":L" + RowHZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowCZ + ":L" + RowHZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //area detail
                                        //worksheet.Cells[incRowExcel, 1].Value = _no2;
                                        worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.FundName;
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 4].Formula = "SUM(K" + RowX + ")";
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 5].Value = rsHeader.Key.AvgNav;
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 6].Value = rsHeader.Key.CloseNav;
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(D" + incRowExcel + "*E" + incRowExcel + ")";
                                        worksheet.Cells[incRowExcel, 7].Calculate();
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(D" + incRowExcel + "*F" + incRowExcel + ")";
                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(H" + incRowExcel + "-G" + incRowExcel + ")";
                                        worksheet.Cells[incRowExcel, 9].Calculate();
                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 11].Formula = "SUM(I" + incRowExcel + "/G" + incRowExcel + ")";
                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 11].Calculate();
                                        worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                        //worksheet.Row(incRowExcel).Height = 15;

                                        _endRowDetail2 = incRowExcel;
                                        //_no2++;
                                        incRowExcel++;

                                        int _endRowDetail3 = incRowExcel;
                                        worksheet.Cells["A" + RowCZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowCZ + ":C" + _endRowDetail2].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowCZ + ":C" + _endRowDetail2].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _endRowDetail2 + ":L" + _endRowDetail2].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["D" + RowCZ + ":D" + _endRowDetail2].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowCZ + ":E" + _endRowDetail2].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowCZ + ":F" + _endRowDetail2].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowCZ + ":G" + _endRowDetail2].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + RowCZ + ":H" + _endRowDetail2].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + RowCZ + ":J" + _endRowDetail2].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["K" + RowCZ + ":L" + _endRowDetail2].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        incRowExcel++;
                                        worksheet.Cells[_endRowDetail3, 6].Value = "TOTAL FUND";
                                        worksheet.Cells[_endRowDetail3, 6].Style.Font.Bold = true;
                                        worksheet.Cells[_endRowDetail3, 7].Formula = "SUM(G" + _startRowDetail2 + ":G" + _endRowDetail2 + ")";
                                        worksheet.Cells[_endRowDetail3, 8].Formula = "SUM(H" + _startRowDetail2 + ":H" + _endRowDetail2 + ")";
                                        worksheet.Cells[_endRowDetail3, 9].Formula = "SUM(I" + _startRowDetail2 + ":I" + _endRowDetail2 + ")";
                                        worksheet.Cells[_endRowDetail3, 7].Style.Font.Bold = true;
                                        worksheet.Cells[_endRowDetail3, 8].Style.Font.Bold = true;
                                        worksheet.Cells[_endRowDetail3, 9].Style.Font.Bold = true;
                                        worksheet.Cells[_endRowDetail3, 7].Calculate();
                                        worksheet.Cells[_endRowDetail3, 8].Calculate();
                                        worksheet.Cells[_endRowDetail3, 9].Calculate();
                                        worksheet.Cells[_endRowDetail3, 7].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[_endRowDetail3, 8].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[_endRowDetail3, 9].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells["I" + _endRowDetail3 + ":J" + _endRowDetail3].Merge = true;
                                        incRowExcel++;

                                        worksheet.Cells["A" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowBZ + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowBZ + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["B" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["C" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["D" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowBZ + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowBZ + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["E" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["F" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowBZ + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowBZ + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["G" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowBZ + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowBZ + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["H" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + RowBZ + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + RowBZ + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["I" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["J" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + RowBZ + ":J" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["J" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["K" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["L" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["L" + RowBZ + ":L" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["L" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                    }

                                    int _lastRow = incRowExcel;

                                    string _rangeA = "A:L" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 12;
                                    }

                                    worksheet.DeleteRow(_lastRow);


                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 12];
                                    worksheet.Column(1).Width = 10;
                                    worksheet.Column(2).Width = 5;
                                    worksheet.Column(3).Width = 20;
                                    worksheet.Column(4).Width = 20;
                                    worksheet.Column(5).Width = 30;
                                    worksheet.Column(6).Width = 30;
                                    worksheet.Column(7).Width = 30;
                                    worksheet.Column(8).Width = 30;
                                    worksheet.Column(9).Width = 30;
                                    worksheet.Column(10).Width = 5;
                                    worksheet.Column(11).Width = 15;
                                    worksheet.Column(12).Width = 15;

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                    //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B SUBSCRIPTION \n &28&B Batch Form";



                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
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

                                    ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                    package.Save();
                                    if (_unitRegistryRpt.DownloadMode == "PDF")
                                    {
                                        Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                    }

                                    return true;

                                }

                                //}

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
            #endregion

            else
            {
                return false;
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

                        cmd.CommandText = @"

                        declare @FundPK int
                        declare @InstrumentPK int 
    
                        Declare @ARManagementFee int 
                        Declare @TaxARManagementFee int 
                        Declare @ManagementFeeExpense int 


                        declare @ManagementFeeAmount numeric(22,6)

                        declare @ARManagementFeeAmount numeric(22,6)
                        declare @TaxARManagementFeeAmount numeric(22,6)
                        declare @ManagementFeeExpenseAmount numeric(22,6)

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
                        select FundPK,ManagementFeeAmount from FundDailyFee where Date = @Date
                        OPEN A 
                        FETCH NEXT FROM A 
                        INTO @FundPK,@ManagementFeeAmount   
                        WHILE @@FETCH_STATUS = 0  
                        BEGIN 


                        select @InstrumentPK = InstrumentPK From Instrument A 
                        left join Fund B on A.ID = B.ID and B.Status = 2 where A.Status = 2 and B.FundPK = @FundPK         
 
                        Select @ARManagementFee = ARManagementFee,@TaxARManagementFee =  TaxARManagementFee,
                        @ManagementFeeExpense = ManagementFeeExpense 
                        From AccountingSetup Where Status = 2


                        IF (@ManagementFeeAmount) > 0
                        BEGIN

                        set @AutoNo = @AutoNo + 1   


                        set @ManagementFeeExpenseAmount = @ManagementFeeAmount
                        set @TaxARManagementFeeAmount = 0.02 * @ManagementFeeAmount
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
                        END

                        ELSE

                        BEGIN
                        set @AutoNo = @AutoNo + 1   


                        set @ManagementFeeExpenseAmount = @ManagementFeeAmount
                        set @TaxARManagementFeeAmount = 0.02 * @ManagementFeeAmount
                        set @ARManagementFeeAmount = @ManagementFeeAmount - @TaxARManagementFeeAmount

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@ARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@ARManagementFeeAmount), 0,abs(@ARManagementFeeAmount),1,0,abs(@ARManagementFeeAmount),@UsersID,@TimeNow 
               
			            
                        set @AutoNo = @AutoNo + 1   


                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@TaxARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@TaxARManagementFeeAmount) ,0,abs(@TaxARManagementFeeAmount),1,0,abs(@TaxARManagementFeeAmount),@UsersID,@TimeNow 
               
			            
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@ManagementFeeExpense,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@ManagementFeeExpenseAmount),abs(@ManagementFeeExpenseAmount), 0,1,abs(@ManagementFeeExpenseAmount), 0,@UsersID,@TimeNow 
                        END
                        
                        FETCH NEXT FROM A 
                        INTO @FundPK,@ManagementFeeAmount 
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

        public Boolean GenerateReportFundAccounting(string _userID, FundAccountingRpt _FundAccountingRpt)
        {
            var _fundName = "";
            #region Fund Fact Sheet
            if (_FundAccountingRpt.ReportName.Equals("Fund Fact Sheet"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = "And FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            cmd.CommandText = @" select * from ffssetup where status = 2";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "FundFactSheet" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "FundFactSheet" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundAccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());


                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Fund Fact Sheet");



                                        //ATUR DATA GROUPINGNYA DULU
                                        List<FundFactSheet> rList = new List<FundFactSheet>();
                                        while (dr0.Read())
                                        {

                                            FundFactSheet rSingle = new FundFactSheet();
                                            rSingle.TanggalEfektif = Convert.ToString(dr0["TanggalEfektif"]);
                                            rSingle.JumlahUnit = Convert.ToDecimal(dr0["JumlahUnit"]);
                                            rSingle.BankCustodian = Convert.ToString(dr0["BankCustodian"]);
                                            rSingle.CustodyFee = Convert.ToDecimal(dr0["CustodyFee"]);
                                            rSingle.ManagementFee = Convert.ToDecimal(dr0["ManagementFee"]);
                                            rSingle.SubscriptionFee = Convert.ToDecimal(dr0["SubscriptionFee"]);
                                            rSingle.RedemptionFee = Convert.ToDecimal(dr0["RedemptionFee"]);
                                            rSingle.SwitchingFee = Convert.ToDecimal(dr0["SwitchingFee"]);
                                            rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                            rSingle.AccountNumber = Convert.ToString(dr0["AccountNumber"]);
                                            rSingle.CustodyAccount = Convert.ToString(dr0["CustodyAccount"]);
                                            rSingle.AboutCompany = Convert.ToString(dr0["AboutCompany"]);
                                            rSingle.AboutFund = Convert.ToString(dr0["AboutFund"]);
                                            rSingle.AboutCustodian = Convert.ToString(dr0["AboutCustodian"]);
                                            rSingle.MarketBrief = Convert.ToString(dr0["MarketBrief"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                        from r in rList
                                        group r by new {  } into rGroup
                                        select rGroup;

                                        int incRowExcel = 1;
                                        //incRowExcel = incRowExcel + 1;
                                        foreach (var rsHeader in GroupByReference)
                                        {

                                            //end area header
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                _fundName = rsDetail.FundName;
                                                worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "INFORMASI PRODUK";
                                                worksheet.Cells[incRowExcel, 4].Value = _host.Get_CompanyName();
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                incRowExcel++;

                                                int _rowA = incRowExcel;
                                                int _rowB = incRowExcel + 3;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Tanggal Efektif";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDateTime(rsDetail.TanggalEfektif).ToString("dd-MMM-yyyy");
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.AboutCompany;
                                                worksheet.Cells["D" + _rowA + ":J" + _rowB].Merge = true;
                                                worksheet.Cells["D" + _rowA + ":J" + _rowB].Style.WrapText = true;
                                                worksheet.Cells["D" + _rowA + ":J" + _rowB].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells["D" + _rowA + ":J" + _rowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                                                worksheet.Row(incRowExcel).Height = 18;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Jumlah Unit Yang Ditawarkan";
                                                worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.JumlahUnit;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Row(incRowExcel).Height = 30;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nilai Aktiva Bersih/Unit";
                                                worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + "Masih HardCode";
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Row(incRowExcel).Height = 18;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Dana Kelolaan";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + "Masih HardCode";
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Row(incRowExcel).Height = 18;
                                                incRowExcel++;


                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Unit Penyertaan";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + "Masih HardCode";
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4].Value = "REKSA DANA JASA CAPITAL CAMPURAN DINAMIS ";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Row(incRowExcel).Height = 18;
                                                incRowExcel++;

                                                int _rowC = incRowExcel;
                                                int _rowD = incRowExcel + 3;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Bank Custodian";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.BankCustodian;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.AboutFund;
                                                worksheet.Cells["D" + _rowC + ":J" + _rowD].Merge = true;
                                                worksheet.Cells["D" + _rowC + ":J" + _rowD].Style.WrapText = true;
                                                worksheet.Cells["D" + _rowC + ":J" + _rowD].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells["D" + _rowC + ":J" + _rowD].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                                                worksheet.Row(incRowExcel).Height = 18;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Custody Fee";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.CustodyFee;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Row(incRowExcel).Height = 18;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Management Fee";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.ManagementFee;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Row(incRowExcel).Height = 18;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Subscription Fee";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.SubscriptionFee;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Row(incRowExcel).Height = 18;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Redemption Fee";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.RedemptionFee;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4].Value = "BANK CUSTODIAN ";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Row(incRowExcel).Height = 18;
                                                incRowExcel++;

                                                int _rowE = incRowExcel;
                                                int _rowF = incRowExcel + 7;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Switching Fee";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.SwitchingFee;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.AboutCustodian;
                                                worksheet.Cells["D" + _rowE + ":J" + _rowF].Merge = true;
                                                worksheet.Cells["D" + _rowE + ":J" + _rowF].Style.WrapText = true;
                                                worksheet.Cells["D" + _rowE + ":J" + _rowF].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells["D" + _rowE + ":J" + _rowF].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                                                worksheet.Row(incRowExcel).Height = 21;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Rekening Pembelian";
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Row(incRowExcel).Height = 21;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Account Name";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.AccountName;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Row(incRowExcel).Height = 21;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Account Number";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.AccountNumber;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Row(incRowExcel).Height = 21;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Custody Account";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.CustodyAccount;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Row(incRowExcel).Height = 21;
                                                incRowExcel++;

                                                worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "5 Besar Efek Portofolio";
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Row(incRowExcel).Height = 21;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "1. PPRE (Infrastukture,Utilities & Transportation)";
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Row(incRowExcel).Height = 21;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "2. PTBA (Mining)";
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells["A" + _rowE + ":C" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Row(incRowExcel).Height = 21;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "3. WSBP (Basic Industry & Chemical)";
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4].Value = "PERKEMBANGAN DAN PREDIKSI PASAR";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Row(incRowExcel).Height = 18;
                                                incRowExcel++;

                                                int _rowG = incRowExcel;
                                                int _rowH = incRowExcel + 8;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "4. BBRI (Finance)";
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.MarketBrief;
                                                worksheet.Cells["D" + _rowG + ":J" + _rowH].Merge = true;
                                                worksheet.Cells["D" + _rowG + ":J" + _rowH].Style.WrapText = true;
                                                worksheet.Cells["D" + _rowG + ":J" + _rowH].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells["D" + _rowG + ":J" + _rowH].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                                                worksheet.Row(incRowExcel).Height = 18;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "5. TBLA (Agriculture)";
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Row(incRowExcel).Height = 18;
                                                worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                incRowExcel = incRowExcel + 5;

                                                //worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + 37 + ":C" + 37].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + 52 + ":C" + 52].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                //1
                                                worksheet.Cells["B" + 25 + ":C" + 25].Style.Font.Color.SetColor(Color.White);
                                                worksheet.Cells[25, 2].Value = "Jan";
                                                worksheet.Cells[25, 3].Value = "Feb";


                                                worksheet.Cells[26, 1].Value = "Blackberry";
                                                worksheet.Cells[26, 2].Value = 5000;
                                                worksheet.Cells[26, 3].Value = 5000;

                                                worksheet.Cells[27, 1].Value = "Strawberry-Rhubarb";
                                                worksheet.Cells[27, 2].Value = 1700;
                                                worksheet.Cells[27, 3].Value = 1700;

                                                worksheet.Cells[28, 1].Value = "Orange";
                                                worksheet.Cells[28, 2].Value = 7000;
                                                worksheet.Cells[28, 3].Value = 7000;

                                                worksheet.Cells["B" + 26 + ":C" + 28].Style.Font.Color.SetColor(Color.White);



                                                //2
                                                worksheet.Cells["B" + 37 + ":C" + 37].Style.Font.Color.SetColor(Color.White);
                                                worksheet.Cells[39, 2].Value = "Jan";
                                                worksheet.Cells[39, 3].Value = "Feb";


                                                worksheet.Cells[40, 1].Value = "Blackberry";
                                                worksheet.Cells[40, 2].Value = 5000;
                                                worksheet.Cells[40, 3].Value = 5000;

                                                worksheet.Cells[41, 1].Value = "Strawberry-Rhubarb";
                                                worksheet.Cells[41, 2].Value = 1700;
                                                worksheet.Cells[41, 3].Value = 1700;

                                                worksheet.Cells[42, 1].Value = "Orange";
                                                worksheet.Cells[42, 2].Value = 7000;
                                                worksheet.Cells[42, 3].Value = 7000;

                                                worksheet.Cells["B" + 40 + ":C" + 42].Style.Font.Color.SetColor(Color.White);

                                            }



                                            incRowExcel = incRowExcel + 28;
                                            int _disA = incRowExcel;
                                            int _disB = incRowExcel + 5;
                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : Seluruh informasi, keterangan, yang disampaikan melalui media elektronik (“e-mail”) ataupun dalam bentuk hardcopy dari PT. Jasa Capital Asset Management (“Dokumen”) hanya merupakan informasi dan/atau keterangan yang tidak dapat diartikan sebagai suatu saran/advise bisnis tertentu, karenanya Dokumen tersebut tidak bersifat mengikat. Segala hal yang berkaitan dengan diterimanya dan/atau dipergunakannya Dokumen tersebut sebagai pengambilan keputusan bisnis dan/atau investasi merupa kan tanggung jawab pribadi atas segala risiko yang mungkin timbul. Sehubungan dengan risiko dan tanggung jawab pribadi atas Dokumen, pengguna dengan ini menyetujui untuk melepaskan segala tanggung jawab dan risiko hukum kepada PT. Jasa Capital Asset Management atas diterimanya dan/atau dipergunakannya Dokumen.";
                                            worksheet.Cells["A" + _disA + ":J" + _disB].Merge = true;
                                            worksheet.Cells["A" + _disA + ":J" + _disB].Style.WrapText = true;
                                            worksheet.Cells["A" + _disA + ":J" + _disB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + _disA + ":J" + _disB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                                            worksheet.Cells["A" + _disA + ":J" + _disB].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["A" + _disA + ":J" + _disB].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                                            incRowExcel = incRowExcel + 5;
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                        }

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 10];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 23;
                                        worksheet.Column(3).Width = 23;
                                        worksheet.Column(4).Width = 10;
                                        worksheet.Column(5).Width = 10;
                                        worksheet.Column(6).Width = 10;
                                        worksheet.Column(7).Width = 10;
                                        worksheet.Column(8).Width = 10;
                                        worksheet.Column(9).Width = 10;
                                        worksheet.Column(10).Width = 10;

                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderRightTextKospin(_fundName);


                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 FUND FACT SHEET \n" + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("MMM yyyy");

                                        //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        //string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        Image thumb = img.GetThumbnailImage(Tools.imgWidth, Tools.imgHeight, null, IntPtr.Zero);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(thumb, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        //worksheet.HeaderFooter.OddFooter.CenteredText = "\n \n \n \n \n \n \n &14&B Disclaimer" + "&10 : Seluruh informasi, keterangan, yang disampaikan melalui media elektronik (“e-mail”) ataupun dalam bentuk hardcopy dari PT. Jasa Capital Asset Management (“Dokumen”)";



                                        //create a new piechart of type Line satu
                                        ExcelPieChart lineChart = worksheet.Drawings.AddChart("lineChart", eChartType.Pie) as ExcelPieChart;
                                        //set the title
                                        lineChart.Title.Text = "Alokasi Investasi";
                                        lineChart.DataLabel.ShowPercent = true;

                                        //create the ranges for the chart
                                        var rangeLabel = worksheet.Cells["A26:A28"];
                                        var range1 = worksheet.Cells["B26:B28"];
                                        var range2 = worksheet.Cells["C26:C28"];

                                        //add the ranges to the chart
                                        lineChart.Series.Add(range1, rangeLabel);
                                        lineChart.Series.Add(range2, rangeLabel);

                                        //set the names of the legend
                                        lineChart.Series[0].Header = worksheet.Cells["B25"].Value.ToString();
                                        lineChart.Series[1].Header = worksheet.Cells["C25"].Value.ToString();

                                        //position of the legend
                                        lineChart.Legend.Position = eLegendPosition.Right;
                                        lineChart.Style = eChartStyle.Style10;




                                        //size of the chart
                                        lineChart.SetSize(452, 248);

                                        //add the chart at cell B6
                                        lineChart.SetPosition(22, 30, 0, 0);

                                        //create a new piechart of type Line dua
                                        ExcelPieChart lineChart2 = worksheet.Drawings.AddChart("lineChart2", eChartType.Pie) as ExcelPieChart;
                                        //set the title
                                        lineChart2.Title.Text = "Alokasi Sektor";
                                        lineChart2.DataLabel.ShowPercent = true;

                                        //create the ranges for the chart
                                        var rangeLabel2 = worksheet.Cells["A26:A28"];
                                        var range21 = worksheet.Cells["B26:B28"];
                                        var range22 = worksheet.Cells["C26:C28"];

                                        //add the ranges to the chart
                                        lineChart2.Series.Add(range21, rangeLabel2);
                                        lineChart2.Series.Add(range22, rangeLabel2);

                                        //set the names of the legend
                                        lineChart2.Series[0].Header = worksheet.Cells["B25"].Value.ToString();
                                        lineChart2.Series[1].Header = worksheet.Cells["C25"].Value.ToString();

                                        //position of the legend
                                        lineChart2.Legend.Position = eLegendPosition.Right;
                                        lineChart2.Style = eChartStyle.Style10;




                                        //size of the chart
                                        lineChart2.SetSize(452, 248);

                                        //add the chart at cell B6
                                        lineChart2.SetPosition(37, 30, 0, 0);

                                        package.Save();


                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
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
            #endregion


            else
            {
                return false;
            }
        }

        public string LaporanKeuangan(string _userID, AccountingRpt _accountingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                string FilePath = Tools.ReportsPath + "LaporanKeuangan" + "_" + _userID + ".xlsx";
                File.Copy(Tools.ReportsTemplatePath + "\\12\\" +  "12_LapKeu.xlsx", FilePath, true);
                FileInfo existingFile = new FileInfo(FilePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    // MKBD05
                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets[1];
                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets[2];
                    ExcelWorksheet worksheet3 = package.Workbook.Worksheets[3];
                    using (SqlConnection DbCon05 = new SqlConnection(Tools.conString))
                    {
                        DbCon05.Open();

                        int incRowExcel = 5;
                        using (SqlCommand cmd05 = DbCon05.CreateCommand())
                        {
                            string _status = "";
                            string _paramAccount = "";
                            string _paramData = "";


                            if (!_host.findString(_accountingRpt.AccountFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.AccountFrom))
                            {
                                _paramAccount = "And A.AccountPK  in ( " + _accountingRpt.AccountFrom + " ) ";
                            }
                            else
                            {
                                _paramAccount = "";
                            }


                            if (_accountingRpt.Status == 1)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 2)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 1 ";
                            }
                            else if (_accountingRpt.Status == 3)
                            {
                                _status = " and B.Status = 2 and B.Posted = 0 and B.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 4)
                            {
                                _status = " and B.Status = 1  ";
                            }
                            else if (_accountingRpt.Status == 5)
                            {
                                _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }
                            else if (_accountingRpt.Status == 6)
                            {
                                _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }

                            if (_accountingRpt.ParamData == 1)
                            {
                                _paramData = "  A.Groups = 1  ";
                            }
                            else if (_accountingRpt.ParamData == 2)
                            {
                                _paramData = "  A.Groups = 0  ";
                            }
                            else if (_accountingRpt.ParamData == 3)
                            {
                                _paramData = "  A.Groups in (0,1)  ";
                            }

                            cmd05.CommandText = @"
                            Declare @PeriodPK int
                            Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2

	                        create table #tempTBKospin
	                        (
		                        ID nvarchar(200),
								Type nvarchar(200),
		                        Name nvarchar(300),
		                        Groups bit,
		                        ParentPK int,
		                        CurrID nvarchar(100),
		                        PreviousBaseBalance numeric(19,4),
		                        BaseDebitMutasi numeric(19,4),
		                        BaseCreditMutasi numeric(19,4),
		                        CurrentBaseBalance numeric(19,4),
	                        )


	                        insert into #tempTBKospin
                            SELECT C.ID, C.Type, C.Name, C.[Groups],C.[ParentPK],    
                            D.ID,       
                            CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                            CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                            CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                            CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                            FROM (      
                            SELECT A.AccountPK,       
                            SUM(B.Balance) AS CurrentBalance,       
                            SUM(B.BaseBalance) AS CurrentBaseBalance,      
                            SUM(B.SumDebit) AS CurrentDebit,       
                            SUM(B.SumCredit) AS CurrentCredit,       
                            SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                            SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                            FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                            SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                            SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                            SUM(A.Debit) AS SumDebit,      
                            SUM(A.Credit) AS SumCredit,      
                            SUM(A.BaseDebit) AS SumBaseDebit,      
                            SUM(A.BaseCredit) AS SumBaseCredit,      
                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                            C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                            FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                            INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                            INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                            WHERE  B.ValueDate <= @ValueDateTo and  B.PeriodPK = @PeriodPK
	                        And B.status <> 3
                            " + _status + _paramAccount + @"     
                            Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                            C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                            C.ParentPK7, C.ParentPK8, C.ParentPK9        
                            ) AS B        
                            WHERE 
	                        " + _paramData + @"  AND
                            (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                            OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                            OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                            OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                            Group BY A.AccountPK       
                            ) AS A LEFT JOIN (       
                            SELECT A.AccountPK,        
                            SUM(B.Balance) AS PreviousBalance,        
                            SUM(B.BaseBalance) AS PreviousBaseBalance,       
                            SUM(B.SumDebit) AS PreviousDebit,        
                            SUM(B.SumCredit) AS PreviousCredit,        
                            SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                            SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                            FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                            SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                            SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                            SUM(A.Debit) AS SumDebit,        
                            SUM(A.Credit) AS SumCredit,        
                            SUM(A.BaseDebit) AS SumBaseDebit,        
                            SUM(A.BaseCredit) AS SumBaseCredit,        
                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                            C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                            FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                            INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                            INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                            WHERE  B.ValueDate < @ValueDateFrom  and  B.PeriodPK = @PeriodPK 

	                        And B.status <> 3
                            " + _status + _paramAccount + @" 
    
	                        Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                            C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                            C.ParentPK7, C.ParentPK8, C.ParentPK9        
                            ) AS B        
                            WHERE
	                        " + _paramData + @" AND 
		 
	                        (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                            OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                            OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                            OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                            Group BY A.AccountPK       
                            ) AS B ON A.AccountPK = B.AccountPK        
                            INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                            INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                            WHERE (A.CurrentBalance <> 0)        
                            OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                            OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                            OR (A.CurrentBaseBalance <> 0)        
                            OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                            OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)     
                            Order BY C.ID          


	                        Select isnull(A.ID,'') ID,isnull(A.Name,'') Name,isnull(A.Groups,0) groups,isnull(A.ParentPK,0) ParentPK
	                        ,isnull(B.PreviousBaseBalance,0)  * case when A.Type in (2,3,4) then -1 else 1 end PreviousBaseBalance
	                        ,isnull(B.BaseDebitMutasi,0) BaseDebitMutasi
	                        ,isnull(B.BaseCreditMutasi,0)  BaseCreditMutasi
	                        ,isnull(B.CurrentBaseBalance,0)  * case when A.Type in (2,3,4) then -1 else 1 end CurrentBaseBalance
	                        from Account A left join 
	                        #tempTBKospin B on A.ID COLLATE DATABASE_DEFAULT = B.ID COLLATE DATABASE_DEFAULT
	                        where A.status in (1,2) order by A.ID asc";
                            cmd05.CommandTimeout = 0;
                            cmd05.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                            cmd05.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);

                            using (SqlDataReader dr05 = cmd05.ExecuteReader())
                            {
                                if (dr05.HasRows)
                                {
                                    List<LaporanKeuanganRpt> rList = new List<LaporanKeuanganRpt>();
                                    while (dr05.Read())
                                    {
                                        LaporanKeuanganRpt rSingle = new LaporanKeuanganRpt();
                                        rSingle.ID = Convert.ToDecimal(dr05["ID"]);
                                        rSingle.Name = Convert.ToString(dr05["Name"]);
                                        rSingle.PrevBalance = Convert.ToDecimal(dr05["PreviousBaseBalance"]);
                                        rSingle.CurrBalance = Convert.ToDecimal(dr05["CurrentBaseBalance"]);
                                        rSingle.Credit = Convert.ToDecimal(dr05["BaseCreditMutasi"]);
                                        rSingle.Debet = Convert.ToDecimal(dr05["BaseDebitMutasi"]);
                                        rList.Add(rSingle);
                                    }
                                    var QueryByClientID5 =
                                     from r in rList
                                     group r by new { } into rGroup
                                     select rGroup;

                                    foreach (var rsHeader in QueryByClientID5)
                                    {

                                        worksheet2.Cells[2, 2].Value = Convert.ToDateTime(_accountingRpt.ValueDateFrom).ToString("dd-MMM-yyyy");
                                        worksheet2.Cells[3, 2].Value = Convert.ToDateTime(_accountingRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                        worksheet1.Cells[3, 2].Value = Convert.ToDateTime(_accountingRpt.ValueDateFrom).ToString("MMM-yyyy");
                                        worksheet3.Cells[3, 2].Value = Convert.ToDateTime(_accountingRpt.ValueDateFrom).ToString("MMM-yyyy");
                                        foreach (var rsDetail in rsHeader)
                                        {

                                            worksheet2.Cells[incRowExcel, 1].Value = rsDetail.ID;
                                            worksheet2.Cells[incRowExcel, 2].Value = rsDetail.Name;
                                            worksheet2.Cells[incRowExcel, 3].Value = rsDetail.PrevBalance;
                                            worksheet2.Cells[incRowExcel, 4].Value = rsDetail.Debet;
                                            worksheet2.Cells[incRowExcel, 5].Value = rsDetail.Credit;
                                            worksheet2.Cells[incRowExcel, 6].Value = rsDetail.CurrBalance;

                                            incRowExcel++;

                                        }


                                        int _IncRowA5 = incRowExcel + 1000;
                                    }


                                }


                            }
                        }


                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN

                        //worksheet2.PrinterSettings.PrintArea = worksheet2.Cells[1, 1, incRowExcel - 1, 6];
                        //worksheet1.PrinterSettings.PrintArea = worksheet1.Cells[1, 1, incRowExcel - 1, 6];
                        worksheet2.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                        worksheet1.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                        //worksheet2.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                        //worksheet1.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                        //worksheet2.HeaderFooter.OddHeader.CenteredText = "\n \n \n  &18&B LAPORAN KEUANGAN";
                        //worksheet1.HeaderFooter.OddHeader.CenteredText = "\n \n \n  &18&B LAPORAN KEUANGAN";



                    }



                    worksheet1.Calculate();
                    worksheet2.Calculate();
                    package.Save();
                    return FilePath;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public Boolean Dealing_ListingRpt(string _userID, InvestmentListing _listing)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _bitIsMature = "";
                        string _paramDealingPK = "";
                        string _paramDealing = "";

                        if (!_host.findString(_listing.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_listing.stringInvestmentFrom))
                        {
                            _paramDealingPK = " And IV.DealingPK in (" + _listing.stringInvestmentFrom + ") ";
                            _paramDealing = " DealingPK in (" + _listing.stringInvestmentFrom + ") ";
                        }
                        else
                        {
                            _paramDealingPK = " And IV.DealingPK in (0) ";
                            _paramDealing = " DealingPK in (0) ";
                        }

                        if (_listing.ParamFundID != "All")
                        {
                            _paramFund = "and F.ID = left(@ParamFundIDFrom,charindex('-',@ParamFundIDFrom) - 1)";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        if (_listing.BitIsMature == true)
                        {
                            _bitIsMature = @" 
                            union all
                            Select Reference,RefNo,ValueDate,InstrumentID,InstrumentName,    
                            FundID,InstrumentType,InvestmentPK,Volume,OrderPrice,InterestPercent,TrxTypeID,DonePrice,   
                            Amount,Notes,RangePrice ,MaturityDate ,DoneVolume,DoneAmount,'',AcqDate,DealingPK,InvestmentPK,CounterpartID  
                            from InvestmentMature where " + _paramDealing;
                        }
                        else
                        {
                            _bitIsMature = "";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        Declare @TrailsPK int
                        Declare @MaxDateEndDayFP datetime

                        select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                        where ValueDate = 
                        (
                        select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @ParamListDate
                        )
                        and status = 2

                        Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,IV.valueDate,I.ID InstrumentID, I.Name InstrumentName,    
                        F.ID FundID,IT.Name InstrumentType,IV.InvestmentPK,IV.Volume,IV.OrderPrice,case when IV.TrxType = 2 then IV.BreakInterestPercent else IV.InterestPercent end InterestPercent,IV.TrxTypeID,isnull(IV.DonePrice,0) DonePrice,    
                        isnull(IV.Amount,0) Amount,IV.Notes, IV.RangePrice,case when IV.TrxType = 2 then IV.ValueDate else IV.MaturityDate end MaturityDate,isnull(IV.DoneVolume,0) DoneVolume
                        ,isnull(IV.DoneAmount,0) DoneAmount,IV.Notes,IV.AcqDate,IV.DealingPK,IV.InvestmentPK,isnull(C.ID,'') CounterpartID 
                        from Investment IV       
                        left join Fund F on IV.FundPK = F.FundPK and F.status = 2      
                        left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2      
                        left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2 
                        left join Counterpart C on IV.CounterpartPK = C.CounterpartPK and C.status = 2         
                        Where  IV.ValueDate = @ParamListDate and IV.StatusInvestment = 2 and IV.StatusDealing <> 3 " + _paramDealingPK + _paramFund + _bitIsMature +
                        @"
                        order by FundID
                        ";


                        cmd.Parameters.AddWithValue("@ParamListDate", _listing.ParamListDate);
                        if (_listing.ParamFundID != "All")
                        {
                            cmd.Parameters.AddWithValue("@ParamFundIDFrom", _listing.ParamFundID);
                        }

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "DealingListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "DealingListing" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "InvestmentReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Dealing Listing");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<InvestmentListing> rList = new List<InvestmentListing>();
                                    while (dr0.Read())
                                    {
                                        InvestmentListing rSingle = new InvestmentListing();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                        rSingle.InstrumentType = Convert.ToString(dr0["InstrumentType"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                        rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                        rSingle.TrxTypeID = Convert.ToString(dr0["TrxTypeID"]);
                                        rSingle.InvestmentPK = Convert.ToInt32(dr0["InvestmentPK"]);
                                        rSingle.DealingPK = Convert.ToInt32(dr0["DealingPK"]);
                                        if (rSingle.InstrumentType != "Deposito Money Market")
                                        {
                                            rSingle.DoneVolume = Convert.ToDecimal(dr0["DoneVolume"]);
                                            rSingle.OrderPrice = Convert.ToDecimal(dr0["OrderPrice"]);
                                            rSingle.DonePrice = Convert.ToDecimal(dr0["DonePrice"]);
                                            rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);

                                        }
                                        else
                                        {
                                            rSingle.DoneVolume = Convert.ToDecimal(dr0["DoneVolume"]);
                                            rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                            if (rSingle.TrxTypeID != "PLACEMENT")
                                            {
                                                rSingle.AcqDateDeposito = Convert.ToDateTime(dr0["AcqDate"]);
                                            }
                                        }
                                        rSingle.DoneAmount = Convert.ToDecimal(dr0["DoneAmount"]);
                                        rSingle.Notes = Convert.ToString(dr0["Notes"]);
                                        rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);
                                        rSingle.CounterpartID = Convert.ToString(dr0["CounterpartID"]);

                                        rList.Add(rSingle);

                                    }

                                    var GroupByFundID =
                                         from r in rList
                                         orderby r.FundID, r.InstrumentType
                                         group r by new { r.FundID, r.InstrumentType, r.TrxTypeID } into rGroup
                                         select rGroup;



                                    int incRowExcel = 1;
                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 1].Value = "TRADE DATE :";
                                    worksheet.Cells["B" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                    worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                    worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_listing.ParamListDate);
                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                    foreach (var rsHeader in GroupByFundID)
                                    {

                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "FUND :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.FundID;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "INS. TYPE :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.InstrumentType;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "TRX. TYPE :";
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.TrxTypeID;
                                        incRowExcel = incRowExcel + 1;
                                        if (rsHeader.Key.InstrumentType == "Equity Reguler")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Volume / Shares";
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = "Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Done Price";
                                            worksheet.Cells[incRowExcel, 7].Value = "Done Amount";
                                            worksheet.Cells[incRowExcel, 8].Value = "Counterpart";
                                        }
                                        else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond")
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                            worksheet.Cells[incRowExcel, 2].Value = "Stock ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = "Done Price";
                                            worksheet.Cells[incRowExcel, 6].Value = "Coupon %";
                                            worksheet.Cells[incRowExcel, 7].Value = "Done Amount";
                                            worksheet.Cells[incRowExcel, 8].Value = "Counterpart";
                                        }
                                        else
                                        {
                                            if (rsHeader.Key.TrxTypeID == "LIQUIDATE" || rsHeader.Key.TrxTypeID == "ROLLOVER")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Bank ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Acq Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Tenor";
                                                worksheet.Cells[incRowExcel, 8].Value = "Done Amount";
                                                worksheet.Cells[incRowExcel, 9].Value = "Int.Percent";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Ticket ID"; ;
                                                worksheet.Cells[incRowExcel, 2].Value = "Bank ID";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nominal";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Value Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Value = "Tenor";
                                                worksheet.Cells[incRowExcel, 8].Value = "Done Amount";
                                                worksheet.Cells[incRowExcel, 9].Value = "Int.Percent";
                                            }

                                        }

                                        //THICK BOX HEADER
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        string _range = "A" + incRowExcel + ":I" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 14;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            //r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        }
                                        incRowExcel++;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;



                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            _range = "A" + incRowExcel + ":I" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                r.Style.Font.Size = 14;
                                                r.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                            }
                                            //area detail
                                            if (rsHeader.Key.InstrumentType == "Equity Reguler")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.DealingPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DoneVolume;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.OrderPrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.DonePrice;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.DoneAmount;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.CounterpartID;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            }
                                            else if (rsHeader.Key.InstrumentType == "Corporate Bond" || rsHeader.Key.InstrumentType == "Government Bond")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.DealingPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DoneVolume;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.OrderPrice;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.DonePrice;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.DoneAmount;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.CounterpartID;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            }


                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.DealingPK;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DoneVolume;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;

                                                if (rsHeader.Key.TrxTypeID == "LIQUIDATE" || rsHeader.Key.TrxTypeID == "ROLLOVER")
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.AcqDateDeposito;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.MaturityDate;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Formula = "F" + incRowExcel + "-E" + incRowExcel;
                                                    worksheet.Cells[incRowExcel, 7].Calculate();
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.DoneAmount;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.InterestPercent;

                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.MaturityDate;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MM/yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Formula = "F" + incRowExcel + "-E" + incRowExcel;
                                                    worksheet.Cells[incRowExcel, 7].Calculate();
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.DoneAmount;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.InterestPercent;

                                                }


                                            }





                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;





                                        }

                                        _range = "A" + incRowExcel + ":I" + incRowExcel;
                                        using (ExcelRange s = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            s.Style.Font.Size = 14;
                                            s.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                        }

                                        //THICK BOX DETAIL
                                        _endRowDetail = incRowExcel - 1;
                                        worksheet.Cells["A" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["B" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["C" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["D" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["E" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["F" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["G" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["H" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["I" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dashed;
                                        worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                        worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 3].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Calculate();

                                    }

                                    incRowExcel = incRowExcel + 2;
                                    worksheet.Cells[incRowExcel, 1].Value = "Notes :";
                                    worksheet.Cells[incRowExcel, 2].Value = _listing.Message;
                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                    worksheet.Cells[incRowExcel, 2].Style.WrapText = true;

                                    //worksheet.Cells["B" + incRowExcel + ":H" + (incRowExcel + 7)].Merge = true;
                                    //int _rowNotes = incRowExcel + 10;
                                    //worksheet.Cells["B" + incRowExcel + ":H" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                    //worksheet.Cells["B" + _rowNotes + ":H" + _rowNotes].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                    //worksheet.Cells["B" + incRowExcel + ":B" + _rowNotes].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                    //worksheet.Cells["H" + incRowExcel + ":H" + _rowNotes].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                    //incRowExcel = incRowExcel + 13;


                                    int _RowA = incRowExcel;
                                    int _RowB = incRowExcel + 7;
                                    worksheet.Cells["A" + _RowB + ":I" + _RowB].Style.Font.Bold = true;
                                    worksheet.Cells["A" + _RowB + ":I" + _RowB].Style.Font.Size = 15;
                                    if (Tools.ClientCode == "01") //ASCEND
                                    {
                                        worksheet.Cells["A" + _RowA + ":B" + _RowA].Merge = true;
                                        worksheet.Cells[_RowA, 2].Value = "PrepareBy";
                                        worksheet.Cells[_RowA, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells["A" + _RowB + ":B" + _RowB].Merge = true;
                                        worksheet.Cells[_RowB, 2].Value = "(                                  )";
                                        worksheet.Cells[_RowB, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells["E" + _RowA + ":F" + _RowA].Merge = true;
                                        worksheet.Cells[_RowA, 6].Value = "Approval";
                                        worksheet.Cells[_RowA, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells["E" + _RowB + ":F" + _RowB].Merge = true;
                                        worksheet.Cells[_RowB, 6].Value = "(                             )";
                                        worksheet.Cells[_RowB, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    }
                                    else
                                    {
                                        if (_listing.Signature1 != 0)
                                        {
                                            worksheet.Cells[_RowA, 2].Value = _host.Get_PositionSignature(_listing.Signature1);
                                            worksheet.Cells[_RowA, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 2].Value = "( " + _host.Get_SignatureName(_listing.Signature1) + " )";
                                            worksheet.Cells[_RowB, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 2].Value = "";
                                            worksheet.Cells[_RowA, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 2].Value = "";
                                            worksheet.Cells[_RowB, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        }


                                        if (_listing.Signature2 != 0)
                                        {
                                            worksheet.Cells[_RowA, 4].Value = _host.Get_PositionSignature(_listing.Signature2);
                                            worksheet.Cells[_RowA, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 4].Value = "( " + _host.Get_SignatureName(_listing.Signature2) + " )";
                                            worksheet.Cells[_RowB, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 4].Value = _host.Get_PositionSignature(_listing.Signature2);
                                            worksheet.Cells[_RowA, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 4].Value = "";
                                            worksheet.Cells[_RowB, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        }

                                        if (_listing.Signature3 != 0)
                                        {
                                            worksheet.Cells[_RowA, 6].Value = _host.Get_PositionSignature(_listing.Signature3);
                                            worksheet.Cells[_RowA, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 6].Value = "( " + _host.Get_SignatureName(_listing.Signature3) + " )";
                                            worksheet.Cells[_RowB, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 6].Value = "";
                                            worksheet.Cells[_RowA, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 6].Value = "";
                                            worksheet.Cells[_RowB, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        }

                                        if (_listing.Signature4 != 0)
                                        {
                                            worksheet.Cells[_RowA, 8].Value = _host.Get_PositionSignature(_listing.Signature4);
                                            worksheet.Cells[_RowA, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 8].Value = "( " + _host.Get_SignatureName(_listing.Signature4) + " )";
                                            worksheet.Cells[_RowB, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                        }
                                        else
                                        {
                                            worksheet.Cells[_RowA, 8].Value = "";
                                            worksheet.Cells[_RowA, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[_RowB, 8].Value = "";
                                            worksheet.Cells[_RowB, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        }
                                    }

                                    incRowExcel = incRowExcel + 8;
                                    worksheet.Row(incRowExcel).PageBreak = true;

                                    string _rangeA = "A1:I" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        r.Style.Font.Size = 18;
                                        r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 8];
                                    worksheet.Column(1).AutoFit();
                                    worksheet.Column(2).Width = 20;
                                    worksheet.Column(3).Width = 25;
                                    worksheet.Column(4).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).AutoFit();
                                    worksheet.Column(9).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&34 DEALING TICKET";
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();
                                    Tools.ExportFromExcelToPDF(filePath, pdfPath);
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

        public string GenerateFFS(string _userID, FundAccountingRpt _FundAccountingRpt)
        {
            #region FFS
            var _fundName = "";
                try
                {

                string filePath = Tools.ReportsPath + "FundFactSheet" + "_" + _userID + ".xlsx";
                string pdfPath = Tools.ReportsPath + "FundFactSheet" + "_" + _userID + ".pdf";
                File.Copy(Tools.ReportsTemplatePath + "\\12\\" +  "12_FFSTemplate.xlsx", filePath, true);
                FileInfo existingFile = new FileInfo(filePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    #region 1
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = @" select b.Name FundName,* from ffssetup a left join fund b on a.fundpk = b.FundPK and b.Status in(1,2) 
                            where a.status = 2 and a.FundPK = @FundPK and a.ValueDate = @Date";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {

                                    return "false";
                                }
                                else
                                {
                                    //ATUR DATA GROUPINGNYA DULU
                                    List<FundFactSheet> rList = new List<FundFactSheet>();
                                    while (dr0.Read())
                                    {

                                        FundFactSheet rSingle = new FundFactSheet();
                                        rSingle.TanggalEfektif = Convert.ToString(dr0["TanggalEfektif"]);
                                        rSingle.JumlahUnit = Convert.ToDecimal(dr0["JumlahUnit"]);
                                        rSingle.BankCustodian = Convert.ToString(dr0["BankCustodian"]);
                                        rSingle.CustodyFee = Convert.ToDecimal(dr0["CustodyFee"]);
                                        rSingle.ManagementFee = Convert.ToDecimal(dr0["ManagementFee"]);
                                        rSingle.SubscriptionFee = Convert.ToDecimal(dr0["SubscriptionFee"]);
                                        rSingle.RedemptionFee = Convert.ToDecimal(dr0["RedemptionFee"]);
                                        rSingle.SwitchingFee = Convert.ToDecimal(dr0["SwitchingFee"]);
                                        rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                        rSingle.AccountNumber = Convert.ToString(dr0["AccountNumber"]);
                                        rSingle.CustodyAccount = Convert.ToString(dr0["CustodyAccount"]);
                                        rSingle.AboutCompany = Convert.ToString(dr0["AboutCompany"]);
                                        rSingle.AboutFund = Convert.ToString(dr0["AboutFund"]);
                                        rSingle.AboutCustodian = Convert.ToString(dr0["AboutCustodian"]);
                                        rSingle.MarketBrief = Convert.ToString(dr0["MarketBrief"]);
                                        rSingle.FundName = Convert.ToString(dr0["FundName"]);

                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                    from r in rList
                                    group r by new { } into rGroup
                                    select rGroup;

                                    int incRowExcel = 7;
                                    //incRowExcel = incRowExcel + 1;
                                    foreach (var rsHeader in GroupByReference)
                                    {

                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            _fundName = rsDetail.FundName;
                                            worksheet.Cells[2, 2].Value = "PT. JASA CAPITAL ASSET MANAGEMENT";
                                            worksheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["B" + 2 + ":H" + 2].Merge = true;
                                            worksheet.Cells[2, 2].Style.Font.Size = 22;
                                            worksheet.Cells[2, 2].Style.Font.Bold = true;
                                            worksheet.Cells[2, 2].Style.Font.Color.SetColor(Color.FromArgb(0, 153, 153));

                                            worksheet.Cells[2, 11].Value = "FUND FACT SHEET ";
                                            worksheet.Cells[2, 11].Style.Font.Size = 17;
                                            worksheet.Cells[2, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[2, 11].Style.Font.Color.SetColor(Color.FromArgb(0, 153, 153));

                                            worksheet.Cells[3, 2].Value = _fundName;
                                            worksheet.Cells["B" + 3 + ":H" + 3].Merge = true;
                                            worksheet.Cells[3, 2].Style.Font.Size = 22;
                                            worksheet.Cells[3, 2].Style.Font.Bold = true;
                                            worksheet.Cells[3, 2].Style.Font.Color.SetColor(Color.Black);
                                            worksheet.Cells[3, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[3, 11].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("MMMM yyyy");
                                            worksheet.Cells[3, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[3, 11].Style.Font.Size = 17;
                                            worksheet.Cells[3, 11].Style.Font.Color.SetColor(Color.FromArgb(0, 153, 153));
                                            worksheet.Cells["A" + 4 + ":K" + 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                                            worksheet.Cells["A" + 4 + ":K" + 4].Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 153, 153));



                                            worksheet.Cells["B" + 1 + ":K" + 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["B" + 1 + ":K" + 3].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(146,208,80));


                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "INFORMASI PRODUK";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Color.SetColor(Color.ForestGreen);
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Value = _host.Get_CompanyName().ToUpper(); 
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Color.SetColor(Color.ForestGreen);
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 16;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 2].Style.Font.Size = 16;
                                            incRowExcel++;

                                            int _rowA = incRowExcel;
                                            int _rowB = incRowExcel + 4;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Tanggal Efektif";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 14;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + Convert.ToDateTime(rsDetail.TanggalEfektif).ToString("dd MMMM yyyy");
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.AboutCompany;
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Size = 14;
                                            worksheet.Cells["E" + _rowA + ":K" + _rowB].Merge = true;
                                            worksheet.Cells["E" + _rowA + ":K" + _rowB].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                                            worksheet.Row(incRowExcel).Height = 20;
                                            worksheet.Row(7).Height = 24;
                                            worksheet.Row(8).Height = 24;
                                            worksheet.Row(9).Height = 24;
                                            worksheet.Row(10).Height = 24;
                                            worksheet.Row(11).Height = 24;
                                            worksheet.Row(12).Height = 24;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Jumlah Unit Yang Ditawarkan";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + Convert.ToDecimal(rsDetail.JumlahUnit).ToString("#,##0") + " Unit";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 14;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(incRowExcel).Height = 25;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Nilai Aktiva Bersih/Unit";
                                            worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            //worksheet.Cells[incRowExcel, 3].Value = Convert.ToDecimal(rsDetail.TanggalEfektif).ToString("#,##0.0000");
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + Convert.ToDecimal(_host.Get_CloseNAVByFundPK(_FundAccountingRpt.FundPK, _FundAccountingRpt.ValueDateFrom)).ToString("#,##0.00");
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 14;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(incRowExcel).Height = 25;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Dana Kelolaan";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + Convert.ToDecimal(_host.Get_AUM(_FundAccountingRpt.FundPK, _FundAccountingRpt.ValueDateFrom)).ToString("#,##0.00");
                                            //worksheet.Cells[incRowExcel, 3].Value = Convert.ToDecimal(rsDetail.TanggalEfektif).ToString("#,##0");
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 14;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(incRowExcel).Height = 25;
                                            incRowExcel++;


                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Unit Penyertaan";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + Convert.ToDecimal(_host.Get_UnitYesterday(_FundAccountingRpt.FundPK, _FundAccountingRpt.ValueDateFrom)).ToString("#,##0.000");
                                            //worksheet.Cells[incRowExcel, 3].Value = Convert.ToDecimal(rsDetail.TanggalEfektif).ToString("#,##0");
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 14;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[13, 5].Value = rsDetail.FundName;
                                            worksheet.Cells[13, 5].Style.Font.Color.SetColor(Color.ForestGreen);
                                            worksheet.Cells[13, 5].Style.Font.Bold = true;
                                            worksheet.Cells[13, 5].Style.Font.Size = 16;
                                            worksheet.Row(incRowExcel).Height = 25;
                                            incRowExcel++;

                                            int _rowC = 14;
                                            int _rowD = 14 + 2;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Bank Custodian";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + rsDetail.BankCustodian;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 14;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[14, 5].Value = rsDetail.AboutFund;
                                            worksheet.Cells[14, 5].Style.Font.Size = 14;
                                            worksheet.Cells["E" + _rowC + ":K" + _rowD].Merge = true;
                                            worksheet.Cells["E" + _rowC + ":K" + _rowD].Style.WrapText = true;
                                            worksheet.Cells["E" + _rowC + ":K" + _rowD].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells["E" + _rowC + ":K" + _rowD].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Custody Fee";
                                            //worksheet.Cells[incRowExcel, 3].Value = " : " + rsDetail.CustodyFee;
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + rsDetail.CustodyFee + "%";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 14;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(incRowExcel).Height = 27;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Management Fee";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + rsDetail.ManagementFee + "%";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 14;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(incRowExcel).Height = 27;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Subscription Fee";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : Max " + rsDetail.SubscriptionFee + "%";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 14;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(incRowExcel).Height = 27;
                                            incRowExcel++;
                                            int _rowBank = 18;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Redemption Fee";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : Max " + rsDetail.RedemptionFee + "%";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 14;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[17, 5].Value = "BANK CUSTODIAN ";
                                            worksheet.Cells[17, 5].Style.Font.Bold = true;
                                            worksheet.Cells[17, 5].Style.Font.Color.SetColor(Color.ForestGreen);
                                            worksheet.Cells[17, 5].Style.Font.Size = 16;
                                            worksheet.Row(17).Height = 22;
                                            incRowExcel++;

                                            int _rowE = _rowBank;
                                            int _rowF = _rowBank + 7;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Switching Fee";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : Max " + rsDetail.SwitchingFee + "%";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 14;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[_rowBank, 5].Value = rsDetail.AboutCustodian;
                                            worksheet.Cells[_rowBank, 5].Style.Font.Size = 14;
                                            worksheet.Cells["E" + _rowE + ":K" + _rowF].Merge = true;
                                            worksheet.Cells["E" + _rowE + ":K" + _rowF].Style.WrapText = true;
                                            worksheet.Cells["E" + _rowE + ":K" + _rowF].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells["E" + _rowE + ":K" + _rowF].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                                            //worksheet.Row(incRowExcel).Height = 21;
                                            incRowExcel++;

                                            worksheet.Row(18).Height = 36;
                                            worksheet.Row(19).Height = 28;
                                            worksheet.Row(20).Height = 28;
                                            worksheet.Row(21).Height = 28;
                                            worksheet.Row(22).Height = 28;
                                            worksheet.Row(23).Height = 28;
                                            worksheet.Row(24).Height = 28;
                                            worksheet.Row(25).Height = 28;
                                            //worksheet.Row(26).Height = 24;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Rekening Pembelian";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 16;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Color.SetColor(Color.ForestGreen);
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Row(incRowExcel).Height = 24;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Account Name";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + rsDetail.AccountName;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 14;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Color.SetColor(Color.FromArgb(0, 0, 255));
                                            worksheet.Row(incRowExcel).Height = 26;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Account Number";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + rsDetail.AccountNumber;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 14;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Color.SetColor(Color.FromArgb(0, 0, 255));
                                            worksheet.Row(incRowExcel).Height = 26;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Custody Account";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = " : " + rsDetail.CustodyAccount;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Size = 14;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Font.Color.SetColor(Color.FromArgb(0,0,255));
                                            worksheet.Row(incRowExcel).Height = 37;
                                            incRowExcel++;

                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                            worksheet.Cells[incRowExcel, 1].Value = "5 Besar Efek Portofolio";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Color.SetColor(Color.ForestGreen);
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Row(incRowExcel).Height = 25;
                                            incRowExcel++;

                                            worksheet.Cells["A" + 29 + ":C" + 29].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            incRowExcel++;
                                            incRowExcel = incRowExcel + 5;

                                            //worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + 56 + ":C" + 56].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + 56 + ":K" + 56].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + 39 + ":C" + 39].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[26, 5].Value = "PERKEMBANGAN DAN PREDIKSI PASAR";
                                            worksheet.Cells[26, 5].Style.Font.Bold = true;
                                            worksheet.Cells[26, 5].Style.Font.Color.SetColor(Color.ForestGreen);
                                            worksheet.Cells["E" + 26 + ":K" + 26].Merge = true;
                                            worksheet.Cells[26, 5].Style.Font.Size = 16;
                                            worksheet.Row(26).Height = 27;


                                            int _rowZ = 27;
                                            int _rowXZ = 27 + 6;
                                            worksheet.Row(27).Height = 27;
                                            worksheet.Row(28).Height = 27;
                                            worksheet.Row(29).Height = 15;
                                            worksheet.Row(30).Height = 28;
                                            worksheet.Row(31).Height = 24;
                                            worksheet.Row(32).Height = 24;
                                            worksheet.Row(33).Height = 24;

                                            worksheet.Cells[27, 5].Value = rsDetail.MarketBrief;
                                            worksheet.Cells[27, 5].Style.Font.Size = 14;
                                            worksheet.Cells["E" + _rowZ + ":K" + _rowXZ].Merge = true;
                                            worksheet.Cells["E" + _rowZ + ":K" + _rowXZ].Style.WrapText = true;
                                            worksheet.Cells["E" + _rowZ + ":K" + _rowXZ].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells["E" + _rowZ + ":K" + _rowXZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;


                                            int _rowKinerja = 35;
                                            int _rowKinerja2 = 35 + 4;

                                            worksheet.Cells[34, 5].Value = "KINERJA DAN INDIKATOR PEMBANDING ";
                                            worksheet.Cells[34, 5].Style.Font.Size = 16;
                                            worksheet.Cells[34, 5].Style.Font.Bold = true;
                                            worksheet.Cells[34, 5].Style.Font.Color.SetColor(Color.ForestGreen);
                                            worksheet.Cells["E" + 34 + ":K" + 34].Merge = true;
                                            worksheet.Cells[35, 5].Value = "Keterangan";
                                            worksheet.Cells[35, 6].Value = "1 Bulan";
                                            worksheet.Cells["F" + 35 + ":G" + 35].Merge = true;
                                            worksheet.Cells[35, 8].Value = "3 Bulan";
                                            worksheet.Cells["H" + 35 + ":I" + 35].Merge = true;
                                            worksheet.Cells[35, 10].Value = "6 Bulan";
                                            worksheet.Cells[35, 11].Value = "Ytd";
                                            worksheet.Cells["E" + 35 + ":K" + 35].Style.Font.Size = 16;

                                            worksheet.Cells["E" + 35 + ":K" + 35].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["E" + 35 + ":K" + 35].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["E" + 35 + ":K" + 35].Style.Fill.BackgroundColor.SetColor(Color.ForestGreen);

                                            worksheet.Cells["E" + _rowKinerja + ":K" + _rowKinerja2].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + _rowKinerja + ":K" + _rowKinerja2].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + _rowKinerja + ":K" + _rowKinerja2].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + _rowKinerja + ":K" + _rowKinerja2].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Row(34).Height = 22;
                                            worksheet.Row(35).Height = 22;
                                            worksheet.Row(36).Height = 22;
                                            worksheet.Row(37).Height = 22;
                                            worksheet.Row(38).Height = 22;
                                            worksheet.Row(39).Height = 25;

                                            worksheet.Cells["F" + 36 + ":G" + 36].Merge = true;
                                            worksheet.Cells["H" + 36 + ":I" + 36].Merge = true;

                                            worksheet.Cells["F" + 37 + ":G" + 37].Merge = true;
                                            worksheet.Cells["H" + 37 + ":I" + 37].Merge = true;

                                            worksheet.Cells["F" + 38 + ":G" + 38].Merge = true;
                                            worksheet.Cells["H" + 38 + ":I" + 38].Merge = true;

                                            worksheet.Cells["F" + 39 + ":G" + 39].Merge = true;
                                            worksheet.Cells["H" + 39 + ":I" + 39].Merge = true;






                                            worksheet.Cells[41, 5].Value = "KINERJA BULANAN REKSA DANA";
                                            worksheet.Cells[41, 5].Style.Font.Size = 16;
                                            worksheet.Cells[41, 5].Style.Font.Color.SetColor(Color.ForestGreen);
                                            worksheet.Cells[41, 5].Style.Font.Bold = true;


                                        }



                                        incRowExcel = incRowExcel + 27;
                                        int _disA = incRowExcel;
                                        int _disB = incRowExcel + 5;
                                        worksheet.Cells[57, 1].Style.Font.Size = 12;
                                        worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : Seluruh informasi, keterangan, yang disampaikan melalui media elektronik (“e-mail”) ataupun dalam bentuk hardcopy dari PT. Jasa Capital Asset Management (“Dokumen”) hanya merupakan informasi dan/atau keterangan yang tidak dapat diartikan sebagai suatu saran/advise bisnis tertentu, karenanya Dokumen tersebut tidak bersifat mengikat. Segala hal yang berkaitan dengan diterimanya dan/atau dipergunakannya Dokumen tersebut sebagai pengambilan keputusan bisnis dan/atau investasi merupa kan tanggung jawab pribadi atas segala risiko yang mungkin timbul. Sehubungan dengan risiko dan tanggung jawab pribadi atas Dokumen, pengguna dengan ini menyetujui untuk melepaskan segala tanggung jawab dan risiko hukum kepada PT. Jasa Capital Asset Management atas diterimanya dan/atau dipergunakannya Dokumen.";
                                        worksheet.Cells["A" + _disA + ":K" + _disB].Merge = true;
                                        worksheet.Cells["A" + _disA + ":K" + _disB].Style.WrapText = true;
                                        worksheet.Cells["A" + _disA + ":K" + _disB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        worksheet.Cells["A" + _disA + ":K" + _disB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                                        worksheet.Cells["A" + _disA + ":K" + _disB].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + _disA + ":K" + _disB].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(191, 191, 191));

                                        worksheet.Row(59).Height = 13;
                                        worksheet.Row(60).Height = 13;
                                        worksheet.Row(61).Height = 13;
                                        worksheet.Row(62).Height = 13;
                                        worksheet.Row(63).Height = 25;
                                        worksheet.Row(64).Height = 25;

                                        worksheet.Cells["A" + _disA + ":K" + _disB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _disA + ":K" + _disB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _disA + ":K" + _disB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _disA + ":K" + _disB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                        //int rowIndex0 = 61;
                                        //int colIndex0 = 2;
                                        //int Height0 = 50;
                                        //int Width0 = 150;
                                        ////int PixelTop = 100;
                                        ////int PixelLeft = 150;
                                        //Image img0 = Image.FromFile(@"E:\Radsoft\24 Oktober\CORE\W1\Images\12\baru.PNG");
                                        ////Image thumb1 = img0.GetThumbnailImage(80, 70, null, IntPtr.Zero);
                                        //ExcelPicture pic0 = worksheet.Drawings.AddPicture("Sample0", img0);
                                        //pic0.SetPosition(rowIndex0, 0, colIndex0, 0);
                                        ////pic.SetPosition(PixelTop, PixelLeft);  
                                        //pic0.SetSize(Height0, Width0);
                                        ////pic0.SetSize(80);  
                                        //worksheet.Protection.IsProtected = false;
                                        //worksheet.Protection.AllowEditObject = true;

                                        incRowExcel = incRowExcel + 8;
                                        worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 11];
                                    worksheet.Column(1).Width = 20;
                                    worksheet.Column(2).Width = 16;
                                    worksheet.Column(3).Width = 45;
                                    worksheet.Column(4).Width = 3;
                                    worksheet.Column(5).Width = 18;
                                    worksheet.Column(6).Width = 9;
                                    worksheet.Column(7).Width = 9;
                                    worksheet.Column(8).Width = 9;
                                    worksheet.Column(9).Width = 9;
                                    worksheet.Column(10).Width = 18;
                                    worksheet.Column(11).Width = 19;

                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    //worksheet.HeaderFooter.OddHeader.CenteredText = "&16&B PT. JASA CAPITAL ASSET MANAGEMENT \n " + Convert.ToString(_fundName); 


                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 FUND FACT SHEET \n" + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("MMM yyyy");

                                    //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    //string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //Image thumb = img.GetThumbnailImage(165, 75, null, IntPtr.Zero);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(thumb, PictureAlignment.Left);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    //worksheet.HeaderFooter.OddFooter.CenteredText = "\n \n \n \n \n \n \n &14&B Disclaimer" + "&10 : Seluruh informasi, keterangan, yang disampaikan melalui media elektronik (“e-mail”) ataupun dalam bentuk hardcopy dari PT. Jasa Capital Asset Management (“Dokumen”)";


                                }
                            }
                        }
                    }
                    #endregion


                    #region 2

                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets[1];
                    using (SqlConnection DbCon02 = new SqlConnection(Tools.conString))
                    {
                        DbCon02.Open();
                        using (SqlCommand cmd02 = DbCon02.CreateCommand())
                        {
                            cmd02.CommandText = @"Select top 5 A.InstrumentID + ' (' + isnull(D.Name,'') + ')' PortfolioEfek from FundPosition A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                            left join SubSector C on B.SectorPK = C.SubSectorPK and C.status in (1,2)
                            left join Sector D on C.SectorPK = D.SectorPK and D.status in (1,2)
                            where A.FundPK = @FundPK and A.date = @Date
                            and A.status = 2 and B.InstrumentTypePK in (1,4,16)
                            order by A.MarketValue Desc";
                            cmd02.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                            cmd02.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                            using (SqlDataReader dr02 = cmd02.ExecuteReader())
                            {
                                if (!dr02.HasRows)
                                {
                                    return "false";
                                }
                                else
                                {
                                    List<FundAccountingRpt> rList = new List<FundAccountingRpt>();
                                    while (dr02.Read())
                                    {
                                        FundAccountingRpt rSingle = new FundAccountingRpt();
                                        rSingle.PortfolioEfek = Convert.ToString(dr02["PortfolioEfek"]);
                                        rList.Add(rSingle);
                                    }
                                    var QueryByClientID2 =
                                     from r in rList
                                     group r by new { } into rGroup
                                     select rGroup;

                                    int incRowExcel2 = 24;
                                    int _no = 1;
                                    foreach (var rsHeader in QueryByClientID2)
                                    {
                                        foreach (var rsDetail in rsHeader)
                                        {

                                            worksheet2.Cells[incRowExcel2, 1].Value = _no + ". " + rsDetail.PortfolioEfek;
                                            //worksheet.Row(incRowExcel2).Height = 28;
                                            _no++;
                                            incRowExcel2++;


                                        }


                                    }

                                }


                            }

                        }

                    }
                    #endregion


                    #region 3

                    ExcelWorksheet worksheet3 = package.Workbook.Worksheets[1];
                    using (SqlConnection DbCon02 = new SqlConnection(Tools.conString))
                    {
                        DbCon02.Open();
                        using (SqlCommand cmd02 = DbCon02.CreateCommand())
                        {
                            cmd02.CommandText = @"Declare @Aum numeric(22,4)
                            Select @aum = aum from closeNAV where date = @Date and FundPK = @FundPK and status = 2

                            Declare @TotalInvestment numeric(22,4)
                            Select @TotalInvestment = sum(MarketValue) From FundPosition A
                            where A.FundPK = @FundPK and A.date = @Date
                            and A.status = 2


                            Select  
                            Case when InstrumentTypePK in  (1,4,16) then 'SAHAM'
	                             when InstrumentTypePK in  (2,3,8,9,11,12,13,14,15) then 'OBLIGASI'
	                             when InstrumentTypePK in  (5) then 'DEPOSITO'
	                             end InsType,sum(MarketValue)/@Aum * 100  PercentInvestment
                            From FundPosition A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                            where A.FundPK = @FundPK and A.date = @Date
                            and A.status = 2
                            group by B.InstrumentTypePK
                            UNION ALL
                            Select 'KAS' InsType , (@Aum - @TotalInvestment) / @Aum * 100 PercentInvestment";
                            cmd02.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                            cmd02.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                            using (SqlDataReader dr02 = cmd02.ExecuteReader())
                            {
                                if (!dr02.HasRows)
                                {
                                    return "false";
                                }
                                else
                                {
                                    List<FundAccountingRpt> rList = new List<FundAccountingRpt>();
                                    while (dr02.Read())
                                    {
                                        FundAccountingRpt rSingle = new FundAccountingRpt();
                                        rSingle.InsType = Convert.ToString(dr02["InsType"]);
                                        rSingle.PercentInvestment = Convert.ToDecimal(dr02["PercentInvestment"]);
                                        rList.Add(rSingle);
                                    }
                                    var QueryByClientID2 =
                                     from r in rList
                                     group r by new { } into rGroup
                                     select rGroup;

                                    int incRowExcel3 = 33;
                                    foreach (var rsHeader in QueryByClientID2)
                                    {
                                        foreach (var rsDetail in rsHeader)
                                        {


                                            worksheet2.Cells[incRowExcel3, 1].Value = rsDetail.InsType;
                                            worksheet2.Cells[incRowExcel3, 2].Value = rsDetail.PercentInvestment;
                                            incRowExcel3++;

                                            //worksheet.Cells["A" + incRowExcel3 + ":B" + incRowExcel3].Style.Font.Color.SetColor(Color.White);




                                        }


                                    }

                                }


                            }

                        }

                    }
                    #endregion


                    #region 4

                    ExcelWorksheet worksheet4 = package.Workbook.Worksheets[1];
                    using (SqlConnection DbCon02 = new SqlConnection(Tools.conString))
                    {
                        DbCon02.Open();
                        using (SqlCommand cmd02 = DbCon02.CreateCommand())
                        {
                            cmd02.CommandText = @"Declare @TotalInvestmentEquity numeric(22,4)
                            Select @TotalInvestmentEquity = sum(MarketValue) From FundPosition A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)

                            where A.FundPK = @FundPK and A.date = @Date and B.InstrumentTypePK in (1,4,16)
                            and A.status = 2
                            Select D.Name Sector,sum(marketValue)/@TotalInvestmentEquity * 100 AlokasiSector from FundPosition A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status in (1,2)
                            left join SubSector C on B.SectorPK = C.SubSectorPK and C.status in (1,2)
                            left join Sector D on C.SectorPK = D.SectorPK and D.status in (1,2)
                            where A.FundPK = @FundPK and A.date = @Date
                            and A.status = 2 and B.InstrumentTypePK in (1,4,16)
                            group by D.Name";
                            cmd02.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                            cmd02.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                            using (SqlDataReader dr02 = cmd02.ExecuteReader())
                            {
                                if (!dr02.HasRows)
                                {
                                    return "false";
                                }
                                else
                                {
                                    List<FundAccountingRpt> rList = new List<FundAccountingRpt>();
                                    while (dr02.Read())
                                    {
                                        FundAccountingRpt rSingle = new FundAccountingRpt();
                                        rSingle.Sectors = Convert.ToString(dr02["Sector"]);
                                        rSingle.AlokasiSector = Convert.ToDecimal(dr02["AlokasiSector"]);
                                        rList.Add(rSingle);
                                    }
                                    var QueryByClientID2 =
                                     from r in rList
                                     group r by new { } into rGroup
                                     select rGroup;

                                    int incRowExcel4 = 48;
                                    foreach (var rsHeader in QueryByClientID2)
                                    {
                                        foreach (var rsDetail in rsHeader)
                                        {


                                            worksheet2.Cells[incRowExcel4, 1].Value = rsDetail.Sectors;
                                            worksheet2.Cells[incRowExcel4, 2].Value = rsDetail.AlokasiSector;
                                            incRowExcel4++;

                                            ////2
                                            //worksheet.Cells["A" + incRowExcel4 + ":B" + incRowExcel4].Style.Font.Color.SetColor(Color.White);
                                        }


                                    }

                                }


                            }

                        }

                    }
                    #endregion


                    //create a new piechart of type Line satu
                    ExcelPieChart lineChart = worksheet.Drawings.AddChart("lineChart", eChartType.Pie) as ExcelPieChart;
                    //set the title
                    lineChart.Title.Text = "Alokasi Investasi";
                    lineChart.DataLabel.ShowPercent = true;

                    //create the ranges for the chart
                    var rangeLabel = worksheet.Cells["A33:A36"];
                    var range1 = worksheet.Cells["B33:B36"];

                    //add the ranges to the chart
                    lineChart.Series.Add(range1, rangeLabel);

                    //set the names of the legend
                    //lineChart.Series[0].Header = worksheet.Cells["B25"].Value.ToString();
                    //lineChart.Series[1].Header = worksheet.Cells["C25"].Value.ToString();

                    //position of the legend
                    lineChart.Legend.Position = eLegendPosition.Right;
                    lineChart.Style = eChartStyle.Style10;




                    //size of the chart
                    lineChart.SetSize(452, 250);

                    //add the chart at cell B6
                    lineChart.SetPosition(29, 30, 0, 0);



                    //create a new piechart of type Line dua
                    ExcelPieChart lineChart2 = worksheet.Drawings.AddChart("lineChart2", eChartType.Pie) as ExcelPieChart;
                    //set the title
                    lineChart2.Title.Text = "Alokasi Sektor";
                    lineChart2.DataLabel.ShowPercent = true;

                    //create the ranges for the chart
                    var rangeLabel2 = worksheet.Cells["A48:A51"];
                    var range21 = worksheet.Cells["B48:B51"];

                    //add the ranges to the chart
                    lineChart2.Series.Add(range21, rangeLabel2);

                    //set the names of the legend
                    //lineChart2.Series[0].Header = worksheet.Cells["B25"].Value.ToString();
                    //lineChart2.Series[1].Header = worksheet.Cells["C25"].Value.ToString();

                    //position of the legend
                    lineChart2.Legend.Position = eLegendPosition.Right;
                    lineChart2.Style = eChartStyle.Style10;




                    //size of the chart
                    lineChart2.SetSize(452, 290);

                    //add the chart at cell B6
                    lineChart2.SetPosition(39, 30, 0, 0);

                                        package.Save();
                                        return filePath;
                                    

                    }

                }
                catch (Exception err)
                {
                    return "false";
                    throw err;
                }

            
            #endregion
        }

        public Boolean GenerateReportAccounting(string _userID, AccountingRpt _accountingRpt)
        {
            // yang uda dibenerin
           
            #region Trial Balance Plain
            if (_accountingRpt.ReportName.Equals("Trial Balance Plain"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _status = "";
                            string _paramData = "";
                            string _paramAccount = "";
                            DateTime _compareDate = Convert.ToDateTime("10/28/2015");

                            if (!_host.findString(_accountingRpt.AccountFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.AccountFrom))
                            {
                                _paramAccount = "and A.AccountPK  in ( " + _accountingRpt.AccountFrom + " ) ";
                            }
                            else
                            {
                                _paramAccount = "";
                            }

                            if (_accountingRpt.Status == 1)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 2)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 1 ";
                            }
                            else if (_accountingRpt.Status == 3)
                            {
                                _status = " and B.Status = 2 and B.Posted = 0 and B.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 4)
                            {
                                _status = " and B.Status = 1  ";
                            }
                            else if (_accountingRpt.Status == 5)
                            {
                                _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }
                            else if (_accountingRpt.Status == 6)
                            {
                                _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }
                            if (_accountingRpt.ParamData == 1)
                            {
                                _paramData = "  A.Groups = 1  ";
                            }
                            else if (_accountingRpt.ParamData == 2)
                            {
                                _paramData = "  A.Groups = 0  ";
                            }
                            else if (_accountingRpt.ParamData == 3)
                            {
                                _paramData = "  A.Groups in (0,1)  ";
                            }


                            cmd.CommandText = @"  
	                        Declare @PeriodPK int
                            Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2

                            SELECT C.ID, C.Name, C.[Groups],C.[ParentPK],A.Type,    
                            D.ID,       
                            isnull(B.PreviousBaseBalance,0)  * case when A.Type in (2,3) then -1 else 1 end PreviousBaseBalance,      
                            CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                            CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                            isnull(A.CurrentBaseBalance,0)  * case when A.Type in (2,3) then -1 else 1 end CurrentBaseBalance  
                            FROM (      
                            SELECT A.AccountPK, A.Type,      
                            SUM(B.Balance) AS CurrentBalance,       
                            SUM(B.BaseBalance) AS CurrentBaseBalance,      
                            SUM(B.SumDebit) AS CurrentDebit,       
                            SUM(B.SumCredit) AS CurrentCredit,       
                            SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                            SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                            FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                            SELECT A.AccountPK, C.Type,SUM(A.Debit-A.Credit) AS Balance,       
                            SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                            SUM(A.Debit) AS SumDebit,      
                            SUM(A.Credit) AS SumCredit,      
                            SUM(A.BaseDebit) AS SumBaseDebit,      
                            SUM(A.BaseCredit) AS SumBaseCredit,      
                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                            C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                            FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                            INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                            INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                            WHERE  B.ValueDate <= @ValueDateTo and  B.PeriodPK = @PeriodPK
                            " + _status + _paramAccount + @"     
                            Group BY A.AccountPK,C.Type, C.ParentPK1, C.ParentPK2,        
                            C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                            C.ParentPK7, C.ParentPK8, C.ParentPK9        
                            ) AS B        
                            WHERE 
	                        " + _paramData + @"  AND
                            (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                            OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                            OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                            OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                            Group BY A.AccountPK, A.Type       
                            ) AS A LEFT JOIN (       
                            SELECT A.AccountPK, A.Type,       
                            SUM(B.Balance) AS PreviousBalance,        
                            SUM(B.BaseBalance) AS PreviousBaseBalance,       
                            SUM(B.SumDebit) AS PreviousDebit,        
                            SUM(B.SumCredit) AS PreviousCredit,        
                            SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                            SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                            FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                            SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                            SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                            SUM(A.Debit) AS SumDebit,        
                            SUM(A.Credit) AS SumCredit,        
                            SUM(A.BaseDebit) AS SumBaseDebit,        
                            SUM(A.BaseCredit) AS SumBaseCredit,        
                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                            C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                            FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                            INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                            INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                            WHERE  B.ValueDate < @ValueDateFrom  and  B.PeriodPK = @PeriodPK 
                            " + _status + _paramAccount + @" 
                            Group BY A.AccountPK, C.Type,C.ParentPK1, C.ParentPK2,        
                            C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                            C.ParentPK7, C.ParentPK8, C.ParentPK9        
                            ) AS B        
                            WHERE 
	                        " + _paramData + @" AND  
	                        (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                            OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                            OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                            OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                            Group BY A.AccountPK,A.Type       
                            ) AS B ON A.AccountPK = B.AccountPK        
                            INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                            INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                            WHERE (A.CurrentBalance <> 0)        
                            OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                            OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                            OR (A.CurrentBaseBalance <> 0)        
                            OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                            OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)     
                            Order BY C.ID  ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@AccountFrom", _accountingRpt.AccountFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "TrialBalancePlain" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "TrialBalancePlain" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "AccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Trial Balance");

                                        int incRowExcel = 1;
                                        worksheet.Cells[incRowExcel, 1].Value = "TRIAL BALANCE";
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "DATE FROM : ";
                                        worksheet.Cells[incRowExcel, 2].Value = _accountingRpt.ValueDateFrom;
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "DATE TO : ";
                                        worksheet.Cells[incRowExcel, 2].Value = _accountingRpt.ValueDateTo;
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        //incRowExcel++;
                                        worksheet.Cells[4, 1].Value = "ID";
                                        worksheet.Cells[4, 2].Value = "NAME";
                                        worksheet.Cells[4, 3].Value = "PREVIOUS BALANCE";
                                        worksheet.Cells[4, 4].Value = "DEBIT";
                                        worksheet.Cells[4, 5].Value = "CREDIT";
                                        worksheet.Cells[4, 6].Value = "CURRENT BALANCE";
                                        //incRowExcel++;


                                        int incRowExcelA = 5;
                                        while (dr0.Read())
                                        {

                                            if (Convert.ToInt32(dr0["Groups"]) == 1)
                                            {
                                                int _parentPK = 0;
                                                worksheet.Cells["A" + incRowExcelA + ":G" + incRowExcelA].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcelA, 1].Value = Convert.ToString(dr0["ID"]);
                                                worksheet.Cells[incRowExcelA, 2].Value = Convert.ToString(dr0["Name"]);

                                                _parentPK = dr0["ParentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["ParentPK"]);
                                                if (_parentPK == 0)
                                                {
                                                    worksheet.Cells["B" + incRowExcelA + ":B" + incRowExcelA].Style.Font.Color.SetColor(Color.Red);
                                                }
                                                else
                                                {
                                                    worksheet.Cells["B" + incRowExcelA + ":B" + incRowExcelA].Style.Font.Color.SetColor(Color.Blue);
                                                }


                                            }
                                            else
                                            {

                                                worksheet.Cells[incRowExcelA, 1].Value = Convert.ToString(dr0["ID"]);
                                                worksheet.Cells[incRowExcelA, 2].Value = Convert.ToString(dr0["Name"]);

                                            }
                                            worksheet.Cells[incRowExcelA, 3].Value = Convert.ToDecimal(dr0["PreviousBaseBalance"]);
                                            worksheet.Cells[incRowExcelA, 4].Value = Convert.ToDecimal(dr0["BaseDebitMutasi"]);
                                            worksheet.Cells[incRowExcelA, 5].Value = Convert.ToDecimal(dr0["BaseCreditMutasi"]);
                                            worksheet.Cells[incRowExcelA, 6].Value = Convert.ToDecimal(dr0["CurrentBaseBalance"]);



                                            if (_accountingRpt.DecimalPlaces == 0)
                                            {
                                                worksheet.Cells[incRowExcelA, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcelA, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcelA, 6].Style.Numberformat.Format = "#,##0";
                                            }
                                            else if (_accountingRpt.DecimalPlaces == 2)
                                            {
                                                worksheet.Cells[incRowExcelA, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcelA, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcelA, 6].Style.Numberformat.Format = "#,##0.00";
                                            }
                                            else if (_accountingRpt.DecimalPlaces == 4)
                                            {
                                                worksheet.Cells[incRowExcelA, 3].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcelA, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcelA, 6].Style.Numberformat.Format = "#,##0.0000";
                                            }
                                            else if (_accountingRpt.DecimalPlaces == 6)
                                            {
                                                worksheet.Cells[incRowExcelA, 3].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcelA, 5].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcelA, 6].Style.Numberformat.Format = "#,##0.000000";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcelA, 3].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[incRowExcelA, 4].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[incRowExcelA, 5].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[incRowExcelA, 6].Style.Numberformat.Format = "#,##0.00000000";
                                            }
                                            incRowExcelA = incRowExcelA + 1;

                                        }


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.Cells["A:H"].AutoFitColumns();
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        //worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 8];
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&14 TRIAL BALANCE";
                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        if (_accountingRpt.ValueDateTo <= _compareDate)
                                        {
                                            worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftBatchReport();
                                        }
                                        else
                                        {
                                            //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();
                                        }

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_accountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
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
            #endregion
            //


            else
            {
                return false;
            }
        }

        public string Validate_CheckStatusPosting(DateTime _dateFrom, string _type)
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
                       IF EXISTS(select TrxNo,CashierID from Cashier where Status = 1 and Type = @Type and ValueDate <= @DateFrom )
                        BEGIN
	                        DECLARE @CashierID nvarchar(50)
                            SELECT @CashierID = CashierID 
                            FROM Cashier where Status = 1  and Type = @Type and ValueDate <= @DateFrom
                            SELECT 'Posting Cancel, Cashier ID : ' + @CashierID + ' , Has Status = Pending' as Result 
                        END
                        ELSE
                        BEGIN
	                        select '' Result
                        END   ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@Type", _type);

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

        public string Validate_CheckStatusPostingJournal(DateTime _dateFrom)
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
                       IF EXISTS(select * from Journal where Status = 1 and ValueDate <= @DateFrom and Posted = 0)
                        BEGIN
	                        DECLARE @JournalPK int
                            SELECT @JournalPK = JournalPK 
                            FROM Journal where Status = 1 and ValueDate <= @DateFrom and Posted = 0
                            SELECT 'Posting Cancel, JournalPK : ' + CONVERT(varchar(10), @JournalPK) + ' , Has Status = Pending' as Result 
                        END
                        ELSE
                        BEGIN
	                        select '' Result
                        END ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);

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

        public string Validate_CheckStatusPostingTrxPortfolio(DateTime _dateFrom)
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
                        IF EXISTS(select * from TrxPortfolio where Status = 1 and ValueDate <= @DateFrom and Posted = 0)
                        BEGIN
	                        DECLARE @Reference nvarchar(50)
                            SELECT @Reference = Reference 
                            FROM TrxPortfolio where Status = 1 and ValueDate <= @DateFrom and Posted = 0
                            SELECT 'Posting Cancel, Reference : ' + @Reference + ' , Has Status = Pending' as Result 
                        END
                        ELSE
                        BEGIN
	                        select '' Result
                        END ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);

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


        public Boolean Journal_Voucher(string _userID, Journal _journal)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select B.EntryUsersID CheckedBy,B.ApprovedUsersID ApprovedBy,B.ValueDate,B.Reference,   
                                             B.Description,C.ID AccountID,C.Name AccountName,    
                                             D.ID BankCurrencyID,A.DebitCredit DebitCredit,A.Amount Amount,A.Debit Debit, A.Credit Credit,  
                                             A.CurrencyRate Rate,A.BaseDebit BaseDebit,A.BaseCredit BaseCredit,   
                                             E.ID OfficeID,F.ID DepartmentID,G.ID AgentID,H.ID ConsigneeID,I.ID InstrumentID     
                                             from JournalDetail A      
                                             left join Journal B on A.JournalPK = B.JournalPK     
                                             left join Account C on A.AccountPK = C.AccountPK and C.status = 2    
                                             left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2    
                                             left join Office E on A.OfficePK = E.OfficePK and E.status = 2    
                                             left join Department F on A.DepartmentPK = F.DepartmentPK and F.status = 2    
                                             left join Agent G on A.AgentPK = G.AgentPK and G.status = 2    
                                             left join Consignee H on A.ConsigneePK = H.ConsigneePK and H.status = 2    
                                             left join Instrument I on A.InstrumentPK = I.InstrumentPK and I.status = 2   
                                             Where A.JournalPK = @JournalPK ";

                        cmd.Parameters.AddWithValue("@JournalPK", _journal.JournalPK);


                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "JournalVoucher" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "JournalVoucher" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "AccountingReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Journal Voucher");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<JournalVoucher> rList = new List<JournalVoucher>();
                                    while (dr0.Read())
                                    {
                                        JournalVoucher rSingle = new JournalVoucher();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                        rSingle.Description = Convert.ToString(dr0["Description"]);
                                        rSingle.AccountID = Convert.ToString(dr0["AccountID"]);
                                        rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                        rSingle.BankCurrencyID = Convert.ToString(dr0["BankCurrencyID"]);
                                        rSingle.DebitCredit = Convert.ToString(dr0["DebitCredit"]);
                                        rSingle.Debit = Convert.ToDecimal(dr0["Debit"]);
                                        rSingle.Credit = Convert.ToDecimal(dr0["Credit"]);
                                        rSingle.CheckedBy = Convert.ToString(dr0["CheckedBy"]);
                                        rSingle.ApprovedBy = Convert.ToString(dr0["ApprovedBy"]);
                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                        from r in rList
                                        //group r by new { r.Reference, r.ValueDate, r.AccountName, r.OfficeID, r.DepartmentID, r.AgentID, r.ConsigneeID, r.InstrumentID, r.CheckedBy, r.ApprovedBy } into rGroup
                                        group r by new { r.Reference, r.ValueDate, r.CheckedBy, r.ApprovedBy } into rGroup
                                        select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "REFERENCE";                                 
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Reference;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "VALUE DATE : ";                                  
                                        worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsHeader.Key.ValueDate).ToShortDateString();
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        incRowExcel++;

                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "NO";
                                        worksheet.Cells[incRowExcel, 2].Value = "ACCOUNT ID";
                                        worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 4].Value = "Curr.ID";
                                        worksheet.Cells[incRowExcel, 5].Value = "D/C";
                                        worksheet.Cells[incRowExcel, 6].Value = "DEBIT";
                                        worksheet.Cells[incRowExcel, 7].Value = "CREDIT";
                                        string _range = "A" + incRowExcel + ":G" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 11;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                        }

                                        incRowExcel++;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {

                                              worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;



                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.AccountID;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.AccountName;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.BankCurrencyID;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.DebitCredit;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00"; ;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Debit;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00"; ;
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.Credit;
                                            _endRowDetail = incRowExcel;
                                            _no++;
                                            incRowExcel++;

                                        }

                                        int RowF = incRowExcel - 1;
                                        worksheet.Cells["A" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["C" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["E" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["F" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                       

                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells["F" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                        worksheet.Cells["G" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        worksheet.Cells[incRowExcel, 7].Calculate();
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Check By";
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                        worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel = incRowExcel + 5;
                                        worksheet.Cells[incRowExcel, 1].Value = "(";
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;    
                                        worksheet.Cells[incRowExcel, 3].Value = ")";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 5].Value = "(";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 7].Value = ")";
                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                    }

                                    string _rangeDetail = "A:G";

                                    using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                        r.Style.Font.Size = 11;
                                        r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 8];
                                    worksheet.Column(3).Width = 30;
                                    worksheet.Column(4).Width = 15;
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(8).Width = 1;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 Receipt VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 JOURNAL VOUCHER";
                                    worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();
                                    Tools.ExportFromExcelToPDF(filePath, pdfPath);
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

        public decimal Get_CloseNavByFundPK_12(int _fundPK, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"  
                        
                        declare @BegNav numeric(19,8)
                        declare @Nav numeric(19,8)

                        select @BegNav =  isnull(Nav,0)  from CloseNAV 
                        where  FundPK = @FundPK and Date = '11/29/17'

                        select @Nav = isnull(Nav,0) from CloseNAV 
                        where  FundPK = @FundPK and Date = 
                        (
                        select max(Date) from CloseNAV where Date <= @Date and status in (1,2) and FundPK =  @FundPK --3143.13530000
                        )

                        IF NOT EXISTS(
                        select * from CloseNAV 
                        where  FundPK = @FundPK and Date = 
                        (
                        select max(Date) from CloseNAV where Date <= @Date and status in (1,2) and FundPK =  @FundPK --3143.13530000
                        ))
                        BEGIN
                        select @BegNav Nav
                        END
                        ELSE
                        BEGIN
                        select @Nav Nav
                        END ";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    return dr["Nav"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Nav"]);
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


        public Boolean Payment_Voucher(string _userID, Cashier _cashier)
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
                            
                        Select lower(C.ApprovedUsersID) CheckedBy,lower(C.ApprovedUsersID) ApprovedBy, lower(C.EntryUsersID) PreparedBy, C.Reference ,      
                        valuedate,A.ID AccountID, A.Name AccountName, C.Description,     
                        DebitCredit,(Case When DebitCredit ='D' then BaseDebit else 0 end) Debit,(Case When DebitCredit ='D' then 0 else BaseCredit end) Credit,F.ID DepartmentID,Case When DebitCredit ='D' then 1 else 2 end Row       
                        from Cashier C       
                        left join Account A on C.Debitaccountpk =A.Accountpk and A.status = 2       
                        left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                        left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                        left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                        left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                        left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                        Where C.CashierID = @CashierID and C.Type = 'CP' and C.PeriodPK = @PeriodPK and C.Status in (1,2)         
                        UNION ALL       
                        Select lower(C.ApprovedUsersID) CheckedBy,lower(C.ApprovedUsersID) ApprovedBy, lower(C.EntryUsersID) PreparedBy, C.Reference ,      
                        valuedate,A.ID AccountID, A.Name AccountName, '' Description,         
                        'C', 0 Debit,SUM(Case When DebitCredit ='D' then BaseDebit else BaseCredit * -1 end)Credit,'' DepartmentID ,3 Row          
                        from Cashier C       
                        left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2       
                        left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                        left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                        left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                        left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                        left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                        Where C.CashierID = @CashierID  and C.Type = 'CP' and C.PeriodPK = @PeriodPK and C.Status in (1,2)    
                        group by C.EntryUsersID, C.ApprovedUsersID, Valuedate,A.ID, A.Name, C.Reference     
                        Order By row,AccountID asc ";

                        cmd.Parameters.AddWithValue("@CashierID", _cashier.CashierID);
                        cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
                        //cmd.Parameters.AddWithValue("@Status", _cashier.Status);

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "PaymentVoucher" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "PaymentVoucher" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "FinanceReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Payment Voucher");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<CashierVoucher> rList = new List<CashierVoucher>();
                                    while (dr0.Read())
                                    {
                                        CashierVoucher rSingle = new CashierVoucher();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                        rSingle.Description = Convert.ToString(dr0["Description"]);
                                        rSingle.AccountID = Convert.ToString(dr0["AccountID"]);
                                        rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                        rSingle.DebitCredit = Convert.ToString(dr0["DebitCredit"]);
                                        rSingle.Debit = Convert.ToDecimal(dr0["Debit"]);
                                        rSingle.Credit = Convert.ToDecimal(dr0["Credit"]);
                                        rSingle.DepartmentID = Convert.ToString(dr0["DepartmentID"]);
                                        rSingle.CheckedBy = Convert.ToString(dr0["CheckedBy"]);
                                        rSingle.ApprovedBy = Convert.ToString(dr0["ApprovedBy"]);
                                        rSingle.PreparedBy = Convert.ToString(dr0["PreparedBy"]);
                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                                 from r in rList
                                                 orderby r.DebitCredit descending
                                                 group r by new { r.Reference, r.ValueDate, r.CheckedBy, r.PreparedBy, r.ApprovedBy } into rGroup
                                                 select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "REFERENCE : ";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Reference;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "DATE : ";
                                        //worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MM-yyyy";
                                        worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsHeader.Key.ValueDate).ToShortDateString();
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1].Value = "BANK";
                                        //worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        //worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.BankID + "-" + rsHeader.Key.BankName;
                                        incRowExcel++;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "NO";
                                        worksheet.Cells[incRowExcel, 2].Value = "ACCOUNT ID";
                                        worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 4].Value = "DESCRIPTION";
                                        worksheet.Cells[incRowExcel, 5].Value = "DEBIT";
                                        worksheet.Cells[incRowExcel, 6].Value = "CREDIT";
                                        worksheet.Cells[incRowExcel, 7].Value = "DEPT";
                                        string _range = "A" + incRowExcel + ":G" + incRowExcel;

                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 11;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                        }

                                        incRowExcel++;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;


                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {



                                            //ThickBox Border HEADER

                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.AccountID;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.AccountName;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Description;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            //worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 6].Value = rsDetail.Amount;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.Debit;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Credit;
                                            if (_cashier.DecimalPlaces == 0)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            }
                                            else if (_cashier.DecimalPlaces == 2)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            }
                                            else if (_cashier.DecimalPlaces == 4)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            }
                                            else if (_cashier.DecimalPlaces == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                            }
                                            //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                            //worksheet.Cells[incRowExcel, 10].Value = rsDetail.Rate;
                                            //worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 11].Value = rsDetail.BaseDebit;
                                            //worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 12].Value = rsDetail.BaseCredit;
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.DepartmentID;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            _endRowDetail = incRowExcel;

                                            _no++;
                                            incRowExcel++;





                                        }

                                        int RowF = incRowExcel - 1;
                                        worksheet.Cells["A" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["C" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["E" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["F" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["I" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["I" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["J" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["J" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["K" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["K" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["L" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["L" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["M" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["M" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;



                                        //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";;
                                        //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":F" + _endRowDetail + ")";


                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        if (_cashier.DecimalPlaces == 0)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                        }
                                        else if (_cashier.DecimalPlaces == 2)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                        }
                                        else if (_cashier.DecimalPlaces == 4)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                        }
                                        else if (_cashier.DecimalPlaces == 6)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                        }
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Calculate();

                                        incRowExcel = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 5].Value = worksheet.Cells[incRowExcel - 1, 5].Value;
                                        worksheet.Cells[incRowExcel, 6].Value = worksheet.Cells[incRowExcel - 1, 6].Value;
                                        if (_cashier.DecimalPlaces == 0)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                        }
                                        else if (_cashier.DecimalPlaces == 2)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                        }
                                        else if (_cashier.DecimalPlaces == 4)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                        }
                                        else if (_cashier.DecimalPlaces == 6)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                        }

                                        worksheet.Cells[incRowExcel - 1, 5].Value = "";
                                        worksheet.Cells[incRowExcel - 1, 6].Value = "";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Prepared By";
                                        worksheet.Cells[incRowExcel, 4].Value = "Checked By";
                                        worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 5;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 2].Value = "(     " + rsHeader.Key.PreparedBy + "      )";
                                        worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 4].Value = "(      " + rsHeader.Key.CheckedBy + "      )";
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 6].Value = "(                      )";
                                        worksheet.Cells["F" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        worksheet.Row(incRowExcel).PageBreak = true;

                                    }

                                    string _rangeDetail = "A:G";

                                    using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                        r.Style.Font.Size = 11;
                                        r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 8];
                                    worksheet.Column(3).Width = 30;
                                    worksheet.Column(4).Width = 30;
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(8).Width = 1;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 PAYMENT / JOURNAL VOUCHER";
                                    worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();

                                }
                                Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                return true;
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

        public Boolean Receipt_Voucher(string _userID, Cashier _cashier)
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
                            Select lower(C.ApprovedUsersID) CheckedBy,lower(C.ApprovedUsersID) ApprovedBy, lower(C.EntryUsersID) PreparedBy, C.Reference ,      
                            valuedate,A.ID AccountID, A.Name AccountName, C.Description,       
                            DebitCredit,(Case When DebitCredit ='C' then 0 else BaseCredit end) Debit,(Case When DebitCredit ='C' then BaseDebit else 0 end) Credit,F.ID DepartmentID , case when DebitCredit ='D' then 2 else 3 end Row    
                            from Cashier C         
                            left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2         
                            Where 
							C.CashierID = @CashierID  and 
							C.Type = 'CR' and 
							C.PeriodPK = @PeriodPK and 
							C.Status in (1,2)         
                            UNION ALL         
                            Select lower(C.ApprovedUsersID) CheckedBy,lower(C.ApprovedUsersID) ApprovedBy, lower(C.EntryUsersID) PreparedBy, C.Reference ,        
                            valuedate,A.ID AccountID, A.Name AccountName, '' Description,       
                            'D', SUM(Case When DebitCredit ='C' then BaseDebit else BaseCredit * -1 end) Debit,0 Credit,'' DepartmentID, 1 Row             
                            from Cashier C         
                            left join Account A on C.DebitAccountPK =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2        
                            Where 
							C.CashierID = @CashierID  and 
							C.Type = 'CR' and 
							C.PeriodPK = @PeriodPK and 
							C.Status in (1,2)       
                            group by C.EntryUsersID , C.ApprovedUsersID, Valuedate,A.ID, A.Name,C.Reference     
                            Order By Row,AccountID asc ";

                        cmd.Parameters.AddWithValue("@CashierID", _cashier.CashierID);
                        cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
                        //cmd.Parameters.AddWithValue("@Status", _cashier.Status);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "ReceiptVoucher" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "ReceiptVoucher" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "FinanceReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Receipt Voucher");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<CashierVoucher> rList = new List<CashierVoucher>();
                                    while (dr0.Read())
                                    {
                                        CashierVoucher rSingle = new CashierVoucher();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                        rSingle.Description = Convert.ToString(dr0["Description"]);
                                        rSingle.AccountID = Convert.ToString(dr0["AccountID"]);
                                        rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                        rSingle.DebitCredit = Convert.ToString(dr0["DebitCredit"]);
                                        rSingle.Debit = Convert.ToDecimal(dr0["Debit"]);
                                        rSingle.Credit = Convert.ToDecimal(dr0["Credit"]);
                                        rSingle.DepartmentID = Convert.ToString(dr0["DepartmentID"]);
                                        rSingle.CheckedBy = Convert.ToString(dr0["CheckedBy"]);
                                        rSingle.ApprovedBy = Convert.ToString(dr0["ApprovedBy"]);
                                        rSingle.PreparedBy = Convert.ToString(dr0["PreparedBy"]);
                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                                 from r in rList
                                                 orderby r.DebitCredit descending
                                                 group r by new { r.Reference, r.ValueDate, r.CheckedBy, r.ApprovedBy, r.PreparedBy } into rGroup
                                                 select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "REFERENCE : ";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Reference;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "DATE : ";
                                        //worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MM-yyyy";
                                        worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsHeader.Key.ValueDate).ToShortDateString();
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1].Value = "BANK";
                                        //worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        //worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.BankID + "-" + rsHeader.Key.BankName;
                                        incRowExcel++;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "NO";
                                        worksheet.Cells[incRowExcel, 2].Value = "ACCOUNT ID";
                                        worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 4].Value = "DESCRIPTION";
                                        worksheet.Cells[incRowExcel, 5].Value = "DEBIT";
                                        worksheet.Cells[incRowExcel, 6].Value = "CREDIT";
                                        worksheet.Cells[incRowExcel, 7].Value = "DEPT";
                                        string _range = "A" + incRowExcel + ":G" + incRowExcel;

                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = 11;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                        }

                                        incRowExcel++;
                                        int _no = 1;

                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;


                                        //end area header
                                        foreach (var rsDetail in rsHeader)
                                        {



                                            //ThickBox Border HEADER

                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.AccountID;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.AccountName;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Description;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            //worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 6].Value = rsDetail.Amount;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.Debit;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Credit;
                                            if (_cashier.DecimalPlaces == 0)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            }
                                            else if (_cashier.DecimalPlaces == 2)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            }
                                            else if (_cashier.DecimalPlaces == 4)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            }
                                            else if (_cashier.DecimalPlaces == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                            }

                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.DepartmentID;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            _endRowDetail = incRowExcel;

                                            _no++;
                                            incRowExcel++;





                                        }

                                        int RowF = incRowExcel - 1;
                                        worksheet.Cells["A" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["B" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["C" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["C" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["E" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["E" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["F" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["G" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;





                                        //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";;
                                        //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":F" + _endRowDetail + ")";

                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        if (_cashier.DecimalPlaces == 0)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                        }
                                        else if (_cashier.DecimalPlaces == 2)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                        }
                                        else if (_cashier.DecimalPlaces == 4)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                        }
                                        else if (_cashier.DecimalPlaces == 6)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                        }

                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 5].Value = worksheet.Cells[incRowExcel - 1, 5].Value;
                                        worksheet.Cells[incRowExcel, 6].Value = worksheet.Cells[incRowExcel - 1, 6].Value;
                                        if (_cashier.DecimalPlaces == 0)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                        }
                                        else if (_cashier.DecimalPlaces == 2)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                        }
                                        else if (_cashier.DecimalPlaces == 4)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                        }
                                        else if (_cashier.DecimalPlaces == 6)
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                        }
                                        else
                                        {
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                        }

                                        worksheet.Cells[incRowExcel - 1, 5].Value = "";
                                        worksheet.Cells[incRowExcel - 1, 6].Value = "";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Prepared By";
                                        worksheet.Cells[incRowExcel, 4].Value = "Checked By";
                                        worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 5;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 2].Value = "(     " + rsHeader.Key.PreparedBy + "      )";
                                        worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 4].Value = "(      " + rsHeader.Key.CheckedBy + "      )";
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 6].Value = "(                      )";
                                        worksheet.Cells["F" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        worksheet.Row(incRowExcel).PageBreak = true;


                                    }

                                    string _rangeDetail = "A:G";

                                    using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                        r.Style.Font.Size = 11;
                                        r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 8];
                                    worksheet.Column(3).Width = 30;
                                    worksheet.Column(4).Width = 30;
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(8).Width = 1;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 Receipt VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 RECEIPT / JOURNAL VOUCHER";
                                    worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();

                                }
                                Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                return true;
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


        public Boolean GenerateReportFinance(string _userID, FinanceRpt _financeRpt)
        {
            #region Cashier Voucher
            if (_financeRpt.ReportName.Equals("Cashier Voucher"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _status = "";
                            string _type = "";
                            string _paramBankFromDebitCashRef = "";
                            string _paramBankFromCreditCashRef = "";
                            string _paramReferenceFrom = "";
                            DateTime _compareDate = Convert.ToDateTime("10/28/2015");

                            if (!_host.findString(_financeRpt.BankFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_financeRpt.BankFrom))
                            {
                                _paramBankFromDebitCashRef = "And C.DebitCashRefPK  in ( " + _financeRpt.BankFrom + " ) ";
                            }
                            else
                            {
                                _paramBankFromDebitCashRef = "";
                            }

                            if (!_host.findString(_financeRpt.BankFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_financeRpt.BankFrom))
                            {
                                _paramBankFromCreditCashRef = "And C.CreditCashRefPK  in ( " + _financeRpt.BankFrom + " ) ";
                            }
                            else
                            {
                                _paramBankFromCreditCashRef = "";
                            }


                            if (!_host.findString(_financeRpt.ReferenceFrom.ToLower(), "'all'", ",") && !string.IsNullOrEmpty(_financeRpt.ReferenceFrom))
                            {
                                _paramReferenceFrom = " and C.Reference  in ( " + _financeRpt.ReferenceFrom + " ) ";
                            }
                            else
                            {
                                _paramReferenceFrom = "";
                            }

                            if (_financeRpt.Status == 1)
                            {
                                _status = " and C.Status = 2 and C.Posted = 1 and C.Revised = 0 ";
                            }
                            else if (_financeRpt.Status == 2)
                            {
                                _status = " and C.Status = 2 and C.Posted = 1 and C.Revised = 1 ";
                            }
                            else if (_financeRpt.Status == 3)
                            {
                                _status = " and C.Status = 2 and C.Posted = 0 and C.Revised = 0 ";
                            }
                            else if (_financeRpt.Status == 4)
                            {
                                _status = " and C.Status = 1  ";
                            }
                            else if (_financeRpt.Status == 5)
                            {
                                _status = " and (C.Status = 2 or C.Posted = 1) and C.Revised = 0 and C.status not in (3,4) ";
                            }
                            else if (_financeRpt.Status == 6)
                            {
                                _status = " and (C.Status = 1 Or C.Status = 2 or C.Posted = 1) and C.Revised = 0 and C.status not in (3,4) ";
                            }

                            if (_financeRpt.CashierType == "IN")
                            {
                                _type = "  C.Type in ( 'CR') ";
                            }
                            else if (_financeRpt.CashierType == "OUT")
                            {
                                _type = "  C.Type in ( 'CP') ";
                            }
                            else if (_financeRpt.CashierType == "ALL")
                            {
                                _type = "  C.Type in ( 'CR','CP') ";
                            }


                            cmd.CommandText =
                                @" SELECT A.CheckedBy,A.ApprovedBy,A.PreparedBy,A.Reference,A.ValueDate,A.AccountID,A.AccountName 
                            ,A.Description,A.DebitCredit,SUM(A.Debit) BaseDebit,SUM(A.Credit) BaseCredit,A.DepartmentID,A.RefNo,A.InstrumentID,A.OfficeID,A.AgentID,A.ConsigneeID
                            FROM (
	                            Select C.ApprovedUsersID CheckedBy,C.ApprovedUsersID ApprovedBy,C.EntryUsersID PreparedBy,       
	                            reference , valuedate,A.ID AccountID, A.Name AccountName, '' Description,         
	                            CASE WHEN SUM(Case When DebitCredit ='D' AND C.Type = 'CR' 
	                            THEN BaseDebit ELSE CASE WHEN DebitCredit = 'C' AND C.Type = 'CR' then BaseDebit * -1 
	                            ELSE CASE WHEN DebitCredit = 'D' AND C.Type = 'CP' THEN C.BaseCredit * - 1 ELSE BaseCredit END END END
	                            ) < 0 AND C.Type = 'CR' THEN 'D' ELSE 'C' END DebitCredit, 

	                            CASE WHEN SUM(Case When DebitCredit ='D' AND C.Type = 'CR' 
	                            THEN BaseDebit ELSE CASE WHEN DebitCredit = 'C' AND C.Type = 'CR' then BaseDebit * -1 
	                            ELSE CASE WHEN DebitCredit = 'D' AND C.Type = 'CP' THEN C.BaseCredit * - 1 ELSE BaseCredit END END END
	                            ) < 0 AND C.Type = 'CR'
	                            THEN ABS(SUM(Case When DebitCredit ='D' AND C.Type = 'CR' 
	                            THEN BaseDebit ELSE CASE WHEN DebitCredit = 'C' AND C.Type = 'CR' then BaseDebit * -1 
	                            ELSE CASE WHEN DebitCredit = 'D' AND C.Type = 'CP' THEN C.BaseCredit * - 1 ELSE BaseCredit END END END)) 
	                            ELSE 0 END Debit,

	                            CASE WHEN SUM(Case When DebitCredit ='D' AND C.Type = 'CR' 
	                            THEN BaseDebit ELSE CASE WHEN DebitCredit = 'C' AND C.Type = 'CR' then BaseDebit * -1 
	                            ELSE CASE WHEN DebitCredit = 'D' AND C.Type = 'CP' THEN C.BaseCredit * - 1 ELSE BaseCredit END END END
	                            ) < 0 AND C.Type = 'CR' 
	                            THEN 0 
	                            ELSE ABS(SUM(Case When DebitCredit ='D' AND C.Type = 'CR' 
	                            THEN BaseDebit ELSE CASE WHEN DebitCredit = 'C' AND C.Type = 'CR' then BaseDebit * -1 
	                            ELSE CASE WHEN DebitCredit = 'D' AND C.Type = 'CP' THEN C.BaseCredit * - 1 ELSE BaseCredit END END END)) END Credit,

	                            '' DepartmentID,
	                            case when Reference = '' or Reference is null then 0 else cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) end RefNo,'' InstrumentID,'' OfficeID,'' AgentID,'' ConsigneeID   
	                            from Cashier C       
	                            left join Account A ON CASE WHEN C.Type = 'CR' then C.DebitAccountPK ELSE C.CreditAccountPK end =A.Accountpk and A.status in (1,2)       
	                            left join Office E on C.OfficePK = E.OfficePK and E.status in (1,2)       
	                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status in (1,2)       
	                            left join Agent G on C.AgentPK = G.AgentPK and G.status in (1,2)       
	                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status in (1,2)       
	                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status in (1,2)       
	                            WHERE 
								--" + _type + _paramReferenceFrom + _status + @"and 
								C.ValueDate between @Datefrom and @DateTo  
	                            group by C.EntryUsersID,C.ApprovedUsersID,Reference , Valuedate,A.ID, A.Name ,F.ID ,C.Type	
	                            )A
	                            GROUP BY  A.CheckedBy,A.ApprovedBy,A.PreparedBy,A.Reference,A.ValueDate,A.AccountID,A.AccountName ,A.Description,A.DebitCredit,
	                            A.DepartmentID,A.RefNo,A.InstrumentID,A.OfficeID,A.AgentID,A.ConsigneeID      
                                UNION ALL       
                                 Select C.ApprovedUsersID CheckedBy,C.ApprovedUsersID ApprovedBy,C.EntryUsersID PreparedBy,    
                                Reference , ValueDate,A.ID AccountID, A.Name AccountName, C.Description,     
                                DebitCredit,(Case When DebitCredit ='D' then BaseDebit else 0 end) BaseDebit,(Case When DebitCredit ='D' then 0 else BaseCredit end) BaseCredit,F.ID DepartmentID,
                                case when Reference = '' or Reference is null then 0 else cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) end RefNo,'' InstrumentID,'' OfficeID,'' AgentID,'' ConsigneeID       
                                from Cashier C       
                                left join Account A on CASE WHEN C.Type = 'CR' then C.CreditAccountPK ELSE C.DebitAccountPK end =A.Accountpk and A.status = 2     
                                left join Office E on C.OfficePK = E.OfficePK and E.status in (1,2)       
                                left join Department F on C.DepartmentPK = F.DepartmentPK and F.status in (1,2)      
                                left join Agent G on C.AgentPK = G.AgentPK and G.status in (1,2)       
                                left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status in (1,2)     
                                left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status in (1,2)     
                                WHERE " + _type + _paramReferenceFrom + _status + "and C.ValueDate between @Datefrom and @DateTo";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _financeRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _financeRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@BankFrom", _financeRpt.BankFrom);
                            cmd.Parameters.AddWithValue("@ReferenceFrom", _financeRpt.ReferenceFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "CashierVoucher" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "CashierVoucher" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FinanceReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Cashier Voucher");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<CashierVoucher> rList = new List<CashierVoucher>();
                                        while (dr0.Read())
                                        {
                                            CashierVoucher rSingle = new CashierVoucher();
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.RefNo = Convert.ToInt32(dr0["RefNo"]);
                                            rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                            rSingle.Description = Convert.ToString(dr0["Description"]);
                                            rSingle.AccountID = Convert.ToString(dr0["AccountID"]);
                                            rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                            rSingle.BaseDebit = Convert.ToDecimal(dr0["BaseDebit"]);
                                            rSingle.BaseCredit = Convert.ToDecimal(dr0["BaseCredit"]);
                                            rSingle.OfficeID = Convert.ToString(dr0["OfficeID"]);
                                            rSingle.DepartmentID = Convert.ToString(dr0["DepartmentID"]);
                                            rSingle.AgentID = Convert.ToString(dr0["AgentID"]);
                                            rSingle.ConsigneeID = Convert.ToString(dr0["ConsigneeID"]);
                                            rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.CheckedBy = Convert.ToString(dr0["CheckedBy"]);
                                            rSingle.ApprovedBy = Convert.ToString(dr0["ApprovedBy"]);
                                            rSingle.PreparedBy = Convert.ToString(dr0["PreparedBy"]);
                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                                from r in rList
                                                orderby r.ValueDate, r.RefNo ascending
                                                group r by new { r.Reference, r.ValueDate, r.CheckedBy, r.ApprovedBy, r.PreparedBy } into rGroup
                                                select rGroup;


                                        int incRowExcel = 0;

                                        foreach (var rsHeader in GroupByReference)
                                        {

                                            incRowExcel = incRowExcel + 1;
                                            if (rsHeader.Key.ValueDate >= _compareDate)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                                worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 13;
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                                worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 13;
                                            }

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "REFERENCE : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Reference;
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 10].Value = "DATE : ";
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.ValueDate;

                                            //worksheet.Cells[incRowExcel, 11].Value = Convert.ToDateTime(rsHeader.Key.ValueDate).ToShortDateString();
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1].Value = "BANK";
                                            //worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            //worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.BankID + "-" + rsHeader.Key.BankName;
                                            incRowExcel++;

                                            //Row B = 3
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "ACCOUNT ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                            worksheet.Cells[incRowExcel, 4].Value = "DESCRIPTION";
                                            worksheet.Cells[incRowExcel, 5].Value = "BASE DEBIT";
                                            worksheet.Cells[incRowExcel, 6].Value = "BASE CREDIT";
                                            worksheet.Cells[incRowExcel, 7].Value = "DEPT";
                                            worksheet.Cells[incRowExcel, 8].Value = "INST";
                                            worksheet.Cells[incRowExcel, 9].Value = "OFF";
                                            worksheet.Cells[incRowExcel, 10].Value = "DIR";
                                            worksheet.Cells[incRowExcel, 11].Value = "CONS";
                                            string _range = "A" + incRowExcel + ":K" + incRowExcel;

                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                r.Style.Font.Size = 11;
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                                r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                            }

                                            incRowExcel++;
                                            int _no = 1;

                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;


                                            //end area header
                                            foreach (var rsDetail in rsHeader)
                                            {



                                                //ThickBox Border HEADER

                                                worksheet.Cells["A" + RowB + ":K" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":K" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":K" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":K" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.AccountID;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AccountName;
                                                worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Description;
                                                worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                                //worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                //worksheet.Cells[incRowExcel, 6].Value = rsDetail.Amount;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.BaseDebit;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.BaseCredit;
                                                if (_financeRpt.DecimalPlaces == 0)
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                }
                                                else if (_financeRpt.DecimalPlaces == 2)
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                }
                                                else if (_financeRpt.DecimalPlaces == 4)
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                }
                                                else if (_financeRpt.DecimalPlaces == 6)
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                                }


                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.DepartmentID;
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.OfficeID;
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.AgentID;
                                                worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.ConsigneeID;
                                                worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                _endRowDetail = incRowExcel;

                                                _no++;
                                                incRowExcel++;





                                            }

                                            int RowF = incRowExcel - 1;
                                            worksheet.Cells["A" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["C" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells["D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells["E" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + RowB + ":E" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells["F" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowB + ":F" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells["G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowB + ":G" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["I" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["J" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["K" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;





                                            //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            if (_financeRpt.DecimalPlaces == 0)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            }
                                            else if (_financeRpt.DecimalPlaces == 2)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            }
                                            else if (_financeRpt.DecimalPlaces == 4)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            }
                                            else if (_financeRpt.DecimalPlaces == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                            }

                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            incRowExcel = incRowExcel + 1;
                                            worksheet.Cells[incRowExcel, 5].Value = worksheet.Cells[incRowExcel - 1, 5].Value;
                                            worksheet.Cells[incRowExcel, 6].Value = worksheet.Cells[incRowExcel - 1, 6].Value;
                                            if (_financeRpt.DecimalPlaces == 0)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            }
                                            else if (_financeRpt.DecimalPlaces == 2)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            }
                                            else if (_financeRpt.DecimalPlaces == 4)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            }
                                            else if (_financeRpt.DecimalPlaces == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                            }
                                            worksheet.Cells[incRowExcel - 1, 5].Value = "";
                                            worksheet.Cells[incRowExcel - 1, 6].Value = "";
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 2].Value = "Prepared By";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Value = "Checked By";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel = incRowExcel + 5;
                                            worksheet.Cells[incRowExcel, 2].Value = "(     " + rsHeader.Key.PreparedBy;
                                            worksheet.Cells[incRowExcel, 3].Value = "      )";
                                            worksheet.Cells[incRowExcel, 4].Value = "(     " + rsHeader.Key.CheckedBy;
                                            worksheet.Cells[incRowExcel, 5].Value = "      )";
                                            worksheet.Cells[incRowExcel, 6].Value = "(     ";
                                            worksheet.Cells[incRowExcel, 7].Value = "      )";
                                            worksheet.Row(incRowExcel).PageBreak = true;

                                        }

                                        string _rangeDetail = "A:G";

                                        using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Size = 11;
                                            r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        }

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 12];
                                        worksheet.Column(3).Width = 30;
                                        worksheet.Column(4).Width = 30;
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(11).Width = 1;
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).AutoFit();
                                        worksheet.Column(8).AutoFit();
                                        worksheet.Column(9).AutoFit();
                                        worksheet.Column(10).AutoFit();
                                        worksheet.Column(11).AutoFit();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 CASHIER VOUCHER";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);


                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_financeRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
                                        return true;

                                    }
                                    return true;
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
            #endregion
          

            else
            {
                return false;
            }

        }



    }
}