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

namespace RFSRepository
{
    public class EndYearClosingReps
    {
        Host _host = new Host();

        string _insertCommand = "INSERT INTO [dbo].[EndYearClosing] " +
                            "([EndYearClosingPK],[HistoryPK],[Status],[PeriodPK],[Mode],[LogMessages],";
        string _paramaterCommand = "@PeriodPK,@Mode,@LogMessages,";

        private EndYearClosing setEndYearClosing(SqlDataReader dr)
        {
            EndYearClosing M_EndYearClosing = new EndYearClosing();
            M_EndYearClosing.EndYearClosingPK = Convert.ToInt32(dr["EndYearClosingPK"]);
            M_EndYearClosing.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_EndYearClosing.Status = Convert.ToInt32(dr["Status"]);
            M_EndYearClosing.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_EndYearClosing.Notes = Convert.ToString(dr["Notes"]);
            M_EndYearClosing.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_EndYearClosing.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_EndYearClosing.Mode = Convert.ToInt32(dr["Mode"]);
            M_EndYearClosing.ModeDesc = Convert.ToString(dr["ModeDesc"]);
            M_EndYearClosing.FundPK = dr["FundPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["FundPK"]);
            M_EndYearClosing.LogMessages = Convert.ToString(dr["LogMessages"]);
            M_EndYearClosing.EntryUsersID = dr["EntryUsersID"].ToString();
            M_EndYearClosing.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_EndYearClosing.VoidUsersID = dr["VoidUsersID"].ToString();
            M_EndYearClosing.EntryTime = dr["EntryTime"].ToString();
            M_EndYearClosing.ApprovedTime = dr["ApprovedTime"].ToString();
            M_EndYearClosing.VoidTime = dr["VoidTime"].ToString();
            M_EndYearClosing.DBUserID = dr["DBUserID"].ToString();
            M_EndYearClosing.DBTerminalID = dr["DBTerminalID"].ToString();
            M_EndYearClosing.LastUpdate = dr["LastUpdate"].ToString();
            M_EndYearClosing.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_EndYearClosing;
        }
        
        public List<EndYearClosing> EndYearClosing_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<EndYearClosing> L_EndYearClosing = new List<EndYearClosing>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                                @"
                                    select b.ID as PeriodID, c.DescOne as ModeDesc, 
                                        case when a.[Status] = 1 then 'PENDING' else case when a.[Status] = 2 then 'APPROVED' else case when a.[Status] = 3 then 'VOID' else 'WAITING' end end end as StatusDesc, a.*
                                    from EndYearClosing a
	                                    left join Period b on a.PeriodPK = b.PeriodPK and b.[Status] = 2
	                                    left join MasterValue c on c.ID = 'EndYearClosingMode' and a.Mode = c.Code and c.[Status] = 2
                                    where a.[Status] = @Status
                                    order by a.PeriodPK desc
                                ";
                            cmd.Parameters.AddWithValue("@Status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                                @"
                                    select b.ID as PeriodID, c.DescOne as ModeDesc, 
                                        case when a.[Status] = 1 then 'PENDING' else case when a.[Status] = 2 then 'APPROVED' else case when a.[Status] = 3 then 'VOID' else 'WAITING' end end end as StatusDesc, a.*
                                    from EndYearClosing a
	                                    left join Period b on a.PeriodPK = b.PeriodPK and b.[Status] = 2
	                                    left join MasterValue c on c.ID = 'EndYearClosingMode' and a.Mode = c.Code and c.[Status] = 2
                                    order by a.PeriodPK desc
                                ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_EndYearClosing.Add(setEndYearClosing(dr));
                                }
                            }
                            return L_EndYearClosing;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void EndYearClosing_Approved(EndYearClosing _EndYearClosing)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                update EndYearClosing set [Status] = 2, ApprovedUsersID = @ApprovedUsersID, ApprovedTime = @ApprovedTime, LastUpdate = @LastUpdate
                                where EndYearClosingPK = @PK and HistoryPK = @HistoryPK and status = 1
                            ";
                        cmd.Parameters.AddWithValue("@PK", _EndYearClosing.EndYearClosingPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _EndYearClosing.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _EndYearClosing.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                update EndYearClosing set [Status] = 3, VoidUsersID = @VoidUsersID, VoidTime = @VoidTime, LastUpdate = @LastUpdate
                                where EndYearClosingPK = @PK and [Status] = 4
                            ";
                        cmd.Parameters.AddWithValue("@PK", _EndYearClosing.EndYearClosingPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _EndYearClosing.ApprovedUsersID);
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

        public void EndYearClosing_Void(EndYearClosing _EndYearClosing)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                declare @PeriodPK int
                                update EndYearClosing set [Status] = 3, VoidUsersID = @VoidUsersID, VoidTime = @VoidTime, LastUpdate = @LastUpdate
                                where EndYearClosingPK = @PK and HistoryPK = @HistoryPK and status = 2

                                select @PeriodPK = PeriodPK from EndYearClosing where status = 2 and EndYearClosingPK = @PK

                                update journal set status = 3 where Description = 'PERIOD CLOSING' and PeriodPK = @PeriodPK

                                delete EndYearPortfolio where periodpk = @periodPK

                            ";
                        cmd.Parameters.AddWithValue("@PK", _EndYearClosing.EndYearClosingPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _EndYearClosing.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _EndYearClosing.VoidUsersID);
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

        public void EndYearClosing_Reject(EndYearClosing _EndYearClosing)
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
                        declare @PeriodPK int

                        select @PeriodPK = PeriodPK from EndYearClosing where status = 2 and EndYearClosingPK = @PK

                        update EndYearClosing set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate 
                        where EndYearClosingPK = @PK and historypk = @historyPK and status = 1 

                        update journal set status = 3 where Description = 'PERIOD CLOSING' and PeriodPK = @PeriodPK

                        ";
                        cmd.Parameters.AddWithValue("@PK", _EndYearClosing.EndYearClosingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _EndYearClosing.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _EndYearClosing.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update EndYearClosing set status= 2,LastUpdate=@LastUpdate where EndYearClosingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _EndYearClosing.EndYearClosingPK);
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

        public int EndYearClosing_Generate(EndYearClosing _EndYearClosing)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_EndYearClosing.Mode == 4)
                        {

                            cmd.CommandText =
                                @"
                                DECLARE @PeriodID nvarchar(4)
                                select @PeriodID = ID From period where PeriodPK = @PeriodPK
                                set @PeriodID = @PeriodID - 1

                                Declare @LastDate nvarchar(10)
                                Declare @PrevPeriodPK int

                                Select @PrevPeriodPK = PeriodPK from Period where ID = @PeriodID and status = 2
                                set @LastDate = '12/31/' + @PeriodID

                                IF isnull(@PrevPeriodPK,0) = 0
                                BEGIN
                                return
                                END
                                ELSE
                                BEGIN
                                    update journal set status = 3 ,Notes ='Void By PeriodClosing', posted = 0 
	                                ,VoidUsersID = @UserID,VoidTime = @LastUpdate
	                                WHERE reference = '9999/ADJ/12' + right(@periodID,2)

                                    Declare @PK int
                                    Select @PK = isnull(max(EndYearClosingPK),0) + 1 from EndYearClosing 
                                    set @PK = isnull(@PK,1)  
                                    Insert into EndYearClosing(EndYearClosingPK,HistoryPK,Status,PeriodPK,Mode,LogMessages
                                    ,EntryUsersID,EntryTime,LastUpdate)                    
                                    Select @PK,1,1,@PeriodPK,@Mode,'',@UserID,Getdate(),Getdate()
			                         
	                                DECLARE @AutoNo				INT
	                                DECLARE @DefaultCurrencyPK	INT
	                                DECLARE @JournalPK	INT
	
	                                set @AutoNo = 1
	                                set @DefaultCurrencyPK = 1

                                        
			
	                                Select @JournalPK = max(journalPK) + 1 from Journal
	                                INSERT [Journal] ([JournalPK], [HistoryPK], [Selected], [status], [Notes], [PeriodPK], [ValueDate], [TrxNo], 
		                                [TrxName], [Reference], [Type], [Description], [Posted], [PostedBy], [PostedTime],EntryUsersID,EntryTime,ApprovedUsersID,ApprovedTime,LastUpdate)
	                                SELECT @JournalPK,1,0,2,'',@PeriodPK,@LastDate,0,'','9999/ADJ/12' + right(@periodID,2),1,'PERIOD CLOSING',1,@UserID,GetDate(),@UserID,Getdate(),@UserID,GetDate(),GetDate()

	

	                                INSERT [JournalDetail] ([JournalPK], [AutoNo], [HistoryPK], [Status], [AccountPK], [CurrencyPK], 
		                                [OfficePK], [DepartmentPK], [AgentPK], [CounterpartPK], [InstrumentPK], [ConsigneePK], [DetailDescription], 
		                                [DocRef], [DebitCredit], [Amount], [Debit], [Credit], [CurrencyRate], [BaseDebit],BaseCredit,LastUsersID,LastUpdate) 

	                                SELECT @JournalPK, ROW_NUMBER() OVER (ORDER BY A.AccountPK) AS AutoPK,1,2, A.AccountPK, A.CurrencyPK, A.OfficePK, A.DepartmentPK, 
		                                A.AgentPK, 
		                                A.CounterPartPK, A.InstrumentPK, A.ConsigneePK,'','', CASE A.[Type] WHEN 1 THEN 'D' ELSE 'C' END, 
		                                CASE A.[Type] WHEN 1 THEN A.Balance ELSE -A.Balance END, 
		                                CASE A.[Type] WHEN 1 THEN A.Balance ELSE 0 END, CASE A.[Type] WHEN 2 THEN -A.Balance ELSE 0 END, 1,
		                                CASE A.[Type] WHEN 1 THEN A.BaseBalance ELSE 0 END, CASE A.[Type] WHEN 2 THEN -A.BaseBalance ELSE 0 END, 
		                                @userID, GetDate() FROM (
		                                SELECT A.AccountPK, C.[Type], C.CurrencyPK, isnull(A.OfficePK,0) OfficePK, isnull(A.DepartmentPK,0) DepartmentPK, 
			                                isnull(A.AgentPK,0) AgentpK, isnull(A.CounterpartPK,0) CounterpartPK, isnull(A.InstrumentPK,0) InstrumentPK ,isnull(A.ConsigneePK,0) ConsigneePK ,
			                                SUM(A.Debit-A.Credit) AS Balance, SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance
			                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK 
			                                INNER JOIN [Account] C ON A.AccountPK = C.AccountPK and C.status = 2
			                                WHERE B.PeriodPK = @PrevPeriodPK 
			                                AND B.ValueDate <= @LastDate
			                                AND B.Posted = 1 AND B.Reversed = 0 AND B.Status = 2
			                                AND C.[Type] <= 2
			                                AND C.[Groups] = 0
			                                GROUP BY A.AccountPK, C.[Type], isnull(A.OfficePK,0), isnull(A.DepartmentPK,0), isnull(A.AgentPK,0), isnull(A.CounterpartPK,0), C.CurrencyPK,
			                                isnull(A.InstrumentPK,0),isnull(A.ConsigneePK,0)
			                                HAVING SUM(A.Debit-A.Credit) <> 0 OR SUM(A.BaseDebit-A.BaseCredit) <> 0
		                                ) A		
		  
	                                SELECT @AutoNo = ISNULL(MAX(AutoNo), 0) + 1 FROM JournalDetail WHERE [JournalPK] = @JournalPK
	                                INSERT [JournalDetail] ([JournalPK], [AutoNo], [HistoryPK], [Status], [AccountPK], [CurrencyPK], 
		                                [OfficePK], [DepartmentPK], [AgentPK], [CounterpartPK], [InstrumentPK], [ConsigneePK], [DetailDescription], 
		                                [DocRef], [DebitCredit], [Amount], [Debit], [Credit], [CurrencyRate], [BaseDebit],BaseCredit,LastUsersID,LastUpdate) 
		                                SELECT @JournalPK, @AutoNo + ROW_NUMBER() OVER (ORDER BY  A.OfficePK, A.DepartmentPK, A.AgentPK, 
		                                A.CounterPartPK, A.CurrencyPK,A.InstrumentPK,A.ConsigneePK),1,2,
		                                @ProfitPreviousYearPK, A.CurrencyPK, A.OfficePK, A.DepartmentPK, 
		                                A.AgentPK, 
		                                A.CounterPartPK, A.InstrumentPK, A.ConsigneePK,'','', 'C', -A.Balance,  0, -A.Balance,1, 0, -A.BaseBalance, 
		                                @userID, GetDate()FROM (
		                                SELECT A.OfficePK, A.DepartmentPK, A.CurrencyPK, A.AgentPK, 
		                                A.CounterPartPK, A.InstrumentPK, A.ConsigneePK,
			                                SUM(A.Debit-A.Credit) AS Balance, SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance
			                                FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK 
			                                INNER JOIN [Account] C ON A.AccountPK = C.AccountPK and C.status = 2
			                                WHERE B.PeriodPK = @PrevPeriodPK 
			                                AND B.ValueDate <= @LastDate
			                                AND B.Posted = 1 AND B.Reversed = 0 AND B.status = 2
			                                AND (A.AccountPK = @ProfitCurrentYearPK OR C.parentPK1 = @ProfitCurrentYearPK 
			                                OR C.parentPK2 = @ProfitCurrentYearPK OR C.parentPK3 = @ProfitCurrentYearPK 
			                                OR C.parentPK4 = @ProfitCurrentYearPK OR C.parentPK5 = @ProfitCurrentYearPK 
			                                OR C.parentPK6 = @ProfitCurrentYearPK OR C.parentPK7 = @ProfitCurrentYearPK 
			                                OR C.parentPK8 = @ProfitCurrentYearPK OR C.parentPK9 = @ProfitCurrentYearPK)
			                                GROUP BY A.OfficePK, A.DepartmentPK, A.CurrencyPK,A.AgentPK, 
		                                A.CounterPartPK, A.InstrumentPK, A.ConsigneePK
		                                ) A, Account B
		                                WHERE B.AccountPK = @ProfitCurrentYearPK and B.status = 2

	                                UPDATE [JournalDetail] 
		                                SET DebitCredit = CASE WHEN DebitCredit = 'D' then 'C' else 'D' end 
		                                WHERE JournalPK = @JournalPK AND Amount < 0
	
	                                UPDATE [JournalDetail] 
		                                SET Debit = -Credit, BaseDebit = -BaseCredit 
		                                WHERE JournalPK = @JournalPK AND Amount < 0 AND DebitCredit = 'D'
		
	                                UPDATE [JournalDetail] 
		                                SET Credit = 0, BaseCredit = 0
		                                WHERE JournalPK = @JournalPK AND Amount < 0 AND DebitCredit = 'D'
		
	                                UPDATE [JournalDetail] 
		                                SET Credit = -Debit, BaseCredit = -BaseDebit
		                                WHERE JournalPK = @JournalPK AND Amount < 0 AND DebitCredit = 'C'
		
	                                UPDATE [JournalDetail] 
		                                SET Debit = 0, BaseDebit = 0
		                                WHERE JournalPK = @JournalPK AND Amount < 0 AND DebitCredit = 'C'
		
	                                UPDATE [JournalDetail] 
		                                SET Amount = ABS(Amount) 
		                                WHERE JournalPK = @JournalPK AND Amount < 0
	
	                                UPDATE [JournalDetail] SET [CurrencyRate] = ([BaseDebit] - [BaseCredit])/([Debit] - [Credit])
		                                WHERE CurrencyPK <> 1 AND JournalPK = @JournalPK

                                    Update EndYearClosing set LogMessages = 'End Year Closing Success' where EndYearClosingPK = @PK

                                    Select @PK LastPK
                                END
                                ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@PeriodPK", _EndYearClosing.PeriodPK);
                            cmd.Parameters.AddWithValue("@FundPK", _EndYearClosing.FundPK);
                            cmd.Parameters.AddWithValue("@ProfitPreviousYearPK", _EndYearClosing.AccountPKFrom);
                            cmd.Parameters.AddWithValue("@ProfitCurrentYearPK", _EndYearClosing.AccountPKTo);
                            cmd.Parameters.AddWithValue("@Mode", _EndYearClosing.Mode);
                            cmd.Parameters.AddWithValue("@UserID", _EndYearClosing.EntryUsersID);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        }
                        else if (_EndYearClosing.Mode == 5)
                        {

                            cmd.CommandText =
                                @"
                                DECLARE @PeriodID nvarchar(4)
                                select @PeriodID = ID From period where PeriodPK = @PeriodPK

                                DECLARE @PrevPeriodID NVARCHAR(4)
                                set @PrevPeriodID = @PeriodID - 1

                                Declare @PrevPeriodPK int
                                Select @PrevPeriodPK = PeriodPK from Period where ID = @PrevPeriodID and status = 2

                                DECLARE @ProfitPreviousYearPK INT
                                DECLARE @ProfitCurrentYearPK INT


                                Declare @LastDate nvarchar(10)
                                set @LastDate = '12/31/' + @PrevPeriodID

                                IF isnull(@PrevPeriodPK,0) = 0 
                                BEGIN
                                return
                                END
                                ELSE
                                BEGIN

	                                UPDATE A SET A.Status = 3,Notes ='Void By PeriodClosing', posted = 0 
	                                ,VoidUsersID = @UserID,VoidTime = @LastUpdate FROM FundJournal A
	                                LEFT JOIN FundJournalDetail B ON A.FundJournalPK = B.FundJournalPK
	                                WHERE A.ValueDate = @Date AND A.Type = 14 AND B.FundPK = @FundPK
	
	                                SELECT @ProfitCurrentYearPK = CurrentYearAccount,@ProfitPreviousYearPK = PriorYearAccount 
	                                FROM dbo.FundAccountingSetup WHERE status = 2 AND FundPK = @FundPk

			                         
	                                DECLARE @AutoNo				INT
	                                DECLARE @DefaultCurrencyPK	INT
	                                DECLARE @JournalPK	INT
	
	                                set @AutoNo = 1
	                                set @DefaultCurrencyPK = 1

                                        
			
	                                Select @JournalPK = max(FundJournalPK) + 1 from dbo.FundJournal
	                                INSERT [FundJournal] ([FundJournalPK], [HistoryPK], [Selected], [status], [Notes], [PeriodPK], [ValueDate], [TrxNo], 
		                                [TrxName], [Reference], [Type], [Description], [Posted], [PostedBy], [PostedTime],EntryUsersID,EntryTime,ApprovedUsersID,ApprovedTime,LastUpdate)
	                                SELECT @JournalPK,1,0,2,'',@PeriodPK,@Date,0,'','9999/ADJ/99' + right(@periodID,2),14,'PERIOD CLOSING',1,@UserID,GetDate(),@UserID,Getdate(),@UserID,GetDate(),GetDate()
	

	                                INSERT [FundJournalDetail] ([FundJournalPK], [AutoNo], [HistoryPK], [Status], [FundJournalAccountPK], [CurrencyPK], 
		                                [FundPK], [FundClientPK], [InstrumentPK], [DetailDescription], 
		                                [DebitCredit], [Amount], [Debit], [Credit], [CurrencyRate], [BaseDebit],BaseCredit,LastUsersID,LastUpdate) 

	                                SELECT @JournalPK, ROW_NUMBER() OVER (ORDER BY A.FundJournalAccountPK) AS AutoPK,1,2, A.FundJournalAccountPK, A.CurrencyPK
	                                , A.FundPK, A.FundClientPK, 
		                                A.InstrumentPK
	                                    ,'', CASE A.[Type] WHEN 1 THEN 'D' ELSE 'C' END, 
		                                CASE A.[Type] WHEN 1 THEN A.Balance ELSE -A.Balance END, 
		                                CASE A.[Type] WHEN 1 THEN A.Balance ELSE 0 END, CASE A.[Type] WHEN 2 THEN -A.Balance ELSE 0 END, 1,
		                                CASE A.[Type] WHEN 1 THEN A.BaseBalance ELSE 0 END, CASE A.[Type] WHEN 2 THEN -A.BaseBalance ELSE 0 END, 
		                                @userID, GetDate() FROM (
		                                SELECT A.FundJournalAccountPK, C.[Type], C.CurrencyPK, isnull(A.FundPK,0) FundPK, isnull(A.FundClientPK,0) FundClientPK, 
			                                isnull(A.InstrumentPK,0) InstrumentPK
			                                ,SUM(A.Debit-A.Credit) AS Balance, SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance
			                                FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK 
			                                INNER JOIN [FundJournalAccount] C ON A.FundJournalAccountPK = C.FundJournalAccountPK and C.status = 2
			                                WHERE B.PeriodPK = @PrevPeriodPK 
			                                AND B.ValueDate <= @LastDate
			                                AND B.Posted = 1 AND B.Reversed = 0 AND B.Status = 2
			                                AND C.[Type] <= 2
			                                AND C.[Groups] = 0 AND A.FundPK = @FundPk
			                                GROUP BY A.FundJournalAccountPK, C.[Type], isnull(A.FundPK,0), isnull(A.FundClientPK,0), isnull(A.InstrumentPK,0)
			                                ,  C.CurrencyPK
			                                HAVING SUM(A.Debit-A.Credit) <> 0 OR SUM(A.BaseDebit-A.BaseCredit) <> 0
		                                ) A		
		  
	                                SELECT @AutoNo = ISNULL(MAX(AutoNo), 0) + 1 FROM FundJournalDetail WHERE [FundJournalPK] = @JournalPK
	                                INSERT [FundJournalDetail] ([FundJournalPK], [AutoNo], [HistoryPK], [Status], [FundJournalAccountPK], [CurrencyPK], 
		                                [FundPK], [FundClientPK], [InstrumentPK], [DetailDescription], 
		                                [DebitCredit], [Amount], [Debit], [Credit], [CurrencyRate], [BaseDebit],BaseCredit,LastUsersID,LastUpdate) 
		                                SELECT @JournalPK, @AutoNo + ROW_NUMBER() OVER (ORDER BY  A.FundPK, A.FundClientPK, A.InstrumentPK, 
		                                A.CurrencyPK),1,2,
		                                @ProfitPreviousYearPK, A.CurrencyPK, A.FundPK, A.FundClientPK, 
		                                A.InstrumentPk, 
		                                '', 'C', -A.Balance,  0, -A.Balance,1, 0, -A.BaseBalance, 
		                                @userID, GetDate()FROM (
		                                SELECT A.FundPK, A.FundClientPK, A.CurrencyPK, A.InstrumentPk, 
			                                SUM(A.Debit-A.Credit) AS Balance, SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance
			                                FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK 
			                                INNER JOIN [FundJournalAccount] C ON A.FundJournalAccountPK = C.FundJournalAccountPK and C.status = 2
			                                WHERE B.PeriodPK = @PrevPeriodPK 
			                                AND B.ValueDate <= @LastDate
			                                AND B.Posted = 1 AND B.Reversed = 0 AND B.status = 2 AND A.FundPK = @FundPK
			                                AND (A.FundJournalAccountPK = @ProfitCurrentYearPK OR C.parentPK1 = @ProfitCurrentYearPK 
			                                OR C.parentPK2 = @ProfitCurrentYearPK OR C.parentPK3 = @ProfitCurrentYearPK 
			                                OR C.parentPK4 = @ProfitCurrentYearPK OR C.parentPK5 = @ProfitCurrentYearPK 
			                                OR C.parentPK6 = @ProfitCurrentYearPK OR C.parentPK7 = @ProfitCurrentYearPK 
			                                OR C.parentPK8 = @ProfitCurrentYearPK OR C.parentPK9 = @ProfitCurrentYearPK)
			                                GROUP BY A.FundPK, A.FundClientPK, A.CurrencyPK,A.InstrumentPK
		                                ) A, FundJournalAccount B
		                                WHERE B.FundJournalAccountPK = @ProfitCurrentYearPK and B.status = 2

	                                UPDATE [FundJournalDetail] 
		                                SET DebitCredit = CASE WHEN DebitCredit = 'D' then 'C' else 'D' end 
		                                WHERE FundJournalPK = @JournalPK AND Amount < 0 AND FundPK = @FundPK
	
	                                UPDATE [FundJournalDetail] 
		                                SET Debit = -Credit, BaseDebit = -BaseCredit 
		                                WHERE FundJournalPK = @JournalPK AND Amount < 0 AND DebitCredit = 'D'
		                                AND FundPK = @FundPK
		
	                                UPDATE [FundJournalDetail] 
		                                SET Credit = 0, BaseCredit = 0
		                                WHERE FundJournalPK = @JournalPK AND Amount < 0 AND DebitCredit = 'D'
		                                AND FundPK = @FundPK
		
	                                UPDATE [FundJournalDetail] 
		                                SET Credit = -Debit, BaseCredit = -BaseDebit
		                                WHERE FundJournalPK = @JournalPK AND Amount < 0 AND DebitCredit = 'C'
		                                AND FundPK = @FundPK
		
	                                UPDATE [FundJournalDetail] 
		                                SET Debit = 0, BaseDebit = 0
		                                WHERE FundJournalPK = @JournalPK AND Amount < 0 AND DebitCredit = 'C'
		                                AND FundPK = @FundPK
		
	                                UPDATE [FundJournalDetail] 
		                                SET Amount = ABS(Amount) 
		                                WHERE FundJournalPK = @JournalPK AND Amount < 0
		                                AND FundPK = @FundPK
	
	                                UPDATE [FundJournalDetail] SET [CurrencyRate] = ([BaseDebit] - [BaseCredit])/([Debit] - [Credit])
		                                WHERE CurrencyPK <> 1 AND FundJournalPK = @JournalPK
		                                AND FundPK = @FundPK

                                    Declare @PK int
                                    Select @PK = isnull(max(EndYearClosingPK),0) + 1 from EndYearClosing 
                                    set @PK = isnull(@PK,1)  
                                    Insert into EndYearClosing(EndYearClosingPK,HistoryPK,Status,PeriodPK,Mode,FundPK,LogMessages
                                    ,EntryUsersID,EntryTime,LastUpdate)                    
                                    Select @PK,1,1,@PeriodPK,@Mode,@FundPK,'Success Fund Accounting Closing Per Fund',@UserID,Getdate(),Getdate()

                                    Select @PK LastPK
                                END
                                ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@PeriodPK", _EndYearClosing.PeriodPK);
                            cmd.Parameters.AddWithValue("@FundPK", _EndYearClosing.FundPK);
                            cmd.Parameters.AddWithValue("@Mode", _EndYearClosing.Mode);
                            cmd.Parameters.AddWithValue("@Date", _EndYearClosing.Date);
                            cmd.Parameters.AddWithValue("@UserID", _EndYearClosing.EntryUsersID);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        }
                        else if (_EndYearClosing.Mode == 6)
                        {

                            cmd.CommandText =
                                @"
                                Declare @PrevPeriodPK INT
                                DECLARE @PeriodID nvarchar(4)
                                select @PeriodID = ID From period where PeriodPK = @PeriodPK
                                set @PeriodID = @PeriodID - 1

                                Select @PrevPeriodPK = PeriodPK from Period where ID = @PeriodID and status = 2


                                DECLARE @MaxEnddayTrails DATETIME
                                DECLARE @MaxEnddayTrailFundPortfolio DATETIME

                                SELECT @MaxEnddayTrails =  MAX(date) FROM dbo.FundClientPosition WHERE YEAR(Date) = @PeriodID


                                UPDATE dbo.FundClientPositionEndYear SET Status = 3
                                ,VoidUsersID = @UserID
                                ,VoidTime = @LastUpdate
                                ,LastUpdate = @LastUpdate
                                WHERE PeriodPK = @PeriodPK

                                DECLARE @MaxPKFCPEndYear INT

                                SELECT @MaxPKFCPEndYear = MAX(FundClientPositionEndYearPK) + 1 FROM dbo.FundClientPositionEndYear 

                                SET @MaxPKFCPEndYear = ISNULL(@MaxPKFCPEndYear,0)

                                INSERT INTO dbo.FundClientPositionEndYear
                                        ( 
		                                FundClientPositionEndYearPK,
		                                HistoryPK,
		                                Status,
		                                PeriodPK ,
                                          FundPK ,
                                          FundClientPK ,
                                          UnitAmount ,
                                          AvgNAV ,
		                                  Entrytime,
		                                  EntryUsersID,
		                                  ApprovedTime,
		                                  ApprovedUsersID,
                                          LastUpdate
                                        )
                                SELECT @MaxPKFCPEndYear + ROW_NUMBER() OVER	(ORDER BY FundClientPK ASC)
                                ,1,2
                                ,@PeriodPK,FundPK,FundClientPK 
                                ,UnitAmount,AvgNAV,@LastUpdate,@UserID,@LastUpdate,@UserID,@LastUpdate
                                FROM dbo.FundClientPosition WHERE Date = @MaxEnddayTrails


                                Delete dbo.FundEndYearPortfolio 

                                WHERE PeriodPK = @PeriodPK


                                SELECT @MaxEnddayTrailFundPortfolio =  MAX(Date) FROM dbo.FundPosition WHERE YEAR(Date) = @PeriodID AND status = 2

                                DECLARE @MaxPKFundEndYear INT

                                SELECT @MaxPKFundEndYear = MAX(FundEndYearPortfolioPK) + 1 FROM dbo.FundEndYearPortfolio 

                                SET @MaxPKFundEndYear = ISNULL(@MaxPKFundEndYear,0)
                                INSERT INTO dbo.FundEndYearPortfolio
                                        ( FundEndYearPortfolioPK ,
                                          PeriodPK ,
                                          InstrumentPK ,
                                          Volume ,
                                          FundPK ,
                                          MarketPK ,
                                          AvgPrice ,
                                          ClosePrice ,
                                          TrxAmount ,
                                          AcqDate ,
                                          MaturityDate ,
                                          InterestPercent ,
                                          CurrencyPK ,
                                          Category ,
                                          TaxExpensePercent ,
                                          InterestDaysType ,
                                          InterestPaymentType ,
                                          PaymentModeOnMaturity ,
                                          BankPK ,
                                          BankBranchPK ,
                                          PaymentInterestSpecificDate ,
                                          PriceMode ,
                                          BitIsAmortized ,
                                          LastUsersID ,
                                          LastUpdate ,
                                          BitBreakable ,
                                          TradeDate
                                        )
                                SELECT @MaxPKFundEndYear + ROW_NUMBER() OVER (ORDER BY FundPK ASC) 
                                ,@PeriodPK
                                ,InstrumentPK
                                ,Balance
                                ,FundPK
                                ,MarketPK
                                ,AvgPrice
                                ,ClosePrice
                                ,TrxAmount
                                ,AcqDate ,
                                MaturityDate ,
                                InterestPercent ,
                                CurrencyPK ,
                                Category ,
                                TaxExpensePercent ,
                                InterestDaysType ,
                                InterestPaymentType ,
                                PaymentModeOnMaturity ,
                                BankPK ,
                                BankBranchPK ,
                                PaymentInterestSpecificDate ,
                                PriceMode ,
                                BitIsAmortized ,
                                @UserID ,
                                @LastUpdate ,
                                BitBreakable ,
                                TradeDate
                                FROM FundPosition WHERE Date = @MaxEnddayTrailFundPortfolio AND Status = 2





                                    Declare @PK int
                                    Select @PK = isnull(max(EndYearClosingPK),0) + 1 from EndYearClosing 
                                    set @PK = isnull(@PK,1)  

                                  Insert into EndYearClosing(EndYearClosingPK,HistoryPK,Status,PeriodPK,Mode,LogMessages
                                    ,EntryUsersID,EntryTime,LastUpdate)                    
                                    Select @PK,1,1,@PeriodPK,@Mode,'General Closing Success',@UserID,Getdate(),Getdate()

                                    Select @PK LastPK";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@PeriodPK", _EndYearClosing.PeriodPK);
                            cmd.Parameters.AddWithValue("@Mode", _EndYearClosing.Mode);
                            cmd.Parameters.AddWithValue("@UserID", _EndYearClosing.EntryUsersID);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["LastPK"]);
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


