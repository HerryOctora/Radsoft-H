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
    public class RetrieveFromBridgeReps
    {
        Host _host = new Host();

        private RetrieveFromBridge setRetrieveFromBridge(SqlDataReader dr)
        {
            RetrieveFromBridge M_RetrieveFromBridge = new RetrieveFromBridge();
            M_RetrieveFromBridge.RetrieveFromBridgePK = Convert.ToInt32(dr["RetrieveFromBridgePK"]);
            M_RetrieveFromBridge.ValueDate = Convert.ToString(dr["ValueDate"]);
            M_RetrieveFromBridge.CheckManagementFee = Convert.ToString(dr["CheckManagementFee"]);
            M_RetrieveFromBridge.CheckUnitRegistry = Convert.ToString(dr["CheckUnitRegistry"]);
            return M_RetrieveFromBridge;
        }

        public string Validate_CheckManagementFee(DateTime _dateFrom, DateTime _dateTo)
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
                        create table #Temp 
                        (
                        Result nvarchar(MAX)
                        )

               
                        Insert Into #Temp(Result)
                        select distinct A.Reference from (
                        select Reference from journal where Posted = 1 and Reversed = 0 and status = 2 and Description = 'Management Fee' and ValueDate >= @DateFrom
                        union all
                        select Reference from journal where Posted = 1 and Reversed = 0 and status = 2 and Description = 'Management Fee' and ValueDate >= @DateTo
                        )A


                        IF EXISTS(
                        select * from journal where Posted = 1 and Reversed = 0 and status = 2 and Description = 'Management Fee' and ValueDate >= @DateFrom
                        union all
                        select * from journal where Posted = 1 and Reversed = 0 and status = 2 and Description = 'Management Fee' and ValueDate >= @DateTo
                        )

                        DECLARE @combinedString VARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Result
                        FROM #Temp
                        BEGIN
	                           SELECT 'Posting Cancel, Please Check Reference Journal : ' + @combinedString as Result 
                        END 
                           ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);

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

        public string Validate_CheckUnitRegistry(DateTime _date, string _param)
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
                        
                        
                        DECLARE @String VARCHAR(MAX)

                        create table #Temp 
                        (
                        Result nvarchar(MAX)
                        )

                        --update BRIDGE.dbo.STG_SIARTransact set ProductCode = 'MNCICON' where TradeDate = @date and ProductCode = 'ICON'

                        create table #A(FundID nvarchar(50))

                        Declare @Description nvarchar(50)
                        select @Description = 'RFB' + REPLACE(CONVERT(VARCHAR(10), @date, 103), '/', '')

                        Insert into #A(FundID)
                        Select ID from Fund where FundPK not in
                        (
                        select FundPK from CloseNAV
                        where Date = @date and status = 2 and FundPK in (1,4,5,6,9,24,34)) and status = 2
                        and FundPK in (1,4,5,6,9,24,34)

                        IF (dbo.CheckTodayIsHoliday(@date) = 1)
                        BEGIN
                        SELECT 'Today is Holiday' as Result 
                        END
                        ELSE
                        BEGIN
                        IF exists(select FundJournalPK from FundJournal where ValueDate = dbo.fworkingday(@Date,1) and status = 2 and Posted = 1 and Description = @Description)
                        BEGIN
	                        Insert Into #Temp(Result)
	                        SELECT 'DONE' Result
                        END
                        ELSE IF NOT EXISTS(select * from RetrieveFromBridge where Date = @date)
                        BEGIN
	                        Insert Into #Temp(Result)
	                        SELECT 'Not Exists Data From Bridge' as Result 
                        END
                        ELSE IF EXISTS(Select FundID from #A)
                        BEGIN
                        SELECT @String = COALESCE(@String + ', ', '') + FundID
                        FROM #A
	                        Insert Into #Temp(Result)
	                        SELECT 'Not Exists Data From Close NAV For : ' + @String as Result 
                        END
                        ELSE
                        BEGIN
	                        Insert Into #Temp(Result)
	                        select 'Ready For Posting Unit Registry' Result
                        END


                        DECLARE @combinedString VARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Result
                        FROM #Temp
	                        BEGIN
		                        SELECT @combinedString as Result 
	                        END 
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

                        create table #ReferenceTemp
                        (Reference nvarchar(50))

                        declare @FundPK int
                        declare @InstrumentPK int 
                        declare @DepartmentPK int
                        declare @AgentPK int
    


                        Declare @ARManagementFee int 
                        Declare @TaxARManagementFee int 
                        Declare @ManagementFeeExpense int 
                        Declare @TaxManagementFeeExpense int 


                        declare @ManagementFeeAmount numeric(22,6)

                        declare @ARManagementFeeAmount numeric(22,6)
                        declare @TaxARManagementFeeAmount numeric(22,6)
                        declare @ManagementFeeExpenseAmount numeric(22,6)
                        declare @TaxManagementFeeExpenseAmount numeric(22,6)

                        --UPDATE BRIDGE.dbo.STG_TRevenue set ProductCode = 'MNCICON' where ProductCode = 'ICON'

                        --insert into #date (valuedate)
                        --SELECT  TOP (DATEDIFF(DAY, @datefrom, @dateto) + 1) Dates = DATEADD(DAY, ROW_NUMBER() OVER(ORDER BY a.object_id) - 1, @datefrom)
                        --FROM sys.all_objects a CROSS JOIN sys.all_objects b

                        --delete from #date where dbo.[CheckTodayIsHoliday](valuedate) = 1

                        --DECLARE Z CURSOR FOR  

                        --select distinct valuedate from #date

                        --OPEN Z 
                        --FETCH NEXT FROM Z 
                        --INTO @Date

                        --While @@Fetch_Status  = 0 
                        --BEGIN





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
                            select F.FundPK,isnull(E.DepartmentPK,0),isnull(C.AgentPK,0),MIFeeAmount from BRIDGE.dbo.STG_TRevenue A
                            left join BRIDGE.dbo.STG_TBranch B on A.BranchName = B.BranchCode
                            left join Agent C on A.IDAgent = C.ID and C.Status = 2
                            left join Department E on B.BranchCode = E.ID and E.Status = 2
                            left join BRIDGE.dbo.STG_TProductMap D on A.ProductCode = D.ProductCode
                            left join Fund F on D.RadsoftCode = F.ID and F.Status = 2 
	                        where AsOfDate between @DateFrom and @DateTo
	                        OPEN A 
	                        FETCH NEXT FROM A 
	                        INTO @FundPK,@DepartmentPK,@AgentPK,@ManagementFeeAmount   
	                        WHILE @@FETCH_STATUS = 0  
	                        BEGIN 


	                        select @InstrumentPK = InstrumentPK From Instrument A 
	                        left join Fund B on A.ID = B.ID and B.Status = 2 where A.Status = 2 and B.FundPK = @FundPK         
 
	                        Select @ARManagementFee = ARManagementFee,@TaxARManagementFee =  TaxARManagementFee,
	                        @ManagementFeeExpense = ManagementFeeExpense, @TaxManagementFeeExpense = TaxManagementFeeExpense  
	                        From AccountingSetup Where Status = 2

	                        set @AutoNo = @AutoNo + 1   

	                        set @ManagementFeeExpenseAmount = @ManagementFeeAmount
	                        set @TaxManagementFeeExpenseAmount = 0.1 * @ManagementFeeExpenseAmount
	                        set @TaxARManagementFeeAmount = 0.02 * @ManagementFeeExpenseAmount
	                        set @ARManagementFeeAmount = (@ManagementFeeAmount + @TaxManagementFeeExpenseAmount) - @TaxARManagementFeeAmount

	                        INSERT INTO [JournalDetail]  
	                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	                        ,[BaseCredit],[LastUsersID],LastUpdate) 
	                        Select @JournalPK,@AutoNo,1,2,@ARManagementFee,1,0,@DepartmentPK,@AgentPK,0,@InstrumentPK,0,'','','D',abs(@ARManagementFeeAmount), abs(@ARManagementFeeAmount),0,1,abs(@ARManagementFeeAmount),0,@UsersID,@TimeNow 
               
			            
	                        set @AutoNo = @AutoNo + 1   


	                        INSERT INTO [JournalDetail]  
	                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	                        ,[BaseCredit],[LastUsersID],LastUpdate) 
	                        Select @JournalPK,@AutoNo,1,2,@TaxARManagementFee,1,0,@DepartmentPK,@AgentPK,0,@InstrumentPK,0,'','','D',abs(@TaxARManagementFeeAmount), abs(@TaxARManagementFeeAmount),0,1,abs(@TaxARManagementFeeAmount),0,@UsersID,@TimeNow 
               
			            
	                        set @AutoNo = @AutoNo + 1   

	                        INSERT INTO [JournalDetail]  
	                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	                        ,[BaseCredit],[LastUsersID],LastUpdate) 
	                        Select @JournalPK,@AutoNo,1,2,@ManagementFeeExpense,1,0,@DepartmentPK,@AgentPK,0,@InstrumentPK,0,'','','C',abs(@ManagementFeeExpenseAmount), 0,abs(@ManagementFeeExpenseAmount),1,0,abs(@ManagementFeeExpenseAmount),@UsersID,@TimeNow 

	                        set @AutoNo = @AutoNo + 1   

	                        INSERT INTO [JournalDetail]  
	                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	                        ,[BaseCredit],[LastUsersID],LastUpdate) 
	                        Select @JournalPK,@AutoNo,1,2,@TaxManagementFeeExpense,1,0,@DepartmentPK,@AgentPK,0,@InstrumentPK,0,'','','C',abs(@TaxManagementFeeExpenseAmount), 0,abs(@TaxManagementFeeExpenseAmount),1,0,abs(@TaxManagementFeeExpenseAmount),@UsersID,@TimeNow 
     
                        
	                        FETCH NEXT FROM A 
	                        INTO @FundPK,@DepartmentPK,@AgentPK,@ManagementFeeAmount  
	                        END 
	                        CLOSE A  
	                        DEALLOCATE A




                        --FETCH NEXT FROM Z 
                        --INTO @Date
                        --END 
                        --CLOSE Z  

                        DECLARE @combinedString NVARCHAR(500)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Reference
                        FROM #ReferenceTemp
                        IF (@combinedString is null)
                        BEGIN
                            SELECT 'No Data Retrieve, Please Check TRevenue' as Result
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

        public List<RetrieveUnitRegistry> Retrieve_SelectUnitRegistry(DateTime _date, string _usersID)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RetrieveUnitRegistry> L_RetrieveUnitRegistry = new List<RetrieveUnitRegistry>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"   
                        select B.Name FundName,* from RetrieveFromBridge A left join Fund B on A.FundID = B.ID and B.status = 2
                        where Date = @Date
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    RetrieveUnitRegistry M_RetrieveUnitRegistry = new RetrieveUnitRegistry();
                                    M_RetrieveUnitRegistry.RetrieveFromBridgePK = Convert.ToInt32(dr["RetrieveFromBridgePK"]);
                                    M_RetrieveUnitRegistry.FundID = Convert.ToString(dr["FundID"]);
                                    M_RetrieveUnitRegistry.FundName = Convert.ToString(dr["FundName"]);
                                    M_RetrieveUnitRegistry.SUBAmount = Convert.ToDecimal(dr["SUBAmount"]);
                                    M_RetrieveUnitRegistry.SUBFeeAmount = Convert.ToDecimal(dr["SUBFeeAmount"]);
                                    M_RetrieveUnitRegistry.SWIInAmount = Convert.ToDecimal(dr["SWIInAmount"]);
                                    M_RetrieveUnitRegistry.SWIInFeeAmount = Convert.ToDecimal(dr["SWIInFeeAmount"]);
                                    M_RetrieveUnitRegistry.REDAmount = Convert.ToDecimal(dr["REDAmount"]);
                                    M_RetrieveUnitRegistry.REDFeeAmount = Convert.ToDecimal(dr["REDFeeAmount"]);
                                    //M_RetrieveUnitRegistry.REDUnit = Convert.ToDecimal(dr["REDUnit"]);
                                    //M_RetrieveUnitRegistry.REDFeeUnit = Convert.ToDecimal(dr["REDFeeUnit"]);
                                    M_RetrieveUnitRegistry.REDPaymentDate = Convert.ToString(dr["REDPaymentDate"]);
                                    M_RetrieveUnitRegistry.SWIOutAmount = Convert.ToDecimal(dr["SWIOutAmount"]);
                                    //M_RetrieveUnitRegistry.SWIOutFeeAmount = Convert.ToDecimal(dr["SWIOutFeeAmount"]);
                                    //M_RetrieveUnitRegistry.SWIOutUnit = Convert.ToDecimal(dr["SWIOutUnit"]);
                                    //M_RetrieveUnitRegistry.SWIOutFeeUnit = Convert.ToDecimal(dr["SWIOutFeeUnit"]);
                                    M_RetrieveUnitRegistry.SWIOutPaymentDate = Convert.ToString(dr["SWIOutPaymentDate"]);
      
                                    L_RetrieveUnitRegistry.Add(M_RetrieveUnitRegistry);

                                }

                            }
                            return L_RetrieveUnitRegistry;
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

        public string Void_UnitRegistry(DateTime _date, string _usersID)
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
                      
                        Declare @counterDate datetime 
                        DECLARE @Description nvarchar(100)
                        DECLARE @UnitAmount numeric(22,8)
                        DECLARE @FundPK int,@SUBS numeric(22,8),@RED numeric(22,8),@RedPaymentDate datetime,@SWIOUT numeric(22,8),@SWIIN numeric(22,8),@SWIOutPaymentDate datetime

                        CREATE TABLE #A (FundPK int,SUBS numeric(22,8),RED numeric(22,8),RedPaymentDate datetime,SWIOUT numeric(22,8),SWIIN numeric(22,8),SWIOutPaymentDate datetime)

                        insert into #A (FundPK,SUBS,RED,RedPaymentDate,SWIOUT,SWIIN,SWIOutPaymentDate)
                        select FundPK,isnull(sum(SUBAmount),0) SUBS,isnull(sum(REDAmount + RedAmountByUnit),0) * -1 RED,REDPaymentDate,isnull(sum(SWIOutAmount + SWIOutAmountByUnit),0) *-1 SWIOUT,isnull(sum(SWIINAmount),0) SWIIN,SWIOutPaymentDate from (
                        select B.FundPK,sum(SUBAmount) SUBAmount,sum(REDAmount) REDAmount,sum(dbo.FgetLastCloseNav(@date,B.FundPK) * REDUnit) RedAmountByUnit,REDPaymentDate,sum(SWIOutAmount) SWIOutAmount,
                        sum(dbo.FgetLastCloseNav(@date,B.FundPK) * SWIOutUnit) SWIOutAmountByUnit,sum(SWIInAmount) SWIInAmount,SWIOutPaymentDate from RetrieveFromBridge A
                        left join Fund B on A.FundID = B.ID and B.Status = 2
                        where A.Date = @date
                        group by B.FundPK,A.REDPaymentDate, A.SWIOutPaymentDate
                        ) A
                        group by A.FundPK,A.REDPaymentDate, A.SWIOutPaymentDate

                        DECLARE A CURSOR FOR 
                        select FundPK,SUBS,RED,RedPaymentDate,SWIOUT,SWIIN,SWIOutPaymentDate from #A
                        Open A
                        Fetch Next From A
                        Into @FundPK,@SUBS,@RED,@RedPaymentDate,@SWIOUT,@SWIIN,@SWIOutPaymentDate

                        While @@FETCH_STATUS = 0
                        Begin

                        select @Description = 'RFB' + REPLACE(CONVERT(VARCHAR(10), @date, 103), '/', '')

                        -- FUND JOURNAL
                        update FundJournal set status = 3,VoidUsersID = @UsersID, VoidTime = @TimeNow where Description = @Description

                        -- FUND CLIENT POSITION
                        set @UnitAmount = 0
                        select @UnitAmount = round(cast(sum(SUBS + RED + SWIOUT + SWIIN)/cast(dbo.FgetLastCloseNav(@date,FundPK) as numeric(19,4)) as numeric(19,4)),4) from #A where FundPK = @FundPK
                        group by FundPK

                        if @@rowCount > 0 
                        begin 
                        Update FundClientPosition set UnitAmount = UnitAmount - @UnitAmount where Date = @Date and FundClientPK = 1 and FundPK = @FundPK 
                        end 

         
                        set @counterDate = @Date 
                        while @counterDate <= (select max(date) from fundClientPosition where FundClientPK = 1 and FundPK = @FundPK) 
                        BEGIN
                        set @counterDate = dbo.fworkingday(@counterDate,1)
                        update fundClientPosition set UnitAmount = UnitAmount - @UnitAmount
                        where FundClientPK = 1 and FundPK = @FundPK and Date = @counterDate end


                        Fetch next From A 
                        Into @FundPK,@SUBS,@RED,@RedPaymentDate,@SWIOUT,@SWIIN,@SWIOutPaymentDate
                        end
                        Close A
                        Deallocate A

                        SELECT 'Void Unit Registry Success !' as Result

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


        public string Posting_ManagementFee(string _usersID, DateTime _dateFrom,DateTime _dateTo, bool _includeTax)
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
                        create table #ReferenceTemp
                        (Reference nvarchar(50))

                        declare @AsOfDate datetime
                        declare @FundPK int
                        declare @InstrumentPK int 
                        declare @DepartmentPK int
                        declare @AgentPK int
    


                        Declare @ARManagementFee int 
                        Declare @TaxARManagementFee int 
                        Declare @ManagementFeeExpense int 
                        Declare @TaxManagementFeeExpense int 


                        declare @ManagementFeeAmount numeric(22,6)

                        declare @ARManagementFeeAmount numeric(22,6)
                        declare @TaxARManagementFeeAmount numeric(22,6)
                        declare @ManagementFeeExpenseAmount numeric(22,6)
                        declare @TaxManagementFeeExpenseAmount numeric(22,6)





                        Declare @JourHeader int  
                        set @JourHeader = 0    
                        Declare @JournalPK int 
                        Declare @PeriodPK int 
                        Declare @Reference nvarchar(50)    


                        Select @PeriodPK = PeriodPK From Period Where DateFrom <= @DateFrom and DateTo >= @DateTo and Status = 2  
                        Select @JournalPK = isnull(Max(JournalPK),0) from Journal   

                        IF EXISTS( select F.FundPK,isnull(E.DepartmentPK,0),isnull(C.AgentPK,0),MIFeeAmount from BRIDGE.dbo.STG_TRevenue A
                        left join BRIDGE.dbo.STG_TBranch B on A.BranchName = B.BranchCode
                        left join Agent C on A.IDAgent = C.ID and C.Status = 2
                        left join Department E on B.BranchCode = E.ID and E.Status = 2
                        left join BRIDGE.dbo.STG_TProductMap D on A.ProductCode = D.ProductCode
                        left join Fund F on D.RadsoftCode = F.ID and F.Status = 2 
                        where AsOfDate between @DateFrom and @DateTo
                        )
                        BEGIN

                        insert into #ReferenceTemp (Reference)
                        exec getJournalReference @DateTo,'ADJ',@Reference out  
 
                        set @JournalPK = @JournalPK + 1   
                        INSERT INTO [Journal]  
                        ([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate]  
                        ,[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy]  
                        ,[PostedTime],[EntryUsersID],[EntryTime]  
                        ,[ApprovedUsersID],[ApprovedTime],[DBUserID],LastUpdate)     
                        SELECT @JournalPK,1,2,'Management Fee',@PeriodPK, @DateTo
                        ,0,'',@Reference,1,'Management Fee',1,@UsersID
                        ,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow 


                        Declare @AutoNo int 
                        set @AutoNo = 0   
                        DECLARE A CURSOR FOR 
                            select A.AsOfDate,F.FundPK,isnull(E.DepartmentPK,0),isnull(C.AgentPK,0),MIFeeAmount from BRIDGE.dbo.STG_TRevenue A
                            left join BRIDGE.dbo.STG_TBranch B on A.BranchName = B.BranchCode
                            left join Agent C on A.IDAgent = C.ID and C.Status = 2
                            left join Department E on B.BranchCode = E.ID and E.Status = 2
                            left join BRIDGE.dbo.STG_TProductMap D on A.ProductCode = D.ProductCode
                            left join Fund F on D.RadsoftCode = F.ID and F.Status = 2 
	                        where AsOfDate between @DateFrom and @DateTo
	                        OPEN A 
	                        FETCH NEXT FROM A 
	                        INTO @AsOfDate,@FundPK,@DepartmentPK,@AgentPK,@ManagementFeeAmount   
	                        WHILE @@FETCH_STATUS = 0  
	                        BEGIN 


	                        select @InstrumentPK = InstrumentPK From Instrument A 
	                        left join Fund B on A.ID = B.ID and B.Status = 2 where A.Status = 2 and B.FundPK = @FundPK         
 
	                        Select @ARManagementFee = ARManagementFee,@TaxARManagementFee =  TaxARManagementFee,
	                        @ManagementFeeExpense = ManagementFeeExpense, @TaxManagementFeeExpense = TaxManagementFeeExpense  
	                        From AccountingSetup Where Status = 2

	                        set @AutoNo = @AutoNo + 1   

	                        set @ManagementFeeExpenseAmount = @ManagementFeeAmount
                            set @TaxARManagementFeeAmount = 0
	                        set @TaxManagementFeeExpenseAmount = 0.1 * @ManagementFeeExpenseAmount
                            IF(@IncludeTax = 1)
                            BEGIN
                                 set @TaxARManagementFeeAmount = 0.02 * @ManagementFeeExpenseAmount
                            END

	                   
	                        set @ARManagementFeeAmount = (@ManagementFeeAmount + @TaxManagementFeeExpenseAmount) - @TaxARManagementFeeAmount



	                        INSERT INTO [JournalDetail]  
	                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	                        ,[BaseCredit],[LastUsersID],LastUpdate) 
	                        Select @JournalPK,@AutoNo,1,2,@ARManagementFee,1,0,@DepartmentPK,@AgentPK,0,@InstrumentPK,0,'','','D',abs(@ARManagementFeeAmount), abs(@ARManagementFeeAmount),0,1,abs(@ARManagementFeeAmount),0,@UsersID,@TimeNow 
               
			            
	                        set @AutoNo = @AutoNo + 1   


	                        INSERT INTO [JournalDetail]  
	                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	                        ,[BaseCredit],[LastUsersID],LastUpdate) 
	                        Select @JournalPK,@AutoNo,1,2,@TaxARManagementFee,1,0,3,0,0,@InstrumentPK,0,'','','D',abs(@TaxARManagementFeeAmount), abs(@TaxARManagementFeeAmount),0,1,abs(@TaxARManagementFeeAmount),0,@UsersID,@TimeNow 
               
			            
	                        set @AutoNo = @AutoNo + 1   

	                        INSERT INTO [JournalDetail]  
	                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	                        ,[BaseCredit],[LastUsersID],LastUpdate) 
	                        Select @JournalPK,@AutoNo,1,2,@ManagementFeeExpense,1,0,@DepartmentPK,@AgentPK,0,@InstrumentPK,0,'','','C',abs(@ManagementFeeExpenseAmount), 0,abs(@ManagementFeeExpenseAmount),1,0,abs(@ManagementFeeExpenseAmount),@UsersID,@TimeNow 

	                        set @AutoNo = @AutoNo + 1   

	                        INSERT INTO [JournalDetail]  
	                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
	                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
	                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
	                        ,[BaseCredit],[LastUsersID],LastUpdate) 
	                        Select @JournalPK,@AutoNo,1,2,@TaxManagementFeeExpense,1,0,3,0,0,@InstrumentPK,0,'','','C',abs(@TaxManagementFeeExpenseAmount), 0,abs(@TaxManagementFeeExpenseAmount),1,0,abs(@TaxManagementFeeExpenseAmount),@UsersID,@TimeNow 
     
                        
	                        FETCH NEXT FROM A 
	                        INTO @AsOfDate,@FundPK,@DepartmentPK,@AgentPK,@ManagementFeeAmount  
	                        END 
	                        CLOSE A  
	                        DEALLOCATE A

                        END


                        DECLARE @combinedString NVARCHAR(500)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Reference
                        FROM #ReferenceTemp
                        IF (@combinedString is null)
                        BEGIN
                            SELECT 'No Data Retrieve, Please Check Fund Daily Fee' as Result
                        END
                        ELSE
                        BEGIN
                        SELECT 'Posting Management Fee Success ! Reference is : ' + @combinedString as Result
                        END
                      

                        ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@IncludeTax", _includeTax);
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



        public string RetrieveFromBridge_Generate(DateTime _date, string _usersID, RetrieveFromBridge _retrieve)
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

                        if (!_host.findString(_retrieve.RetrieveFundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_retrieve.RetrieveFundFrom))
                        {
                            _paramFund = "And D.FundPK in ( " + _retrieve.RetrieveFundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandText = @"
                        Declare @RetrieveFromBridgePK int
                        Declare @FundID nvarchar(100)
                        Declare @SUBAmount numeric(18,2),@SUBFeeAmount numeric(18,2),@SWIInAmount numeric(18,2),@SWIInFeeAmount numeric(18,2)
                        Declare @REDAmount numeric(18,2),@REDFeeAmount numeric(18,2),@SWIOutAmount numeric(18,2)
                        Declare @REDPaymentDate datetime,@SWIOutPaymentDate datetime

                        Delete A from RetrieveFromBridge A left join Fund D on A.FundID = D.ID and status = 2 
                        where A.Date = @Date " + _paramFund + @" 

                        --update BRIDGE.dbo.STG_SIARTransact set ProductCode = 'MNCICON' where ProductCode = 'ICON' and TradeDate = @Date
      
                        Declare A Cursor For   

                        select D.ID FundID,sum(SUBAmount) SUBAmount,sum(SUBFeeAmount) SUBFeeAmount,sum(SWIInAmount) SWIInAmount,sum(SWIInFeeAmount) SWIInFeeAmount,
                        sum(REDAmount  +  (REDUnit * (dbo.FgetLastCloseNav(@date,FundPK))) ) REDAmount,sum(RedFeeAmount) RedFeeAmount,REDPaymentDate,
                        sum(SWIOutAmount + (SWIOutUnit * (dbo.FgetLastCloseNav(@date,FundPK))) ) SWIOutAmount,
                        case when SWIOutPaymentDate = @Date then dbo.fworkingday(@Date,4) else SWIOutPaymentDate end from BRIDGE.dbo.STG_SIARTransact A 
                        left join BRIDGE.dbo.STG_TProductMap C on A.ProductCode = C.ProductCode
                        left join Fund D on C.RadsoftCode = D.ID and D.Status = 2 
                        where TradeDate = @Date " + _paramFund + @"
                        and D.ID is not null  
                        group by TradeDate,D.ID,REDPaymentDate,SWIOutPaymentDate

                        Open A                  
                        Fetch Next From A                  
                        Into @FundID,@SUBAmount,@SUBFeeAmount,@SWIInAmount,@SWIInFeeAmount,@REDAmount,
                        @REDFeeAmount,@REDPaymentDate,@SWIOutAmount,@SWIOutPaymentDate

                        While @@FETCH_STATUS = 0                  
                        Begin 

                        select @RetrieveFromBridgePK = isnull(max(RetrieveFromBridgePK), 0) + 1 from RetrieveFromBridge 

                        INSERT INTO [dbo].[RetrieveFromBridge]
                                    ([RetrieveFromBridgePK]
                                    ,[Date]
                                    ,[FundID]
                                    ,[SUBAmount]
                                    ,[SUBFeeAmount]
                                    ,[SWIInAmount]
                                    ,[SWIInFeeAmount]
                                    ,[REDAmount]
                                    ,[REDFeeAmount]
                                    ,[REDPaymentDate]
                                    ,[SWIOutAmount]
                                    ,[SWIOutPaymentDate])

                        select @RetrieveFromBridgePK,@Date,@FundID,@SUBAmount,@SUBFeeAmount,@SWIInAmount,@SWIInFeeAmount,@REDAmount,
                        @REDFeeAmount,@REDPaymentDate,@SWIOutAmount,@SWIOutPaymentDate

                        Fetch next From A                   
                        Into @FundID,@SUBAmount,@SUBFeeAmount,@SWIInAmount,@SWIInFeeAmount,@REDAmount,
                        @REDFeeAmount,@REDPaymentDate,@SWIOutAmount,@SWIOutPaymentDate
                        END                  
                        Close A                  
                        Deallocate A
                        select 'Retrieve From Bridge Success' Result
                        ";

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


        public int Update_UnitRegistry(RetrieveFromBridge _retrieveFromBridge)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Update A set SUBAmount= @SUBAmount,SUBFeeAmount= @SUBFeeAmount,SWIInAmount= @SWIInAmount,SWIInFeeAmount= @SWIInFeeAmount,
                        REDAmount= @REDAmount,REDFeeAmount= @REDFeeAmount,REDPaymentDate= @REDPaymentDate,
                        SWIOutAmount= isnull(@SWIOutAmount,0),SWIOutPaymentDate= @SWIOutPaymentDate from RetrieveFromBridge A
                        left join Fund B on A.FundID = B.ID and B.status = 2
                        where RetrieveFromBridgePK = @RetrieveFromBridgePK ";

                        cmd.Parameters.AddWithValue("@RetrieveFromBridgePK", _retrieveFromBridge.RetrieveFromBridgePK);
                        cmd.Parameters.AddWithValue("@SUBAmount", _retrieveFromBridge.SUBAmount);
                        cmd.Parameters.AddWithValue("@SUBFeeAmount", _retrieveFromBridge.SUBFeeAmount);
                        cmd.Parameters.AddWithValue("@SWIInAmount", _retrieveFromBridge.SWIInAmount);
                        cmd.Parameters.AddWithValue("@SWIInFeeAmount", _retrieveFromBridge.SWIInFeeAmount);

                        cmd.Parameters.AddWithValue("@REDAmount", _retrieveFromBridge.REDAmount);
                        cmd.Parameters.AddWithValue("@REDFeeAmount", _retrieveFromBridge.REDFeeAmount);
                        if (_retrieveFromBridge.REDPaymentDate == "" || _retrieveFromBridge.REDPaymentDate == null)
                        {
                            cmd.Parameters.AddWithValue("@REDPaymentDate", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@REDPaymentDate", _retrieveFromBridge.REDPaymentDate);
                        }

                        cmd.Parameters.AddWithValue("@SWIOutAmount", _retrieveFromBridge.SWIOutAmount);

                        if (_retrieveFromBridge.SWIOutPaymentDate == "" || _retrieveFromBridge.SWIOutPaymentDate == null)
                        {
                            cmd.Parameters.AddWithValue("@SWIOutPaymentDate", "");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@SWIOutPaymentDate", _retrieveFromBridge.SWIOutPaymentDate);
                        }

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



    }
}