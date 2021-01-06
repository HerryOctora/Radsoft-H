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
using System.Drawing;
using OfficeOpenXml.Drawing;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;
using SucorInvest.Connect;


namespace RFSRepository
{
    public class UploadUnitAllocationReps
    {
        #region UnitAllocation
        public string ImportUnitAllocation(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table UnitAllocationTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.UnitAllocationTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromUnitAllocation(_fileSource));
                        }

                        //logic kalo Reconcile success

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                              @"

                                declare @Message nvarchar(max)

                                if Exists( Select * from [UnitAllocationTemp] 
                                where ISNUMERIC(unit) = 0 OR ISNUMERIC(FeeAmount) = 0 OR ISNUMERIC(Amount) = 0)
                                BEGIN

									select distinct @Message = COALESCE(@message + ', ', '') + [No] from [UnitAllocationTemp] 
									where ISNUMERIC(unit) = 0 OR ISNUMERIC(FeeAmount) = 0 OR ISNUMERIC(Amount) = 0

									Select top 1  'false' result,'Wrong Number format Amount or Unit For No: ' + @Message ResultDesc

                                    delete UnitAllocationTemp
									return
                                END


                                if Exists( Select * from [UnitAllocationTemp] 
                                where ISDATE(Date) = 0 )
                                BEGIN

									select distinct @Message = COALESCE(@message + ', ', '') + [No] from [UnitAllocationTemp] 
									where ISDATE(Date) = 0

									Select top 1  'false' result,'Format Date is wrong For No: ' + @Message ResultDesc

                                    delete UnitAllocationTemp
									return
                                END


                                if Exists( Select * from [UnitAllocationTemp] 
                                where FundClientID not in
                                (
									select ID from FundClient where status in (1,2)
                                ))
                                BEGIN

									select distinct @Message = COALESCE(@message + ', ', '') + FundClientID  from [UnitAllocationTemp] 
									where FundClientID not in
									(
									select ID from FundClient where status in (1,2)
									)

									Select top 1  'false' result,'No Insurance Product For data: ' + @Message ResultDesc

                                    delete UnitAllocationTemp
									return
                                END


                                if Exists( Select * from [UnitAllocationTemp] 
                                where FundFrom not in
                                (
									select ID from Fund where status in (1,2)
                                ))
                                BEGIN

									select distinct @Message = COALESCE(@message + ', ', '') + FundFrom from [UnitAllocationTemp] 
									where FundFrom not in
									(
										select ID from Fund where status in (1,2)
									)

									Select top 1  'false' result,'No Fund For data: ' + @Message ResultDesc

                                    delete UnitAllocationTemp
									return
                                END


                                if Exists( Select * from [UnitAllocationTemp] 
                                where ISNULL(Date,'') = '' )
                                BEGIN

									select distinct @Message = COALESCE(@message + ', ', '') + [No] from [UnitAllocationTemp] 
									where ISNULL(Date,'') = '' 

									Select top 1  'false' result,'No Date For No: ' + @Message ResultDesc
									return
                                END

                                if Exists( Select * from [UnitAllocationTemp] 
                                where ISNULL(Date,'') = '' )
                                BEGIN

									select distinct @Message = COALESCE(@message + ', ', '') +' , ' + [No] from [UnitAllocationTemp] 
									where ISNULL(Date,'') = '' 

									Select top 1  'false' result,'No Date For No: ' + @Message ResultDesc

                                    delete UnitAllocationTemp
									return
                                END



                                SELECT 'true' result,'' ResultDesc

