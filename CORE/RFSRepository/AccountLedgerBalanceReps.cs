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
    public class AccountLedgerBalanceReps
    {
        Host _host = new Host();
        static readonly AccountLedgerBalanceReps _AccountLedgerBalanceReps = new AccountLedgerBalanceReps();

        public List<AccountLedgerTrialBalance> Get_TrialBalance(string _date, int _paramStatus)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountLedgerTrialBalance> L_TrialBalance = new List<AccountLedgerTrialBalance>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _status = "";
                        if (_paramStatus == 1)
                        {
                            _status = "  B.Status = 2 and B.Posted = 1 and B.Reversed = 0 ";
                        }
                        else if (_paramStatus == 2)
                        {
                            _status = "  B.Status = 2 and B.Posted = 1 and B.Reversed = 1 ";
                        }
                        else if (_paramStatus == 3)
                        {
                            _status = "  B.Status = 2 and B.Posted = 0 and B.Reversed = 0 ";
                        }
                        else if (_paramStatus == 4)
                        {
                            _status = "  B.Status = 1  ";
                        }
                        else if (_paramStatus == 5)
                        {
                            _status = "  B.Status = 3  ";
                        }
                        else if (_paramStatus == 6)
                        {
                            _status = "  (B.Status = 2 or B.Posted = 1) and B.Reversed = 0  and B.status not in (3,4)";
                        }
                        else if (_paramStatus == 7)
                        {
                            _status = "  (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and  B.Reversed = 0 and B.status not in (3,4) ";
                        }

                        cmd.CommandText = @"
                        Declare @PeriodPK int
                        Select @PeriodPK = PeriodPK from Period where @ValueDateTo between DateFrom and DateTo and status = 2

                        Declare @ValueDateFrom datetime

                        set @ValueDateFrom = @ValueDateTo

                        Select  case when A.Groups = 1 then isnull(A.Name,'') else '' end Header,A.ID ID
                        ,case when A.Groups = 1 then '' else A.Name end Name
                        ,isnull(cast(E.HeaderOrder as Nvarchar(50)) + '.' + E.Name,'') ParentName 
                        ,D.ID CurrencyID,A.BitIsChange,A.Groups
                        ,isnull(B.PreviousBaseBalance,0) PreviousBaseBalance
                        ,isnull(B.Movement,0) Movement
                        ,isnull(B.CurrentBaseBalance,0) CurrentBaseBalance
                        from Account A
                        left join (


                        SELECT C.ID, C.Name,    
                        C.BitIsChange, C.groups,
                        CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                        CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) 
                        - CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4))  AS Movement,       
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
                        WHERE  B.ValueDate <= @ValueDateTo and B.PeriodPK = @PeriodPK and A.status in (1,2)  and " + _status + @"
                        --and C.Depth < 3
                        Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                        C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                        C.ParentPK7, C.ParentPK8, C.ParentPK9        
                        ) AS B        
                        WHERE
                        (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                        OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                        OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                        OR B.ParentPK9 = A.AccountPK)       and A.Status in (1,2)
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
                        INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)    
                        WHERE  B.ValueDate < @ValueDateFrom  and B.PeriodPK = @PeriodPK and A.status in (1,2)  and   " + _status + @"
                        --and C.Depth < 3
                        Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                        C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                        C.ParentPK7, C.ParentPK8, C.ParentPK9        
                        ) AS B        
                        WHERE  (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                        OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                        OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                        OR B.ParentPK9 = A.AccountPK)  and A.Status in (1,2)
                        Group BY A.AccountPK       
                        ) AS B ON A.AccountPK = B.AccountPK        
                        INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)     
		
		
                        WHERE C.Show=1  And C.Status in (1,2)
                        ) B on A.ID = B.ID 
                        left JOIN Account E ON A.ParentPK = E.AccountPK   And E.Status in (1,2)   
                        left JOIN Currency D ON A.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)     
                        where 	A.show = 1 And A.Status in (1,2)
                        order by A.ID";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@ValueDateTo", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AccountLedgerTrialBalance M_TrialBalance = new AccountLedgerTrialBalance();
                                    M_TrialBalance.Header = dr["Header"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Header"]);
                                    M_TrialBalance.ID = dr["ID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ID"]);
                                    M_TrialBalance.Name = dr["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Name"]);
                                    M_TrialBalance.ParentName = dr["ParentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ParentName"]);
                                    M_TrialBalance.Currency = dr["CurrencyID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CurrencyID"]);
                                    M_TrialBalance.BitIsChange = dr["BitIsChange"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitIsChange"]);
                                    M_TrialBalance.BitIsGroups = dr["groups"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["groups"]);
                                    M_TrialBalance.PreviousBaseBalance = dr["PreviousBaseBalance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["PreviousBaseBalance"]);
                                    M_TrialBalance.Movement = dr["Movement"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["Movement"]);
                                    M_TrialBalance.CurrentBaseBalance = dr["CurrentBaseBalance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["CurrentBaseBalance"]);
                                    L_TrialBalance.Add(M_TrialBalance);
                                }

                            }
                            return L_TrialBalance;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

//        public string TrialBalance_InsertDiffEndBalance(List<AccountLedgerTrialBalance> _tb, int _fundPK, string _valueDate, string _userID)
//        {
//            try
//            {
//                DateTime _dateTimeNow = DateTime.Now;
//                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
//                {
//                    DbCon.Open();

//                    using (SqlCommand cmd = DbCon.CreateCommand())
//                    {

//                        cmd.CommandText = @"
//                                              Truncate table FundTBAdjustment
//                                          ";
//                        cmd.CommandTimeout = 0;
//                        cmd.ExecuteNonQuery();

//                    }

//                    foreach (var _obj in _tb)
//                    {
//                        AccountLedgerTrialBalance _m = new AccountLedgerTrialBalance();
//                        _m.ID = _obj.ID;
//                        _m.CurrentBaseBalance = _obj.CurrentBaseBalance;

//                        using (SqlCommand cmd = DbCon.CreateCommand())
//                        {
                         

//                            cmd.CommandText = @"
//                                                insert into FundTBAdjustment
//                                                Select @ID,@CurrentBaseBalance,@BKBalance
//                                          ";
//                            cmd.CommandTimeout = 0;
//                            cmd.Parameters.AddWithValue("@ID", _m.ID);
//                            cmd.Parameters.AddWithValue("@CurrentBaseBalance", _m.CurrentBaseBalance);
//                            cmd.ExecuteNonQuery();

//                        }
//                    }

//                    using (SqlCommand cmd = DbCon.CreateCommand())
//                    {

//                        cmd.CommandText = @"
//                            Declare @PPeriodPK int            
//                            Declare @FundJournalPK int
//                            select @PPeriodPK = PeriodPK from Period Where @ValueDate Between DateFrom and DateTo and Status = 2                     
//                            select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal               
//                            set @FundJournalPK = isnull(@FundJournalPK,1)
//            
//                            INSERT INTO [FundJournal]([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                
//                                ,[Description],[Posted],[EntryUsersID],[EntryTime],[LastUpdate])                
//                                Select  @FundJournalPK, 1,2,'TB Reconcile',@PPeriodPK,@ValueDate,11,0,'TB RECONCILE',                
//                                    '','',1,@UsersID,@LastUpdate,@LastUpdate  
//                  
//
//                            Declare @FundJournalAccountPK int,@Type int,@Balance numeric(22,4),@EndBalance numeric(22,4),@CurrencyPK int                              
//
//                            DECLARE A CURSOR FOR 
//		                            Select B.FundJournalAccountPK,B.Type,A.Balance,A.EndBalance,B.CurrencyPK from FundTBAdjustment A
//		                            left join FundJournalAccount B on A.ID = B.ID and B.status = 2
//                            Open A
//                            Fetch Next From A
//                            Into @FundJournalAccountPK,@Type,@Balance,@EndBalance,@CurrencyPK
//                            While @@FETCH_STATUS = 0
//                            BEGIN
//
//                            declare @AutoNo int
//                            Select @AutoNo = max(AutoNo) + 1 From FundJournalDetail where FundJournalPK = @FundJournalPK
//                            if isnull(@AutoNo,0) = 0 BEGIN  Select @AutoNo = isnull(max(AutoNo),0) + 1 From FundJournalDetail where FundJournalPK = @FundJournalPK END
//
//
//                            INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK],[FundClientPK]                
//                            ,[DetailDescription],[DebitCredit],[Amount],Debit,Credit,CurrencyRate,BaseDebit,BaseCredit,[LastUpdate]) 
//
//                            select @FundJournalPK,@AutoNo,1,2,@FundJournalAccountPK,@CurrencyPK,@FundPK,0,0,'',
//                            case when @Balance - @EndBalance > 0 then 'C' else 'D' end
//                            ,abs(@Balance-@EndBalance),case when @Balance - @EndBalance > 0 then 0 else abs(@Balance-@EndBalance) end,
//                            case when @Balance - @EndBalance > 0 then abs(@Balance-@EndBalance) else 0 end,1
//                            ,case when @Balance - @EndBalance > 0 then 0 else abs(@Balance-@EndBalance) end
//                            ,case when @Balance - @EndBalance > 0 then abs(@Balance-@EndBalance) else 0 end
//                            ,getdate() 
//                                         
//                            Fetch next From A Into @FundJournalAccountPK,@Type,@Balance,@EndBalance,@CurrencyPK
//                            END
//                            Close A
//                            Deallocate A
//                                          ";
//                        cmd.Parameters.AddWithValue("@UsersID", _userID);
//                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
//                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
//                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
//                        cmd.CommandTimeout = 0;
//                        cmd.ExecuteNonQuery();

//                    }

                    
//                    return "Success Save to Fund Journal";
//                }
//            }
//            catch (Exception err)
//            {
//                throw err;
//            }

//        }

        public List<AccountLedgerBalanceActivity> Get_DataAccountActivity(string _dateFrom, string _dateTo, string _id, int _paramStatus)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountLedgerBalanceActivity> L_AccountActivity = new List<AccountLedgerBalanceActivity>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _status = "";
                        if (_paramStatus == 1)
                        {
                            _status = "  A.Status = 2 and A.Posted = 1 and A.Reversed = 0 ";
                        }
                        else if (_paramStatus == 2)
                        {
                            _status = "  A.Status = 2 and A.Posted = 1 and A.Reversed = 1 ";
                        }
                        else if (_paramStatus == 3)
                        {
                            _status = "  A.Status = 2 and A.Posted = 0 and A.Reversed = 0 ";
                        }
                        else if (_paramStatus == 4)
                        {
                            _status = "  A.Status = 1  ";
                        }
                        else if (_paramStatus == 5)
                        {
                            _status = "  A.Status = 3  ";
                        }
                        else if (_paramStatus == 6)
                        {
                            _status = "  (A.Status = 2 or A.Posted = 1) and A.Reversed = 0 and A.status not in (3,4)  ";
                        }
                        else if (_paramStatus == 7)
                        {
                            _status = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Reversed = 0 and A.status not in (3,4)  ";
                        }

                        cmd.CommandText = @"
Declare @PeriodPK int
Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2

                        select A.ValueDate Date,C.Type AccountType,A.JournalPK,A.ValueDate,A.Reference,C.ID AccountID,C.Name AccountName,    
                        isnull(D.ID,'') CurrencyID,isnull(E.ID,'') OfficeID,isnull(F.ID,'') DepartmentID,isnull(G.ID,'') AgentID,isnull(H.ID,'') ConsigneeID,    
                        isnull(I.ID,'') InstrumentID,B.DetailDescription,B.DebitCredit,B.Amount,B.Debit,B.Credit,B.CurrencyRate Rate,    
                        B.BaseDebit,B.BaseCredit,[dbo].[FGetStartAccountBalance](dbo.FWorkingDay(@ValueDateTo,-30),B.AccountPK) StartBalance ,    
                        cast(substring(A.reference,1,charindex('/',A.reference,1) - 1) as integer) RefNo,
                        [dbo].[FGetStartAccountBalance](@ValuedateFrom,B.AccountPK) + SUM(B.BaseDebit - B.BaseCredit ) OVER(ORDER BY ValueDate 
                        ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS EndBalance       
                        from journal A    
                        left join journalDetail B on A.JournalPK = B.JournalPK and B.status in (1,2)    
                        left join Account C on B.AccountPK = C.AccountPK and C.status in (1,2)    
                        left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status in (1,2)    
                        left join Office E on B.OfficePK = E.OfficePK and E.status in (1,2)    
                        left join Department F on B.DepartmentPK = F.DepartmentPK and F.status in (1,2)    
                        left join Agent G on B.AgentPK = G.AgentPK and G.status in (1,2)    
                        left join Consignee H on B.consigneePK = H.ConsigneePK and H.status in (1,2)    
                        left join Instrument I on B.InstrumentPK = I.InstrumentPK and I.status in (1,2)    
                        Where A.ValueDate Between @ValueDateFrom and @ValueDateTo 
                        and A.PeriodPK = @PeriodPK and A.description <> 'PERIOD CLOSING' and C.ID = @ID
                        and " + _status + @"
                        ";
                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@ID", _id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AccountLedgerBalanceActivity M_AccountActivity = new AccountLedgerBalanceActivity();
                                    M_AccountActivity.Date = dr["Date"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Date"]);
                                    M_AccountActivity.AccountID = dr["AccountID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AccountID"]);
                                    M_AccountActivity.AccountName = dr["AccountName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AccountName"]);
                                    M_AccountActivity.Description = dr["DetailDescription"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["DetailDescription"]);
                                    M_AccountActivity.Debit = dr["BaseDebit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["BaseDebit"]);
                                    M_AccountActivity.Credit = dr["BaseCredit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["BaseCredit"]);
                                    M_AccountActivity.Balance = dr["EndBalance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["EndBalance"]);
                                    L_AccountActivity.Add(M_AccountActivity);
                                }

                            }
                            return L_AccountActivity;
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