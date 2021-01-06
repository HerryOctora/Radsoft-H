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
    public class FAAdjustmentReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FAAdjustment] " +
                            "([FAAdjustmentPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Reference],[Description],";

        string _paramaterCommand = "@PeriodPK,@ValueDate,@Reference,@Description,";


        string _paramFAAdjustmentDetail = "INSERT INTO [FAAdjustmentDetail] " +
                                    "([FAAdjustmentPK],[AutoNo],[HistoryPK],[Status],[FundPK],[FACOAAdjustmentPK],[FundJournalAccountPK], " +
                                    "[DebitCredit],[Amount],[LastUsersID],LastUpdate) \n ";

        string _paramFAAdjustment = "INSERT INTO [FAAdjustment] " +
                                    "([FAAdjustmentPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate] " +
                                    ",[Reference],[Description],[Posted],[PostedBy] " +
                                    ",[PostedTime],[Revised],[RevisedBy],[RevisedTime] " +
                                    ",[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime] " +
                                    ",[ApprovedUsersID],[ApprovedTime],[VoidUsersID],[VoidTime],[DBUserID],LastUpdate) ";
        //2
        private FAAdjustment setFAAdjustment(SqlDataReader dr)
        {
            FAAdjustment M_FAAdjustment = new FAAdjustment();
            M_FAAdjustment.FAAdjustmentPK = Convert.ToInt32(dr["FAAdjustmentPK"]);
            M_FAAdjustment.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FAAdjustment.Selected = Convert.ToBoolean(dr["Selected"]);
            M_FAAdjustment.Status = Convert.ToInt32(dr["Status"]);
            M_FAAdjustment.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FAAdjustment.Notes = Convert.ToString(dr["Notes"]);
            M_FAAdjustment.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_FAAdjustment.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_FAAdjustment.ValueDate = dr["ValueDate"].ToString();
            M_FAAdjustment.RefNo = Convert.ToInt32(dr["RefNo"]);
            M_FAAdjustment.Reference = Convert.ToString(dr["Reference"]);
            M_FAAdjustment.Description = Convert.ToString(dr["Description"]);
            M_FAAdjustment.Posted = Convert.ToBoolean(dr["Posted"]);
            M_FAAdjustment.PostedBy = Convert.ToString(dr["PostedBy"]);
            M_FAAdjustment.PostedTime = Convert.ToString(dr["PostedTime"]);
            M_FAAdjustment.Revised = Convert.ToBoolean(dr["Revised"]);
            M_FAAdjustment.RevisedBy = Convert.ToString(dr["RevisedBy"]);
            M_FAAdjustment.RevisedTime = Convert.ToString(dr["RevisedTime"]);
            M_FAAdjustment.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FAAdjustment.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FAAdjustment.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FAAdjustment.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FAAdjustment.EntryTime = dr["EntryTime"].ToString();
            M_FAAdjustment.UpdateTime = dr["UpdateTime"].ToString();
            M_FAAdjustment.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FAAdjustment.VoidTime = dr["VoidTime"].ToString();
            M_FAAdjustment.DBUserID = dr["DBUserID"].ToString();
            M_FAAdjustment.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FAAdjustment.LastUpdate = dr["LastUpdate"].ToString();
            M_FAAdjustment.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FAAdjustment;
        }

        //3
        public List<FAAdjustment> FAAdjustment_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FAAdjustment> L_FAAdjustment = new List<FAAdjustment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,B.ID PeriodID,* From FAAdjustment A Left join " +
                                "Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 " +
                                "Where A.Status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,B.ID PeriodID,* From FAAdjustment A Left join " +
                             "Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 ";

                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FAAdjustment.Add(setFAAdjustment(dr));
                                }
                            }
                            return L_FAAdjustment;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<FAAdjustment> FAAdjustment_SelectFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FAAdjustment> L_FAAdjustment = new List<FAAdjustment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select  case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,A.Reference,B.ID PeriodID,* From FAAdjustment A Left join " +
                                        "Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 " +
                                        "Where  A.Status = @Status and ValueDate between @DateFrom and @DateTo Order By RefNo ";
                            cmd.Parameters.AddWithValue("@Status", _status);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        }
                        else
                        {
                            cmd.CommandText = "Select  case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,A.Reference,B.ID PeriodID,* From FAAdjustment A Left join " +
                                      "Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 " +
                                      "Where  ValueDate between @DateFrom and @DateTo Order By RefNo ";
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FAAdjustment.Add(setFAAdjustment(dr));
                                }
                            }
                            return L_FAAdjustment;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //4
        public FAAdjustment FAAdjustment_SelectByFAAdjustmentPK(int _FAAdjustmentPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "Select B.ID PeriodID,* From FAAdjustment A Left join " +
                                "Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 " +
                                "Where A.FAAdjustmentPK = @FAAdjustmentPK and A.Status = 4 ";
                        cmd.Parameters.AddWithValue("@FAAdjustmentPK", _FAAdjustmentPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setFAAdjustment(dr);
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

        //5
        public FAAdjustmentAddNew FAAdjustment_Add(FAAdjustment _FAAdjustment, bool _havePrivillege)
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
                            cmd.CommandText = "Declare @newFAAdjustmentPK int \n " +
                                 "Select @newFAAdjustmentPK = isnull(max(FAAdjustmentPk),0) + 1 from FAAdjustment \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate) " +
                                 "Select @newFAAdjustmentPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate \n " +
                                 "Select @newFAAdjustmentPK newFAAdjustmentPK, 1 newHistoryPK ";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FAAdjustment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = "Declare @newFAAdjustmentPK int \n " +
                                 "Select @newFAAdjustmentPK = isnull(max(FAAdjustmentPk),0) + 1 from FAAdjustment \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select @newFAAdjustmentPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate \n " +
                                 "Select @newFAAdjustmentPK newFAAdjustmentPK, 1 newHistoryPK ";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@PeriodPK", _FAAdjustment.PeriodPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _FAAdjustment.ValueDate);
                        cmd.Parameters.AddWithValue("@Reference", _FAAdjustment.Reference);
                        cmd.Parameters.AddWithValue("@Description", _FAAdjustment.Description);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FAAdjustment.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new FAAdjustmentAddNew()
                                {
                                    FAAdjustmentPK = Convert.ToInt32(dr["newFAAdjustmentPK"]),
                                    HistoryPK = Convert.ToInt32(dr["newHistoryPK"]),
                                    Message = "Insert FAAdjustment Header Success"
                                };
                            }
                            else
                            {
                                return null;
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

        //6
        public int FAAdjustment_Update(FAAdjustment _FAAdjustment, bool _havePrivillege)
        {
            int _newHisPK;
            int _historyPK;
            int _status;
            try
            {
                _historyPK = _host.Get_NewHistoryPK(_FAAdjustment.FAAdjustmentPK, "FAAdjustment");
                _status = _host.Get_Status(_FAAdjustment.FAAdjustmentPK, _FAAdjustment.HistoryPK, "FAAdjustment");
            }
            catch (Exception err)
            {
                throw err;
            }
            using (SqlConnection DbCon = new SqlConnection(Tools.conString))
            {
                DbCon.Open();
                SqlTransaction sqlT;
                sqlT = DbCon.BeginTransaction("FAAdjustmentUpdate");
                try
                {
                    DateTime _datetimeNow = DateTime.Now;
                    if (_havePrivillege)
                    {

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.Transaction = sqlT;
                            cmd.CommandText = "Update FAAdjustment set Notes=@Notes,PeriodPK=@PeriodPK,ValueDate=@ValueDate," +
                                "Reference=@Reference,Description=@Description," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where FAAdjustmentPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _FAAdjustment.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FAAdjustment.FAAdjustmentPK);
                            cmd.Parameters.AddWithValue("@Notes", _FAAdjustment.Notes);
                            cmd.Parameters.AddWithValue("@PeriodPK", _FAAdjustment.PeriodPK);
                            cmd.Parameters.AddWithValue("@ValueDate", _FAAdjustment.ValueDate);
                            cmd.Parameters.AddWithValue("@Reference", _FAAdjustment.Reference);
                            cmd.Parameters.AddWithValue("@Description", _FAAdjustment.Description);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FAAdjustment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FAAdjustment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Approvedtime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.Transaction = sqlT;
                            cmd.CommandText = "Update FAAdjustment " +
                                "Set Notes = @Notes,Status = 3, " +
                                "VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastupdate " +
                                "Where FAAdjustmentPk = @PK and status = 4 ";
                            cmd.Parameters.AddWithValue("@PK", _FAAdjustment.FAAdjustmentPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _FAAdjustment.HistoryPK);
                            cmd.Parameters.AddWithValue("@Notes", _FAAdjustment.Notes);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FAAdjustment.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        sqlT.Commit();
                        return 0;
                    }
                    else
                    {
                        if (_status == 1)
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.Transaction = sqlT;
                                cmd.CommandText = "Update FAAdjustment set Notes=@Notes,PeriodPK=@PeriodPK,ValueDate=@ValueDate," +
                                    "Reference=@Reference,Description=@Description," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                    "where FAAdjustmentPK = @PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _FAAdjustment.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FAAdjustment.FAAdjustmentPK);
                                cmd.Parameters.AddWithValue("@Notes", _FAAdjustment.Notes);
                                cmd.Parameters.AddWithValue("@PeriodPK", _FAAdjustment.PeriodPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _FAAdjustment.ValueDate);
                                cmd.Parameters.AddWithValue("@Reference", _FAAdjustment.Reference);
                                cmd.Parameters.AddWithValue("@Description", _FAAdjustment.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FAAdjustment.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            sqlT.Commit();
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.Transaction = sqlT;
                                _newHisPK = _host.Get_NewHistoryPK(_FAAdjustment.FAAdjustmentPK, "FAAdjustment");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FAAdjustment where FAAdjustmentPK =@PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _FAAdjustment.FAAdjustmentPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FAAdjustment.HistoryPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _FAAdjustment.PeriodPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _FAAdjustment.ValueDate);
                                cmd.Parameters.AddWithValue("@Reference", _FAAdjustment.Reference);
                                cmd.Parameters.AddWithValue("@Description", _FAAdjustment.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FAAdjustment.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }


                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                cmd.Transaction = sqlT;
                                cmd.CommandText = "Update FAAdjustment " +
                                    "Set Notes = @Notes,Status = 4, " +
                                    "LastUpdate=@lastupdate " +
                                    "Where FAAdjustmentPk = @PK and HistoryPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _FAAdjustment.FAAdjustmentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FAAdjustment.HistoryPK);
                                cmd.Parameters.AddWithValue("@Notes", _FAAdjustment.Notes);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            sqlT.Commit();
                            return _newHisPK;
                        }
                    }

                }
                catch (Exception err)
                {
                    sqlT.Rollback();
                    throw err;
                }
            }
        }


        //7
        public void FAAdjustment_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                    Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2      
                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)      
                     Select @Time,@PermissionID,'FAAdjustment',FAAdjustmentPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  
                     from FAAdjustment where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1    
                     update FAAdjustment set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                     where FAAdjustmentPK in ( Select FAAdjustmentPK from FAAdjustment where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 )  and Status = 1 and Selected  = 1    
                     Update FAAdjustment set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  
                     where FAAdjustmentPK in (Select FAAdjustmentPK from FAAdjustment where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1)   and Status = 4 and Selected  = 1 

                    ";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void FAAdjustment_PostingBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                         Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2      
                         Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)      
                         Select @LastUpdate,@PermissionID,'FAAdjustment',FAAdjustmentPK,1,'Posting by Selected Data',@UsersID,@IPAddress,
                         @LastUpdate  from FAAdjustment where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Revised = 0 and Selected  = 1    


                        declare @FundJournalPK int
                        declare @FAAdjustmentPK int
                        declare @FundPK int
                        declare @FundJournalAccountPK int
                        declare @DebitCredit nvarchar(1)
                        declare @Amount numeric(22,4)


                        DECLARE A CURSOR FOR 
                        select FAAdjustmentPK from FAAdjustment 
                        where ValueDate between @DateFrom and @DateTo and Selected = 1

                        Open A
                        Fetch Next From A
                        Into @FAAdjustmentPK


                        While @@FETCH_STATUS = 0
                        BEGIN

		                        select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal

		                        INSERT INTO [FundJournal]([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate])                  

		                        Select  @FundJournalPK, 1,2,'Posting FA Adjustment No: ' + CAST(FAAdjustmentPK as nvarchar(15)),PeriodPK,ValueDate,5,FAAdjustmentPK,'FA Adjustment',                  
		                        Reference,'',1,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from FAAdjustment where FAAdjustmentPK = @FAAdjustmentPK

		                        DECLARE B CURSOR FOR 
		                        select FundPK,FundJournalAccountPK,DebitCredit,Amount from FAAdjustmentDetail A
		                        left join  FAAdjustment B on A.FAAdjustmentPK = B.FAAdjustmentPK and B.status = 2  
		                        where ValueDate between @DateFrom and @DateTo and Selected = 1 and B.FAAdjustmentPK = @FAAdjustmentPK

		                        Open B
		                        Fetch Next From B
		                        Into @FundPK,@FundJournalAccountPK,@DebitCredit,@Amount


		                        While @@FETCH_STATUS = 0
		                        BEGIN

		                        INSERT INTO [FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[InstrumentPK],[FundClientPK]                  
		                        ,[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                  

		                        Select  @FundJournalPK,1,1,2,@FundJournalAccountPK,1,@FundPK,0,0,'FA Adjustment No : ' + CAST(@FAAdjustmentPK as nvarchar(15)),@DebitCredit,@Amount,case when @DebitCredit = 'D' then @Amount else 0 end,                   
		                        case when @DebitCredit = 'C' then @Amount else 0 end,1,case when @DebitCredit = 'D' then @Amount else 0 end, case when @DebitCredit = 'C' then @Amount else 0 end,@LastUpdate             

		                        Fetch next From B Into @FundPK,@FundJournalAccountPK,@DebitCredit,@Amount
		                        END
		                        Close B
		                        Deallocate B 

                        Fetch next From A Into @FAAdjustmentPK
                        END
                        Close A
                        Deallocate A 


                        update FAAdjustment set Posted = 1,PostedBy = @UsersID,PostedTime = @LastUpdate,LastUpdate=@LastUpdate 
                        where FAAdjustmentPK in ( Select FAAdjustmentPK from FAAdjustment where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Revised = 0 and Selected  = 1 )      
                        and Status = 2 and Posted = 0 and Revised = 0 and Selected  = 1




                        ";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
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

        public void FAAdjustment_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2      
                    Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)      
                 Select @Time,@PermissionID,'FAAdjustment',FAAdjustmentPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  
                 from FAAdjustment where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1    
                   update FAAdjustment set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time 
                   where FAAdjustmentPK in ( Select FAAdjustmentPK from FAAdjustment where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 )      and Status = 1 and Selected  = 1
                 Update FAAdjustment set status= 2  where FAAdjustmentPK in (Select FAAdjustmentPK from FAAdjustment where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1)  and Status = 4 and Selected  = 1 

                ";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void FAAdjustment_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                    Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2      
                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)      
                     Select @Time,@PermissionID,'FAAdjustment',FAAdjustmentPK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  
                     from FAAdjustment where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Revised = 0 and Selected  = 1    
                       update FAAdjustment set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time 
                       where FAAdjustmentPK in ( Select FAAdjustmentPK from FAAdjustment where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Revised = 0 and Selected  = 1 )    
                       and Status = 2 and Posted = 0 and Reversed = 0 and Selected  = 1  

                    ";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        //public void FAAdjustment_ReverseBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        //{
        //    try
        //    {
        //        DateTime _datetimeNow = DateTime.Now;
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {
        //                cmd.CommandText = "Declare @FAAdjustmentPK int Declare @MaxFAAdjustmentPK int Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
        //                                  " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
        //                                  "Select @Time,@PermissionID,'FAAdjustment',FAAdjustmentPK,1,'Reverse by Selected Data',@UsersID,@IPAddress,@Time  from FAAdjustment where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 1 and Reversed = 0 and Selected  = 1 \n " +
        //                                  "Declare A Cursor For Select FAAdjustmentPK from FAAdjustment where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 1 and Selected  = 1 Open A Fetch Next From A Into @FAAdjustmentPK \n " +
        //                                  "While @@FETCH_STATUS = 0 Begin \n " +
                                          
        //                                  " select @maxFAAdjustmentPK = isnull(MAX(FAAdjustmentPK),0) + 1 from FAAdjustment \n " +
        //                                    _paramFAAdjustment +
        //                                " \n " +
        //                                "select @maxFAAdjustmentPK,[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate] " +
        //                                            ",[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy] " +
        //                                            ",[PostedTime],1,@FAAdjustmentPK,@UsersID,@Time " +
        //                                            ",[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime] " +
        //                                            ",[ApprovedUsersID],[ApprovedTime],[VoidUsersID],[VoidTime],@UsersID,@Time from FAAdjustment where FAAdjustmentPK = @FAAdjustmentPK and status = 2 and Posted = 1 and Reversed = 0 and Selected  = 1 " +
        //                                " \n " +
        //                                _paramFAAdjustmentDetail +
        //                                " \n " +
        //                                "select @maxFAAdjustmentPK,AutoNo,HistoryPK,[Status],[AccountPK],[CurrencyPK],[OfficePK] " +
        //                                            ",[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription] " +
        //                                            ",[DocRef],Case when [DebitCredit] = 'D' then 'C' else 'D' end " +
        //                                            ",[Amount],Case when [DebitCredit] = 'D' then 0 else Amount end " +
        //                                            ",Case when [DebitCredit] = 'D' then Amount else 0 end " +
        //                                            ",[CurrencyRate],Case when [DebitCredit] = 'D' then 0 else (Amount * CurrencyRate) end " +
        //                                            ",Case when [DebitCredit] = 'D' then (Amount * CurrencyRate) else 0 end ,@UsersID,@Time  from FAAdjustmentDetail " +
        //                                            " where FAAdjustmentPK = @FAAdjustmentPK  " +
        //                                 "UPDATE FAAdjustment SET Reversed = 1, ReverseNo = @maxFAAdjustmentPK, ReversedBy = @UsersID, ReversedTime = @Time,LastUpdate = @Time where FAAdjustmentPK = @FAAdjustmentPK and Status = 2 and Posted = 1 and Reversed = 0 and Selected  = 1 \n " +
        //                                  "FETCH NEXT FROM A INTO @FAAdjustmentPK End Close A Deallocate A " +
        //                                  " " +
        //                                  "";
        //                cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
        //                cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
        //                cmd.Parameters.AddWithValue("@DateTo", _dateTo);
        //                cmd.Parameters.AddWithValue("@UsersID", _usersID);
        //                cmd.Parameters.AddWithValue("@Time", _datetimeNow);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }

        //}

        //7
        public void FAAdjustment_Approved(FAAdjustment _FAAdjustment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FAAdjustment set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                        "where FAAdjustmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FAAdjustment.FAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FAAdjustment.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FAAdjustment.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FAAdjustment set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where FAAdjustmentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FAAdjustment.FAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FAAdjustment.ApprovedUsersID);
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
        public void FAAdjustment_Reject(FAAdjustment _FAAdjustment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FAAdjustment set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                        "where FAAdjustmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FAAdjustment.FAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FAAdjustment.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FAAdjustment.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FAAdjustment set status= 2,LastUpdate=@lastUpdate where FAAdjustmentPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FAAdjustment.FAAdjustmentPK);
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

        //9
        public void FAAdjustment_Void(FAAdjustment _FAAdjustment)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FAAdjustment set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate " +
                        "where FAAdjustmentPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FAAdjustment.FAAdjustmentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FAAdjustment.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FAAdjustment.VoidUsersID);
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

   

        //10

        //public void FAAdjustment_Posting(FAAdjustment _FAAdjustment)
        //{
        //    try
        //    {
        //        DateTime _datetimeNow = DateTime.Now;
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {
        //                //Posting FAAdjustment By Type
        //                if (_FAAdjustment.ParamMode == "By FAAdjustment From To")
        //                {

        //                    cmd.CommandText = " Update FAAdjustment Set Posted = 1,PostedBy=@PostedBy,PostedTime = @PostedTime,LastUpdate = @LastUpdate " +
        //                                      " where FAAdjustmentPK between @PostFAAdjustmentFrom and @PostFAAdjustmentTo and PeriodPK = @PeriodPK  " +
        //                                      " and Posted = 0 and Reversed = 0 and status = 2 ";
        //                    cmd.Parameters.AddWithValue("@PeriodPK", _FAAdjustment.ParamPeriod);
        //                    cmd.Parameters.AddWithValue("@PostFAAdjustmentFrom", _FAAdjustment.ParamPostFAAdjustmentFrom);
        //                    cmd.Parameters.AddWithValue("@PostFAAdjustmentTo", _FAAdjustment.ParamPostFAAdjustmentTo);

        //                }
        //                else if (_FAAdjustment.ParamMode == "By Date From To")
        //                {

        //                    cmd.CommandText = " Update FAAdjustment Set Posted = 1,PostedBy=@PostedBy,PostedTime =@PostedTime,LastUpdate=@LastUpdate " +
        //                                      " where ValueDate between @PostDateFrom and @PostDateTo and PeriodPK = @PeriodPK " +
        //                                      " and Posted = 0 and Reversed = 0 and status = 2 ";
        //                    cmd.Parameters.AddWithValue("@PeriodPK", _FAAdjustment.ParamPeriod);
        //                    cmd.Parameters.AddWithValue("@PostDateFrom", _FAAdjustment.ParamPostDateFrom);
        //                    cmd.Parameters.AddWithValue("@PostDateTo", _FAAdjustment.ParamPostDateTo);
        //                }

        //                else
        //                {
        //                    cmd.CommandText = " Update FAAdjustment Set Posted = 1,PostedBy=@PostedBy,PostedTime =@PostedTime,LastUpdate=@LastUpdate " +
        //                                      " where FAAdjustmentPK = @FAAdjustmentPK and PeriodPK = @PeriodPK " +
        //                                      " and Posted = 0 and Reversed = 0 and status = 2 ";
        //                    cmd.Parameters.AddWithValue("@FAAdjustmentPK", _FAAdjustment.FAAdjustmentPK);
        //                    cmd.Parameters.AddWithValue("@PeriodPK", _FAAdjustment.PeriodPK);
        //                }

        //                cmd.Parameters.AddWithValue("@PostedBy", _FAAdjustment.PostedBy);
        //                cmd.Parameters.AddWithValue("@PostedTime", _datetimeNow);
        //                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }
        //}

        //public void FAAdjustment_Reversed(FAAdjustment _FAAdjustment)
        //{
        //    try
        //    {
        //        DateTime _datetimeNow = DateTime.Now;
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {
        //                cmd.CommandText =
        //                "Declare @maxFAAdjustmentPK int " +
        //                " \n " +
        //                "select @maxFAAdjustmentPK = isnull(MAX(FAAdjustmentPK),0) + 1 from FAAdjustment " +
        //                " \n " +
        //                "select * from FAAdjustment  where FAAdjustmentPK=@FAAdjustmentPK and PeriodPK = @PeriodPK " +
        //                " \n " +
        //                "if @@RowCount > 0 " +
        //                "Begin " +
        //                " \n " +
        //                "UPDATE FAAdjustment SET Reversed = 1, ReverseNo = @maxFAAdjustmentPK, ReversedBy = @ReversedBy, ReversedTime = @ReversedTime where FAAdjustmentPK = @FAAdjustmentPK and PeriodPK = @PeriodPK " +
        //                " \n " +
        //                _paramFAAdjustment +
        //                " \n " +
        //                "select @maxFAAdjustmentPK,[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate] " +
        //                            ",[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy] " +
        //                            ",[PostedTime],1,@FAAdjustmentPK,@ReversedBy,@ReversedTime " +
        //                            ",[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime] " +
        //                            ",[ApprovedUsersID],[ApprovedTime],[VoidUsersID],[VoidTime],@ReversedBy,@LastUpdate from FAAdjustment where FAAdjustmentPK = @FAAdjustmentPK and status = 2 " +
        //                " \n " +
        //                _paramFAAdjustmentDetail +
        //                " \n " +
        //                "select @maxFAAdjustmentPK,AutoNo,HistoryPK,[Status],[AccountPK],[CurrencyPK],[OfficePK] " +
        //                            ",[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription] " +
        //                            ",[DocRef],Case when [DebitCredit] = 'D' then 'C' else 'D' end " +
        //                            ",[Amount],Case when [DebitCredit] = 'D' then 0 else Amount end " +
        //                            ",Case when [DebitCredit] = 'D' then Amount else 0 end " +
        //                            ",[CurrencyRate],Case when [DebitCredit] = 'D' then 0 else (Amount * CurrencyRate) end " +
        //                            ",Case when [DebitCredit] = 'D' then (Amount * CurrencyRate) else 0 end ,@ReversedBy,@LastUpdate  from FAAdjustmentDetail " +
        //                            " where FAAdjustmentPK = @FAAdjustmentPK   " +
        //                 "end ";

        //                cmd.Parameters.AddWithValue("@FAAdjustmentPK", _FAAdjustment.FAAdjustmentPK);
        //                cmd.Parameters.AddWithValue("@PeriodPK", _FAAdjustment.PeriodPK);
        //                cmd.Parameters.AddWithValue("@ReversedBy", _FAAdjustment.ReversedBy);
        //                cmd.Parameters.AddWithValue("@ReversedTime", _datetimeNow);
        //                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
        //                cmd.ExecuteNonQuery();
        //            }

        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }

        //}
      

        public List<FAAdjustment> Get_ReferenceCombo(DateTime _valuedateFrom, DateTime _valuedateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FAAdjustment> L_FAAdjustment = new List<FAAdjustment>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = " select distinct(Reference) Reference,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo from FAAdjustment " +
                                          " where valuedate between @ValueDateFrom and @ValueDateTo " +
                                          " order by Refno ";


                        cmd.Parameters.AddWithValue("@ValueDateFrom", _valuedateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _valuedateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FAAdjustment M_FAAdjustment = new FAAdjustment();
                                    M_FAAdjustment.Reference = Convert.ToString(dr["Reference"]);
                                    L_FAAdjustment.Add(M_FAAdjustment);
                                }
                            }
                            return L_FAAdjustment;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public List<NavForecast> FAAdjustment_GenerateNavForecast(DateTime _date)
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<NavForecast> L_Model = new List<NavForecast>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"  
                 
                        Declare @FundPK  int
                        Declare @StandartFundAdmin int
                        declare @WorkingDate datetime
                        select @WorkingDate =  dbo.FWorkingDay(@ValueDate, - 1)

                        CREATE TABLE #TotalAUM
                        (Valuedate datetime,FundPK int,AUM numeric (22,2))
                        CREATE TABLE #TotalUnit
                        (FundPK int,Unit numeric (22,4))
                        CREATE TABLE #NAVForecast
                        (Valuedate datetime,FundName nvarchar(50),NAV numeric (22,8),AUM numeric (22,2))

                        DECLARE A CURSOR FOR 
                        Select FundPK,StandartFundAdmin from Fund A
                        where A.status  = 2 

                        Open A
                        Fetch Next From A
                        Into @FundPK,@StandartFundAdmin

                        Declare @FundJournalAccountPK int

                        While @@FETCH_STATUS = 0
                        BEGIN
                        insert into #TotalUnit (FundPK,Unit)
                        select @FundPK,case when isnull([dbo].[FGetTotalUnitByFundPK](@WorkingDate,@FundPK),0) = 0 then 1 else isnull([dbo].[FGetTotalUnitByFundPK](@WorkingDate,@FundPK),0) end

                        IF (@StandartFundAdmin = 1)
                        BEGIN
                        Insert Into #TotalAUM (Valuedate,FundPK,AUM)
                        Select Valuedate,FundPK, sum(Amount) from (
                        Select @Valuedate Valuedate,@FundPK FundPK,sum(basedebit-basecredit) Amount from FundJournalDetail A
                        left join FundjournalAccount B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.Status= 2
                        left join FundJournal C on A.FundJournalPK = C.FundJournalPK and C.Status= 2 and C.Posted = 1 and C.Reversed = 0
                        where FundPK = @FundPK and A.Status  = 2 and DetailDescription <> 'ADJUST' and Valuedate <= @ValueDate
                        and B.Type in (1,2) and B.ID not like '202%'
                        union all
                        select @Valuedate Valuedate,@FundPK FundPK,sum(case when DebitCredit = 'D' then Amount else Amount * -1 End) Amount  from FAAdjustmentDetail A 
                        left join FAAdjustment B on A.FAAdjustmentPK = B.FAAdjustmentPK
                        left join FundjournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK and C.Status= 2
                        where B.Status in (1,2) and ValueDate = @ValueDate and FundPK = @FundPK and C.Type in (1,2))A
                        Group By Valuedate,A.FundPK
                        END
                        ELSE
                        BEGIN
                        Insert Into #TotalAUM (Valuedate,FundPK,AUM)
                        Select Valuedate,FundPK, sum(Amount) from (
                        Select @Valuedate Valuedate,@FundPK FundPK,sum(basedebit-basecredit) Amount from FundJournalDetail A
                        left join FundjournalAccount B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.Status= 2
                        left join FundJournal C on A.FundJournalPK = C.FundJournalPK and C.Status= 2 and C.Posted = 1 and C.Reversed = 0
                        where FundPK = @FundPK and A.Status  = 2 and DetailDescription <> 'ADJUST' and Valuedate <= @ValueDate and A.FundJournalAccountPK not in (109,110)
                        union all
                        select @Valuedate Valuedate,@FundPK FundPK,sum(case when DebitCredit = 'D' then Amount else Amount * - 1 End) Amount from FAAdjustmentDetail A 
                        left join FAAdjustment B on A.FAAdjustmentPK = B.FAAdjustmentPK
                        where B.Status in (1,2) and ValueDate = @ValueDate  and FundPK = @FundPK)A
                        Group By Valuedate,A.FundPK                     
                        END


                        Fetch next From A Into @FundPK,@StandartFundAdmin
                        END
                        Close A
                        Deallocate A 


                        insert into #NavForecast (ValueDate,FundName,NAV,AUM)

                        select @Valuedate,C.Name FundName,isnull(Sum(AUM/Unit),0) Nav,isnull(A.AUM,0) from #TotalAUM A 
                        left join #TotalUnit B on A.FundPK = B.FundPK
                        left join Fund C on A.FundPK = C.FundPK and C.Status = 2
                        Group By A.ValueDate,C.Name,A.AUM

                        select * from #NavForecast


                        ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Model.Add(setNavForecast(dr));
                                }
                            }
                            return L_Model;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private NavForecast setNavForecast(SqlDataReader dr)
        {
            NavForecast M_Model = new NavForecast();
            M_Model.ValueDate = Convert.ToDateTime(dr["ValueDate"]);
            M_Model.FundName = dr["FundName"].ToString();
            M_Model.Nav = Convert.ToDecimal(dr["Nav"]);
            M_Model.AUM = Convert.ToDecimal(dr["AUM"]);

            return M_Model;
        }


        public List<TBForecast> FAAdjustment_GenerateTBForecast(DateTime _date, int _fundPK)
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TBForecast> L_Model = new List<TBForecast>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"  

                        Declare @StandartFundAdmin int

                        CREATE TABLE #TBForecast
                        (Valuedate datetime,FundName nvarchar(100),AccountID nvarchar(100),AccountName nvarchar(100),Amount numeric (22,2))

                        DECLARE A CURSOR FOR 
                        Select FundPK,StandartFundAdmin from Fund A
                        where A.status  = 2 and A.FundPK = @FundPK

                        Open A
                        Fetch Next From A
                        Into @FundPK,@StandartFundAdmin

                        Declare @FundJournalAccountPK int

                        While @@FETCH_STATUS = 0
                        BEGIN

                        IF (@StandartFundAdmin = 1)
                        BEGIN
                        Insert Into #TBForecast (Valuedate,FundName,AccountID,AccountName,Amount)
                        Select Valuedate,FundName,FundJournalAccountID,FundJournalAccountName, sum(Amount) from (
                        Select @Valuedate Valuedate,D.Name FundName,B.ID FundJournalAccountID ,B.Name FundJournalAccountName ,sum(basedebit-basecredit) Amount from FundJournalDetail A
                        left join FundjournalAccount B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.Status= 2
                        left join FundJournal C on A.FundJournalPK = C.FundJournalPK and C.Status= 2 and C.Posted = 1 and C.Reversed = 0
                        left join Fund D on A.FundPK = D.FundPK and D.Status= 2
                        where A.FundPK = @FundPK and A.Status  = 2 and DetailDescription <> 'ADJUST' and Valuedate <= @ValueDate
                        and B.Type in (1,2) and B.ID not like '202%' and A.FundJournalAccountPK not in (195,196)
                        Group By Valuedate,D.Name,B.ID,B.Name
                        union all
                        select @Valuedate Valuedate,D.Name FundName,C.ID FundJournalAccountID ,C.Name FundJournalAccountName ,sum(case when DebitCredit = 'D' then Amount else Amount * -1 End) Amount  from FAAdjustmentDetail A 
                        left join FAAdjustment B on A.FAAdjustmentPK = B.FAAdjustmentPK
                        left join FundjournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK and C.Status= 2
                        left join Fund D on A.FundPK = D.FundPK and D.Status= 2
                        where B.Status in (1,2) and ValueDate = @ValueDate and A.FundPK = @FundPK and C.Type in (1,2)
                        Group By Valuedate,D.Name,C.ID,C.Name
                        )A
                        Group By Valuedate,FundName,FundJournalAccountID,FundJournalAccountName
                        END
                        ELSE
                        BEGIN

                        Insert Into #TBForecast (Valuedate,FundName,AccountID,AccountName,Amount)
                        Select Valuedate,FundName,FundJournalAccountID,FundJournalAccountName, sum(Amount) from (
                        Select @Valuedate Valuedate,D.Name FundName,B.ID FundJournalAccountID,B.Name FundJournalAccountName ,sum(basedebit-basecredit) Amount from FundJournalDetail A
                        left join FundjournalAccount B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.Status= 2
                        left join FundJournal C on A.FundJournalPK = C.FundJournalPK and C.Status= 2 and C.Posted = 1 and C.Reversed = 0
                        left join Fund D on A.FundPK = D.FundPK and D.Status= 2
                        where A.FundPK = @FundPK and A.Status  = 2 and DetailDescription <> 'ADJUST' and Valuedate <= @ValueDate
                        and A.FundJournalAccountPK not in (109,110)
                        Group By Valuedate,D.Name,B.ID,B.Name
                        union all
                        select @Valuedate Valuedate,D.Name FundName,C.ID FundJournalAccountID ,C.Name FundJournalAccountName ,sum(case when DebitCredit = 'D' then Amount else Amount * -1 End) Amount  from FAAdjustmentDetail A 
                        left join FAAdjustment B on A.FAAdjustmentPK = B.FAAdjustmentPK
                        left join FundjournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK and C.Status= 2
                        left join Fund D on A.FundPK = D.FundPK and D.Status= 2
                        where B.Status in (1,2) and ValueDate = @ValueDate and A.FundPK = @FundPK and C.Type in (1,2)
                        Group By Valuedate,D.Name,C.ID,C.Name
                        )A
                        Group By Valuedate,FundName,FundJournalAccountID,FundJournalAccountName
                    
                        END


                        Fetch next From A Into @FundPK,@StandartFundAdmin
                        END
                        Close A
                        Deallocate A 

                        select Valuedate,FundName,AccountID,AccountName,Amount from #TBForecast
                        ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Model.Add(setTBForecast(dr));
                                }
                            }
                            return L_Model;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private TBForecast setTBForecast(SqlDataReader dr)
        {
            TBForecast M_Model = new TBForecast();
            M_Model.ValueDate = Convert.ToDateTime(dr["ValueDate"]);
            M_Model.FundName = dr["FundName"].ToString();
            M_Model.AccountID = dr["AccountID"].ToString();
            M_Model.AccountName = dr["AccountName"].ToString();
            M_Model.Amount = Convert.ToDecimal(dr["Amount"]);

            return M_Model;
        }


        public void FAAdjustment_Revised(FAAdjustment _fAAdjustment)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =

                        "Declare @UserID Nvarchar(70) " +
                        "Declare @Notes Nvarchar(1000) " +


                        //"set @Notes = @B " +
                            //"set @TrxPortfolioPK = @A " +
                            //"set @UserID = @C " +

                        "UPDATE TrxPortfolio Set Revised = 1, RevisedBy = @UserID,Notes = 'Revise From TrxPortfolio', " +
                        "RevisedTime = GetDate()  " +
                        "WHERE TrxPortfolioPK = @TrxPortfolioPK and status = 2 " +
                        " \n " +
                        "UPDATE Journal SET status = 3, Notes = 'Void By TrxPortfolio Revise', " +
                        "VoidUsersID = @UserID,VoidTime = getDate()  " +
                        "WHERE TrxName ='Portfolio' and TrxNo in " +
                        "( " +
                        "	Select TrxPortfolioPK From TrxPortfolio where TrxPortfolioPK = @TrxPortfolioPK " +
                        ") " +
                        " \n " +
                        "Declare @MaxTrxPortfolioPK int " +
                            //"Declare @TrxPortfolioPK int " +
                        " \n " +
                        "DECLARE A CURSOR FOR      " +
                            "SELECT TrxPortfolioPK FROM TrxPortfolio " +
                            "WHERE TrxPortfolioPK = @TrxPortfolioPK and Status = 2 " +
                        " \n " +
                            "OPEN A       " +
                            "FETCH NEXT FROM A INTO @TrxPortfolioPK     " +
                            "WHILE @@FETCH_STATUS = 0      " +
                            "BEGIN     " +
                        " \n " +
                        "	Select @MaxTrxPortfolioPK = ISNULL(MAX(TrxPortfolioPK),0) + 1 From TrxPortfolio  " +
                        "         INSERT INTO [dbo].[TrxPortfolio] " +
                        "					   ([TrxPortfolioPK] " +
                        "					   ,[HistoryPK] " +
                        "					   ,[Status] " +
                        "					   ,[Notes] " +
                        "					   ,[ValueDate] " +
                        "					   ,[InstrumentTypePK] " +
                        "					   ,[InstrumentPK] " +
                        "					   ,[Price] " +
                        "					   ,[Lot] " +
                        "					   ,[LotInShare] " +
                        "					   ,[Volume] " +
                        "					   ,[Amount] " +
                        "					   ,[TrxType] " +
                        "					   ,[CounterpartPK] " +
                        "					   ,[SettledDate] " +
                        "					   ,[AcqDate] " +
                        "					   ,[MaturityDate] " +
                        "					   ,[InterestPercent] " +
                        "					   ,[CompanyAccountTradingPK] " +
                        "					   ,[PeriodPK] " +
                        "					   ,[EntryUsersID] " +
                        "					   ,[EntryTime]) " +
                        "			SELECT	   @MaxTrxPortfolioPK,1,1,'Pending Revised' " +
                        "					   ,[ValueDate] " +
                        "					   ,[InstrumentTypePK] " +
                        "					   ,[InstrumentPK] " +
                        "					   ,[Price] " +
                        "					   ,[Lot] " +
                        "					   ,[LotInShare] " +
                        "					   ,[Volume] " +
                        "					   ,[Amount] " +
                        "					   ,[TrxType] " +
                        "					   ,[CounterpartPK] " +
                        "					   ,[SettledDate] " +
                        "					   ,[AcqDate] " +
                        "					   ,[MaturityDate] " +
                        "					   ,[InterestPercent] " +
                        "					   ,[CompanyAccountTradingPK] " +
                        "					   ,[PeriodPK] " +
                        "					   ,@UserID " +
                        "					   ,GetDate() " +
                        "			FROM TrxPortfolio  " +
                        "			WHERE TrxPortfolioPK = @TrxPortfolioPK    " +
                        " \n " +
                        "FETCH NEXT FROM A INTO @TrxPortfolioPK " +
                        "END              " +
                        " \n " +
                        "CLOSE A           " +
                        "DEALLOCATE A";

                        //cmd.Parameters.AddWithValue("@TrxPortfolioPK", _trxPortfolio.TrxPortfolioPK);
                        //cmd.Parameters.AddWithValue("@UserID", _trxPortfolio.EntryUsersID);
                        cmd.ExecuteNonQuery();
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