                                ";
                                using (SqlDataReader dr = cmd1.ExecuteReader())
                                {
                                    dr.Read();
                                    if (Convert.ToString(dr["Result"]) == "false")
                                    {
                                        _msg = Convert.ToString(dr["ResultDesc"]);
                                        return _msg;
                                    }
                                    else
                                    {
                                        conn.Close();
                                        conn.Open();
                                        using (SqlCommand cmd2 = conn.CreateCommand())
                                        {
                                            cmd2.CommandText =
                                            @"       
                                            declare @CloseNAV table
                                            (
	                                            NAVDate date,
	                                            FundPK int,
	                                            Nav numeric(22,4)
                                            )

                                            declare @TableFundCashRef table
                                            (
	                                            Fundpk int,
	                                            CashRefPK int,
	                                            Type int
                                            )

                                            insert into @TableFundCashRef
                                            select FundPK,FundCashRefPK,Type from FundCashRef where status = 2

                                            DECLARE @PK INT
                                            DECLARE @No VARCHAR(MAX)
                                            DECLARE @Type VARCHAR(MAX)
                                            DECLARE @Amount NUMERIC(32,8)
                                            DECLARE @Unit NUMERIC(32,8)
                                            DECLARE @Date date
                                            DECLARE @CashRefPK int
                                            DECLARE @FundPK int 

                                            DECLARE A CURSOR FOR 
	                                            select No,Type,Amount,Unit,Date from UnitAllocationTemp
                                            OPEN A
 
                                            FETCH NEXT FROM A INTO @No, @Type, @Amount, @Unit, @Date
    
                                            WHILE @@FETCH_STATUS = 0
                                                BEGIN
		                                            delete @CloseNAV

		                                            insert into @CloseNAV
                                                    select maxdate,A.fundpk,B.NAV from (
                                                    select max(date) maxdate,FundPK from CloseNAV 
                                                    where status = 2 and date <= @Date
		                                            group by FundPK
                                                    )A
                                                    left join CloseNAV B on A.FundPK = B.FundPK and A.maxdate = B.Date and b.status = 2

		                                            declare @MaxEDT datetime
                                                    Select @MaxEDT = max(Valuedate) from EndDayTrails where status = 2 and ValueDate <= @Date
		                                            set @MaxEDT = dbo.FWorkingDay(@MaxEDT,1)

		                                            DECLARE @Yesterday DATE
		                                            set @Yesterday = dbo.FWorkingDay(@Date,-1)
		  
		                                            DECLARE @ParamUnitRedemptionAll numeric(22,4)
		                                            set @ParamUnitRedemptionAll = 10000

		                                            if @Type = 'SUB'
		                                            begin
			                                            if @Amount > 0
			                                            begin
				                                            SELECT @PK = isnull(MAX(ClientSubscriptionPK),0) FROM dbo.ClientSubscription

				                                            INSERT INTO [dbo].[ClientSubscription]    
				                                            ([ClientSubscriptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate],   
				                                            [NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Description],[CashAmount],[UnitAmount],[TotalCashAmount],[TotalUnitAmount],   
				                                            [SubscriptionFeePercent],[SubscriptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[Type]
				                                            ,[AutoDebitDate],[ReferenceSInvest],[EntryUsersID],[EntryTime],[LastUpdate],TransactionPromoPK,Notes)   

				                                            SELECT @PK + ROW_NUMBER() OVER	(ORDER BY B.FundClientPK ASC) 
				                                            ,1,1,CONVERT(DATE,A.Date),case when NAVFrom = 0 then null else CONVERT(DATE,A.Date) end,A.NAVFrom,C.FundPK,B.FundClientPK,[dbo].[FgetFundCashRefPKByFundPK](C.FundPK)
				                                            ,C.CurrencyPK,'UPLOAD UNIT ALLOCATION',CAST(cast(A.Amount as numeric(22,8)) AS NUMERIC(24,4)),0,CAST(cast(A.Amount as numeric(22,8)) AS NUMERIC(24,4)),0
				                                            ,0,CAST(A.FeeAmount AS NUMERIC(24,4)),0,0,0,1,0,'',@UsersID,@LastUpdate,@LastUpdate,0,A.Notes
				                                            FROM dbo.UnitAllocationTemp A
				                                            INNER JOIN FundClient B ON A.FundClientID = B.ID AND B.status IN (1,2)
				                                            INNER JOIN Fund C ON A.FundFrom = C.ID AND C.status IN (1,2)
				                                            WHERE NO = @No
			                                            end

			                                            if @Unit > 0
			                                            begin
				                                            SELECT @PK = isnull(MAX(ClientSubscriptionPK),0) FROM dbo.ClientSubscription

				                                            INSERT INTO [dbo].[ClientSubscription]    
				                                            ([ClientSubscriptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate],   
				                                            [NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Description],[CashAmount],[UnitAmount],[TotalCashAmount],[TotalUnitAmount],   
				                                            [SubscriptionFeePercent],[SubscriptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[Type]
				                                            ,[AutoDebitDate],[ReferenceSInvest],[EntryUsersID],[EntryTime],[LastUpdate],TransactionPromoPK,Notes)

				                                            SELECT @PK + ROW_NUMBER() OVER	(ORDER BY B.FundClientPK ASC) 
				                                            ,1,1,CONVERT(DATE,A.Date),CONVERT(DATE,A.Date),A.NAVFrom,C.FundPK,B.FundClientPK,[dbo].[FgetFundCashRefPKByFundPK](C.FundPK)
				                                            ,C.CurrencyPK,'UPLOAD UNIT ALLOCATION',0,CAST(A.Unit AS NUMERIC(24,4)),0,CAST(A.Unit AS NUMERIC(24,4))
				                                            ,0,CAST(A.FeeAmount AS NUMERIC(24,4)),0,0,0,1,0,'',@UsersID,@LastUpdate,@LastUpdate,0,A.Notes
				                                            FROM dbo.UnitAllocationTemp A
				                                            INNER JOIN FundClient B ON A.FundClientID = B.ID AND B.status IN (1,2)
				                                            INNER JOIN Fund C ON A.FundFrom = C.ID AND C.status IN (1,2)
				                                            WHERE NO = @No
			                                            end
			
		                                            end
		                                            else if @Type = 'RED'
		                                            begin
			                                            select @FundPK = B.FundPK from UnitAllocationTemp A 
			                                            left join Fund B on A.FundFrom = B.ID and B.status = 2
			                                            where A.No = @No

			                                            select @CashRefPK = isnull(case when (select CashRefPK from @TableFundCashRef where Type = 2 and FundPK = @FundPK ) is null 
			                                            then (select CashRefPK from @TableFundCashRef where Type = 3 and FundPK = @FundPK)
			                                            else (select CashRefPK from @TableFundCashRef where Type = 2 and FundPK = @FundPK ) end,0)

			                                            if @Amount > 0
			                                            begin

                                                            select @PK = isnull(max(ClientRedemptionPK),0) from ClientRedemption
					
                                                            INSERT INTO [dbo].[ClientRedemption] 
                                                                        ([ClientRedemptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate],[PaymentDate],[Notes],
                                                                        [NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Type],[BitRedemptionAll],[Description],[CashAmount],[UnitAmount],[TotalCashAmount],[TotalUnitAmount],
                                                                        [RedemptionFeePercent],[RedemptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[UnitPosition],[BankRecipientPK],[TransferType],[FeeType],[ReferenceSInvest],
			                                                            [IsBOTransaction],[EntryUsersID],[EntryTime],[LastUpdate])

                                                            select ROW_NUMBER() over (order by A.No) + @PK ClientRedemptionPK,1 HistoryPK,1 Status,A.Date NAVDate,A.Date ValueDate,dbo.FWorkingDay(A.Date,B.DefaultPaymentDate) PaymentDate,A.Notes,
                                                            cast(A.NAVFrom as numeric(22,8)) NAV,K.fundpk,L.fundclientpk,@CashRefPK cashrefpk,isnull(D.CurrencyPK,0) CurrencyPK,1 Type,case when I.FundPK is null then 0 when (A.Unit* cast(A.NAVFrom as numeric(22,8))) <= @ParamUnitRedemptionAll then 1 else 0 end BitRedemptionAll,'UPLOAD UNIT ALLOCATION' Description,@Amount CashAmount,0 UnitAmount,@Amount TotalCashAmount,0 TotalunitAmount,
                                                            0 RedemptionFeePercent,A.FeeAmount RedemptionFeeAmount,C.SellingAgentPK Agentpk,0 AgentFeePercent,0 AgentFeeAmount,0 UnitPosition,
                                                            case when F.BankRecipientPK is not null then F.BankRecipientPK when G1.BankPK is not null then 1 when G2.BankPK is not null then 2 when G3.BankPk is not null then 3 else isnull(G4.NoBank,0) end BankRecipientPK, 
                                                            0 TransferType,1 FeeType, '' ReferenceSInvest,
                                                            1 IsBOTransaction,@UsersID EntryUsersID,@LastUpdate EntryTime,@LastUpdate LastUpdate
                                                            from UnitAllocationTemp A
				                                            left join Fund K on A.FundFrom = K.ID and K.status = 2
				                                            left join FundClient L on A.FundClientID = L.ID and L.status = 2
                                                            left join Fund B on K.FundPk = B.FundPK and B.status in (1,2)
                                                            left join FundClient C on L.FundClientpk = C.FundClientPK and C.status in (1,2)
                                                            left join FundCashRef D on @CashRefPK = D.FundCashRefPK and D.status in (1,2)
                
                                                            left join fundclientbankDefault F on K.FundPk = F.FundPK and L.FundClientPK = F.FundClientPK and F.status = 2
                                                            LEFT JOIN BANK G1 ON C.NamaBank1 = G1.BankPK AND G1.Status = 2 
                                                            LEFT JOIN BANK G2 ON C.NamaBank2 = G1.BankPK AND G1.Status = 2
                                                            LEFT JOIN BANK G3 ON C.NamaBank3 = G1.BankPK AND G1.Status = 2
                                                            LEFT JOIN FundClientBankList G4 ON C.NamaBank1 = G1.BankPK AND G1.Status = 2
				                                            LEFT JOIN (
					                                            SELECT DISTINCT FundPK, FundClientPK FROM ClientSubscription WHERE ValueDate in ( @Yesterday, @Date)
				                                            ) I ON K.FundPK = I.FundPK and L.FundClientPK = I.FundClientPK
				                                            LEFT JOIN FundClientPosition J on K.FundPK = J.FundPK and L.FundClientPK = J.FundClientPK and J.Date = @Yesterday
				                                            where A.No = @No
			                                                group by A.No,K.FundPK,L.FundClientPK,A.date,B.DefaultPaymentDate,C.SellingAgentPK,F.BankRecipientPK,G1.BankPK,G2.BankPK,G3.BankPK,G4.NoBank,A.NAVFrom,I.FundPK,J.UnitAmount,A.Unit,A.FeeAmount,A.Notes,D.CurrencyPK
			                                            end

			                                            if @Unit > 0
			                                            begin
                                                            select @PK = isnull(max(ClientRedemptionPK),0) from ClientRedemption
					
                                                            INSERT INTO [dbo].[ClientRedemption] 
                                                                        ([ClientRedemptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate],[PaymentDate],[Notes],
                                                                        [NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Type],[BitRedemptionAll],[Description],[CashAmount],[UnitAmount],[TotalCashAmount],[TotalUnitAmount],
                                                                        [RedemptionFeePercent],[RedemptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[UnitPosition],[BankRecipientPK],[TransferType],[FeeType],[ReferenceSInvest],
			                                                            [IsBOTransaction],[EntryUsersID],[EntryTime],[LastUpdate])

                                                            select ROW_NUMBER() over (order by A.No) + @PK ClientRedemptionPK,1 HistoryPK,1 Status,A.Date NAVDate,A.Date ValueDate,dbo.FWorkingDay(A.Date,B.DefaultPaymentDate) PaymentDate,A.Notes,
                                                            cast(A.NAVFrom as numeric(22,8)) NAV,K.fundpk,L.fundclientpk,@CashRefPK cashrefpk,isnull(D.CurrencyPK,0) CurrencyPK,1 Type,case when I.FundPK is null then 0 when (A.Unit* cast(A.NAVFrom as numeric(22,8))) <= @ParamUnitRedemptionAll then 1 else 0 end BitRedemptionAll,'UPLOAD UNIT ALLOCATION' Description,0 CashAmount,A.Unit UnitAmount,0 TotalCashAmount,A.Unit TotalunitAmount,
                                                            0 RedemptionFeePercent,A.FeeAmount RedemptionFeeAmount,C.SellingAgentPK Agentpk,0 AgentFeePercent,0 AgentFeeAmount,A.Unit UnitPosition,
                                                            case when F.BankRecipientPK is not null then F.BankRecipientPK when G1.BankPK is not null then 1 when G2.BankPK is not null then 2 when G3.BankPk is not null then 3 else isnull(G4.NoBank,0) end BankRecipientPK, 
                                                            CASE WHEN A.Unit * cast(A.NAVFrom as numeric(22,8)) < 100000000 then 1 when A.Unit * cast(A.NAVFrom as numeric(22,8)) >= 100000000 then 2 else 0 end TransferType,1 FeeType, '' ReferenceSInvest,
                                                            1 IsBOTransaction,@UsersID EntryUsersID,@LastUpdate EntryTime,@LastUpdate LastUpdate
                                                            from UnitAllocationTemp A
				                                            left join Fund K on A.FundFrom = K.ID and K.status = 2
				                                            left join FundClient L on A.FundClientID = L.ID and L.status = 2
                                                            left join Fund B on K.FundPk = B.FundPK and B.status in (1,2)
                                                            left join FundClient C on L.FundClientpk = C.FundClientPK and C.status in (1,2)
                                                            left join FundCashRef D on @CashRefPK = D.FundCashRefPK and D.status in (1,2)
                
                                                            left join fundclientbankDefault F on K.FundPk = F.FundPK and L.FundClientPK = F.FundClientPK and F.status = 2
                                                            LEFT JOIN BANK G1 ON C.NamaBank1 = G1.BankPK AND G1.Status = 2 
                                                            LEFT JOIN BANK G2 ON C.NamaBank2 = G1.BankPK AND G1.Status = 2
                                                            LEFT JOIN BANK G3 ON C.NamaBank3 = G1.BankPK AND G1.Status = 2
                                                            LEFT JOIN FundClientBankList G4 ON C.NamaBank1 = G1.BankPK AND G1.Status = 2
				                                            LEFT JOIN (
					                                            SELECT DISTINCT FundPK, FundClientPK FROM ClientSubscription WHERE ValueDate in ( @Yesterday, @Date)
				                                            ) I ON K.FundPK = I.FundPK and L.FundClientPK = I.FundClientPK
				                                            LEFT JOIN FundClientPosition J on K.FundPK = J.FundPK and L.FundClientPK = J.FundClientPK and J.Date = @Yesterday
				                                            where A.No = @No
			                                                group by A.No,K.FundPK,L.FundClientPK,A.date,B.DefaultPaymentDate,C.SellingAgentPK,F.BankRecipientPK,G1.BankPK,G2.BankPK,G3.BankPK,G4.NoBank,A.NAVFrom,I.FundPK,J.UnitAmount,A.Unit,A.FeeAmount,A.Notes,D.CurrencyPK
			                                            end
		                                            end
		                                            else if @Type = 'SWI'
		                                            begin
			                                            select @FundPK = B.FundPK from UnitAllocationTemp A 
			                                            left join Fund B on A.FundFrom = B.ID and B.status = 2
			                                            where A.No = @No

			                                            select @CashRefPK = isnull(case when (select CashRefPK from @TableFundCashRef where Type = 2 and FundPK = @FundPK ) is null 
			                                            then (select CashRefPK from @TableFundCashRef where Type = 3 and FundPK = @FundPK)
			                                            else (select CashRefPK from @TableFundCashRef where Type = 2 and FundPK = @FundPK ) end,0)

			                                            if @Amount > 0
			                                            begin
				                                            select @PK = isnull(max(ClientSwitchingPK),0) from ClientSwitching

				                                            INSERT INTO dbo.ClientSwitching
				                                            ( ClientSwitchingPK ,
				                                            HistoryPK ,
				                                            Selected ,
				                                            Status ,
				                                            Notes ,           FeeType ,FeeTypeMode ,
				                                            NAVDate ,           ValueDate ,
				                                            PaymentDate ,           NAVFundFrom ,
				                                            NAVFundTo ,           FundPKFrom ,
				                                            FundPKTo ,           FundClientPK ,
				                                            CashRefPKFrom ,           CashRefPKTo ,
				                                            TransferType ,           Description ,
				                                            BitSwitchingAll ,           CashAmount ,
				                                            UnitAmount ,           SwitchingFeePercent ,
				                                            SwitchingFeeAmount ,           

				                                            TotalCashAmountFundFrom ,
				                                            TotalCashAmountFundTo ,           
				                                            TotalUnitAmountFundFrom ,
				                                            TotalUnitAmountFundTo ,           

				                                            CurrencyPK ,
				                                            IsBoTransaction ,           BitSinvest ,           FeeTypeMethod ,       
				                                            EntryUsersID ,           EntryTime 
				                                            ,
				                                            IsFrontSync ,           TransactionPK ,
				                                            ReferenceSInvest ,           AgentPK ,
				                                            Type         )
				                                            SELECT  @PK + ROW_NUMBER() OVER(ORDER BY A.Date ASC) ClientSwitchingPK
				                                            ,1,1,1,A.Notes,'OUT',case when a.FeeAmount > 0 then 2 else 1  end FeeType
				                                            ,A.Date,A.Date
				                                            ,dbo.FWorkingDay(A.Date,B.DefaultPaymentDate)
				                                            ,A.NAVFrom,A.NAVTo,B.FundPK
				                                            ,C.FUndPK,D.FundClientPK,[dbo].[FgetFundCashRefPKByFundPK](C.FundPK),@CashRefPK,
				                                            CASE WHEN ISNULL(cast(A.Amount as numeric(22,8)),0) >= 100000001 THEN 2 ELSE 1 END
				                                            ,'UPLOAD UNIT ALLOCATION'
				                                            ,case when I.FundPK is null then 0 when (A.Unit* cast(A.NAVFrom as numeric(22,8))) <= @ParamUnitRedemptionAll then 1 else 0 end BitRedemptionAll
				                                            ,ISNULL(cast(A.Amount as numeric(22,8)),0) ,0 UnitAmount,0
				                                            ,ISNULL(a.FeeAmount,0)
				                                            ,ISNULL(cast(A.Amount as numeric(22,8)),0),ISNULL(cast(A.Amount as numeric(22,8)),0),0,0
				                                            ,1,0,0,
				                                            case when a.FeeAmount > 0 then 2 else 1  END
				                                            ,@UsersID,@LastUpdate,0,'','',0 AgentPK,1
				                                            FROM dbo.UnitAllocationTemp A
				                                            LEFT JOIN Fund B ON A.FundFrom = B.ID AND B.status IN (1,2)
				                                            LEFT JOIN Fund C ON A.FundTo = C.ID AND C.status IN (1,2)
				                                            LEFT JOIN FundClient D ON A.FundClientID = D.ID AND D.status IN (1,2)
				                                            LEFT JOIN (
					                                            SELECT DISTINCT FundPK, FundClientPK FROM ClientSubscription WHERE ValueDate in ( @Yesterday, @Date)
				                                            ) I ON c.FundPK = I.FundPK and D.FundClientPK = I.FundClientPK
				                                            where A.No = @No
			                                            end

			                                            if @Unit > 0
			                                            begin
				                                            select @PK = isnull(max(ClientSwitchingPK),0) from ClientSwitching

				                                            INSERT INTO dbo.ClientSwitching
				                                            ( ClientSwitchingPK ,
				                                            HistoryPK ,
				                                            Selected ,
				                                            Status ,
				                                            Notes ,           FeeType ,FeeTypeMode ,
				                                            NAVDate ,           ValueDate ,
				                                            PaymentDate ,           NAVFundFrom ,
				                                            NAVFundTo ,           FundPKFrom ,
				                                            FundPKTo ,           FundClientPK ,
				                                            CashRefPKFrom ,           CashRefPKTo ,
				                                            TransferType ,           Description ,
				                                            BitSwitchingAll ,           CashAmount ,
				                                            UnitAmount ,           SwitchingFeePercent ,
				                                            SwitchingFeeAmount ,           

				                                            TotalCashAmountFundFrom ,
				                                            TotalCashAmountFundTo ,           
				                                            TotalUnitAmountFundFrom ,
				                                            TotalUnitAmountFundTo ,           

				                                            CurrencyPK ,
				                                            IsBoTransaction ,           BitSinvest ,           FeeTypeMethod ,       
				                                            EntryUsersID ,           EntryTime 
				                                            ,
				                                            IsFrontSync ,           TransactionPK ,
				                                            ReferenceSInvest ,           AgentPK ,
				                                            Type         )
				                                            SELECT  @PK + ROW_NUMBER() OVER(ORDER BY A.Date ASC) ClientSwitchingPK
				                                            ,1,1,1,A.Notes,'OUT',case when a.FeeAmount > 0 then 2 else 1  end FeeType
				                                            ,A.Date,A.Date
				                                            ,dbo.FWorkingDay(A.Date,B.DefaultPaymentDate)
				                                            ,A.NAVFrom,A.NAVTo,B.FundPK
				                                            ,C.FUndPK,D.FundClientPK,[dbo].[FgetFundCashRefPKByFundPK](C.FundPK),@CashRefPK,
				                                            CASE WHEN ISNULL(cast(A.Amount as numeric(22,8)),0) >= 100000001 THEN 2 ELSE 1 END
				                                            ,'UPLOAD UNIT ALLOCATION'
				                                            ,case when I.FundPK is null then 0 when (A.Unit* cast(A.NAVFrom as numeric(22,8))) <= @ParamUnitRedemptionAll then 1 else 0 end BitRedemptionAll
				                                            ,0 ,A.Unit UnitAmount,0
				                                            ,ISNULL(a.FeeAmount,0)
				                                            ,0,0,A.Unit,A.Unit
				                                            ,1,0,0,
				                                            case when a.FeeAmount > 0 then 2 else 1  END
				                                            ,@UsersID,@LastUpdate,0,'','',0 AgentPK,1
				                                            FROM dbo.UnitAllocationTemp A
				                                            LEFT JOIN Fund B ON A.FundFrom = B.ID AND B.status IN (1,2)
				                                            LEFT JOIN Fund C ON A.FundTo = C.ID AND C.status IN (1,2)
				                                            LEFT JOIN FundClient D ON A.FundClientID = D.ID AND D.status IN (1,2)
				                                            LEFT JOIN (
					                                            SELECT DISTINCT FundPK, FundClientPK FROM ClientSubscription WHERE ValueDate in ( @Yesterday, @Date)
				                                            ) I ON c.FundPK = I.FundPK and D.FundClientPK = I.FundClientPK
				                                            where A.No = @No
			                                            end
			
		                                            end
        
		
		                                            FETCH NEXT FROM A INTO @No, @Type, @Amount, @Unit, @Date
                                                END
 
                                            CLOSE A
 
                                            DEALLOCATE A
                                                    ";
                                            cmd2.Parameters.AddWithValue("@UsersID", _userID);
                                            cmd2.Parameters.AddWithValue("@Lastupdate", _now);
                                            cmd2.ExecuteNonQuery();

                                            _msg = "Import unit allocation success";
                                        }

                                    }
                                }
                                return _msg;

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

        private DataTable CreateDataTableFromUnitAllocation(string _fileSource)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "No";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Date";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Fund";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundClient";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Type";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundFrom";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundTo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "Amount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "Unit";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "NAVFrom";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "NAVTo";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "FeeAmount";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Notes";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "LastUpdate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            string _lastupdate = DateTime.Now.ToString();

            FileInfo excelFile = new System.IO.FileInfo(_fileSource);
            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                int i = 2;
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                var end = worksheet.Dimension.End;
                while (i <= end.Row)
                {
                    dr = dt.NewRow();
                    if (worksheet.Cells[i, 1].Value == null)
                        dr["No"] = "";
                    else
                        dr["No"] = worksheet.Cells[i, 1].Value.ToString();

                    string _strTransactionDate = Convert.ToString(worksheet.Cells[i, 2].Value.ToString());
                    if (!string.IsNullOrEmpty(_strTransactionDate))
                    {
                        string _tgl = _strTransactionDate.Substring(6, 2);
                        string _bln = _strTransactionDate.Substring(4, 2);
                        string _thn = _strTransactionDate.Substring(0, 4);

                        _strTransactionDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                    }

                    if (worksheet.Cells[i, 2].Value == null)
                        dr["Date"] = "";
                    else
                        dr["Date"] = _strTransactionDate;

                    if (worksheet.Cells[i, 3].Value == null)
                        dr["Fund"] = "";
                    else
                        dr["Fund"] = worksheet.Cells[i, 3].Value.ToString();

                    if (worksheet.Cells[i, 4].Value == null)
                        dr["FundClient"] = "";
                    else
                        dr["FundClient"] = worksheet.Cells[i, 4].Value.ToString();

                    if (worksheet.Cells[i, 5].Value == null)
                        dr["Type"] = "";
                    else
                        dr["Type"] = worksheet.Cells[i, 5].Value.ToString();

                    if (worksheet.Cells[i, 6].Value == null)
                        dr["FundFrom"] = "";
                    else
                        dr["FundFrom"] = worksheet.Cells[i, 6].Value.ToString();

                    if (worksheet.Cells[i, 7].Value == null)
                        dr["FundTo"] = "";
                    else
                        dr["FundTo"] = worksheet.Cells[i, 7].Value.ToString();

                    if (worksheet.Cells[i, 8].Value == null)
                        dr["Amount"] = "";
                    else
                        dr["Amount"] = Convert.ToDecimal(worksheet.Cells[i, 8].Value);

                    if (worksheet.Cells[i, 9].Value == null)
                        dr["Unit"] = "";
                    else
                        dr["Unit"] = Convert.ToDecimal(worksheet.Cells[i, 9].Value);

                    if (worksheet.Cells[i, 10].Value == null)
                        dr["NAVFrom"] = "";
                    else
                        dr["NAVFrom"] = Convert.ToDecimal(worksheet.Cells[i, 10].Value);

                    if (worksheet.Cells[i, 11].Value == null)
                        dr["NAVTo"] = "";
                    else
                        dr["NAVTo"] = Convert.ToDecimal(worksheet.Cells[i, 11].Value);

                    if (worksheet.Cells[i, 12].Value == null)
                        dr["FeeAmount"] = "";
                    else
                        dr["FeeAmount"] = Convert.ToDecimal(worksheet.Cells[i, 12].Value);

                    if (worksheet.Cells[i, 14].Value == null)
                        dr["Notes"] = 0;
                    else
                        dr["Notes"] = worksheet.Cells[i, 14].Value.ToString();

                    dr["LastUpdate"] = _lastupdate;

                    //dr["ClosePriceValue"] = worksheet.Cells[i, 2].Value.Equals(null) == true ? "" : worksheet.Cells[i, 2].Value.ToString();

                    if (dr["No"].Equals(null) != true ||
                        dr["Date"].Equals(null) != true ||
                        dr["ID"].Equals(null) != true ||
                        dr["Name"].Equals(null) != true ||
                        dr["Type"].Equals(null) != true ||
                        dr["Product"].Equals(null) != true ||
                        dr["ProductTo"].Equals(null) != true ||
                        dr["Amount"].Equals(null) != true ||
                        dr["Unit"].Equals(null) != true ||
                        dr["NAVFrom"].Equals(null) != true ||
                        dr["NAVTo"].Equals(null) != true ||
                        dr["FeeAmount"].Equals(null) != true ||
                        dr["Notes"].Equals(null) != true ) { dt.Rows.Add(dr); }
                    i++;

                }
            }

            return dt;
        }

        #endregion

        public string GetLastUploadInformation()
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"

                        declare @message nvarchar(max)

                        select @message = COALESCE(@message + ', ', '') + Convert(varchar,cast(Date as date),106) from UnitAllocationTemp
                        group by Date,LastUpdate

                        if @message is not null
	                        select 'Transaction : ' + @message + ' already processed on ' + convert(varchar,(select distinct lastupdate from UnitAllocationTemp),113) Result
                        else
	                        select 'No data uploaded' Result

                    ";
                        cmd.CommandTimeout = 0;
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
    }
}