        public bool EndYearClosing_ValidateGenerate(EndYearClosing _EndYearClosing)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = 
                            @"
                                if exists (select * from EndYearClosing where [Status] in (1,2) and PeriodPK = @PeriodPK and Mode = @Mode)
                                begin
	                                select 1 Result
                                end
                                else
                                begin
	                                select 0 Result
                                end
                            ";
                        cmd.Parameters.AddWithValue("@PeriodPK", _EndYearClosing.PeriodPK);
                        cmd.Parameters.AddWithValue("@Mode", _EndYearClosing.Mode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);
                            }
                            return false;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public string CheckGenerate(EndYearClosing _EndYearClosing)
        {
            try
            {
                var _msg = "";
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        IF EXISTS(
                        SELECT * FROM EndYearClosing 
                        WHERE status in(1,2) and PeriodPK = @PeriodPK and Mode = @Mode
                        )
                        BEGIN

	                        SELECT 'true' ReturnDesc

                        END
                        ELSE
                        BEGIN
	                        SELECT 'false' ReturnDesc
                        END
                        ";
                        cmd.Parameters.AddWithValue("@PeriodPK", _EndYearClosing.PeriodPK);
                        cmd.Parameters.AddWithValue("@Mode", _EndYearClosing.Mode);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return _msg = Convert.ToString(dr["ReturnDesc"]);
                            }
                        }
                    }
                }
                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public string CheckGenerateFundJournalClosing(EndYearClosing _EndYearClosing)
        {
            try
            {
                var _msg = "";
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        IF EXISTS(
                        SELECT * FROM EndYearClosing 
                        WHERE status in(1,2) and PeriodPK = @PeriodPK and Mode = @Mode and FundPK = @FundPK
                        )
                        BEGIN

	                        SELECT 'true' ReturnDesc

                        END
                        ELSE
                        BEGIN
	                        SELECT 'false' ReturnDesc
                        END
                        ";
                        cmd.Parameters.AddWithValue("@PeriodPK", _EndYearClosing.PeriodPK);
                        cmd.Parameters.AddWithValue("@Mode", _EndYearClosing.Mode);
                        cmd.Parameters.AddWithValue("@FundPK", _EndYearClosing.FundPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return _msg = Convert.ToString(dr["ReturnDesc"]);
                            }
                        }
                    }
                }
                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}