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
    public class DormantFundTrailsReps
    {
        Host _host = new Host();

        string _insertCommand = "INSERT INTO [dbo].[DormantFundTrails] " +
                            "([DormantFundTrailsPK],[HistoryPK],[Status],[ValueDate],[BitDormant],[ActivateDate],[FundPK],[DormantDate],";
        string _paramaterCommand = "@ValueDate,@BitDormant,@ActivateDate,@FundPK,@DormantDate,";

        private DormantFundTrails setDormantFundTrails(SqlDataReader dr)
        {
            DormantFundTrails M_DormantFundTrails = new DormantFundTrails();
            M_DormantFundTrails.DormantFundTrailsPK = Convert.ToInt32(dr["DormantFundTrailsPK"]);
            M_DormantFundTrails.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_DormantFundTrails.Status = Convert.ToInt32(dr["Status"]);
            M_DormantFundTrails.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_DormantFundTrails.Notes = Convert.ToString(dr["Notes"]);
            M_DormantFundTrails.ValueDate = dr["ValueDate"].ToString();
            M_DormantFundTrails.BitDormant = Convert.ToBoolean(dr["BitDormant"]);
            M_DormantFundTrails.ActivateDate = dr["ActivateDate"].ToString();
            M_DormantFundTrails.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_DormantFundTrails.FundID = Convert.ToString(dr["FundID"]);
            M_DormantFundTrails.DormantDate = dr["DormantDate"].ToString();
            M_DormantFundTrails.EntryUsersID = dr["EntryUsersID"].ToString();
            M_DormantFundTrails.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_DormantFundTrails.VoidUsersID = dr["VoidUsersID"].ToString();
            M_DormantFundTrails.EntryTime = dr["EntryTime"].ToString();
            M_DormantFundTrails.ApprovedTime = dr["ApprovedTime"].ToString();
            M_DormantFundTrails.VoidTime = dr["VoidTime"].ToString();
            M_DormantFundTrails.DBUserID = dr["DBUserID"].ToString();
            M_DormantFundTrails.DBTerminalID = dr["DBTerminalID"].ToString();
            M_DormantFundTrails.LastUpdate = dr["LastUpdate"].ToString();
            M_DormantFundTrails.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_DormantFundTrails;
        }

        public List<DormantFundTrails> DormantFundTrails_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<DormantFundTrails> L_DormantFundTrails = new List<DormantFundTrails>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = @" Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID FundID,* from DormantFundTrails A
                                                    Left Join Fund B on A.FundPK = B.FundPK and B.status = 2
                                                    where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @" Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID FundID,* from DormantFundTrails A
                                                 Left Join Fund B on A.FundPK = B.FundPK and B.status = 2
                                               ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_DormantFundTrails.Add(setDormantFundTrails(dr));
                                }
                            }
                            return L_DormantFundTrails;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        #region Add
        //public int DormantFundTrails_Add(DormantFundTrails _DormantFundTrails, bool _havePrivillege)
        //{
        //    try
        //    {
        //        DateTime _datetimeNow = DateTime.Now;
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {
        //                if (_havePrivillege)
        //                {
        //                    cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
        //                         "Select isnull(max(DormantFundTrailsPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from DormantFundTrails";
        //                    cmd.Parameters.AddWithValue("@ApprovedUsersID", _DormantFundTrails.EntryUsersID);
        //                    cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
        //                }
        //                else
        //                {
        //                    cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
        //                        "Select isnull(max(DormantFundTrailsPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from DormantFundTrails";
        //                }
        //                cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
        //                cmd.Parameters.AddWithValue("@ValueDate", _DormantFundTrails.ValueDate);
        //                cmd.Parameters.AddWithValue("@BitDormant", _DormantFundTrails.BitDormant);
        //                cmd.Parameters.AddWithValue("@ActivateDate", _DormantFundTrails.ActivateDate);
        //                cmd.Parameters.AddWithValue("@FundPK", _DormantFundTrails.FundPK);
        //                cmd.Parameters.AddWithValue("@DormantDate", _DormantFundTrails.DormantDate);
        //                cmd.Parameters.AddWithValue("@EntryUsersID", _DormantFundTrails.EntryUsersID);
        //                cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
        //                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

        //                cmd.ExecuteNonQuery();

        //                return _host.Get_LastPKByLastUpate(_datetimeNow, "DormantFundTrails");
        //            }
        //        }

        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }

        //}
        #endregion

        #region Update
        //public int DormantFundTrails_Update(DormantFundTrails _DormantFundTrails, bool _havePrivillege)
        //{

        //    try
        //    {
        //        int _newHisPK;
        //        int status = _host.Get_Status(_DormantFundTrails.DormantFundTrailsPK, _DormantFundTrails.HistoryPK, "DormantFundTrails"); ;
        //        DateTime _datetimeNow = DateTime.Now;
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            if (_havePrivillege)
        //            {
        //                using (SqlCommand cmd = DbCon.CreateCommand())
        //                {
        //                    cmd.CommandText = "Update DormantFundTrails set status=2, Notes=@Notes,ValueDate=@ValueDate,BitDormant=@BitDormant,ActivateDate=@ActivateDate,FundPK=@FundPK,DormantDate=@DormantDate," +
        //                        "ApprovedUsersID=@ApprovedUsersID, " +
        //                        "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
        //                        "where DormantFundTrailsPK = @PK and historyPK = @HistoryPK";
        //                    cmd.Parameters.AddWithValue("@HistoryPK", _DormantFundTrails.HistoryPK);
        //                    cmd.Parameters.AddWithValue("@PK", _DormantFundTrails.DormantFundTrailsPK);
        //                    cmd.Parameters.AddWithValue("@Notes", _DormantFundTrails.Notes);
        //                    cmd.Parameters.AddWithValue("@ValueDate", _DormantFundTrails.ValueDate);
        //                    cmd.Parameters.AddWithValue("@BitDormant", _DormantFundTrails.BitDormant);
        //                    cmd.Parameters.AddWithValue("@ActivateDate", _DormantFundTrails.ActivateDate);
        //                    cmd.Parameters.AddWithValue("@FundPK", _DormantFundTrails.FundPK);
        //                    cmd.Parameters.AddWithValue("@DormantDate", _DormantFundTrails.DormantDate);
        //                    cmd.Parameters.AddWithValue("@UpdateUsersID", _DormantFundTrails.EntryUsersID);
        //                    cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
        //                    cmd.Parameters.AddWithValue("@ApprovedUsersID", _DormantFundTrails.EntryUsersID);
        //                    cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
        //                    cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

        //                    cmd.ExecuteNonQuery();
        //                }
        //                using (SqlCommand cmd = DbCon.CreateCommand())
        //                {
        //                    cmd.CommandText = "Update DormantFundTrails set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where DormantFundTrailsPK = @PK and status = 4";
        //                    cmd.Parameters.AddWithValue("@PK", _DormantFundTrails.DormantFundTrailsPK);
        //                    cmd.Parameters.AddWithValue("@VoidUsersID", _DormantFundTrails.EntryUsersID);
        //                    cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
        //                    cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
        //                    cmd.ExecuteNonQuery();
        //                }
        //                return 0;
        //            }
        //            else
        //            {
        //                if (status == 1)
        //                {
        //                    using (SqlCommand cmd = DbCon.CreateCommand())
        //                    {
        //                        cmd.CommandText = "Update DormantFundTrails set Notes=@Notes,ValueDate=@ValueDate,BitDormant=@BitDormant,ActivateDate=@ActivateDate,FundPK=@FundPK,DormantDate=@DormantDate," +
        //                        "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
        //                        "where DormantFundTrailsPK = @PK and historyPK = @HistoryPK";
        //                        cmd.Parameters.AddWithValue("@HistoryPK", _DormantFundTrails.HistoryPK);
        //                        cmd.Parameters.AddWithValue("@PK", _DormantFundTrails.DormantFundTrailsPK);
        //                        cmd.Parameters.AddWithValue("@Notes", _DormantFundTrails.Notes);
        //                        cmd.Parameters.AddWithValue("@ValueDate", _DormantFundTrails.ValueDate);
        //                        cmd.Parameters.AddWithValue("@BitDormant", _DormantFundTrails.BitDormant);
        //                        cmd.Parameters.AddWithValue("@ActivateDate", _DormantFundTrails.ActivateDate);
        //                        cmd.Parameters.AddWithValue("@FundPK", _DormantFundTrails.FundPK);
        //                        cmd.Parameters.AddWithValue("@DormantDate", _DormantFundTrails.DormantDate);
        //                        cmd.Parameters.AddWithValue("@UpdateUsersID", _DormantFundTrails.EntryUsersID);
        //                        cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
        //                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

        //                        cmd.ExecuteNonQuery();
        //                    }
        //                    return 0;
        //                }
        //                else
        //                {
        //                    using (SqlCommand cmd = DbCon.CreateCommand())
        //                    {
        //                        _newHisPK = _host.Get_NewHistoryPK(_DormantFundTrails.DormantFundTrailsPK, "DormantFundTrails");
        //                        cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
        //                        "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
        //                        "From DormantFundTrails where DormantFundTrailsPK =@PK and historyPK = @HistoryPK ";

        //                        cmd.Parameters.AddWithValue("@PK", _DormantFundTrails.DormantFundTrailsPK);
        //                        cmd.Parameters.AddWithValue("@HistoryPK", _DormantFundTrails.HistoryPK);
        //                        cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
        //                        cmd.Parameters.AddWithValue("@ValueDate", _DormantFundTrails.ValueDate);
        //                        cmd.Parameters.AddWithValue("@BitDormant", _DormantFundTrails.BitDormant);
        //                        cmd.Parameters.AddWithValue("@ActivateDate", _DormantFundTrails.ActivateDate);
        //                        cmd.Parameters.AddWithValue("@FundPK", _DormantFundTrails.FundPK);
        //                        cmd.Parameters.AddWithValue("@DormantDate", _DormantFundTrails.DormantDate);
        //                        cmd.Parameters.AddWithValue("@UpdateUsersID", _DormantFundTrails.EntryUsersID);
        //                        cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
        //                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
        //                        cmd.ExecuteNonQuery();
        //                    }

        //                    using (SqlCommand cmd = DbCon.CreateCommand())
        //                    {
        //                        cmd.CommandText = "Update DormantFundTrails set status= 4,Notes=@Notes," +
        //                        " LastUpdate=@lastupdate " +
        //                        " where DormantFundTrailsPK = @PK and historyPK = @HistoryPK";
        //                        cmd.Parameters.AddWithValue("@Notes", _DormantFundTrails.Notes);
        //                        cmd.Parameters.AddWithValue("@PK", _DormantFundTrails.DormantFundTrailsPK);
        //                        cmd.Parameters.AddWithValue("@HistoryPK", _DormantFundTrails.HistoryPK);
        //                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
        //                        cmd.ExecuteNonQuery();
        //                    }
        //                    return _newHisPK;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }

        //}
        #endregion

        public void DormantFundTrails_Approved(DormantFundTrails _DormantFundTrails)
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

                            declare @statusActivate int

                            select @statusActivate = isnull(BitDormant,0) from DormantFundTrails where DormantFundTrailsPK = @PK and historypk = @historyPK 

                            if @statusActivate = 0 
                            begin
                            
                                Declare @BeginDate datetime
                                declare @FundPK int
                                declare @MaxFundJournalPK int
                                declare @xPK int
                                Declare @PeriodPK int                  
                                declare @MaxPK int
                                declare @ActivateDate date

                                declare @tableFund table(
	                                FundPK int
                                )

                                CREATE TABLE #JournalHeader 
                                (
	                                FundJournalPK INT,
	                                HistoryPK INT,
	                                Status INT,
	                                Notes NVARCHAR(1000),
	                                PeriodPK INT,
	                                ValueDate DATETIME,
	                                Type nvarchar(50),
	                                TrxNo INT,
	                                TrxName nvarchar(150),
	                                Reference nvarchar(75),
	                                Description nvarchar(1500)
                                )

                                CREATE CLUSTERED INDEX indx_JournalHeader ON #JournalHeader (FundJournalPK);

                                CREATE TABLE #JournalDetail 
                                (
	                                FundJournalPK INT,
	                                AutoNo INT,
	                                HistoryPK BIGINT,
	                                Status INT,
	                                FundJournalAccountPK  INT,
	                                CurrencyPK INT,
	                                FundPK INT,
	                                FundClientPK INT,
	                                InstrumentPK INT,
	                                DetailDescription nvarchar(1500),
	                                DebitCredit char(1),
	                                Amount NUMERIC(19,4),
	                                Debit NUMERIC(19,4),
	                                Credit NUMERIC(19,4),
	                                CurrencyRate NUMERIC(19,4),
	                                BaseDebit NUMERIC(19,4),
	                                BaseCredit NUMERIC(19,4)
                                )

                                CREATE CLUSTERED INDEX indx_JournalDetail ON #JournalDetail (FundJournalPK,AutoNo);

                                insert into @tableFund(FundPK)
                                select FundPK from DormantFundTrails where status = 1 and DormantFundTrailsPK = @PK

                                select @ActivateDate = ActivateDate from DormantFundTrails where status = 1 and DormantFundTrailsPK = @PK
                                select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@ActivateDate,15) 
                                Select @PeriodPK = PeriodPK From Period where @ActivateDate Between DateFrom and DateTo and status = 2 

                                select @MaxFundJournalPK = max(FundJournalPK) from FundJournal
                                set @MaxFundJournalPK = isnull(@MaxFundJournalPK,0)

                                set @xPK = 1

                                DECLARE A CURSOR
                                FOR 
	                                SELECT FundPK from @tableFund
                                OPEN A;
 
                                FETCH NEXT FROM A INTO @FundPK
 
                                WHILE @@FETCH_STATUS = 0
	                                BEGIN

		                                if exists ( select * from DormantFundTrails where FundPK = @FundPK and BitDormant = 0 and ActivateDate = @ActivateDate and status in (1,2) )
		                                begin
			                                update A set A.Status = 3 from FundJournal A
			                                left join FundJournalDetail B on A.FundJournalPK = B.FundJournalPK 
			                                where A.Status = 2 and TrxName = 'ACTIVATE FUND FROM DORMANT'
			                                and B.FundPK = @FundPK
		                                end

		                                INSERT INTO #JournalHeader
						                                ( FundJournalPK ,
						                                  HistoryPK ,
						                                  Status ,
						                                  Notes ,
						                                  PeriodPK ,
						                                  ValueDate ,
						                                  Type ,
						                                  TrxNo ,
						                                  TrxName ,
						                                  Reference ,
						                                  Description
						                                )
		                                SELECT @xPK,1,2,''
		                                ,@PeriodPK,@ActivateDate,1,0,'ACTIVATE FUND FROM DORMANT','','FUND : ' + ID
		                                FROM Fund 
		                                WHERE FundPK = @FundPK and status = 2

		                                INSERT INTO #JournalDetail
					                                ( FundJournalPK ,
					                                  AutoNo ,
					                                  HistoryPK ,
					                                  Status ,
					                                  FundJournalAccountPK ,
					                                  CurrencyPK ,
					                                  FundPK ,
					                                  FundClientPK ,
					                                  InstrumentPK ,
					                                  DetailDescription ,
					                                  DebitCredit ,
					                                  Amount ,
					                                  Debit ,
					                                  Credit ,
					                                  CurrencyRate ,
					                                  BaseDebit ,
					                                  BaseCredit
					                                )
		                                SELECT @xPK,ROW_NUMBER() over (order by A.FundPK),1,2
		                                ,A.FundJournalAccountPK
		                                ,C.CurrencyPK,A.FundPK,0,0,'' 
		                                ,case when SUM(A.BaseDebit-A.BaseCredit) < 0 then 'D' else 'C' end
		                                ,abs(SUM(A.BaseDebit-A.BaseCredit))
		                                ,case when SUM(A.BaseDebit-A.BaseCredit) < 0 then abs(SUM(A.BaseDebit-A.BaseCredit)) else 0 end
		                                ,case when SUM(A.BaseDebit-A.BaseCredit) > 0 then abs(SUM(A.BaseDebit-A.BaseCredit)) else 0 end
		                                ,1
		                                ,case when SUM(A.BaseDebit-A.BaseCredit) < 0 then abs(SUM(A.BaseDebit-A.BaseCredit)) else 0 end
		                                ,case when SUM(A.BaseDebit-A.BaseCredit) > 0 then abs(SUM(A.BaseDebit-A.BaseCredit)) else 0 end
		                                FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK      
		                                INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)    
		                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)  
		                                WHERE  B.ValueDate between @BeginDate and @ActivateDate and B.posted=1 and B.Status = 2 AND B.Reversed = 0 and A.FundPK = @FundPK
		                                Group BY A.FundPK, A.FundJournalAccountPK, C.ParentPK1, C.ParentPK2,        
		                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
		                                C.ParentPK7, C.ParentPK8, C.ParentPK9, C.CurrencyPK     
		                                having SUM(A.BaseDebit-A.BaseCredit) <> 0
		                                order by FundPK,A.FundJournalAccountPK

		                                set @xPK = @xPK + 1

		                                FETCH NEXT FROM A INTO @FundPK
	                                END;
 
                                CLOSE A;
 
                                DEALLOCATE A;


                                Insert Into FundJournal(FundJournalPK,HistoryPK,Status,Notes,PeriodPK,ValueDate,Type,TrxNo,TrxName,Reference,Description,
                                Posted,PostedBy,PostedTime,EntryUsersID,EntryTime,LastUpdate)

                                select 	@MaxFundJournalPK + FundJournalPK,1,2,Notes,PeriodPK,ValueDate,Type,TrxNo,TrxName,Reference,Description,
                                1,@ApprovedUsersID,@Lastupdate,@ApprovedUsersID,@Lastupdate,@Lastupdate
                                from #JournalHeader


                                Insert Into FundJournalDetail(FundJournalPK,AutoNo,HistoryPK,Status,FundJournalAccountPK,CurrencyPK,FundPK,FundClientPK,InstrumentPK,DetailDescription,DebitCredit,
                                Amount,Debit,Credit,CurrencyRate,BaseDebit,BaseCredit,LastUsersID,LastUpdate)

                                select 	@MaxFundJournalPK + FundJournalPK,AutoNo,HistoryPK,2,FundJournalAccountPK,CurrencyPK,FundPK,FundClientPK,InstrumentPK,DetailDescription,DebitCredit,
                                Amount,Debit,Credit,CurrencyRate,BaseDebit,BaseCredit,@ApprovedUsersID,@LastUpdate
                                from #JournalDetail
                            end

                            update DormantFundTrails set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate 
                            where DormantFundTrailsPK = @PK and historypk = @historyPK
