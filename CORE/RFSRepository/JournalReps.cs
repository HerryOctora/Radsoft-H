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
using System.Drawing;



namespace RFSRepository
{
    public class JournalReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[Journal] " +
                            "([JournalPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[TrxNo],[TrxName],[Reference],[Type],[Description],[ReverseNo],";

        string _paramaterCommand = "@PeriodPK,@ValueDate,@TrxNo,@TrxName,@Reference,@Type,@Description,@ReverseNo,";


        string _paramjournalDetail = "INSERT INTO [JournalDetail] " +
                                    "([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK] " +
                                    ",[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription] " +
                                    ",[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit] " +
                                    ",[BaseCredit],[LastUsersID],LastUpdate) \n ";

        string _paramjournal = "INSERT INTO [Journal] " +
                                    "([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate] " +
                                    ",[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy] " +
                                    ",[PostedTime],[Reversed],[ReverseNo],[ReversedBy],[ReversedTime] " +
                                    ",[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime] " +
                                    ",[ApprovedUsersID],[ApprovedTime],[VoidUsersID],[VoidTime],[DBUserID],LastUpdate) ";
        //2
        private Journal setJournal(SqlDataReader dr)
        {
            Journal M_Journal = new Journal();
            M_Journal.JournalPK = Convert.ToInt32(dr["JournalPK"]);
            M_Journal.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Journal.Selected = Convert.ToBoolean(dr["Selected"]);
            M_Journal.Status = Convert.ToInt32(dr["Status"]);
            M_Journal.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Journal.Notes = Convert.ToString(dr["Notes"]);
            M_Journal.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_Journal.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_Journal.ValueDate = dr["ValueDate"].ToString();
            M_Journal.TrxNo = Convert.ToInt32(dr["TrxNo"]);
            M_Journal.TrxName = Convert.ToString(dr["TrxName"]);
            M_Journal.RefNo = Convert.ToInt32(dr["RefNo"]);
            M_Journal.Reference = Convert.ToString(dr["Reference"]);
            M_Journal.Type = Convert.ToString(dr["Type"]);
            M_Journal.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_Journal.Description = Convert.ToString(dr["Description"]);
            M_Journal.Posted = Convert.ToBoolean(dr["Posted"]);
            M_Journal.PostedBy = Convert.ToString(dr["PostedBy"]);
            M_Journal.PostedTime = Convert.ToString(dr["PostedTime"]);
            M_Journal.Reversed = Convert.ToBoolean(dr["Reversed"]);
            M_Journal.ReverseNo = Convert.ToInt32(dr["ReverseNo"]);
            M_Journal.ReversedBy = Convert.ToString(dr["ReversedBy"]);
            M_Journal.ReversedTime = Convert.ToString(dr["ReversedTime"]);
            M_Journal.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Journal.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Journal.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Journal.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Journal.EntryTime = dr["EntryTime"].ToString();
            M_Journal.UpdateTime = dr["UpdateTime"].ToString();
            M_Journal.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Journal.VoidTime = dr["VoidTime"].ToString();
            M_Journal.DBUserID = dr["DBUserID"].ToString();
            M_Journal.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Journal.LastUpdate = dr["LastUpdate"].ToString();
            M_Journal.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_Journal;
        }

