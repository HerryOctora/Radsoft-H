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
    public class FundJournalReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundJournal] " +
                            "([FundJournalPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[TrxNo],[TrxName],[Reference],[Type],[Description],[ReverseNo],";

        string _paramaterCommand = "@PeriodPK,@ValueDate,@TrxNo,@TrxName,@Reference,@Type,@Description,@ReverseNo,";


        string _paramFundJournalDetail = "INSERT INTO [FundJournalDetail] " +
                                    "([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK] " +
                                    ",[FundClientPK],[DetailDescription] " +
                                    ",[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit] " +
                                    ",[BaseCredit],[LastUsersID],LastUpdate) \n ";

        string _paramFundJournal = "INSERT INTO [FundJournal] " +
                                    "([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate] " +
                                    ",[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy] " +
                                    ",[PostedTime],[Reversed],[ReverseNo],[ReversedBy],[ReversedTime] " +
                                    ",[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime] " +
                                    ",[ApprovedUsersID],[ApprovedTime],[VoidUsersID],[VoidTime],[DBUserID],LastUpdate) ";
        //2
        private FundJournal setFundJournal(SqlDataReader dr)
        {
            FundJournal M_FundJournal = new FundJournal();
            M_FundJournal.FundJournalPK = Convert.ToInt32(dr["FundJournalPK"]);
            M_FundJournal.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundJournal.Selected = Convert.ToBoolean(dr["Selected"]);
            M_FundJournal.Status = Convert.ToInt32(dr["Status"]);
            M_FundJournal.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundJournal.Notes = Convert.ToString(dr["Notes"]);
            M_FundJournal.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_FundJournal.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_FundJournal.ValueDate = dr["ValueDate"].ToString();
            M_FundJournal.TrxNo = Convert.ToInt32(dr["TrxNo"]);
            M_FundJournal.TrxName = Convert.ToString(dr["TrxName"]);
            M_FundJournal.RefNo = Convert.ToInt32(dr["RefNo"]);
            M_FundJournal.Reference = Convert.ToString(dr["Reference"]);
            M_FundJournal.Type = Convert.ToString(dr["Type"]);
            M_FundJournal.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_FundJournal.Description = Convert.ToString(dr["Description"]);
            M_FundJournal.Posted = Convert.ToBoolean(dr["Posted"]);
            M_FundJournal.PostedBy = Convert.ToString(dr["PostedBy"]);
            M_FundJournal.PostedTime = Convert.ToString(dr["PostedTime"]);
            M_FundJournal.Reversed = Convert.ToBoolean(dr["Reversed"]);
            M_FundJournal.ReverseNo = Convert.ToInt32(dr["ReverseNo"]);
            M_FundJournal.ReversedBy = Convert.ToString(dr["ReversedBy"]);
            M_FundJournal.ReversedTime = Convert.ToString(dr["ReversedTime"]);
            M_FundJournal.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundJournal.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundJournal.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundJournal.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundJournal.EntryTime = dr["EntryTime"].ToString();
            M_FundJournal.UpdateTime = dr["UpdateTime"].ToString();
            M_FundJournal.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundJournal.VoidTime = dr["VoidTime"].ToString();
            M_FundJournal.DBUserID = dr["DBUserID"].ToString();
            M_FundJournal.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundJournal.LastUpdate = dr["LastUpdate"].ToString();
            M_FundJournal.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FundJournal;
        }

        //3
        public List<FundJournal> FundJournal_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundJournal> L_fundJournal = new List<FundJournal>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,C.DescOne TypeDesc,B.ID PeriodID,* From FundJournal A Left join " +
                                "Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 " +
                                "left join MasterValue C on A.Type = C.Code and C.Id = 'FundJournalType' and C.status = 2  " +
                                "Where A.Status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,C.DescOne TypeDesc,B.ID PeriodID,* From FundJournal A Left join " +
                             "Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 " +
                             "left join MasterValue C on A.Type = C.Code and C.Id = 'FundJournalType' and C.status = 2  ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_fundJournal.Add(setFundJournal(dr));
                                }
                            }
                            return L_fundJournal;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<FundJournal> FundJournal_SelectFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundJournal> L_fundJournal = new List<FundJournal>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select  case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,case when A.Reference like '%/%' then cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) else '' end RefNo,A.Reference,C.DescOne TypeDesc,B.ID PeriodID,* From FundJournal A Left join 
                            Period B on A.PeriodPK = B.PeriodPK and B.Status = 2
                            left join MasterValue C on A.Type = C.Code and C.Id = 'FundJournalType' and C.status = 2
                            Where  ValueDate between @DateFrom and @DateTo and A.status = @Status Order By RefNo  ";
                            cmd.Parameters.AddWithValue("@Status", _status);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        }
                        else
                        {
                            cmd.CommandText = @"Select  case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,case when A.Reference like '%/%' then cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) else '' end RefNo,A.Reference,C.DescOne TypeDesc,B.ID PeriodID,* From FundJournal A Left join 
                            Period B on A.PeriodPK = B.PeriodPK and B.Status = 2
                            left join MasterValue C on A.Type = C.Code and C.Id = 'FundJournalType' and C.status = 2
                            Where  ValueDate between @DateFrom and @DateTo Order By RefNo  ";
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_fundJournal.Add(setFundJournal(dr));
                                }
                            }
                            return L_fundJournal;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public FundJournalAddNew FundJournal_Add(FundJournal _fundJournal, bool _havePrivillege)
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
                            cmd.CommandText = "Declare @newFundJournalPK int \n " +
                                 "Select @newFundJournalPK = isnull(max(FundJournalPk),0) + 1 from FundJournal \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate) " +
                                 "Select @newFundJournalPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate \n " +
                                 "Select @newFundJournalPK newFundJournalPK, 1 newHistoryPK ";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundJournal.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = "Declare @newFundJournalPK int \n " +
                                 "Select @newFundJournalPK = isnull(max(FundJournalPk),0) + 1 from FundJournal \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select @newFundJournalPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate \n " +
                                 "Select @newFundJournalPK newFundJournalPK, 1 newHistoryPK ";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@PeriodPK", _fundJournal.PeriodPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _fundJournal.ValueDate);
                        cmd.Parameters.AddWithValue("@TrxNo", 0);
                        cmd.Parameters.AddWithValue("@TrxName", "");
                        cmd.Parameters.AddWithValue("@Reference", _fundJournal.Reference);
                        cmd.Parameters.AddWithValue("@Type", _fundJournal.Type);
                        cmd.Parameters.AddWithValue("@Description", _fundJournal.Description);
                        cmd.Parameters.AddWithValue("@ReverseNo", _fundJournal.ReverseNo);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _fundJournal.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new FundJournalAddNew()
                                {
                                    FundJournalPK = Convert.ToInt32(dr["newFundJournalPK"]),
                                    HistoryPK = Convert.ToInt64(dr["newHistoryPK"]),
                                    Message = "Insert Fund Journal Header Success"
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

        public int FundJournal_Update(FundJournal _fundJournal, bool _havePrivillege)
        {
            int _newHisPK;
            int _historyPK;
            int _status;
            try
            {
                _historyPK = _host.Get_NewHistoryPK(_fundJournal.FundJournalPK, "FundJournal");
                _status = _host.Get_Status(_fundJournal.FundJournalPK, _fundJournal.HistoryPK, "FundJournal");
            }
            catch (Exception err)
            {
                throw err;
            }
            using (SqlConnection DbCon = new SqlConnection(Tools.conString))
            {
                DbCon.Open();
                SqlTransaction sqlT;
                sqlT = DbCon.BeginTransaction("FundJournalUpdate");
                try
                {
                    DateTime _datetimeNow = DateTime.Now;
                    if (Tools.ClientCode == "11")
                    {
                        _havePrivillege = true;
                    }

                    if (_havePrivillege)
                    {

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.Transaction = sqlT;
                            cmd.CommandText = "Update FundJournal set Notes=@Notes,PeriodPK=@PeriodPK,ValueDate=@ValueDate,Type=@Type," +
                                "Reference=@Reference,Description=@Description,TrxNo = @TrxNo, TrxName = @TrxName," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where FundJournalPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _fundJournal.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _fundJournal.FundJournalPK);
                            cmd.Parameters.AddWithValue("@Notes", _fundJournal.Notes);
                            cmd.Parameters.AddWithValue("@PeriodPK", _fundJournal.PeriodPK);
                            cmd.Parameters.AddWithValue("@ValueDate", _fundJournal.ValueDate);
                            cmd.Parameters.AddWithValue("@Type", _fundJournal.Type);
                            cmd.Parameters.AddWithValue("@Reference", _fundJournal.Reference);
                            cmd.Parameters.AddWithValue("@Description", _fundJournal.Description);
                            cmd.Parameters.AddWithValue("@TrxNo", _fundJournal.TrxNo);
                            cmd.Parameters.AddWithValue("@TrxName", _fundJournal.TrxName);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _fundJournal.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundJournal.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Approvedtime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.Transaction = sqlT;
                            cmd.CommandText = "Update FundJournal " +
                                "Set Notes = @Notes,Status = 3, " +
                                "VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastupdate " +
                                "Where FundJournalPk = @PK and status = 4 ";
                            cmd.Parameters.AddWithValue("@PK", _fundJournal.FundJournalPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _fundJournal.HistoryPK);
                            cmd.Parameters.AddWithValue("@Notes", _fundJournal.Notes);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _fundJournal.EntryUsersID);
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
                                cmd.CommandText = "Update FundJournal set Notes=@Notes,PeriodPK=@PeriodPK,ValueDate=@ValueDate,Type=@Type," +
                                    "Reference=@Reference,Description=@Description,TrxNo = @TrxNo, TrxName = @TrxName," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                    "where FundJournalPK = @PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _fundJournal.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _fundJournal.FundJournalPK);
                                cmd.Parameters.AddWithValue("@Notes", _fundJournal.Notes);
                                cmd.Parameters.AddWithValue("@PeriodPK", _fundJournal.PeriodPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _fundJournal.ValueDate);
                                cmd.Parameters.AddWithValue("@Type", _fundJournal.Type);
                                cmd.Parameters.AddWithValue("@Reference", _fundJournal.Reference);
                                cmd.Parameters.AddWithValue("@Description", _fundJournal.Description);
                                cmd.Parameters.AddWithValue("@TrxNo", _fundJournal.TrxNo);
                                cmd.Parameters.AddWithValue("@TrxName", _fundJournal.TrxName);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _fundJournal.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_fundJournal.FundJournalPK, "FundJournal");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundJournal where FundJournalPK =@PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _fundJournal.FundJournalPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _fundJournal.HistoryPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _fundJournal.PeriodPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _fundJournal.ValueDate);
                                cmd.Parameters.AddWithValue("@TrxNo", _fundJournal.TrxNo);
                                cmd.Parameters.AddWithValue("@TrxName", _fundJournal.TrxName);
                                cmd.Parameters.AddWithValue("@Reference", _fundJournal.Reference);
                                cmd.Parameters.AddWithValue("@Type", _fundJournal.Type);
                                cmd.Parameters.AddWithValue("@Description", _fundJournal.Description);
                                cmd.Parameters.AddWithValue("@ReverseNo", _fundJournal.ReverseNo);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _fundJournal.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }


                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                cmd.Transaction = sqlT;
                                cmd.CommandText = "Update FundJournal " +
                                    "Set Notes = @Notes,Status = 4, " +
                                    "LastUpdate=@lastupdate " +
                                    "Where FundJournalPk = @PK and HistoryPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _fundJournal.FundJournalPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _fundJournal.HistoryPK);
                                cmd.Parameters.AddWithValue("@Notes", _fundJournal.Notes);
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


        public void FundJournal_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                 " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                 "Select @Time,@PermissionID,'FundJournal',FundJournalPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from FundJournal where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                 "\n update FundJournal set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time where FundJournalPK in ( Select FundJournalPK from FundJournal where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 ) \n " +
                                 "Update FundJournal set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  where FundJournalPK in (Select FundJournalPK from FundJournal where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1) " +
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

        public void FundJournal_PostingBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                 " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                 "Select @Time,@PermissionID,'FundJournal',FundJournalPK,1,'Posting by Selected Data',@UsersID,@IPAddress,@Time  from FundJournal where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Reversed = 0 and Selected  = 1 " +
                                 "\n update FundJournal set Posted = 1,PostedBy = @UsersID,PostedTime = @Time,LastUpdate=@Time where FundJournalPK in ( Select FundJournalPK from FundJournal where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Reversed = 0 and Selected  = 1 ) \n " +
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

        public void FundJournal_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                          " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                          "Select @Time,@PermissionID,'FundJournal',FundJournalPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from FundJournal where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                          "\n update FundJournal set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where FundJournalPK in ( Select FundJournalPK from FundJournal where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 ) \n " +
                                          "Update FundJournal set status= 2  where FundJournalPK in (Select FundJournalPK from FundJournal where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1) " +
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

        public void FundJournal_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                          " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                          "Select @Time,@PermissionID,'FundJournal',FundJournalPK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from FundJournal where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Reversed = 0 and Selected  = 1 " +
                                          "\n update FundJournal set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where FundJournalPK in ( Select FundJournalPK from FundJournal where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Reversed = 0 and Selected  = 1 ) \n " +
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


        public void FundJournal_ReverseBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @FundJournalPK int Declare @MaxFundJournalPK int Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                          " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                          "Select @Time,@PermissionID,'FundJournal',FundJournalPK,1,'Reverse by Selected Data',@UsersID,@IPAddress,@Time  from FundJournal where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 1 and Selected  = 1 \n " +
                                          "Declare A Cursor For Select FundJournalPK from FundJournal where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 1 and Selected  = 1 Open A Fetch Next From A Into @FundJournalPK \n " +
                                          "While @@FETCH_STATUS = 0 Begin \n " +

                                          " select @maxFundJournalPK = isnull(MAX(FundJournalPK),0) + 1 from FundJournal \n " +
                                            _paramFundJournal +
                                        " \n " +
                                        "select @maxFundJournalPK,[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate] " +
                                                    ",[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy] " +
                                                    ",[PostedTime],1,@FundJournalPK,@UsersID,@Time " +
                                                    ",[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime] " +
                                                    ",[ApprovedUsersID],[ApprovedTime],[VoidUsersID],[VoidTime],@UsersID,@Time from FundJournal where FundJournalPK = @FundJournalPK and status = 2 and Posted = 1 and Selected  = 1 " +
                                        " \n " +
                                        _paramFundJournalDetail +
                                        " \n " +
                                        "select @maxFundJournalPK,AutoNo,HistoryPK,[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK] " +
                                                    ",[FundClientPK],[DetailDescription] " +
                                                    ",Case when [DebitCredit] = 'D' then 'C' else 'D' end " +
                                                    ",[Amount],Case when [DebitCredit] = 'D' then 0 else Amount end " +
                                                    ",Case when [DebitCredit] = 'D' then Amount else 0 end " +
                                                    ",[CurrencyRate],Case when [DebitCredit] = 'D' then 0 else (Amount * CurrencyRate) end " +
                                                    ",Case when [DebitCredit] = 'D' then (Amount * CurrencyRate) else 0 end ,@UsersID,@Time  from FundJournalDetail " +
                                                    " where FundJournalPK = @FundJournalPK  " +
                                         "UPDATE FundJournal SET Reversed = 1, ReverseNo = @maxFundJournalPK, ReversedBy = @UsersID, ReversedTime = @Time,LastUpdate = @Time where FundJournalPK = @FundJournalPK and Status = 2 and Posted = 1 and Selected  = 1 \n " +
                                          "FETCH NEXT FROM A INTO @FundJournalPK End Close A Deallocate A " +
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
        public void FundJournal_Approved(FundJournal _fundJournal)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundJournal set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                        "where FundJournalPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundJournal.FundJournalPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundJournal.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundJournal.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundJournal set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where FundJournalPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _fundJournal.FundJournalPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundJournal.ApprovedUsersID);
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
        public void FundJournal_Reject(FundJournal _fundJournal)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundJournal set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                        "where FundJournalPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundJournal.FundJournalPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundJournal.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundJournal.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundJournal set status= 2,LastUpdate=@lastUpdate where FundJournalPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _fundJournal.FundJournalPK);
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
        public void FundJournal_Void(FundJournal _fundJournal)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundJournal set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate " +
                        "where FundJournalPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundJournal.FundJournalPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundJournal.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundJournal.VoidUsersID);
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

    

        public void FundJournal_Posting(FundJournal _fundJournal)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //Posting FundJournal By Type
                        if (_fundJournal.ParamMode == "By Fund Journal From To")
                        {

                            cmd.CommandText = " Update FundJournal Set Posted = 1,PostedBy=@PostedBy,PostedTime = @PostedTime,LastUpdate = @LastUpdate " +
                                              " where FundJournalPK between @PostFundJournalFrom and @PostFundJournalTo and PeriodPK = @PeriodPK  " +
                                              " and Posted = 0 and Reversed = 0 and status = 2 ";
                            cmd.Parameters.AddWithValue("@PeriodPK", _fundJournal.ParamPeriod);
                            cmd.Parameters.AddWithValue("@PostFundJournalFrom", _fundJournal.ParamPostFundJournalFrom);
                            cmd.Parameters.AddWithValue("@PostFundJournalTo", _fundJournal.ParamPostFundJournalTo);

                        }
                        else if (_fundJournal.ParamMode == "By Date From To")
                        {

                            cmd.CommandText = " Update FundJournal Set Posted = 1,PostedBy=@PostedBy,PostedTime =@PostedTime,LastUpdate=@LastUpdate " +
                                              " where ValueDate between @PostDateFrom and @PostDateTo and PeriodPK = @PeriodPK " +
                                              " and Posted = 0 and Reversed = 0 and status = 2 ";
                            cmd.Parameters.AddWithValue("@PeriodPK", _fundJournal.ParamPeriod);
                            cmd.Parameters.AddWithValue("@PostDateFrom", _fundJournal.ParamPostDateFrom);
                            cmd.Parameters.AddWithValue("@PostDateTo", _fundJournal.ParamPostDateTo);
                        }

                        else
                        {
                            cmd.CommandText = " Update FundJournal Set Posted = 1,PostedBy=@PostedBy,PostedTime =@PostedTime,LastUpdate=@LastUpdate " +
                                              " where FundJournalPK = @FundJournalPK and PeriodPK = @PeriodPK " +
                                              " and Posted = 0 and Reversed = 0 and status = 2 ";
                            cmd.Parameters.AddWithValue("@FundJournalPK", _fundJournal.FundJournalPK);
                            cmd.Parameters.AddWithValue("@PeriodPK", _fundJournal.PeriodPK);
                        }

                        cmd.Parameters.AddWithValue("@PostedBy", _fundJournal.PostedBy);
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

        public void FundJournal_Reversed(FundJournal _fundJournal)
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
                        "Declare @maxFundJournalPK int " +
                        " \n " +
                        "select @maxFundJournalPK = isnull(MAX(FundJournalPK),0) + 1 from FundJournal " +
                        " \n " +
                        "select * from FundJournal  where FundJournalPK=@FundJournalPK and PeriodPK = @PeriodPK " +
                        " \n " +
                        "if @@RowCount > 0 " +
                        "Begin " +
                        " \n " +
                        "UPDATE FundJournal SET Reversed = 1, ReverseNo = @maxFundJournalPK, ReversedBy = @ReversedBy, ReversedTime = @ReversedTime where FundJournalPK = @FundJournalPK and PeriodPK = @PeriodPK " +
                        " \n " +
                        _paramFundJournal +
                        " \n " +
                        "select @maxFundJournalPK,[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate] " +
                                    ",[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy] " +
                                    ",[PostedTime],1,@FundJournalPK,@ReversedBy,@ReversedTime " +
                                    ",[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime] " +
                                    ",[ApprovedUsersID],[ApprovedTime],[VoidUsersID],[VoidTime],@ReversedBy,@LastUpdate from FundJournal where FundJournalPK = @FundJournalPK and status = 2 " +
                        " \n " +
                        _paramFundJournalDetail +
                        " \n " +
                        "select @maxFundJournalPK,AutoNo,HistoryPK,[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK] " +
                                    ",[FundClientPK],[DetailDescription] " +
                                    ",Case when [DebitCredit] = 'D' then 'C' else 'D' end " +
                                    ",[Amount],Case when [DebitCredit] = 'D' then 0 else Amount end " +
                                    ",Case when [DebitCredit] = 'D' then Amount else 0 end " +
                                    ",[CurrencyRate],Case when [DebitCredit] = 'D' then 0 else (Amount * CurrencyRate) end " +
                                    ",Case when [DebitCredit] = 'D' then (Amount * CurrencyRate) else 0 end ,@ReversedBy,@LastUpdate  from FundJournalDetail " +
                                    " where FundJournalPK = @FundJournalPK   " +
                         "end ";

                        cmd.Parameters.AddWithValue("@FundJournalPK", _fundJournal.FundJournalPK);
                        cmd.Parameters.AddWithValue("@PeriodPK", _fundJournal.PeriodPK);
                        cmd.Parameters.AddWithValue("@ReversedBy", _fundJournal.ReversedBy);
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
      
        public List<FundJournal> Get_ReferenceCombo(DateTime _valuedateFrom, DateTime _valuedateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundJournal> L_fundJournal = new List<FundJournal>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = " select distinct(Reference) Reference,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo from FundJournal " +
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
                                    FundJournal M_fundJournal = new FundJournal();
                                    M_fundJournal.Reference = Convert.ToString(dr["Reference"]);
                                    L_fundJournal.Add(M_fundJournal);
                                }
                            }
                            return L_fundJournal;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public Boolean Fund_JournalCheckBalance(int _fundJournalPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "Select case when sum(BaseDebit-BaseCredit) between -1 and 1 then 1 else 0 end Result From FundJournalDetail where FundJournalPK = @FundJournalPK ";
                        cmd.Parameters.AddWithValue("@FundJournalPK", _fundJournalPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();

                                return Convert.ToBoolean(dr["Result"]);
                               
                            }
                            else
                            {
                                return false;
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


        public Boolean FundJournal_Voucher(string _userID, FundJournal _fundJournal)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = " Select B.ValueDate,B.Reference, B.TrxName TrxName , " +
                                             " A.DetailDescription Description,C.ID FundJournalAccountID,C.Name FundJournalAccountName,      " +
                                             " D.ID BankCurrencyID,A.DebitCredit DebitCredit,A.Amount Amount,A.Debit Debit, A.Credit Credit,    " +
                                             " A.CurrencyRate Rate,A.BaseDebit BaseDebit,A.BaseCredit BaseCredit,     " +
                                             " E.ID FundClientID,F.ID FundID,F.Name FundName,I.ID InstrumentID ,      " +
                                             " case when B.Reference <> '' then cast(substring(B.reference,1,charindex('/',B.reference,1) - 1) as integer) else '' end RefNo      " +
                                             " from FundJournalDetail A        " +
                                             " left join FundJournal B on A.FundJournalPK = B.FundJournalPK and B.status = 2     " +
                                             " left join FundJournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK and C.status = 2     " +
                                             " left join Currency D on A.CurrencyPK = D.CurrencyPK and D.status = 2      " +
                                             " left join FundClient E on A.FundClientPK = E.FundClientPK and E.status = 2        " +
                                             " left join Fund F on A.FundPK = F.FundPK and F.status = 2        " +
                                             " left join Instrument I on A.InstrumentPK = I.InstrumentPK and I.status = 2     " +
                                             " Where A.FundJournalPK = @FundJournalPK ";

                        cmd.Parameters.AddWithValue("@FundJournalPK", _fundJournal.FundJournalPK);


                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "FundJournalVoucher" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "FundJournalVoucher" + "_" + _userID + ".pdf";
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

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Fund Journal Voucher");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<FundJournalVoucher> rList = new List<FundJournalVoucher>();
                                    while (dr0.Read())
                                    {
                                        FundJournalVoucher rSingle = new FundJournalVoucher();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.RefNo = Convert.ToInt32(dr0["RefNo"]);
                                        rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                        rSingle.TrxName = Convert.ToString(dr0["TrxName"]);
                                        rSingle.Description = Convert.ToString(dr0["Description"]);
                                        rSingle.FundJournalAccountID = Convert.ToString(dr0["FundJournalAccountID"]);
                                        rSingle.FundJournalAccountName = Convert.ToString(dr0["FundJournalAccountName"]);
                                        rSingle.DebitCredit = Convert.ToString(dr0["DebitCredit"]);
                                        rSingle.BaseDebit = Convert.ToDecimal(dr0["BaseDebit"]);
                                        rSingle.BaseCredit = Convert.ToDecimal(dr0["BaseCredit"]);
                                        rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                        rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                        rSingle.FundClientID = Convert.ToString(dr0["FundClientID"]);
                                        rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);

                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                        from r in rList
                                        orderby r.FundJournalAccountID, r.ValueDate, r.RefNo, r.FundClientID, r.InstrumentID ascending
                                        group r by new { r.FundName, r.Reference, r.ValueDate, r.TrxName, } into rGroup
                                        select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "FUND : ";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.FundName;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "TRX NAME : ";
                                        worksheet.Cells[incRowExcel, 6].Value = rsHeader.Key.TrxName;
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "REFERENCE : ";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Reference;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "DATE : ";
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "MM/dd/yyyy";
                                        worksheet.Cells[incRowExcel, 6].Value = rsHeader.Key.ValueDate;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;

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
                                        worksheet.Cells[incRowExcel, 7].Value = "CLIENT";
                                        worksheet.Cells[incRowExcel, 8].Value = "INS";
                                        string _range = "A" + incRowExcel + ":H" + incRowExcel;

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

                                            worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundJournalAccountID;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundJournalAccountName;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Description;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.BaseDebit;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.BaseCredit;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.FundClientName;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail.InstrumentID;
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
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





                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        incRowExcel = incRowExcel + 2;

                                        //worksheet.Row(incRowExcel).PageBreak = _fundJournal.PageBreak;

                                    }

                                    string _rangeDetail = "A:H";

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
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 9];
                                    worksheet.Column(3).Width = 30;
                                    worksheet.Column(4).Width = 30;
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(9).Width = 1;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 FUND JOURNAL VOUCHER";



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

    }
}