";
                        cmd.Parameters.AddWithValue("@PK", _DormantFundTrails.DormantFundTrailsPK);
                        cmd.Parameters.AddWithValue("@historyPK", _DormantFundTrails.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _DormantFundTrails.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update DormantFundTrails set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where DormantFundTrailsPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _DormantFundTrails.DormantFundTrailsPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _DormantFundTrails.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
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

        //8
        public void DormantFundTrails_Reject(DormantFundTrails _DormantFundTrails)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update DormantFundTrails set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where DormantFundTrailsPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _DormantFundTrails.DormantFundTrailsPK);
                        cmd.Parameters.AddWithValue("@historyPK", _DormantFundTrails.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _DormantFundTrails.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update DormantFundTrails set status= 2,LastUpdate=@LastUpdate  where DormantFundTrailsPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _DormantFundTrails.DormantFundTrailsPK);
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

        public void DormantFundTrails_Void(DormantFundTrails _DormantFundTrails)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update DormantFundTrails set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where DormantFundTrailsPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _DormantFundTrails.DormantFundTrailsPK);
                        cmd.Parameters.AddWithValue("@historyPK", _DormantFundTrails.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _DormantFundTrails.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
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


        public int DormantFundTrails_GenerateDormant(string _usersID, DateTime _dormantDate, DormantFundTrails _dft)
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

                        if (!_host.findString(_dft.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_dft.FundFrom))
                        {
                            _paramFund = "And FundPK in ( " + _dft.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                                declare @MaxPK int

                                select @MaxPK = max(DormantFundTrailsPK) from DormantFundTrails

                                set @MaxPK = isnull(@MaxPK,0)


                                insert into DormantFundTrails(DormantFundTrailsPK,HistoryPK,Status,ValueDate,BitDormant,ActivateDate,DormantDate,FundPK,EntryUsersID,EntryTime,LastUpdate)
                                select ROW_NUMBER() over (order by FundPK desc) + @MaxPK,1,1,cast(@LastUpdate as date),1,'1900-01-01',@DormantDate, FundPK,@UsersID,@LastUpdate,@LastUpdate 
                                from fund where status = 2" + _paramFund
                        ;
                        cmd.Parameters.AddWithValue("@DormantDate", _dormantDate);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
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

        public int DormantFundTrails_ActivateFund(string _usersID, DateTime _activateDate, DormantFundTrails _dft)
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

                        if (!_host.findString(_dft.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_dft.FundFrom))
                        {
                            _paramFund = "And FundPK in ( " + _dft.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                                declare @MaxPK int

                                select @MaxPK = max(DormantFundTrailsPK) from DormantFundTrails

                                set @MaxPK = isnull(@MaxPK,0)


                                insert into DormantFundTrails(DormantFundTrailsPK,HistoryPK,Status,ValueDate,BitDormant,ActivateDate,DormantDate,FundPK,EntryUsersID,EntryTime,LastUpdate)
                                select ROW_NUMBER() over (order by FundPK desc) + @MaxPK,1,1,cast(@LastUpdate as date),0,@ActivateDate,'1900-01-01', FundPK,@UsersID,@LastUpdate,@LastUpdate 
                                from fund where status = 2 " + _paramFund
                        ;
                        cmd.Parameters.AddWithValue("@ActivateDate", _activateDate);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
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

        public List<ActivateFund> ActivateFund_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ActivateFund> L_ActivateFund = new List<ActivateFund>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            declare @ListDormantFund table (
	                            FundPK int
                            )

                            insert into @ListDormantFund
                            select distinct fundpk from DormantFundTrails where BitDormant = 1 and isnull(Dormantdate,'1900-01-01') <> '1900-01-01' and status = 2
                            and fundpk not in (select distinct fundpk from DormantFundTrails where isnull(BitDormant,0) = 0 and isnull(ActivateDate,'1900-01-01') <> '1900-01-01')

                            SELECT A.FundPK,ID, Name,1 Status FROM @ListDormantFund A
                            left join Fund B on A.FundPK = B.FundPK
                            where status = 2 order by ID,Name
";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ActivateFund M_ActivateFund = new ActivateFund();
                                    M_ActivateFund.FundPK = Convert.ToInt32(dr["FundPK"]);
                                    M_ActivateFund.ID = Convert.ToString(dr["ID"]);
                                    M_ActivateFund.Name = Convert.ToString(dr["Name"]);
                                    M_ActivateFund.StatusDormant = Convert.ToBoolean(dr["Status"]);
                                    L_ActivateFund.Add(M_ActivateFund);
                                }

                            }
                            return L_ActivateFund;
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