        //3
        public List<Journal> Journal_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Journal> L_Journal = new List<Journal>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,C.DescOne TypeDesc,B.ID PeriodID,* From Journal A Left join " +
                                "Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 " +
                                "left join MasterValue C on A.Type = C.Code and C.Id = 'JournalType' and C.status = 2  " +
                                "Where A.Status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,C.DescOne TypeDesc,B.ID PeriodID,* From Journal A Left join " +
                             "Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 " +
                             "left join MasterValue C on A.Type = C.Code and C.Id = 'JournalType' and C.status = 2  ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Journal.Add(setJournal(dr));
                                }
                            }
                            return L_Journal;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<Journal> Journal_SelectFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Journal> L_Journal = new List<Journal>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select  case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,case when Reference = '' or Reference is null then 0 else cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) end RefNo,A.Reference,C.DescOne TypeDesc,B.ID PeriodID,* From Journal A Left join " +
                                        "Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 " +
                                        "left join MasterValue C on A.Type = C.Code and C.Id = 'JournalType' and C.status = 2  " +
                                        "Where  A.Status = @Status and ValueDate between @DateFrom and @DateTo Order By RefNo ";
                            cmd.Parameters.AddWithValue("@Status", _status);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        }
                        else
                        {
                            cmd.CommandText = "Select  case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,case when Reference = '' or Reference is null then 0 else cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) end RefNo,A.Reference,C.DescOne TypeDesc,B.ID PeriodID,* From Journal A Left join " +
                                      "Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 " +
                                      "left join MasterValue C on A.Type = C.Code and C.Id = 'JournalType' and C.status = 2  " +
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
                                    L_Journal.Add(setJournal(dr));
                                }
                            }
                            return L_Journal;
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
        public Journal Journal_SelectByJournalPK(int _journalPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "Select C.DescOne TypeDesc,B.ID PeriodID,* From Journal A Left join " +
                                "Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 " +
                                "left join MasterValue C on A.Type = C.Code and C.Id = 'JournalType' and C.status = 2  " +
                                "Where A.JournalPK = @JournalPK and A.Status = 4 ";
                        cmd.Parameters.AddWithValue("@JournalPK", _journalPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setJournal(dr);
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
        public JournalAddNew Journal_Add(Journal _journal, bool _havePrivillege)
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
                            cmd.CommandText = "Declare @newJournalPK int \n " +
                                 "Select @newJournalPK = isnull(max(JournalPk),0) + 1 from Journal \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate) " +
                                 "Select @newJournalPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate \n " +
                                 "Select @newJournalPK newJournalPK, 1 newHistoryPK ";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _journal.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = "Declare @newJournalPK int \n " +
                                 "Select @newJournalPK = isnull(max(JournalPk),0) + 1 from Journal \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select @newJournalPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate \n " +
                                 "Select @newJournalPK newJournalPK, 1 newHistoryPK ";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@PeriodPK", _journal.PeriodPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _journal.ValueDate);
                        cmd.Parameters.AddWithValue("@TrxNo", 0);
                        cmd.Parameters.AddWithValue("@TrxName", "");
                        cmd.Parameters.AddWithValue("@Reference", _journal.Reference);
                        cmd.Parameters.AddWithValue("@Type", _journal.Type);
                        cmd.Parameters.AddWithValue("@Description", _journal.Description);
                        cmd.Parameters.AddWithValue("@ReverseNo", _journal.ReverseNo);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _journal.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new JournalAddNew()
                                {
                                    JournalPK = Convert.ToInt32(dr["newJournalPK"]),
                                    HistoryPK = Convert.ToInt32(dr["newHistoryPK"]),
                                    Message = "Insert Journal Header Success"
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
        public int Journal_Update(Journal _journal, bool _havePrivillege)
        {
            int _newHisPK;
            int _historyPK;
            int _status;
            try
            {
                _historyPK = _host.Get_NewHistoryPK(_journal.JournalPK, "Journal");
                _status = _host.Get_Status(_journal.JournalPK, _journal.HistoryPK, "Journal");
            }
            catch (Exception err)
            {
                throw err;
            }
            using (SqlConnection DbCon = new SqlConnection(Tools.conString))
            {
                DbCon.Open();
                SqlTransaction sqlT;
                sqlT = DbCon.BeginTransaction("JournalUpdate");
                try
                {
                    DateTime _datetimeNow = DateTime.Now;
                    if (_havePrivillege)
                    {

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.Transaction = sqlT;
                            cmd.CommandText = "Update Journal set Notes=@Notes,PeriodPK=@PeriodPK,ValueDate=@ValueDate,Type=@Type," +
                                "Reference=@Reference,Description=@Description,TrxNo = @TrxNo, TrxName = @TrxName," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where JournalPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _journal.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _journal.JournalPK);
                            cmd.Parameters.AddWithValue("@Notes", _journal.Notes);
                            cmd.Parameters.AddWithValue("@PeriodPK", _journal.PeriodPK);
                            cmd.Parameters.AddWithValue("@ValueDate", _journal.ValueDate);
                            cmd.Parameters.AddWithValue("@Type", _journal.Type);
                            cmd.Parameters.AddWithValue("@Reference", _journal.Reference);
                            cmd.Parameters.AddWithValue("@Description", _journal.Description);
                            cmd.Parameters.AddWithValue("@TrxNo", _journal.TrxNo);
                            cmd.Parameters.AddWithValue("@TrxName", _journal.TrxName);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _journal.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _journal.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Approvedtime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.Transaction = sqlT;
                            cmd.CommandText = "Update Journal " +
                                "Set Notes = @Notes,Status = 3, " +
                                "VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastupdate " +
                                "Where JournalPk = @PK and status = 4 ";
                            cmd.Parameters.AddWithValue("@PK", _journal.JournalPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _journal.HistoryPK);
                            cmd.Parameters.AddWithValue("@Notes", _journal.Notes);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _journal.EntryUsersID);
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
                                cmd.CommandText = "Update Journal set Notes=@Notes,PeriodPK=@PeriodPK,ValueDate=@ValueDate,Type=@Type," +
                                    "Reference=@Reference,Description=@Description,TrxNo = @TrxNo, TrxName = @TrxName," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                    "where JournalPK = @PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _journal.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _journal.JournalPK);
                                cmd.Parameters.AddWithValue("@Notes", _journal.Notes);
                                cmd.Parameters.AddWithValue("@PeriodPK", _journal.PeriodPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _journal.ValueDate);
                                cmd.Parameters.AddWithValue("@Type", _journal.Type);
                                cmd.Parameters.AddWithValue("@Reference", _journal.Reference);
                                cmd.Parameters.AddWithValue("@Description", _journal.Description);
                                cmd.Parameters.AddWithValue("@TrxNo", _journal.TrxNo);
                                cmd.Parameters.AddWithValue("@TrxName", _journal.TrxName);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _journal.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_journal.JournalPK, "Journal");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Journal where JournalPK =@PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _journal.JournalPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _journal.HistoryPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _journal.PeriodPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _journal.ValueDate);
                                cmd.Parameters.AddWithValue("@TrxNo", _journal.TrxNo);
                                cmd.Parameters.AddWithValue("@TrxName", _journal.TrxName);
                                cmd.Parameters.AddWithValue("@Reference", _journal.Reference);
                                cmd.Parameters.AddWithValue("@Type", _journal.Type);
                                cmd.Parameters.AddWithValue("@Description", _journal.Description);
                                cmd.Parameters.AddWithValue("@ReverseNo", _journal.ReverseNo);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _journal.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }


                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                cmd.Transaction = sqlT;
                                cmd.CommandText = "Update Journal " +
                                    "Set Notes = @Notes,Status = 4, " +
                                    "LastUpdate=@lastupdate " +
                                    "Where JournalPk = @PK and HistoryPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _journal.JournalPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _journal.HistoryPK);
                                cmd.Parameters.AddWithValue("@Notes", _journal.Notes);
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
        public void Journal_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                     Select @Time,@PermissionID,'Journal',JournalPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  
                     from Journal where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1    
                     update Journal set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                     where JournalPK in ( Select JournalPK from Journal where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 )  and Status = 1 and Selected  = 1    
                     Update Journal set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  
                     where JournalPK in (Select JournalPK from Journal where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1)   and Status = 4 and Selected  = 1 

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

        public void Journal_PostingBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                         Select @Time,@PermissionID,'Journal',JournalPK,1,'Posting by Selected Data',@UsersID,@IPAddress,
                         @Time  from Journal where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Reversed = 0 and Selected  = 1    

                           update Journal set Posted = 1,PostedBy = @UsersID,PostedTime = @Time,LastUpdate=@Time 
                           where JournalPK in ( Select JournalPK from Journal where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Reversed = 0 and Selected  = 1 )      
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

        public void Journal_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                 Select @Time,@PermissionID,'Journal',JournalPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  
                 from Journal where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1    
                   update Journal set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time 
                   where JournalPK in ( Select JournalPK from Journal where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 )      and Status = 1 and Selected  = 1
                 Update Journal set status= 2  where JournalPK in (Select JournalPK from Journal where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1)  and Status = 4 and Selected  = 1 

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

        public void Journal_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                     Select @Time,@PermissionID,'Journal',JournalPK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  
                     from Journal where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Reversed = 0 and Selected  = 1    
                       update Journal set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time 
                       where JournalPK in ( Select JournalPK from Journal where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Reversed = 0 and Selected  = 1 )    
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


        public void Journal_ReverseBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @JournalPK int Declare @MaxJournalPK int Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                          " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                          "Select @Time,@PermissionID,'Journal',JournalPK,1,'Reverse by Selected Data',@UsersID,@IPAddress,@Time  from Journal where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 1 and Reversed = 0 and Selected  = 1 \n " +
                                          "Declare A Cursor For Select JournalPK from Journal where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 1 and Selected  = 1 Open A Fetch Next From A Into @JournalPK \n " +
                                          "While @@FETCH_STATUS = 0 Begin \n " +
                                          
                                          " select @maxJournalPK = isnull(MAX(journalPK),0) + 1 from Journal \n " +
                                            _paramjournal +
                                        " \n " +
                                        "select @maxJournalPK,[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate] " +
                                                    ",[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy] " +
                                                    ",[PostedTime],1,@JournalPK,@UsersID,@Time " +
                                                    ",[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime] " +
                                                    ",[ApprovedUsersID],[ApprovedTime],[VoidUsersID],[VoidTime],@UsersID,@Time from Journal where journalPK = @JournalPK and status = 2 and Posted = 1 and Reversed = 0 and Selected  = 1 " +
                                        " \n " +
                                        _paramjournalDetail +
                                        " \n " +
                                        "select @maxJournalPK,AutoNo,HistoryPK,[Status],[AccountPK],[CurrencyPK],[OfficePK] " +
                                                    ",[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription] " +
                                                    ",[DocRef],Case when [DebitCredit] = 'D' then 'C' else 'D' end " +
                                                    ",[Amount],Case when [DebitCredit] = 'D' then 0 else Amount end " +
                                                    ",Case when [DebitCredit] = 'D' then Amount else 0 end " +
                                                    ",[CurrencyRate],Case when [DebitCredit] = 'D' then 0 else (Amount * CurrencyRate) end " +
                                                    ",Case when [DebitCredit] = 'D' then (Amount * CurrencyRate) else 0 end ,@UsersID,@Time  from journalDetail " +
                                                    " where JournalPK = @JournalPK  " +
                                         "UPDATE Journal SET Reversed = 1, ReverseNo = @maxJournalPK, ReversedBy = @UsersID, ReversedTime = @Time,LastUpdate = @Time where JournalPK = @JournalPK and Status = 2 and Posted = 1 and Reversed = 0 and Selected  = 1 \n " +
                                          "FETCH NEXT FROM A INTO @JournalPK End Close A Deallocate A " +
                                          " " +
                                          "";
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

        //7
        public void Journal_Approved(Journal _journal)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Journal set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                        "where JournalPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _journal.JournalPK);
                        cmd.Parameters.AddWithValue("@historyPK", _journal.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _journal.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Journal set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where JournalPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _journal.JournalPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _journal.ApprovedUsersID);
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
        public void Journal_Reject(Journal _journal)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Journal set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                        "where JournalPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _journal.JournalPK);
                        cmd.Parameters.AddWithValue("@historyPK", _journal.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _journal.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Journal set status= 2,LastUpdate=@lastUpdate where JournalPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _journal.JournalPK);
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
        public void Journal_Void(Journal _journal)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Journal set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate " +
                        "where JournalPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _journal.JournalPK);
                        cmd.Parameters.AddWithValue("@historyPK", _journal.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _journal.VoidUsersID);
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

        //public void Journal_UnApproved(Journal _journal)
        //{
        //    try
        //    {
        //        DateTime _datetimeNow = DateTime.Now;
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {
        //                cmd.CommandText = "update Journal set status = 1,LastUpdate=@LastUpdate " +
        //                "where JournalPK = @PK and historypk = @historyPK";
        //                cmd.Parameters.AddWithValue("@PK", _journal.JournalPK);
        //                cmd.Parameters.AddWithValue("@historyPK", _journal.HistoryPK);
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

        //10

        public void Journal_Posting(Journal _journal)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //Posting Journal By Type
                        if (_journal.ParamMode == "By Journal From To")
                        {

                            cmd.CommandText = " Update Journal Set Posted = 1,PostedBy=@PostedBy,PostedTime = @PostedTime,LastUpdate = @LastUpdate " +
                                              " where JournalPK between @PostJournalFrom and @PostJournalTo and PeriodPK = @PeriodPK  " +
                                              " and Posted = 0 and Reversed = 0 and status = 2 ";
                            cmd.Parameters.AddWithValue("@PeriodPK", _journal.ParamPeriod);
                            cmd.Parameters.AddWithValue("@PostJournalFrom", _journal.ParamPostJournalFrom);
                            cmd.Parameters.AddWithValue("@PostJournalTo", _journal.ParamPostJournalTo);

                        }
                        else if (_journal.ParamMode == "By Date From To")
                        {

                            cmd.CommandText = " Update Journal Set Posted = 1,PostedBy=@PostedBy,PostedTime =@PostedTime,LastUpdate=@LastUpdate " +
                                              " where ValueDate between @PostDateFrom and @PostDateTo and PeriodPK = @PeriodPK " +
                                              " and Posted = 0 and Reversed = 0 and status = 2 ";
                            cmd.Parameters.AddWithValue("@PeriodPK", _journal.ParamPeriod);
                            cmd.Parameters.AddWithValue("@PostDateFrom", _journal.ParamPostDateFrom);
                            cmd.Parameters.AddWithValue("@PostDateTo", _journal.ParamPostDateTo);
                        }

                        else
                        {
                            cmd.CommandText = " Update Journal Set Posted = 1,PostedBy=@PostedBy,PostedTime =@PostedTime,LastUpdate=@LastUpdate " +
                                              " where JournalPK = @JournalPK and PeriodPK = @PeriodPK " +
                                              " and Posted = 0 and Reversed = 0 and status = 2 ";
                            cmd.Parameters.AddWithValue("@JournalPK", _journal.JournalPK);
                            cmd.Parameters.AddWithValue("@PeriodPK", _journal.PeriodPK);
                        }

                        cmd.Parameters.AddWithValue("@PostedBy", _journal.PostedBy);
                        cmd.Parameters.AddWithValue("@PostedTime", _datetimeNow);
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

        public void Journal_Reversed(Journal _journal)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                        "Declare @maxJournalPK int " +
                        " \n " +
                        "select @maxJournalPK = isnull(MAX(journalPK),0) + 1 from Journal " +
                        " \n " +
                        "select * from journal  where JournalPK=@JournalPK and PeriodPK = @PeriodPK " +
                        " \n " +
                        "if @@RowCount > 0 " +
                        "Begin " +
                        " \n " +
                        "UPDATE Journal SET Reversed = 1, ReverseNo = @maxJournalPK, ReversedBy = @ReversedBy, ReversedTime = @ReversedTime where JournalPK = @JournalPK and PeriodPK = @PeriodPK " +
                        " \n " +
                        _paramjournal +
                        " \n " +
                        "select @maxJournalPK,[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate] " +
                                    ",[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy] " +
                                    ",[PostedTime],1,@JournalPK,@ReversedBy,@ReversedTime " +
                                    ",[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime] " +
                                    ",[ApprovedUsersID],[ApprovedTime],[VoidUsersID],[VoidTime],@ReversedBy,@LastUpdate from Journal where journalPK = @JournalPK and status = 2 " +
                        " \n " +
                        _paramjournalDetail +
                        " \n " +
                        "select @maxJournalPK,AutoNo,HistoryPK,[Status],[AccountPK],[CurrencyPK],[OfficePK] " +
                                    ",[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription] " +
                                    ",[DocRef],Case when [DebitCredit] = 'D' then 'C' else 'D' end " +
                                    ",[Amount],Case when [DebitCredit] = 'D' then 0 else Amount end " +
                                    ",Case when [DebitCredit] = 'D' then Amount else 0 end " +
                                    ",[CurrencyRate],Case when [DebitCredit] = 'D' then 0 else (Amount * CurrencyRate) end " +
                                    ",Case when [DebitCredit] = 'D' then (Amount * CurrencyRate) else 0 end ,@ReversedBy,@LastUpdate  from journalDetail " +
                                    " where JournalPK = @JournalPK   " +
                         "end ";

                        cmd.Parameters.AddWithValue("@JournalPK", _journal.JournalPK);
                        cmd.Parameters.AddWithValue("@PeriodPK", _journal.PeriodPK);
                        cmd.Parameters.AddWithValue("@ReversedBy", _journal.ReversedBy);
                        cmd.Parameters.AddWithValue("@ReversedTime", _datetimeNow);
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
        public string JournalImport(string _userID, string _fileSource)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;
                // delete data yang lama
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "truncate table JournalImportTemp";
                        cmd.ExecuteNonQuery();
                    }
                }

                // import data ke temp dulu
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.JournalImportTemp";
                    bulkCopy.WriteToServer(CreateDataTableFromJournalExcelFile(_fileSource));
                }

                // logic kalo import success
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"Declare @Msg nvarchar(1000) Declare @Balance numeric(19,0)  Declare @No	nvarchar(1000)  
                    Declare @TempTableBalance Table (tmpNo int, tmpBalance numeric(19,4))  
                    Declare @TempTableAccount Table (tmpAccountID nvarchar(100))  
                    Declare @TempTableCurrency Table (tmpCurrencyID nvarchar(100))  
                    Declare @TempTableOffice Table (tmpOfficeID nvarchar(100))  
                    Declare @TempTableDepartment Table (tmpDepartmentID nvarchar(100))  
                    Declare @TempTableAgent Table (tmpAgentID nvarchar(100))  
                    Declare @TempTableCounterPart Table (tmpCounterPartID nvarchar(100))  
                    Declare @TempTableInstrument Table (tmpInstrumentID nvarchar(100))  
                    Declare @TempTableConsignee Table (tmpConsigneeID nvarchar(100))   

                    set @No = ''   

                    INSERT into @TempTableBalance  
                    Select A,Sum(isnull(N,0)*isnull(P,0)) - Sum(isnull(O,0)*isnull(P,0)) Balance  from JournalImportTemp   
                    Group By A having Sum(isnull(N,0)*isnull(P,0)) - Sum(isnull(O,0)*isnull(P,0)) <> 0  

                    INSERT into @TempTableAccount  
                    Select C From JournalImportTemp Where (Len(B) = 0 or B is null) and C not in  (  
                    Select ID from Account where status = 2 and Groups = 0 )  

                    Insert into @TempTableCurrency Select E From JournalImportTemp  
                    Where (Len(B) = 0 or B is null) and (Len(E) > 0 and E is Not null  and E not in ( Select ID from Currency where status = 2  ))  

                    Insert into @TempTableOffice  
                    Select F From JournalImportTemp Where (Len(B) = 0 or B is null) and (Len(F) > 0 and F is Not null and F not in (  
                    Select ID from Office where status = 2  ))  

                    INSERT into @TempTableDepartment Select G From JournalImportTemp Where (Len(B) = 0 or B is null) and (Len(G) > 0 and G is Not null and G not in  
                    (Select ID from Department where status = 2 ))  

                    INSERT into @TempTableAgent Select H From JournalImportTemp  Where (Len(B) = 0 or B is null) and (Len(H) > 0 and H is Not null and H not in  
                    (Select ID from Agent where status = 2 ))  

                    INSERT into @TempTableCounterPart Select I From JournalImportTemp Where (Len(B) = 0 or B is null) and (Len(I) > 0 and I is Not null and I not in  
                    (Select ID from CounterPart where status = 2 ))  

                    INSERT into @TempTableInstrument Select J From JournalImportTemp Where (Len(B) = 0 or B is null) and (Len(J) > 0 and J is Not null and J not in  
                    (Select ID from Instrument where status = 2 ))  

                    INSERT into @TempTableConsignee Select K From JournalImportTemp Where (Len(B) = 0 or B is null) and (Len(K) > 0 and K is Not null and K not in  
                    (Select ID from Consignee where status = 2 ))  
 
                    IF Exists(Select * from @TempTableBalance)BEGIN   
                    Select @No = @No +  Cast(tmpNo as Nvarchar(10)) + ',' from @TempTableBalance  
                    set @Msg = 'Debit Credit Not Balance in No : ' + @No  

                    INSERT into ImportLogEvent(Description,BitStatus,LastUpdate,UserID) Select @Msg,0,@TimeNow,@UserID  Return END  
                     ELSE IF EXISTS(Select * From @TempTableAccount ) BEGIN  
                     Select @No = @No + tmpAccountID + ' , ' from @TempTableAccount set @Msg = 'Please Check This Account : ' + @No  Insert into ImportLogEvent(Description,BitStatus,LastUpdate,UserID) Select @Msg,0,@TimeNow,@UserID  RETURN END  
                     ELSE IF Exists( Select * From @TempTableCurrency  ) BEGIN Select @No = @No + tmpCurrencyID +  ' , ' from @TempTableCurrency set @Msg = 'Please Check This Currency : ' + @No   Insert into ImportLogEvent(Description,BitStatus,LastUpdate,UserID) Select @Msg,0,@TimeNow,@UserID   Return END  
                     ELSE IF Exists( Select * From @TempTableOffice   ) BEGIN Select @No = @No + tmpOfficeID  + ' , ' from @TempTableOffice  set @Msg = 'Please Check This Office : ' + @No  Insert into ImportLogEvent(Description,BitStatus,LastUpdate,UserID) Select @Msg,0,@TimeNow,@UserID   RETURN END  
                     ELSE IF Exists( Select * From @TempTableDepartment ) BEGIN Select @No = @No + tmpDepartmentID + ' , ' from @TempTableDepartment  set @Msg = 'Please Check This Department : ' + @No  Insert into ImportLogEvent(Description,BitStatus,LastUpdate,UserID) Select @Msg,0,@TimeNow,@UserID   RETURN END  
                     ELSE IF Exists( Select * From @TempTableAgent ) BEGIN Select @No = @No + tmpAgentID  + ' , ' from @TempTableAgent set @Msg = 'Please Check This Agent : ' + @No  Insert into ImportLogEvent(Description,BitStatus,LastUpdate,UserID) Select @Msg,0,@TimeNow,@UserID   RETURN END  
                     ELSE IF Exists( Select * From @TempTableCounterPart  ) BEGIN Select @No = @No + tmpCounterPartID +  ' , ' from @TempTableCounterPart set @Msg = 'Please Check This CounterPart : ' + @No  Insert into ImportLogEvent(Description,BitStatus,LastUpdate,UserID) Select @Msg,0,@TimeNow,@UserID   RETURN END  
                     ELSE IF Exists( Select * From @TempTableInstrument  ) BEGIN Select @No = @No + tmpInstrumentID +  ' , ' from @TempTableInstrument set @Msg = 'Please Check This Instrument : ' + @No  Insert into ImportLogEvent(Description,BitStatus,LastUpdate,UserID) Select @Msg,0,@TimeNow,@UserID   RETURN END  
                     ELSE IF Exists( Select * From @TempTableConsignee  ) BEGIN Select @No = @No + tmpConsigneeID  + ' , ' from @TempTableConsignee set @Msg = 'Please Check This Consignee : ' + @No  Insert into ImportLogEvent(Description,BitStatus,LastUpdate,UserID) Select @Msg,0,@TimeNow,@UserID   RETURN END  
                     ELSE BEGIN  
                     set @Msg = 'Debit Credit Balance'  
                     Declare @CountHeader nvarchar(10)  
                     Declare @CountDetail nvarchar(10)  
                     Declare @TotalRow nvarchar(10)  
                     Select @TotalRow = Max(PK) From JournalImportTemp  
                     Select @CountHeader = Count(PK) From JournalImportTemp  
                     Where A is not null and isDate(B) = 1 and dbo.IsInteger(A) = 1  
                     Select @CountDetail = Count(PK) From JournalImportTemp Where len(B)= 0 or B is null  
                     Set @Msg = @Msg + ' <br> Total Row : ' + @TotalRow + ' <br> Total Header Voucher : ' + @CountHeader + ' <br> Total Detail : ' + @CountDetail  
                     END  
                     declare @MaxJournalPK int  
                     select @MaxJournalPK = max(JournalPK) from Journal set @MaxJournalPK = isnull(@MaxJournalPK,0)  
                     declare @Bridge table  
                     (  
                     JournalImportPK int,  
                     JournalPK int  
                     )  
                     insert into @Bridge  
                     select A, row_number() over (order by A) + @MaxJournalPK from ( Select distinct A from JournalImportTemp ) a  
                     INSERT INTO [dbo].[Journal]  
                     ([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Reference],[Type],[Description],[EntryUsersID],[EntryTime],[LastUpdate])  
                     select b.JournalPK, 1, 1, isnull(C,''), c.PeriodPK,B,  Case when len(D) > 0 or D is not null then D else '' end,2,isnull(C,''),@UserID,@TimeNow,@TimeNow from JournalImportTemp t  
                     left join @Bridge b on t.A = b.JournalImportPK left join Period c on t.b between c.datefrom and c.dateto and c.status = 2 where isdate(t.b) = 1  
                     INSERT INTO [dbo].[JournalDetail]  
                     ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],  
                     [DetailDescription],[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUsersID])  
                     select B.JournalPK,row_number() over (order by A),1,2,Ac.AccountPK, isnull(Ac.CurrencyPK,1),isnull(O.OfficePK,1),isnull(De.DepartmentPK,0),isnull(Ag.AgentPK,0),isnull(Co.CounterpartPK,0),isnull(Ins.InstrumentPK,0), isnull(Con.ConsigneePK,0),T.L,T.M,Case when N is null or N = 0 then 'C' else 'D' end, case when N is Null or N = 0  then isnull(O,0) else isnull(N,0) End,  
                     case when N is Null or N = 0 then 0 else N End,case when O is Null or O = 0 then 0 else O end,T.P, case when N is Null or N = 0 then 0 else N * P end,case when O is Null Or O = 0 then 0 else O * P end,@UserID  
                     from JournalImportTemp t  
                     left join @Bridge b on T.A = b.JournalImportPK left join Account Ac on T.C = Ac.ID and Ac.Status = 2 left join Currency Cu on T.E = Cu.ID and Cu.status = 2 left join Office O on T.F = O.ID and O.status = 2  
                     left join Department De on T.G = De.ID  and De.Status = 2 left join Agent Ag on T.H = Ag.ID and Ag.Status = 2 left join Counterpart Co on T.I = Co.ID And Co.Status = 2 left join Instrument Ins on T.J = Ins.ID and Ins.Status = 2  
                     left join Consignee Con on T.K = Con.ID and Con.status = 2 where isdate(t.b) <> 1  
                     Declare @CurJournalPK int  
                     Declare @CurReference nvarchar(100)  
                     Declare @CurValueDate datetime  
                     Declare A Cursor For  
                     select JournalPK from @Bridge  
                     Open A  
                     Fetch Next From A  
                     Into @CurJournalPK  
                     WHILE @@FETCH_STATUS = 0  
                     BEGIN  
                     if Exists ( Select * from journal Where JournalPK = @CurJournalPK and len(Reference) = 0 or reference is null )  
                     Begin  
                     select @CurValueDate = valueDate from Journal where JournalPK = @CurJournalPK  
                     exec getJournalReference @CurValueDate,'ADJ',@CurReference out  
                     Update journal Set Reference = @CurReference where JournalPK = @CurJournalPK  
                     end  
                     FETCH NEXT FROM A INTO @CurJournalPK  
                     END  
                     Close A  
                     Deallocate A  
                     Insert into ImportLogEvent(Description,BitStatus,LastUpdate,UserID) Select @Msg,1,@TimeNow,@UserID  ";
                        cmd.Parameters.AddWithValue("@Userid", _userID);
                        cmd.Parameters.AddWithValue("@TimeNow", _now);
                        cmd.ExecuteNonQuery();

                    }
                }
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Select Description From ImportLogEvent where ImportLogEventPK = (select Max(ImportLogEventPK) From ImportLogEvent where UserID = @UserID)";
                        cmd.Parameters.AddWithValue("@Userid", _userID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                _msg = Convert.ToString(dr["Description"]);
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
        private DataTable CreateDataTableFromJournalExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "AutoNo";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "NID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Date";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Description";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Reference";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CurrencyID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "OfficeID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "DepartmentID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AgentID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CounterpartID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InstrumentID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Consigne";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "DetailDescription";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "DocRef";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    //dc.DataType = System.Type.GetType("System.Decimal");
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Debit";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    //dc.DataType = System.Type.GetType("System.Decimal");
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Credit";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    //dc.DataType = System.Type.GetType("System.Decimal");
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Rate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    //using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    //{
                    //    odConnection.Open();
                    //    using (OleDbCommand odCmd = odConnection.CreateCommand())
                    //    {
                    //        // _oldfilename = nama sheet yang ada di file excel yang diimport
                    //        odCmd.CommandText = "SELECT * FROM [Sheet1$]";
                    //        using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                    //        {
                    //            // start counting from index = 1 --> skipping the header (index=0)
                    //            for (int i = 1; i <= 10; i++)
                    //            {
                    //                odRdr.Read();
                    //            }
                    //            do
                    //            {
                    //                dr = dt.NewRow();

                    //                dr["NID"] = odRdr[0];
                    //                dr["Date"] = odRdr[1];
                    //                dr["Description"] = odRdr[2];
                    //                dr["Reference"] = odRdr[3];
                    //                dr["CurrencyID"] = odRdr[4];
                    //                dr["OfficeID"] = odRdr[5];
                    //                dr["DepartmentID"] = odRdr[6];
                    //                dr["AgentID"] = odRdr[7];
                    //                dr["CounterpartID"] = odRdr[8];
                    //                dr["InstrumentID"] = odRdr[9];
                    //                dr["Consigne"] = odRdr[10];
                    //                dr["DetailDescription"] = odRdr[11];
                    //                dr["DocRef"] = odRdr[12];
                    //                dr["Debit"] = Tools.IsNumber(odRdr[13].ToString().Replace(",", "")) == true ? odRdr[13] : 0;
                    //                dr["Credit"] = Tools.IsNumber(odRdr[14].ToString().Replace(",", "")) == true ? odRdr[14] : 0;
                    //                dr["Rate"] = Tools.IsNumber(odRdr[15].ToString().Replace(",", "")) == true ? odRdr[15] : 0;


                    //                if (dr["NID"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                    //            } while (odRdr.Read());
                    //        }
                    //    }
                    //    odConnection.Close();
                    //}

                    FileInfo excelFile = new FileInfo(_path);
                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        int i = 11;
                        int a;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        var end = worksheet.Dimension.End;
                        a = end.Row;
                        while (i <= end.Row)
                        {
                            dr = dt.NewRow();
                            if (worksheet.Cells[i, 1].Value == null)
                                dr["NID"] = "";
                            else
                                dr["NID"] = worksheet.Cells[i, 1].Value.ToString();

                            if (worksheet.Cells[i, 2].Value == null)
                                dr["Date"] = "";
                            else
                                dr["Date"] = worksheet.Cells[i, 2].Value.ToString();

                            if (worksheet.Cells[i, 3].Value == null)
                                dr["Description"] = "";
                            else
                                dr["Description"] = worksheet.Cells[i, 3].Value.ToString();

                            if (worksheet.Cells[i, 4].Value == null)
                                dr["Reference"] = "";
                            else
                                dr["Reference"] = worksheet.Cells[i, 4].Value.ToString();

                            if (worksheet.Cells[i, 5].Value == null)
                                dr["CurrencyID"] = "";
                            else
                                dr["CurrencyID"] = worksheet.Cells[i, 5].Value.ToString();

                            if (worksheet.Cells[i, 6].Value == null)
                                dr["OfficeID"] = "";
                            else
                                dr["OfficeID"] = worksheet.Cells[i, 6].Value.ToString();

                            if (worksheet.Cells[i, 7].Value == null)
                                dr["DepartmentID"] = "";
                            else
                                dr["DepartmentID"] = worksheet.Cells[i, 7].Value.ToString();

                            if (worksheet.Cells[i, 8].Value == null)
                                dr["AgentID"] = "";
                            else
                                dr["AgentID"] = worksheet.Cells[i, 8].Value.ToString();

                            if (worksheet.Cells[i, 9].Value == null)
                                dr["CounterpartID"] = "";
                            else
                                dr["CounterpartID"] = worksheet.Cells[i, 9].Value.ToString();

                            if (worksheet.Cells[i, 10].Value == null)
                                dr["InstrumentID"] = "";
                            else
                                dr["InstrumentID"] = worksheet.Cells[i, 10].Value.ToString();

                            if (worksheet.Cells[i, 11].Value == null)
                                dr["Consigne"] = "";
                            else
                                dr["Consigne"] = worksheet.Cells[i, 11].Value.ToString();

                            if (worksheet.Cells[i, 12].Value == null)
                                dr["DetailDescription"] = "";
                            else
                                dr["DetailDescription"] = worksheet.Cells[i, 12].Value.ToString();

                            if (worksheet.Cells[i, 13].Value == null)
                                dr["DocRef"] = "";
                            else
                                dr["DocRef"] = worksheet.Cells[i, 13].Value.ToString();

                            if (worksheet.Cells[i, 14].Value == null)
                                dr["Debit"] = 0;
                            else
                                dr["Debit"] = worksheet.Cells[i, 14].Value.ToString();

                            if (worksheet.Cells[i, 15].Value == null)
                                dr["Credit"] = 0;
                            else
                                dr["Credit"] = worksheet.Cells[i, 15].Value.ToString();

                            if (worksheet.Cells[i, 16].Value == null)
                                dr["Rate"] = 0;
                            else
                                dr["Rate"] = worksheet.Cells[i, 16].Value.ToString();


                            //dr["ClosePriceValue"] = worksheet.Cells[i, 2].Value.Equals(null) == true ? "" : worksheet.Cells[i, 2].Value.ToString();

                            if (dr["NID"].Equals(null) != true ||
                                dr["Date"].Equals(null) != true ||
                                dr["Description"].Equals(null) != true ||
                                dr["Reference"].Equals(null) != true ||
                                dr["CurrencyID"].Equals(null) != true ||
                                dr["OfficeID"].Equals(null) != true ||
                                dr["DepartmentID"].Equals(null) != true ||
                                dr["AgentID"].Equals(null) != true ||
                                dr["CounterpartID"].Equals(null) != true ||
                                dr["InstrumentID"].Equals(null) != true ||
                                dr["Consigne"].Equals(null) != true ||
                                dr["DetailDescription"].Equals(null) != true ||
                                dr["DocRef"].Equals(null) != true ||
                                dr["Debit"].Equals(null) != true ||
                                dr["Credit"].Equals(null) != true ||
                                dr["Rate"].Equals(null) != true) { dt.Rows.Add(dr); }
                            i++;

                        }
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public void Generate_CurrencyRevaluation(string _userID, DateTime _valueDateFrom, DateTime _valueDateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @Date datetime " +
                        "Declare @ForeignExchangeRevalAccount int " +
                        "Declare @PeriodPK int " +
                        "Declare @DefaultCurrencyPK int " +
                        "Declare @CurrencyPK int " +
                        "Declare @CurrencyRate numeric(19,4) " +
                        "Declare @CurrencyRevalAmount numeric(19,4) " +
                        "Declare @DecimalPlaces int " +
                        "Declare @JournalPK int " +
                        "Declare @AutoNo int " +
                        "Declare @inc int \n \n " +
                        "Set @Date = @DateFrom \n " +
                        "Select @PeriodPK = PeriodPK from Period Where @Date Between DateFrom and DateTo and status = 2 \n " +
                        "SELECT @DecimalPlaces = A.DecimalPlaces,@ForeignExchangeRevalAccount = A.ForeignExchangeRevalAccount,@DefaultCurrencyPK = A.DefaultCurrencyPK FROM AccountingSetup A \n " +
                        "WHERE A.Status = 2 \n " +
                        "WHILE @Date <= @DateTo \n BEGIN \n " +
                        "set @AutoNo = 0 " +
                        "DECLARE CCurrency CURSOR FOR SELECT A.CurrencyPK FROM Currency A WHERE A.CurrencyPK <> @DefaultCurrencyPK and A.status = 2  " +
                        "OPEN CCurrency FETCH NEXT FROM CCurrency INTO @CurrencyPK  " +
                        "WHILE @@FETCH_STATUS = 0  \n Begin \n " +
                        "SELECT @CurrencyRate = [dbo].[FGetLastCurrencyRate] (@CurrencyPK,@Date) " +
                        "IF @CurrencyRate <> 0 BEGIN \n set @inc = 0 " +
                        "SELECT @CurrencyRevalAmount = SUM(JD.Debit - JD.Credit) * @CurrencyRate - SUM(JD.BaseDebit - JD.BaseCredit) " +
                        "FROM JournalDetail JD       " +
                        "LEFT JOIN Journal J ON JD.JournalPK = J.JournalPK and J.status = 2 " +
                        "LEFT JOIN Account A ON JD.AccountPK = A.AccountPK and A.status = 2 WHERE A.CurrencyPK = @CurrencyPK " +
                        "AND J.status = 2 and j.Posted = 1 AND J.PeriodPK = @PeriodPK AND J.ValueDate <= @Date " +
                        "\n " +
                        "SET @CurrencyRevalAmount = isnull(ROUND(@CurrencyRevalAmount, @DecimalPlaces),0)   " +
                        "IF @CurrencyRevalAmount = 0  " +
                        "BEGIN FETCH NEXT FROM CCurrency INTO @CurrencyPK  END \n " +
                        "ELSE BEGIN \n " +
                        "SELECT @journalPK = Max(JournalPK) + 1 From Journal \n " +
                        "INSERT INTO Journal([JournalPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[TrxNo],[TrxName],[Reference], " +
                        "[Type],[Description],[ReverseNo],[EntryUsersID],[EntryTime],LastUpdate) " +
                        "\n SELECT @JournalPK,1,1,@PeriodPK,@Date,0,'','',1,'Journal From Currency Revaluation',0,@UsersID,@TimeNow,@TimeNow " +
                        "\n INSERT INTO JournalDetail ([JournalPK], [AutoNo],[HistoryPK],[Status], [AccountPK], [CurrencyPK], [OfficePK], [DepartmentPK], " +
                        "[AgentPK],[CounterPartPK],[InstrumentPK],[ConsigneePK],[FundPK],[DetailDescription], [DocRef],[DebitCredit],[Amount], " +
                        "[CurrencyRate], [Debit], [Credit],[BaseDebit], [BaseCredit],[LastUsersID],[LastUpdate])   \n    " +
                        "SELECT	 @JournalPK, @AutoNo + ROW_NUMBER () OVER(ORDER BY A.AccountPK),1,2, " +
                        "A.AccountPK, @CurrencyPK,0,0,0,0,0,0,0,'Revaluation','',        " +
                        "CASE WHEN sum(JD.Debit - JD.Credit) * @CurrencyRate - sum(JD.BaseDebit - JD.BaseCredit) > 0 THEN 'D' ELSE 'C' END,        " +
                        "abs(sum(JD.Debit - JD.Credit) * @CurrencyRate - sum(JD.BaseDebit - JD.BaseCredit)),0,0,0, " +
                        "CASE WHEN sum(JD.Debit - JD.Credit) * @CurrencyRate - sum(JD.BaseDebit - JD.BaseCredit) > 0  " +
                        "THEN abs(sum(JD.Debit - JD.Credit) * @CurrencyRate - sum(JD.BaseDebit - JD.BaseCredit)) ELSE 0 END, " +
                        "CASE WHEN sum(JD.Debit - JD.Credit) * @CurrencyRate - sum(JD.BaseDebit - JD.BaseCredit) < 0 " +
                        "THEN abs(sum(JD.Debit - JD.Credit) * @CurrencyRate - sum(JD.BaseDebit - JD.BaseCredit)) ELSE 0 END, " +
                        "@UsersID,@TimeNow FROM JournalDetail JD " +
                        "LEFT JOIN Journal J ON JD.JournalPK = J.JournalPK and J.status = 2 " +
                        "LEFT JOIN Account A ON JD.AccountPK = A.AccountPK and A.status = 2 " +
                        "WHERE A.CurrencyPK = @CurrencyPK  " +
                        "AND J.status = 2  and J.posted = 1 AND J.PeriodPK = @PeriodPK AND J.ValueDate <= @Date  GROUP BY A.AccountPK " +
                        "HAVING sum(JD.Debit - JD.Credit) * @CurrencyRate - sum(JD.BaseDebit - JD.BaseCredit) <> 0 \n  " +
                        "select @inc = @@Rowcount " +
                        "Set @AutoNo = @AutoNo + @inc " +
                        "IF @CurrencyRevalAmount <> 0 AND @JournalPK <> 0 BEGIN  \n " +
                        "INSERT INTO JournalDetail ([JournalPK], [AutoNo],[HistoryPK],[Status], [AccountPK], [CurrencyPK], [OfficePK], [DepartmentPK], " +
                        "[AgentPK],[CounterPartPK],[InstrumentPK],[ConsigneePK],[FundPK],[DetailDescription], [DocRef],[DebitCredit],[Amount], " +
                        "[CurrencyRate], [Debit], [Credit],[BaseDebit], [BaseCredit],[LastUsersID],[LastUpdate]) " +
                        "SELECT @JournalPK, @AutoNo + 1 ,1,2, @ForeignExchangeRevalAccount, @CurrencyPK, 0,0, " +
                        "0,0,0,0,0,'Revaluation', Null,  " +
                        "CASE WHEN @CurrencyRevalAmount < 0 THEN 'D' ELSE 'C' END, " +
                        "CASE WHEN @CurrencyRevalAmount > 0 THEN @CurrencyRevalAmount ELSE -@CurrencyRevalAmount END,1, " +
                        "CASE WHEN @CurrencyRevalAmount < 0 " +
                        "THEN -@CurrencyRevalAmount ELSE 0 END, " +
                        "CASE WHEN @CurrencyRevalAmount > 0 " +
                        "THEN @CurrencyRevalAmount ELSE 0 END, " +
                        "CASE WHEN @CurrencyRevalAmount < 0 " +
                        "THEN -@CurrencyRevalAmount ELSE 0 END, " +
                        "CASE WHEN @CurrencyRevalAmount > 0 " +
                        "THEN @CurrencyRevalAmount ELSE 0 END,@UsersID,@TimeNow " +
                        "SET @AutoNo = @AutoNo + 1 END \n " +
                        "END END \n " +
                        "FETCH NEXT FROM CCurrency INTO @CurrencyPK " +
                        "CLOSE CCurrency DEALLOCATE CCurrency \n END " +
                        "Select @Date = dbo.FNextWorkingDay(@Date,1) END " +
                        " " +
                        "";

                        cmd.Parameters.AddWithValue("@DateFrom", _valueDateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _valueDateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _userID);
                        cmd.Parameters.AddWithValue("@TimeNow", _datetimeNow);
                        cmd.ExecuteNonQuery();
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
                                        rSingle.Amount = Convert.ToDecimal(dr0["Amount"]);
                                        rSingle.Debit = Convert.ToDecimal(dr0["Debit"]);
                                        rSingle.Credit = Convert.ToDecimal(dr0["Credit"]);
                                        rSingle.Rate = Convert.ToDecimal(dr0["Rate"]);
                                        rSingle.BaseDebit = Convert.ToDecimal(dr0["BaseDebit"]);
                                        rSingle.BaseCredit = Convert.ToDecimal(dr0["BaseCredit"]);
                                        rSingle.OfficeID = Convert.ToString(dr0["OfficeID"]);
                                        rSingle.DepartmentID = Convert.ToString(dr0["DepartmentID"]);
                                        rSingle.AgentID = Convert.ToString(dr0["AgentID"]);
                                        rSingle.ConsigneeID = Convert.ToString(dr0["ConsigneeID"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
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
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = "REFERENCE";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Reference;

                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 6].Value = "VALUE DATE";
                                        worksheet.Cells["G" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                        //worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MM-yyyy";
                                        worksheet.Cells[incRowExcel, 7].Value = Convert.ToDateTime(rsHeader.Key.ValueDate).ToShortDateString();
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1].Value = "BANK";
                                        //worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        //worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.BankID + "-" + rsHeader.Key.BankName;
                                        incRowExcel++;

                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        worksheet.Row(incRowExcel).Height = 30;
                                        worksheet.Cells[incRowExcel, 1].Value = "NO";
                                        worksheet.Cells[incRowExcel, 2].Value = "ACCOUNT ID";
                                        worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 4].Value = "Curr.ID";
                                        worksheet.Cells[incRowExcel, 5].Value = "D/C";
                                        worksheet.Cells[incRowExcel, 6].Value = "AMOUNT";
                                        worksheet.Cells[incRowExcel, 7].Value = "DEBIT";
                                        worksheet.Cells[incRowExcel, 8].Value = "CREDIT";
                                        if (Tools.ClientCode != "12")
                                        {
                                            worksheet.Cells[incRowExcel, 9].Value = "RATE";
                                            worksheet.Cells[incRowExcel, 10].Value = "BASE DEBIT";
                                            worksheet.Cells[incRowExcel, 11].Value = "BASE CREDIT";
                                            worksheet.Cells[incRowExcel, 12].Value = "Off";
                                            worksheet.Cells[incRowExcel, 13].Value = "Dept";
                                            worksheet.Cells[incRowExcel, 14].Value = "Agent";
                                            worksheet.Cells[incRowExcel, 15].Value = "Consg";
                                            worksheet.Cells[incRowExcel, 16].Value = "Instrument";
                                        }
                                        string _range = "A" + incRowExcel + ":P" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
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

                                            worksheet.Cells["A" + RowB + ":P" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":P" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":P" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":P" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;



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
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Amount;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00"; ;
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.Debit;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00"; ;
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.Credit;
                                            if (Tools.ClientCode != "12")
                                            {
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Rate;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00"; ;
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.BaseDebit;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00"; ;
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.BaseCredit;
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.OfficeID;
                                                worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.DepartmentID;
                                                worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.AgentID;
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.ConsigneeID;
                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.InstrumentID;
                                            }
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
                                        if (Tools.ClientCode != "12")
                                        {
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

                                            worksheet.Cells["L" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["M" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["M" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["N" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["N" + RowB + ":N" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["N" + RowB + ":N" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["N" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["O" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["O" + RowB + ":O" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["O" + RowB + ":O" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["O" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["P" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["P" + RowB + ":P" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["P" + RowB + ":P" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["P" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        }


                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                        worksheet.Cells["G" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                        worksheet.Cells["H" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                        worksheet.Cells[incRowExcel, 7].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                        if (Tools.ClientCode != "12")
                                        {
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00"; ;
                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells["J" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00"; ;
                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells["K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;

                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                        }
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Check By";
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                        worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel = incRowExcel + 5;
                                        worksheet.Cells[incRowExcel, 1].Value = "(";
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.CheckedBy;
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 3].Value = ")";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 5].Value = "(";
                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 6].Value = rsHeader.Key.ApprovedBy;
                                        worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 7].Value = ")";
                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                    }

                                    string _rangeDetail = "A:P";

                                    using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                        r.Style.Font.Size = 12;
                                        r.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    if (Tools.ClientCode != "12")
                                    {
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 16];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 30;
                                        worksheet.Column(4).Width = 10;
                                        worksheet.Column(5).Width = 5;
                                        worksheet.Column(6).Width = 17;
                                        worksheet.Column(7).Width = 17;
                                        worksheet.Column(8).Width = 17;
                                        worksheet.Column(9).Width = 10;
                                        worksheet.Column(10).Width = 17;
                                        worksheet.Column(11).Width = 17;
                                        worksheet.Column(12).Width = 7;
                                        worksheet.Column(13).Width = 7;
                                        worksheet.Column(14).AutoFit();
                                        worksheet.Column(15).AutoFit();
                                        worksheet.Column(16).AutoFit();
                                    }
                                    else
                                    {
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 8];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 30;
                                        worksheet.Column(4).Width = 10;
                                        worksheet.Column(5).Width = 5;
                                        worksheet.Column(6).Width = 17;
                                        worksheet.Column(7).Width = 17;
                                        worksheet.Column(8).Width = 17;
                                    }
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT

                                    worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                    worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                    worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

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

        public List<Journal> Get_ReferenceCombo(DateTime _valuedateFrom, DateTime _valuedateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Journal> L_Journal = new List<Journal>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = " select distinct(Reference) Reference,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo from journal " +
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
                                    Journal M_Journal = new Journal();
                                    M_Journal.Reference = Convert.ToString(dr["Reference"]);
                                    L_Journal.Add(M_Journal);
                                }
                            }
                            return L_Journal;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public string Retrieve_ManagementFeeByDateFromTo(string _userID, DateTime _dateFrom, DateTime _dateTo)
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
                        create table #date 
                        (
                        valuedate datetime
                        )   
                        declare @date datetime
                        declare @FundPK int
                        declare @InstrumentPK int 
    
                        Declare @ARManagementFee int 
                        Declare @TaxARManagementFee int 
                        Declare @ManagementFeeExpense int 
                        Declare @TaxManagementFeeExpense int 


                        declare @ManagementFeeAmount numeric(22,6)

                        declare @ARManagementFeeAmount numeric(22,6)
                        declare @TaxARManagementFeeAmount numeric(22,6)
                        declare @ManagementFeeExpenseAmount numeric(22,6)
                        declare @TaxManagementFeeExpenseAmount numeric(22,6)

                        create table #ReferenceTemp
                        (Reference nvarchar(50))

                        Declare @JourHeader int  
                        set @JourHeader = 0    
                        Declare @JournalPK int 
                        Declare @PeriodPK int 
                        Declare @Reference nvarchar(50)    
                        Select @PeriodPK = PeriodPK From Period Where DateFrom <= @DateFrom and Dateto >= @DateTo and Status = 2  
                        Select @JournalPK = isnull(Max(JournalPK),0) from Journal   


                        DECLARE Z CURSOR FOR  
                        select distinct Date from FundDailyFee where Date between @DateFrom and @DateTo
                        order by Date 

                        OPEN Z 
                        FETCH NEXT FROM Z 
                        INTO @Date

                        While @@Fetch_Status  = 0 
                        BEGIN


                        insert into #ReferenceTemp (Reference)
                        exec getJournalReference @Date,'ADJ',@Reference out  
 
                        set @JournalPK = @JournalPK + 1   
                        INSERT INTO [Journal]  
                        ([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate]  
                        ,[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy]  
                        ,[PostedTime],[EntryUsersID],[EntryTime]  
                        ,[ApprovedUsersID],[ApprovedTime],[DBUserID],LastUpdate)     
                        SELECT @JournalPK,1,2,'Management Fee',@PeriodPK, @Date
                        ,0,'',@Reference,1,'Management Fee',1,@UserID
                        ,@TimeNow,@UserID,@TimeNow,@UserID,@TimeNow,@UserID,@TimeNow 


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
 
                        Select @ARManagementFee = ARManagementFee,@TaxARManagementFee =  IncomeTaxArt23,
                        @ManagementFeeExpense = ManagementFeeExpense, @TaxManagementFeeExpense = PPHFinal  
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
                        Select @JournalPK,@AutoNo,1,2,@ARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@ARManagementFeeAmount), abs(@ARManagementFeeAmount),0,1,abs(@ARManagementFeeAmount),0,@userID,@TimeNow 
               
			            
                        set @AutoNo = @AutoNo + 1   


                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@TaxARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@TaxARManagementFeeAmount), abs(@TaxARManagementFeeAmount),0,1,abs(@TaxARManagementFeeAmount),0,@userID,@TimeNow 
               
			            
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@ManagementFeeExpense,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@ManagementFeeExpenseAmount), 0,abs(@ManagementFeeExpenseAmount),1,0,abs(@ManagementFeeExpenseAmount),@userID,@TimeNow 

                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@TaxManagementFeeExpense,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@TaxManagementFeeExpenseAmount), 0,abs(@TaxManagementFeeExpenseAmount),1,0,abs(@TaxManagementFeeExpenseAmount),@userID,@TimeNow 
     
                        
                        FETCH NEXT FROM A 
                        INTO @FundPK,@ManagementFeeAmount 
                        END 
                        CLOSE A  
                        DEALLOCATE A

                        FETCH NEXT FROM Z 
                        INTO @Date
                        END 
                        CLOSE Z  
                        DEALLOCATE Z



                        DECLARE @combinedString NVARCHAR(500)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Reference
                        FROM #ReferenceTemp
                        IF (@combinedString is null)
                        BEGIN
                            SELECT 'No Data Retrieve, Please Check Fund Daily Fee' as Reference
                        END
                        ELSE
                        BEGIN
                        SELECT 'Retrieve Management Fee Success ! Reference is : ' + @combinedString as Reference
                        END
                        ";


                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UserID", _userID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);
                        

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Reference"]);

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

        public string Validate_CheckJournal(DateTime _dateFrom, DateTime _dateTo, string _param)
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
                        
                        Declare @Description nvarchar(50)
                        select @Description = dbo.SpaceBeforeCap(@Param)

                        Create Table #Journal
                        (Reference nvarchar(50))
                        
                        Insert Into #Journal(Reference)
                        select Reference from Journal where ValueDate between @DateFrom and @DateTo and status = 2 and Posted = 1 and Reversed = 0 and Description = @Description

                        if exists(select JournalPK from Journal where ValueDate between @DateFrom and @DateTo and status = 2 and Posted = 1 and Description = @Description)
                        BEGIN
                        DECLARE @combinedString VARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Reference
                        FROM #Journal
                        SELECT 'Retrive Cancel, Please Check Journal in Reference : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END   ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
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

        public string Validate_CheckSumJournal(DateTime _dateFrom, DateTime _dateTo)
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
                        
                        Create Table #Journal
                        (Balance numeric (18,4),
                         JournalPK nvarchar(50))
                        
                        Insert Into #Journal(Balance,JournalPK)
                        select sum(isnull(BaseDebit,0) - isnull(BaseCredit,0))Balance,A.JournalPK from Journal A left join JournalDetail B on A.JournalPK = B.JournalPK and B.status = 2
                        where ValueDate between @DateFrom and @DateTo and A.status = 1 and A.Posted = 0 and A.Reversed = 0 
                        Group By A.JournalPK
                        having Sum(isnull(BaseDebit,0) - isnull(BaseCredit,0)) <> 0

                        if exists(select * from #Journal)
                        BEGIN
                        DECLARE @combinedString VARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + JournalPK
                        FROM #Journal
                        SELECT 'Retrive Cancel, Journal Not Balance. Please Check Journal in SysNo : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END "       ;

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


        public string Generate_PortfolioRevaluation(string _userID, DateTime _dateFrom, DateTime _dateTo)
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
                        create table #date 
                        (
                        valuedate datetime
                        )
       
                        Declare @date datetime
                        Declare @InstrumentPK int 
                        Declare @LastVolume numeric(19,4) 
                        Declare @InstrumentTypePK int 
                        Declare @CadanganAccountPK int 
                        Declare @UnrealisedAccountPK int 
                        Declare @MarketValue Numeric(19,4) 
                        Declare @PortfolioValue Numeric(19,4) 
                        Declare @CadanganValue Numeric(19,4)  
                        Declare @MarginValue Numeric(19,4) 
                        Declare @Amount numeric(19,4) 
                        Declare @PrevRevaluationAmount numeric(19,4) 
                        Declare @TrxAmount numeric(19,4) 

                        create table #ReferenceTemp
                        (Reference nvarchar(50))

                        Declare @JourHeader int  
                        set @JourHeader = 0    
                        Declare @JournalPK int 
                        Declare @PeriodPK int 
                        Declare @Reference nvarchar(50)    
                        Select @PeriodPK = PeriodPK From Period Where DateFrom <= @DateFrom and Dateto >= @DateTo and Status = 2  
                        Select @JournalPK = isnull(Max(JournalPK),0) from Journal    


                        DECLARE Z CURSOR FOR  
                        select distinct Date from ClosePrice where Date between @DateFrom and @DateTo
                        order by Date 

                        OPEN Z 
                        FETCH NEXT FROM Z 
                        INTO @Date

                        While @@Fetch_Status  = 0 
                        BEGIN


                        insert into #ReferenceTemp (Reference)
                        exec getJournalReference @Date,'ADJ',@Reference out  
 
                        set @JournalPK = @JournalPK + 1   
                        INSERT INTO [Journal]  
                        ([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate]  
                        ,[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy]  
                        ,[PostedTime],[EntryUsersID],[EntryTime]  
                        ,[ApprovedUsersID],[ApprovedTime],[DBUserID],LastUpdate)      
                        SELECT @JournalPK,1,2,'Portfolio Revaluation',@PeriodPK, @Date
                        ,0,'',@Reference,1,'Portfolio Revaluation',1,@UserID
                        ,@TimeNow,@UserID,@TimeNow,@UserID,@TimeNow,@UserID,@TimeNow 

                        Declare @AutoNo int 
                        set @AutoNo = 0   
                        DECLARE A CURSOR FOR 
                        Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume ,A.InstrumentTypePK    
                        from (
                        select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume,InstrumentTypePK from trxPortfolio 
                        where ValueDate <= @Date and Posted = 1 and trxType = 1  
                        Group By InstrumentPK,InstrumentTypePK  
                        UNION ALL   
                        select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume,InstrumentTypePK from trxPortfolio 
                        where ValueDate <= @Date and Posted = 1  and trxType = 2  
                        Group By InstrumentPK,InstrumentTypePK  
                        )A 
                        Group By A.InstrumentPK,A.InstrumentTypePK  
                        OPEN A 
                        FETCH NEXT FROM A 
                        INTO @InstrumentPk,@LastVolume,@InstrumentTypePK   
                        WHILE @@FETCH_STATUS = 0  
                        BEGIN 
                        set @MarketValue = 0 
                        If @InstrumentTypePK in (1,2,4) 
                        BEGIN 
                        IF (@InstrumentTypePK = 1)
                        BEGIN
                        Select @CadanganAccountPK = CadanganEquity ,@UnrealisedAccountPK = UnrealisedEquity From AccountingSetup 
                        Where Status = 2

                        Select @MarketValue =  @LastVolume * dbo.FGetLastClosePriceForFundPosition(@Date,@InstrumentPK) 
                        Select @PortfolioValue =  dbo.FGetPortfolioValue(@Date,@InstrumentPK)             
                        Select @PrevRevaluationAmount = isnull(dbo.[FGetAccountBalanceJournalByDateByInstrumentPK](@Date,@CadanganAccountPK,@InstrumentPK),0)    
                        Set @Amount = @MarketValue - (@PortfolioValue + @PrevRevaluationAmount)
                        END
                        ELSE IF (@InstrumentTypePK = 2)
                        BEGIN
                        Select @CadanganAccountPK = CadanganBond ,@UnrealisedAccountPK = UnrealisedBond From AccountingSetup 
                        Where Status = 2

                        Select @MarketValue =  @LastVolume * dbo.FGetLastClosePriceForFundPosition(@Date,@InstrumentPK) 
                        Select @PortfolioValue =  dbo.FGetPortfolioValue(@Date,@InstrumentPK)             
                        Select @PrevRevaluationAmount = isnull(dbo.[FGetAccountBalanceJournalByDateByInstrumentPK](@Date,@CadanganAccountPK,@InstrumentPK),0)    
                        Set @Amount = @MarketValue - (@PortfolioValue + @PrevRevaluationAmount)
                        END
                        ELSE IF (@InstrumentTypePK = 4)
                        BEGIN
                        Select @CadanganAccountPK = CadanganReksadana ,@UnrealisedAccountPK = UnrealisedReksadana From AccountingSetup 
                        Where Status = 2

                        Select @MarketValue =  @LastVolume * dbo.FGetLastClosePriceForFundPosition(@Date,@InstrumentPK)  
                        Select @TrxAmount =  @LastVolume * dbo.FGetLastClosePriceForFundPosition(dbo.FWorkingDay(@Date,-1),@InstrumentPK) 
                        Set @Amount = isnull(@MarketValue,0) - isnull(@TrxAmount,0)
                        END
			             
 
                        Declare @AllocDepartmentPK int 
                        Declare @AllocPercent numeric(18,8) 
                        declare @AfterAllocateAmount numeric(18,4) 
                        Declare @CounterAmount numeric(18,4) 
                        Declare @Count Int 
                        Declare @Inc int 
                        set @Inc = 0 set @CounterAmount = 0 
                        set @AfterAllocateAmount = 0   
                        Declare @RoundAmount numeric(19,4) 
                        Declare @RoundingDepartmentPK int 
                        Declare @LastAmount Numeric(19,4)  
                        Declare @FinalAmountAfterRounding numeric(19,4)  
                        If @Amount < 0 BEGIN       
                        --Mulai dari sini
                        Select @Count = isnull(Count(DepartmentPK) ,0) From AccountAllocateByCostCenterSetup 
                        where AccountPK =  @UnrealisedAccountPK and status = 2  
                        if @Count = 0 
                        begin 
                        Set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate)  
                        Select @JournalPK,@AutoNo,1,2,@UnrealisedAccountPK,1,0,isnull(@AllocDepartmentPk,0),0,0,@InstrumentPK,0,'','','D',Abs(@Amount),Abs(@Amount),0,1,Abs(@Amount),0,@userID,@TimeNow 
                        end   
                        else 
                        begin 
                        Declare B Cursor For 
                        Select departmentPK,AllocationPercentage From AccountAllocateByCostCenterSetup 
                        where AccountPK =  @UnrealisedAccountPK and status = 2 
                        Open B   
                        Fetch Next From B 
                        Into @AllocDepartmentPk,@AllocPercent  
                        While @@Fetch_Status  = 0 
                        Begin 	
                        Set @AfterAllocateAmount = abs(@amount) * @AllocPercent/100  
                        Set @CounterAmount =  @CounterAmount + @AfterAllocateAmount 
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@UnrealisedAccountPK,1,0,isnull(@AllocDepartmentPk,0),0,0,@InstrumentPK,0,'','','D',isnull(@AfterAllocateAmount,0),   
                        isnull(@AfterAllocateAmount,0),0,1,isnull(@AfterAllocateAmount,0),0,@userID,@TimeNow  
                        set @Inc = @Inc + 1 
                        IF @Inc = @count 
                        begin Set @RoundAmount = @CounterAmount - abs(@Amount) 
                        if  @RoundAmount <> 0 
                        begin 
                        select top 1 @roundingDepartmentPK = DepartmentPK From AccountAllocateByCostCenterSetup  
                        where AccountPK = @UnrealisedAccountPK and status = 2 and BitRounding = 1  
                        Select @LastAmount =  Amount From JournalDetail 
                        where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2 
                        set @FinalAmountAfterRounding = @lastAmount +  @RoundAmount  
                        Update JournalDetail Set Amount = @FinalAmountAfterRounding,Debit = @FinalAmountAfterRounding,baseDebit = @FinalAmountAfterRounding * CurrencyRate 
                        where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2  end   end  
                        FETCH NEXT FROM B 
                        INTO  @AllocDepartmentPk,@AllocPercent  
                        End  
                        Close B 
                        DEALLOCATE  B  
                        end   
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@CadanganAccountPK,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@amount), 0,abs(@amount),1,0,abs(@amount),@userID,@TimeNow 
                        END   
                        If @Amount > 0 
                        Begin   
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@CadanganAccountPK,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@amount),abs(@amount),0,1,abs(@amount),0,@userID,@TimeNow   
                        set @AutoNo = @AutoNo + 1   

                        Select @Count = isnull(Count(DepartmentPK) ,0) From AccountAllocateByCostCenterSetup 
                        where AccountPK =  @UnrealisedAccountPK and status = 2  
                        if @Count = 0 
                        begin   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate)   
                        Select @JournalPK,@AutoNo,1,2,@UnrealisedAccountPK,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@amount), 0,abs(@amount),1,0,abs(@amount),@userID,@TimeNow 
                        end  
                        else 
                        begin 
                        Declare C Cursor For 
                        Select departmentPK,AllocationPercentage From AccountAllocateByCostCenterSetup 
                        where AccountPK =  @UnrealisedAccountPK and status = 2 
                        Open C   
                        Fetch Next From C 
                        Into @AllocDepartmentPk,@AllocPercent  
                        While @@Fetch_Status  = 0 
                        Begin 	
                        Set @AfterAllocateAmount = abs(@amount) * @AllocPercent/100  
                        Set @CounterAmount =  @CounterAmount + @AfterAllocateAmount 
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@UnrealisedAccountPK,1,0,isnull(@AllocDepartmentPk,0),0,0,@InstrumentPK,0,'','','C',isnull(@AfterAllocateAmount,0),   
                        0,isnull(@AfterAllocateAmount,0),1,0,isnull(@AfterAllocateAmount,0),@userID,@TimeNow  
                        set @Inc = @Inc + 1 
                        IF @Inc = @count 
                        begin 
                        Set @RoundAmount = @CounterAmount - abs(@Amount) 
                        if  @RoundAmount <> 0 
                        begin 
                        select top 1 @roundingDepartmentPK = DepartmentPK From AccountAllocateByCostCenterSetup  
                        where AccountPK = @UnrealisedAccountPK and status = 2 and BitRounding = 1  
                        Select @LastAmount =  Amount From JournalDetail 
                        where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2 
                        set @FinalAmountAfterRounding = @lastAmount  + @RoundAmount  
                        Update JournalDetail Set Amount = @FinalAmountAfterRounding,Debit = @FinalAmountAfterRounding,baseDebit = @FinalAmountAfterRounding * CurrencyRate 
                        where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2  end   
                        end  
                        FETCH NEXT FROM C 
                        INTO  @AllocDepartmentPk,@AllocPercent  
                        End  
                        Close C 
                        DEALLOCATE  C  
                        end end end   

                        FETCH NEXT FROM A 
                        INTO @InstrumentPk,@LastVolume,@InstrumentTypePK 
                        END 
                        CLOSE A  
                        DEALLOCATE A


                        FETCH NEXT FROM Z 
                        INTO @Date
                        END 
                        CLOSE Z  
                        DEALLOCATE Z




                        DECLARE @combinedString VARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Reference
                        FROM #ReferenceTemp
                        IF (@combinedString is null)
                        BEGIN
                            SELECT 'No Data Revaluation, Please Check Close Price' as Reference
                        END
                        ELSE
                        BEGIN
                            SELECT 'Portfolio Revaluation Success ! Reference is : ' + @combinedString as Reference
                        END

        
                        
                        ";


                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UserID", _userID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Reference"]);

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

        public List<ReferenceFromJournal> Reference_SelectFromJournal(DateTime _valueDate, int _periodPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ReferenceFromJournal> L_Journal = new List<ReferenceFromJournal>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                         @"
                            select Distinct A.Reference,A.RefNo from    
                            (    
                            Select distinct(Reference) Reference,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo  
                            From Journal where status not in (3,4)and Posted = 0 and PeriodPK = @PeriodPK   
                            Union All    
                            select Reference,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo from cashierReference where left(right(reference,4),2) =  month(@ValueDate)   
                            and reference in ( select distinct reference from cashier where status not in (3,4)and posted = 0 ) and PeriodPK = @PeriodPK       
                            ) A  
                            order By A.Refno 

                        ";
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@PeriodPK", _periodPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ReferenceFromJournal M_Journal = new ReferenceFromJournal();
                                    M_Journal.Reference = Convert.ToString(dr["Reference"]);
                                    L_Journal.Add(M_Journal);
                                }
                            }
                            return L_Journal;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string Retrieve_ClosePriceReksadanaByDateFromTo(string _userID, DateTime _dateFrom, DateTime _dateTo)
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

                        create table #date 
                        (
                        valuedate datetime
                        )
    
                        declare @InstrumentPK int
                        declare @Nav numeric (22,4)
                        declare @Date datetime
                        declare @AUM numeric (22,4)


                        DECLARE A CURSOR FOR  
                        select C.InstrumentPK,A.Date,A.Nav,D.AUM from CloseNav A 
                        left join Fund B on A.FundPK = B.FundPK and B.status = 2
                        left join Instrument C on B.ID = C.ID and C.Status = 2 
                        left join CloseNav D on A.FundPK = D.FundPK and dbo.FWorkingDay(A.Date,-1) = D.Date and D.status = 2 
                        where A.Date between @DateFrom and @DateTo and A.status = 2


                        OPEN A 
                        FETCH NEXT FROM A 
                        INTO @InstrumentPK,@Date,@Nav,@AUM

                        While @@Fetch_Status  = 0 
                        BEGIN
                        declare @ClosePricePK int
                        Select @ClosePricePK = isnull(max(ClosePricePK),0) + 1 from ClosePrice
                        insert into ClosePrice (ClosePricePK,HistoryPK,status,Date,InstrumentPK,ClosePriceValue,TotalNAVReksadana,EntryUsersID,EntryTime,LastUpdate)
                        select @ClosePricePK,1,2,@Date,@InstrumentPK,@Nav,@AUM,@UsersID,@TimeNow,@TimeNow
                        FETCH NEXT FROM A 
                        INTO @InstrumentPK,@Date,@Nav,@AUM
                        END 
                        CLOSE A  
                        DEALLOCATE A


                    
                        
                        SELECT 'Retrieve Close Price Reksadana Success !' as Result
                        ";


                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _userID);
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


        public string Retrieve_DataMKBDByDateFromTo(string _userID, DateTime _dateFrom, DateTime _dateTo)
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

                        DECLARE @combinedString VARCHAR(MAX)

                        -- CLOSE PRICE REKSADANA

                        declare @InstrumentPK int
                        declare @Nav numeric (22,4)
                        declare @Date datetime
                        declare @AUM numeric (22,4)


                        DECLARE A CURSOR FOR  
                        select C.InstrumentPK,A.Date,A.Nav,D.AUM from CloseNav A 
                        left join Fund B on A.FundPK = B.FundPK and B.status = 2
                        left join Instrument C on B.ID = C.ID and C.Status = 2 
                        left join CloseNav D on A.FundPK = D.FundPK and dbo.FWorkingDay(A.Date,-1) = D.Date and D.status = 2 
                        where A.Date between @DateFrom and @DateTo and A.status = 2


                        OPEN A 
                        FETCH NEXT FROM A 
                        INTO @InstrumentPK,@Date,@Nav,@AUM

                        While @@Fetch_Status  = 0 
                        BEGIN
                        declare @ClosePricePK int
                        Select @ClosePricePK = isnull(max(ClosePricePK),0) + 1 from ClosePrice
                        insert into ClosePrice (ClosePricePK,HistoryPK,status,Date,InstrumentPK,ClosePriceValue,TotalNAVReksadana,EntryUsersID,EntryTime,LastUpdate)
                        select @ClosePricePK,1,2,@Date,@InstrumentPK,@Nav,@AUM,@UserID,@TimeNow,@TimeNow
                        FETCH NEXT FROM A 
                        INTO @InstrumentPK,@Date,@Nav,@AUM
                        END 
                        CLOSE A  
                        DEALLOCATE A


                        -- REVAL

                        Declare @LastVolume numeric(19,4) 
                        Declare @InstrumentTypePK int 
                        Declare @CadanganAccountPK int 
                        Declare @UnrealisedAccountPK int 
                        Declare @MarketValue Numeric(19,4) 
                        Declare @PortfolioValue Numeric(19,4) 
                        Declare @CadanganValue Numeric(19,4)  
                        Declare @MarginValue Numeric(19,4) 
                        Declare @Amount numeric(19,4) 
                        Declare @PrevRevaluationAmount numeric(19,4) 
                        Declare @TrxAmount numeric(19,4) 



                        Declare @JourHeader int  
                        set @JourHeader = 0    
                        Declare @JournalPK int 
                        Declare @PeriodPK int 
                        Declare @Reference nvarchar(50)    
                        Select @PeriodPK = PeriodPK From Period Where DateFrom <= @DateFrom and Dateto >= @DateTo and Status = 2  
                        Select @JournalPK = isnull(Max(JournalPK),0) from Journal    


                        DECLARE Z CURSOR FOR  
                        select distinct Date from ClosePrice where Date between @DateFrom and @DateTo
                        order by Date 

                        OPEN Z 
                        FETCH NEXT FROM Z 
                        INTO @Date

                        While @@Fetch_Status  = 0 
                        BEGIN


                        insert into #ReferenceTemp (Reference)
                        exec getJournalReference @Date,'ADJ',@Reference out  
 
                        set @JournalPK = @JournalPK + 1   
                        INSERT INTO [Journal]  
                        ([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate]  
                        ,[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy]  
                        ,[PostedTime],[EntryUsersID],[EntryTime]  
                        ,[ApprovedUsersID],[ApprovedTime],[DBUserID],LastUpdate)      
                        SELECT @JournalPK,1,2,'Portfolio Revaluation',@PeriodPK, @Date
                        ,0,'',@Reference,1,'Portfolio Revaluation',1,@UserID
                        ,@TimeNow,@UserID,@TimeNow,@UserID,@TimeNow,@UserID,@TimeNow 

                        Declare @AutoNo int 
                        set @AutoNo = 0   
                        DECLARE A CURSOR FOR 
                        Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume ,A.InstrumentTypePK    
                        from (
                        select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume,InstrumentTypePK from trxPortfolio 
                        where ValueDate <= @Date and Posted = 1 and trxType = 1  
                        Group By InstrumentPK,InstrumentTypePK  
                        UNION ALL   
                        select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume,InstrumentTypePK from trxPortfolio 
                        where ValueDate <= @Date and Posted = 1  and trxType = 2  
                        Group By InstrumentPK,InstrumentTypePK  
                        )A 
                        Group By A.InstrumentPK,A.InstrumentTypePK  
                        OPEN A 
                        FETCH NEXT FROM A 
                        INTO @InstrumentPk,@LastVolume,@InstrumentTypePK   
                        WHILE @@FETCH_STATUS = 0  
                        BEGIN 
                        set @MarketValue = 0 
                        If @InstrumentTypePK in (1,2,4) 
                        BEGIN 
                        IF (@InstrumentTypePK = 1)
                        BEGIN
                        Select @CadanganAccountPK = CadanganEquity ,@UnrealisedAccountPK = UnrealisedEquity From AccountingSetup 
                        Where Status = 2

                        Select @MarketValue =  @LastVolume * dbo.FGetLastClosePriceForFundPosition(@Date,@InstrumentPK) 
                        Select @PortfolioValue =  dbo.FGetPortfolioValue(@Date,@InstrumentPK)             
                        Select @PrevRevaluationAmount = isnull(dbo.[FGetAccountBalanceJournalByDateByInstrumentPK](@Date,@CadanganAccountPK,@InstrumentPK),0)    
                        Set @Amount = @MarketValue - (@PortfolioValue + @PrevRevaluationAmount)
                        END
                        ELSE IF (@InstrumentTypePK = 2)
                        BEGIN
                        Select @CadanganAccountPK = CadanganBond ,@UnrealisedAccountPK = UnrealisedBond From AccountingSetup 
                        Where Status = 2

                        Select @MarketValue =  @LastVolume * dbo.FGetLastClosePriceForFundPosition(@Date,@InstrumentPK) 
                        Select @PortfolioValue =  dbo.FGetPortfolioValue(@Date,@InstrumentPK)             
                        Select @PrevRevaluationAmount = isnull(dbo.[FGetAccountBalanceJournalByDateByInstrumentPK](@Date,@CadanganAccountPK,@InstrumentPK),0)    
                        Set @Amount = @MarketValue - (@PortfolioValue + @PrevRevaluationAmount)
                        END
                        ELSE IF (@InstrumentTypePK = 4)
                        BEGIN
                        Select @CadanganAccountPK = CadanganReksadana ,@UnrealisedAccountPK = UnrealisedReksadana From AccountingSetup 
                        Where Status = 2

                        Select @MarketValue =  @LastVolume * dbo.FGetLastClosePriceForFundPosition(@Date,@InstrumentPK)  
                        Select @TrxAmount =  @LastVolume * dbo.FGetLastClosePriceForFundPosition(dbo.FWorkingDay(@Date,-1),@InstrumentPK) 
                        Set @Amount = isnull(@MarketValue,0) - isnull(@TrxAmount,0)
                        END
			             
 
                        Declare @AllocDepartmentPK int 
                        Declare @AllocPercent numeric(18,8) 
                        declare @AfterAllocateAmount numeric(18,4) 
                        Declare @CounterAmount numeric(18,4) 
                        Declare @Count Int 
                        Declare @Inc int 
                        set @Inc = 0 set @CounterAmount = 0 
                        set @AfterAllocateAmount = 0   
                        Declare @RoundAmount numeric(19,4) 
                        Declare @RoundingDepartmentPK int 
                        Declare @LastAmount Numeric(19,4)  
                        Declare @FinalAmountAfterRounding numeric(19,4)  
                        If @Amount < 0 BEGIN       
                        --Mulai dari sini
                        Select @Count = isnull(Count(DepartmentPK) ,0) From AccountAllocateByCostCenterSetup 
                        where AccountPK =  @UnrealisedAccountPK and status = 2  
                        if @Count = 0 
                        begin 
                        Set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate)  
                        Select @JournalPK,@AutoNo,1,2,@UnrealisedAccountPK,1,0,isnull(@AllocDepartmentPk,0),0,0,@InstrumentPK,0,'','','D',Abs(@Amount),Abs(@Amount),0,1,Abs(@Amount),0,@userID,@TimeNow 
                        end   
                        else 
                        begin 
                        Declare B Cursor For 
                        Select departmentPK,AllocationPercentage From AccountAllocateByCostCenterSetup 
                        where AccountPK =  @UnrealisedAccountPK and status = 2 
                        Open B   
                        Fetch Next From B 
                        Into @AllocDepartmentPk,@AllocPercent  
                        While @@Fetch_Status  = 0 
                        Begin 	
                        Set @AfterAllocateAmount = abs(@amount) * @AllocPercent/100  
                        Set @CounterAmount =  @CounterAmount + @AfterAllocateAmount 
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@UnrealisedAccountPK,1,0,isnull(@AllocDepartmentPk,0),0,0,@InstrumentPK,0,'','','D',isnull(@AfterAllocateAmount,0),   
                        isnull(@AfterAllocateAmount,0),0,1,isnull(@AfterAllocateAmount,0),0,@userID,@TimeNow  
                        set @Inc = @Inc + 1 
                        IF @Inc = @count 
                        begin Set @RoundAmount = @CounterAmount - abs(@Amount) 
                        if  @RoundAmount <> 0 
                        begin 
                        select top 1 @roundingDepartmentPK = DepartmentPK From AccountAllocateByCostCenterSetup  
                        where AccountPK = @UnrealisedAccountPK and status = 2 and BitRounding = 1  
                        Select @LastAmount =  Amount From JournalDetail 
                        where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2 
                        set @FinalAmountAfterRounding = @lastAmount +  @RoundAmount  
                        Update JournalDetail Set Amount = @FinalAmountAfterRounding,Debit = @FinalAmountAfterRounding,baseDebit = @FinalAmountAfterRounding * CurrencyRate 
                        where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2  end   end  
                        FETCH NEXT FROM B 
                        INTO  @AllocDepartmentPk,@AllocPercent  
                        End  
                        Close B 
                        DEALLOCATE  B  
                        end   
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@CadanganAccountPK,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@amount), 0,abs(@amount),1,0,abs(@amount),@userID,@TimeNow 
                        END   
                        If @Amount > 0 
                        Begin   
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@CadanganAccountPK,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@amount),abs(@amount),0,1,abs(@amount),0,@userID,@TimeNow   
                        set @AutoNo = @AutoNo + 1   

                        Select @Count = isnull(Count(DepartmentPK) ,0) From AccountAllocateByCostCenterSetup 
                        where AccountPK =  @UnrealisedAccountPK and status = 2  
                        if @Count = 0 
                        begin   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate)   
                        Select @JournalPK,@AutoNo,1,2,@UnrealisedAccountPK,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@amount), 0,abs(@amount),1,0,abs(@amount),@userID,@TimeNow 
                        end  
                        else 
                        begin 
                        Declare C Cursor For 
                        Select departmentPK,AllocationPercentage From AccountAllocateByCostCenterSetup 
                        where AccountPK =  @UnrealisedAccountPK and status = 2 
                        Open C   
                        Fetch Next From C 
                        Into @AllocDepartmentPk,@AllocPercent  
                        While @@Fetch_Status  = 0 
                        Begin 	
                        Set @AfterAllocateAmount = abs(@amount) * @AllocPercent/100  
                        Set @CounterAmount =  @CounterAmount + @AfterAllocateAmount 
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@UnrealisedAccountPK,1,0,isnull(@AllocDepartmentPk,0),0,0,@InstrumentPK,0,'','','C',isnull(@AfterAllocateAmount,0),   
                        0,isnull(@AfterAllocateAmount,0),1,0,isnull(@AfterAllocateAmount,0),@userID,@TimeNow  
                        set @Inc = @Inc + 1 
                        IF @Inc = @count 
                        begin 
                        Set @RoundAmount = @CounterAmount - abs(@Amount) 
                        if  @RoundAmount <> 0 
                        begin 
                        select top 1 @roundingDepartmentPK = DepartmentPK From AccountAllocateByCostCenterSetup  
                        where AccountPK = @UnrealisedAccountPK and status = 2 and BitRounding = 1  
                        Select @LastAmount =  Amount From JournalDetail 
                        where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2 
                        set @FinalAmountAfterRounding = @lastAmount  + @RoundAmount  
                        Update JournalDetail Set Amount = @FinalAmountAfterRounding,Debit = @FinalAmountAfterRounding,baseDebit = @FinalAmountAfterRounding * CurrencyRate 
                        where JournalPK = @JournalPK and AccountPK = @UnrealisedAccountPK and DepartmentPK = @RoundingDepartmentPK and status = 2  end   
                        end  
                        FETCH NEXT FROM C 
                        INTO  @AllocDepartmentPk,@AllocPercent  
                        End  
                        Close C 
                        DEALLOCATE  C  
                        end end end   

                        FETCH NEXT FROM A 
                        INTO @InstrumentPk,@LastVolume,@InstrumentTypePK 
                        END 
                        CLOSE A  
                        DEALLOCATE A


                        FETCH NEXT FROM Z 
                        INTO @Date
                        END 
                        CLOSE Z  
                        DEALLOCATE Z



                        -- MANAGEMENT FEE

                        declare @FundPK int

                        Declare @ARManagementFee int 
                        Declare @TaxARManagementFee int 
                        Declare @ManagementFeeExpense int 
                        Declare @TaxManagementFeeExpense int 


                        declare @ManagementFeeAmount numeric(22,6)

                        declare @ARManagementFeeAmount numeric(22,6)
                        declare @TaxARManagementFeeAmount numeric(22,6)
                        declare @ManagementFeeExpenseAmount numeric(22,6)
                        declare @TaxManagementFeeExpenseAmount numeric(22,6)


                        set @JourHeader = 0      
                        Select @PeriodPK = PeriodPK From Period Where DateFrom <= @DateFrom and Dateto >= @DateTo and Status = 2  
                        Select @JournalPK = isnull(Max(JournalPK),0) from Journal


                        DECLARE Z CURSOR FOR  
                        select distinct Date from FundDailyFee where Date between @DateFrom and @DateTo
                        order by Date 

                        OPEN Z 
                        FETCH NEXT FROM Z 
                        INTO @Date

                        While @@Fetch_Status  = 0 
                        BEGIN


                        insert into #ReferenceTemp (Reference)
                        exec getJournalReference @Date,'ADJ',@Reference out    
 
                        set @JournalPK = @JournalPK + 1
 
                        INSERT INTO [Journal]  
                        ([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate]  
                        ,[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy]  
                        ,[PostedTime],[EntryUsersID],[EntryTime]  
                        ,[ApprovedUsersID],[ApprovedTime],[DBUserID],LastUpdate)     
                        SELECT @JournalPK,1,2,'Management Fee',@PeriodPK, @Date
                        ,0,'',@Reference,1,'Management Fee',1,@UserID
                        ,@TimeNow,@UserID,@TimeNow,@UserID,@TimeNow,@UserID,@TimeNow 


                        --Declare @AutoNo int 
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
 
                        Select @ARManagementFee = ARManagementFee,@TaxARManagementFee =  IncomeTaxArt23,
                        @ManagementFeeExpense = ManagementFeeExpense, @TaxManagementFeeExpense = PPHFinal  
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
                        Select @JournalPK,@AutoNo,1,2,@ARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@ARManagementFeeAmount), abs(@ARManagementFeeAmount),0,1,abs(@ARManagementFeeAmount),0,@userID,@TimeNow 
               
			            
                        set @AutoNo = @AutoNo + 1   


                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@TaxARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@TaxARManagementFeeAmount), abs(@TaxARManagementFeeAmount),0,1,abs(@TaxARManagementFeeAmount),0,@userID,@TimeNow 
               
			            
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@ManagementFeeExpense,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@ManagementFeeExpenseAmount), 0,abs(@ManagementFeeExpenseAmount),1,0,abs(@ManagementFeeExpenseAmount),@userID,@TimeNow 

                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@TaxManagementFeeExpense,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@TaxManagementFeeExpenseAmount), 0,abs(@TaxManagementFeeExpenseAmount),1,0,abs(@TaxManagementFeeExpenseAmount),@userID,@TimeNow 
     
                        
                        FETCH NEXT FROM A 
                        INTO @FundPK,@ManagementFeeAmount 
                        END 
                        CLOSE A  
                        DEALLOCATE A

                        FETCH NEXT FROM Z 
                        INTO @Date
                        END 
                        CLOSE Z  
                        DEALLOCATE Z

                        -- AUM

                        DECLARE A CURSOR FOR  
                        select Date,sum(AUM) AUM from closenav where Date between @DateFrom and @DateTo and status = 2
                        Group By Date

                        OPEN A 
                        FETCH NEXT FROM A 
                        INTO @Date,@AUM

                        While @@Fetch_Status  = 0 
                        BEGIN
                        declare @AUMPK int
                        Select @AUMPK = isnull(max(AUMPK),0) + 1 from AUM
                        insert into AUM (AUMPK,HistoryPK,status,Date,Amount,EntryUsersID,EntryTime,LastUpdate)
                        select @AUMPK,1,2,@Date,@AUM,@UserID,@TimeNow,@TimeNow
                        FETCH NEXT FROM A 
                        INTO @Date,@AUM
                        END 
                        CLOSE A  
                        DEALLOCATE A

                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Reference
                        FROM #ReferenceTemp

                        SELECT 'Retrieve Data MKBD Success ! Reference is : ' + @combinedString as Result
                        ";


                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UserID", _userID);
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

        public string Validate_CheckRetrieveMKBDResult(DateTime _dateFrom, DateTime _dateTo, string _param)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_param == "ClosePrice")
                        {
                            cmd.CommandText = @"
                        
                            create table #date 
                            (
                            valuedate datetime
                            )

                            insert into #date (valuedate)
                            SELECT  TOP (DATEDIFF(DAY, @datefrom, @dateto) + 1) Dates = DATEADD(DAY, ROW_NUMBER() OVER(ORDER BY a.object_id) - 1, @datefrom)
                            FROM sys.all_objects a CROSS JOIN sys.all_objects b

                            delete from #date where dbo.[CheckTodayIsHoliday](valuedate) = 1

                            delete from #date where ValueDate in 
                            (Select valuedate from RetrieveMKBDResult where ValueDate between @datefrom and @dateto and CheckAUM = 'DONE')

                            IF EXISTS(select valuedate From #date)
                            BEGIN
                            DECLARE @combinedString VARCHAR(MAX)
                            SELECT @combinedString = COALESCE(@combinedString + ', ', '') + replace(convert(NVARCHAR, valuedate, 106), ' ', '/')
                            FROM #date 
                            SELECT 'Retrive Cancel, CheckAUM is NOT CHECK in Date: ' + @combinedString as Result 
                            END
                            ELSE
                            BEGIN
                            select '' Result
                            END ";

                        }
                        else if (_param == "Revaluation")
                        {
                            cmd.CommandText = @"
                        
                            create table #date 
                            (
                            valuedate datetime
                            )

                            insert into #date (valuedate)
                            SELECT  TOP (DATEDIFF(DAY, @datefrom, @dateto) + 1) Dates = DATEADD(DAY, ROW_NUMBER() OVER(ORDER BY a.object_id) - 1, @datefrom)
                            FROM sys.all_objects a CROSS JOIN sys.all_objects b

                            delete from #date where dbo.[CheckTodayIsHoliday](valuedate) = 1

                            delete from #date where ValueDate in 
                            (Select valuedate from RetrieveMKBDResult where ValueDate between @datefrom and @dateto and CheckClosePrice = 'DONE')

                            IF EXISTS(select valuedate From #date)
                            BEGIN
                            DECLARE @combinedString VARCHAR(MAX)
                            SELECT @combinedString = COALESCE(@combinedString + ', ', '') + replace(convert(NVARCHAR, valuedate, 106), ' ', '/')
                            FROM #date 
                            SELECT 'Retrive Cancel, CheckClosePrice is NOT CHECK in Date: ' + @combinedString as Result 
                            END
                            ELSE
                            BEGIN
                            select '' Result
                            END ";

                        }
                        else if (_param == "ManagementFee")
                        {
                            cmd.CommandText = @"
                        
                            create table #date 
                            (
                            valuedate datetime
                            )

                            insert into #date (valuedate)
                            SELECT  TOP (DATEDIFF(DAY, @datefrom, @dateto) + 1) Dates = DATEADD(DAY, ROW_NUMBER() OVER(ORDER BY a.object_id) - 1, @datefrom)
                            FROM sys.all_objects a CROSS JOIN sys.all_objects b

                            delete from #date where dbo.[CheckTodayIsHoliday](valuedate) = 1

                            delete from #date where ValueDate in 
                            (Select valuedate from RetrieveMKBDResult where ValueDate between @datefrom and @dateto and CheckRevaluation = 'DONE')

                            IF EXISTS(select valuedate From #date)
                            BEGIN
                            DECLARE @combinedString VARCHAR(MAX)
                            SELECT @combinedString = COALESCE(@combinedString + ', ', '') + replace(convert(NVARCHAR, valuedate, 106), ' ', '/')
                            FROM #date 
                            SELECT 'Retrive Cancel, CheckRevaluation NOT CHECK in Date: ' + @combinedString as Result 
                            END
                            ELSE
                            BEGIN
                            select '' Result
                            END ";

                        }

                        

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
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

        public decimal Get_StartBalanceByAccountPKByDate(string _valuedate, string _id)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        declare @AccountPK int
                        select @AccountPK = AccountPK from Account where status = 2 and ID = @ID
                        
                        Select [dbo].[FGetStartAccountBalance](@DateFrom,@AccountPK) StartBalance";
                        cmd.Parameters.AddWithValue("@DateFrom", _valuedate);
                        cmd.Parameters.AddWithValue("@ID", _id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToDecimal(dr["StartBalance"]);
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

        public bool CheckJournalStatus(int _pk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from Journal where JournalPK = @PK and Status in (1,2) and Posted = 1";
                        cmd.Parameters.AddWithValue("@PK", _pk);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
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

        public Boolean GenerateExportJournal(string _userID, JournalExport _JournalExport)
        {

            #region ExportJournal

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        //string _paramFundFrom = "";

                        //if (!_host.findString(_CustodianRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_CustodianRpt.FundFrom))
                        //{
                        //    _paramFundFrom = "And A.FundPK  in ( " + _CustodianRpt.FundFrom + " ) ";
                        //}
                        //else
                        //{
                        //    _paramFundFrom = "";
                        //}



                        cmd.CommandText = @"
--declare @Datefrom Date
--declare @dateto date

--set @Datefrom = '2019-09-30'
--set @Dateto = '2019-09-30'

select A.JournalPK,ValueDate,A.Description,A.Reference,isnull(C.ID,'') AccountID,isnull(C.Name,'') AccountName,isnull(D.ID,'') CurrencyID,isnull(O.ID,'') OfficeID,isnull(E.ID,'') DepartmentID,isnull(F.ID,'') AgentID,
isnull(G.ID,'') CounterpartID,isnull(I.ID,'') InstrumentID, isnull(H.ID,'') ConsigneeID,isnull(B.DetailDescription,'') DetailDescription,isnull(B.DocRef,'') DocRef,isnull(B.Debit,0) Debit,isnull(B.Credit,0) Credit,isnull(B.CurrencyRate,0) CurrencyRate from Journal A
left join JournalDetail B on A.JournalPK = B.JournalPK
left join Account C on B.AccountPK = C.AccountPK and C.status in (1,2)
left join Currency D on B.CurrencyPK = D.CurrencyPK and D.Status in (1,2)
left join Office O on B.OfficePK = O.OfficePK and O.status in (1,2)
left join Department E on B.DepartmentPK = E.DepartmentPK and E.Status in (1,2)
left join Agent F on B.AgentPK = F.AgentPK and F.Status in (1,2)
left join Counterpart G on B.CounterpartPK = G.CounterpartPK and G.Status in (1,2)
left join Consignee H on B.ConsigneePK = H.ConsigneePK and H.Status in (1,2)
left join Instrument I on B.InstrumentPK = I.InstrumentPK and I.Status in (1,2)
where ValueDate between @datefrom and @dateto and posted = 1 and A.status = 2
";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Datefrom", _JournalExport.ValueDateFrom);
                        cmd.Parameters.AddWithValue("@Dateto", _JournalExport.ValueDateTo);

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "Journal" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "Journal" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "JournalExport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Journal");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<JournalExport> rList = new List<JournalExport>();
                                    while (dr0.Read())
                                    {

                                        JournalExport rSingle = new JournalExport();
                                        rSingle.JournalPK = Convert.ToInt32(dr0["JournalPK"]);
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.Description = Convert.ToString(dr0["Description"]);
                                        rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                        rSingle.AccountID = Convert.ToString(dr0["AccountID"]);
                                        rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                        rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                        rSingle.OfficeID = Convert.ToString(dr0["OfficeID"]);
                                        rSingle.DepartmentID = Convert.ToString(dr0["DepartmentID"]);
                                        rSingle.AgentID = Convert.ToString(dr0["AgentID"]);
                                        rSingle.CounterpartID = Convert.ToString(dr0["CounterpartID"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                        rSingle.ConsigneeID = Convert.ToString(dr0["ConsigneeID"]);
                                        rSingle.DetailDescription = Convert.ToString(dr0["DetailDescription"]);
                                        rSingle.DocRef = Convert.ToString(dr0["DocRef"]);
                                        rSingle.Debit = Convert.ToDecimal(dr0["Debit"]);
                                        rSingle.Credit = Convert.ToDecimal(dr0["Credit"]);
                                        rSingle.CurrencyRate = Convert.ToDecimal(dr0["CurrencyRate"]);






                                        rList.Add(rSingle);

                                    }


                                    //rsHeader.Key.ValueDate
                                    var GroupByReference =
                                            from r in rList
                                                //orderby r ascending
                                            group r by new { r.JournalPK, r.ValueDate, r.Description, r.Reference } into rGroup
                                            select rGroup;

                                    int incRowExcel = 0;
                                    incRowExcel++;
                                    int RowG = incRowExcel + 1;




                                    worksheet.Cells[incRowExcel, 1].Value = "NO";
                                    worksheet.Cells[incRowExcel, 2].Value = "Value Date";
                                    worksheet.Cells[incRowExcel, 3].Value = "Description";
                                    worksheet.Cells[incRowExcel, 4].Value = "Reference";
                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 3].Value = "Account ID";
                                    worksheet.Cells[incRowExcel, 4].Value = "Account Name";
                                    worksheet.Cells[incRowExcel, 5].Value = "Currency ID";
                                    worksheet.Cells[incRowExcel, 6].Value = "Office ID";
                                    worksheet.Cells[incRowExcel, 7].Value = "DepartmentID";
                                    worksheet.Cells[incRowExcel, 8].Value = "Agent ID";
                                    worksheet.Cells[incRowExcel, 9].Value = "Counterpart ID";
                                    worksheet.Cells[incRowExcel, 10].Value = "Instrument ID";
                                    worksheet.Cells[incRowExcel, 11].Value = "Consignee ID";
                                    worksheet.Cells[incRowExcel, 12].Value = "Detail Description";
                                    worksheet.Cells[incRowExcel, 13].Value = "Document Refefence";
                                    worksheet.Cells[incRowExcel, 14].Value = "Debit";
                                    worksheet.Cells[incRowExcel, 15].Value = "Credit";
                                    worksheet.Cells[incRowExcel, 16].Value = "Currency Rate";
                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 1].Value = "NO";
                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 1].Value = "Example";
                                    worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 1].Value = "1";
                                    worksheet.Cells[incRowExcel, 2].Value = "MM/DD/YYYY";
                                    worksheet.Cells[incRowExcel, 4].Value = "Header Description";
                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 1].Value = "1";
                                    worksheet.Cells[incRowExcel, 3].Value = "Account A";
                                    worksheet.Cells[incRowExcel, 4].Value = "Name A";
                                    worksheet.Cells[incRowExcel, 5].Value = "IDR";
                                    worksheet.Cells[incRowExcel, 7].Value = "CCA";
                                    worksheet.Cells[incRowExcel, 12].Value = "Detail A";
                                    worksheet.Cells[incRowExcel, 16].Value = "1";
                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 1].Value = "1";
                                    worksheet.Cells[incRowExcel, 3].Value = "Account B";
                                    worksheet.Cells[incRowExcel, 4].Value = "Name B";
                                    worksheet.Cells[incRowExcel, 5].Value = "IDR";
                                    worksheet.Cells[incRowExcel, 7].Value = "CCA";
                                    worksheet.Cells[incRowExcel, 12].Value = "Detail B";
                                    worksheet.Cells[incRowExcel, 16].Value = "1";
                                    incRowExcel++;
                                    worksheet.Cells[incRowExcel, 1].Value = "1";
                                    worksheet.Cells[incRowExcel, 3].Value = "Account C";
                                    worksheet.Cells[incRowExcel, 4].Value = "Name C";
                                    worksheet.Cells[incRowExcel, 5].Value = "IDR";
                                    worksheet.Cells[incRowExcel, 7].Value = "CCA";
                                    worksheet.Cells[incRowExcel, 12].Value = "Detail C";
                                    worksheet.Cells[incRowExcel, 16].Value = "1";



                                    incRowExcel++;
                                    incRowExcel++;
                                    incRowExcel++;
                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.JournalPK;
                                        worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.ValueDate.ToString("dd-MMM-yyyy");
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Description;
                                        worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.Reference;

                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 16].Style.Font.Size = 12;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 16].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 16].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);

                                        worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["B" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["D" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["E" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["H" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["J" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["L" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["M" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["N" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["O" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["P" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;




                                        incRowExcel = incRowExcel + 1;

                                        int first = incRowExcel;

                                        int _no = 1;
                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;



                                        foreach (var rsDetail in rsHeader)
                                        {
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 16].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 16].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 16].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 16].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.JournalPK;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.AccountID;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.AccountName;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.CurrencyID;
                                            worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.OfficeID;
                                            worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.DepartmentID;
                                            worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.AgentID;
                                            worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail.CounterpartID;
                                            worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 10].Value = rsDetail.InstrumentID;
                                            worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail.ConsigneeID;
                                            worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 12].Value = rsDetail.DetailDescription;
                                            worksheet.Cells[incRowExcel, 12, incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 12, incRowExcel, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 13].Value = rsDetail.DocRef;
                                            worksheet.Cells[incRowExcel, 13, incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 13, incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 14].Value = rsDetail.Debit;
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 14, incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 14, incRowExcel, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 15].Value = rsDetail.Credit;
                                            worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 15, incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 15, incRowExcel, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 16].Value = rsDetail.CurrencyRate;
                                            worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 16, incRowExcel, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 16, incRowExcel, 16].Style.VerticalAlignment = ExcelVerticalAlignment.Center;




                                            _no++;
                                            _endRowDetail = incRowExcel;
                                            incRowExcel++;
                                        }



                                        //worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                        //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                        //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        //worksheet.Cells[incRowExcel, 5].Calculate();
                                        //int last = incRowExcel - 1;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;




                                        //foreach (var rsHeader in GroupByReference)
                                        //{

                                    }




                                    //worksheet.Cells[incRowExcel, 5, incRowExcel, 8].Style.Font.Size = 12;
                                    //worksheet.Cells[incRowExcel, 5, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    //worksheet.Cells[incRowExcel, 5, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                    //incRowExcel++;

                                    //incRowExcel++;



                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel + 8, 8];
                                    worksheet.Column(1).Width = 10;
                                    worksheet.Column(2).Width = 21;
                                    worksheet.Column(3).Width = 21;
                                    worksheet.Column(4).Width = 21;
                                    worksheet.Column(5).Width = 21;
                                    worksheet.Column(6).Width = 21;
                                    worksheet.Column(7).Width = 21;
                                    worksheet.Column(8).Width = 21;
                                    worksheet.Column(9).Width = 21;
                                    worksheet.Column(10).Width = 21;
                                    worksheet.Column(11).Width = 21;
                                    worksheet.Column(12).Width = 21;
                                    worksheet.Column(13).Width = 21;
                                    worksheet.Column(14).Width = 21;
                                    worksheet.Column(15).Width = 21;
                                    worksheet.Column(16).Width = 21;




                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                    //worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Journal";


                                    //Image img = Image.FromFile(Tools.ReportImage);
                                    //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                    worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                    worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                    worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

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

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();
                                    if (_JournalExport.DownloadMode == "PDF")
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
            #endregion


        }


    }
}