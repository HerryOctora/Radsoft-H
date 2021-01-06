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
using System.Text;
using System.Threading;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;


namespace RFSRepository
{
    public class AUMReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[AUM] " +
                            "([AUMPK],[HistoryPK],[Status],[Date],[Amount],";
        string _paramaterCommand = "@Date,@Amount,";

        //2
        private AUM setAUM(SqlDataReader dr)
        {
            AUM M_AUM = new AUM();
            M_AUM.AUMPK = Convert.ToInt32(dr["AUMPK"]);
            M_AUM.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AUM.Selected = Convert.ToBoolean(dr["Selected"]);
            M_AUM.Status = Convert.ToInt32(dr["Status"]);
            M_AUM.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_AUM.Notes = Convert.ToString(dr["Notes"]);
            M_AUM.Date = dr["Date"].ToString();
            M_AUM.Amount = Convert.ToDecimal(dr["Amount"]);
            M_AUM.EntryUsersID = dr["EntryUsersID"].ToString();
            M_AUM.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_AUM.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_AUM.VoidUsersID = dr["VoidUsersID"].ToString();
            M_AUM.EntryTime = dr["EntryTime"].ToString();
            M_AUM.UpdateTime = dr["UpdateTime"].ToString();
            M_AUM.ApprovedTime = dr["ApprovedTime"].ToString();
            M_AUM.VoidTime = dr["VoidTime"].ToString();
            M_AUM.DBUserID = dr["DBUserID"].ToString();
            M_AUM.DBTerminalID = dr["DBTerminalID"].ToString();
            M_AUM.LastUpdate = dr["LastUpdate"].ToString();
            M_AUM.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_AUM;
        }


        public decimal GetAUMYesterday(DateTime _date, int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<OMSEquityBySector> L_model = new List<OMSEquityBySector>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        Select AUM from CloseNAV where date =  dbo.FWorkingDay(@Date,-1) and FundPK = @FundPK
                        ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["AUM"]);
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
        public List<AUM> AUM_SelectDataByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AUM> L_AUM = new List<AUM>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when Status=1 then 'PENDING' else case when Status = 2 then 'APPROVED' else case when Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,* from AUM where status = @status and Date between @DateFrom and @DateTo ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when Status=1 then 'PENDING' else case when Status = 2 then 'APPROVED' else case when Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,* from AUM where Date between @DateFrom and @DateTo order by Date,Amount";
                        }
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AUM.Add(setAUM(dr));
                                }
                            }
                            return L_AUM;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int AUM_Add(AUM _AUM, bool _havePrivillege)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(AUMPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From AUM";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AUM.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(AUMPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastUpdate From AUM";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _AUM.Date);
                        cmd.Parameters.AddWithValue("@Amount", _AUM.Amount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _AUM.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "AUM");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        
        public int AUM_Update(AUM _AUM, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _dateTimeNow = DateTime.Now;
                int status = _host.Get_Status(_AUM.AUMPK, _AUM.HistoryPK, "AUM");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AUM set status=2,Notes=@Notes,Date=@Date,Amount=@Amount," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastUpdate " +
                                "where AUMPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _AUM.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _AUM.AUMPK);
                            cmd.Parameters.AddWithValue("@Notes", _AUM.Notes);
                            cmd.Parameters.AddWithValue("@Date", _AUM.Date);
                            cmd.Parameters.AddWithValue("@Amount", _AUM.Amount);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _AUM.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AUM.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AUM set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastUpdate=@LastUpdate where AUMPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _AUM.AUMPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _AUM.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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
                                cmd.CommandText = "Update AUM set Notes=@Notes,Date=@Date,Amount=@Amount," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where AUMPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _AUM.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _AUM.AUMPK);
                                cmd.Parameters.AddWithValue("@Notes", _AUM.Notes);
                                cmd.Parameters.AddWithValue("@Date", _AUM.Date);
                                cmd.Parameters.AddWithValue("@Amount", _AUM.Amount);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AUM.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_AUM.AUMPK, "AUM");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From AUM where AUMPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _AUM.AUMPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AUM.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _AUM.Date);
                                cmd.Parameters.AddWithValue("@Amount", _AUM.Amount);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AUM.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update AUM set status= 4,Notes=@Notes," +
                                    "LastUpdate=@LastUpdate where AUMPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _AUM.Notes);
                                cmd.Parameters.AddWithValue("@PK", _AUM.AUMPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AUM.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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

        public void AUM_Approved(AUM _AUM)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AUM set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastUpdate=@LastUpdate " +
                            "where AUMPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AUM.AUMPK);
                        cmd.Parameters.AddWithValue("@historyPK", _AUM.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _AUM.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AUM set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastUpdate=@LastUpdate where AUMPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _AUM.AUMPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AUM.ApprovedUsersID);
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

        public void AUM_Reject(AUM _AUM)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AUM set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where AUMPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AUM.AUMPK);
                        cmd.Parameters.AddWithValue("@historyPK", _AUM.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AUM.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AUM set status= 2,LastUpdate=@LastUpdate where AUMPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _AUM.AUMPK);
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

        public void AUM_Void(AUM _AUM)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AUM set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where AUMPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AUM.AUMPK);
                        cmd.Parameters.AddWithValue("@historyPK", _AUM.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AUM.VoidUsersID);
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

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public string ImportAUM(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    //delete data yang lama
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = "truncate table AUMTemp";
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.AUMTemp";
                        bulkCopy.WriteToServer(CreateDataTableFromAUMTempExcelFile(_fileSource));
                        //_msg = "Import Close Nav Success";
                    }

                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = " Declare @Date datetime " +
                                               " Declare A Cursor For        " +
                                               " SELECT Date   " +
                                               " FROM AUMTemp      " +
                                                 "\n " +
                                               " Open A      " +
                                               " Fetch Next From A   " +
                                               " Into @Date    " +
                                                 "\n " +
                                               " While @@FETCH_STATUS = 0    " +
                                               " Begin         " +
                                                   " if Exists(select Date From AUM  " +
                                                   " where Date = @Date and Status in (1,2))   " +
                                                   " BEGIN Select 'Import Cancel, Already Import AUM For this Day, Void / Reject First' A END " +
                                                   " ELSE BEGIN " +
                                                   " DECLARE @AUMPK BigInt " +
                                                   " SELECT @AUMPK = isnull(Max(AUMPK),0) FROM AUM \n " +
                                                    //" Delete CN From AUM CN Left Join Fund F on F.FundPK = CN.FundPK and F.status = 2 where F.ID  = @FundID and Date =  @Date " +
                                                   " INSERT INTO AUM(AUMPK,HistoryPK,Status,Date, " +
                                                   " Amount,EntryUsersID,EntryTime,LastUpdate) \n " +
                                                   " SELECT Row_number() over(order by AUMPK) + @AUMPK,1,1,convert(datetime, date, 101), " +
                                                   " isnull(AUM,0),@UserID,@TimeNow,@TimeNow " +
                                                   " FROM AUMTemp where Date =  @Date " +
                                                   " Select 'Import Success' A END    " +
                                               " Fetch next From A         " +
                                               " Into @Date           " +
                                               " End         " +
                                               " Close A         " +
                                               " Deallocate A   ";
                            cmd1.Parameters.AddWithValue("@UserID", _userID);
                            cmd1.Parameters.AddWithValue("@TimeNow", _now);

                            using (SqlDataReader dr = cmd1.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    _msg = Convert.ToString(dr["A"]);
                                    return _msg;
                                }
                                else
                                {
                                    _msg = "";
                                    return _msg;
                                }
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
        private DataTable CreateDataTableFromAUMTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "AUMPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Date";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "AUM";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [Sheet1$]";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 2; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    dr["Date"] = odRdr[0];
                                    dr["AUM"] = odRdr[1];
                                    if (dr["AUMPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                                } while (odRdr.Read());
                            }
                        }
                        odConnection.Close();
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void AUM_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                        Select @Time,@PermissionID,'AUM',AUMPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from AUM where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 
                       
                        update AUM set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                        where status = 1 and AUMPK in ( Select AUMPK from AUM where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 ) 
                        
                        Update AUM set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  
                        where status = 4 and AUMPK in (Select AUMPK from AUM where Date between @DateFrom and @DateTo and Status = 4 and Selected  = 1)   

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

        public void AUM_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                                          "Select @Time,@PermissionID,'AUM',AUMPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from AUM where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                          "\n update AUM set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where AUMPK in ( Select AUMPK from AUM where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 ) \n " +
                                          "Update AUM set status= 2  where AUMPK in (Select AUMPK from AUM where Date between @DateFrom and @DateTo and Status = 4 and Selected  = 1) " +
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

        public void AUM_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                                          "Select @Time,@PermissionID,'AUM',AUMPK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from AUM where Date between @DateFrom and @DateTo and Status = 2  and Selected  = 1 " +
                                          "\n update AUM set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where AUMPK in ( Select AUMPK from AUM where Date between @DateFrom and @DateTo and Status = 2 and Selected  = 1 ) \n " +
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

        public string Retrieve_AUMByDateFromTo(string _userID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //Validasi
                        cmd.CommandText = "select * from AUM where Date between @DateFrom and @DateTo and status in (1,2) ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                return "Retrieve Cancel, Already Retrieve For this Date, Void / Reject First!";
                            }
                            else
                            {
                                //delete data yang lama
                                using (SqlConnection conn = new SqlConnection(Tools.conString))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd1 = conn.CreateCommand())
                                    {

                                        cmd1.CommandText = @"       
                                        create table #date 
                                        (
                                        valuedate datetime
                                        ) 
                                        declare @AUM numeric (22,4)
                                        declare @Date datetime


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
                                        select @AUMPK,1,2,@Date,@AUM,@UsersID,@TimeNow,@TimeNow
                                        FETCH NEXT FROM A 
                                        INTO @Date,@AUM
                                        END 
                                        CLOSE A  
                                        DEALLOCATE A


                                     

                                        SELECT 'Retrieve AUM Success !' as Result
                                        ";


                                        cmd1.Parameters.AddWithValue("@DateFrom", _dateFrom);
                                        cmd1.Parameters.AddWithValue("@DateTo", _dateTo);
                                        cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                        cmd1.Parameters.AddWithValue("@TimeNow", _dateTimeNow);


                                        using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                        {
                                            if (dr1.HasRows)
                                            {
                                                dr1.Read();
                                                return Convert.ToString(dr1["Result"]);

                                            }
                                            return "";
                                        }
                                    }
                                }
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
        public string Validate_CheckExistsAUM(DateTime _dateFrom, DateTime _dateTo)
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

                        insert into #date (valuedate)
                        SELECT  TOP (DATEDIFF(DAY, @datefrom, @dateto) + 1) Dates = DATEADD(DAY, ROW_NUMBER() OVER(ORDER BY a.object_id) - 1, @datefrom)
                        FROM sys.all_objects a CROSS JOIN sys.all_objects b

                        delete from #date where dbo.[CheckTodayIsHoliday](valuedate) = 1

                        delete from #date where ValueDate not in 
                        (Select Date from AUM where date between @datefrom and @dateto and status in (1,2))

                        IF EXISTS(select valuedate From #date)
                        BEGIN
                        DECLARE @combinedString VARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + replace(convert(NVARCHAR, valuedate, 106), ' ', '/')
                        FROM #date 
                        SELECT 'Retrive Cancel, Already Data AUM in Date : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END  ";

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

        public List<CloseNavByDate> GetNavByDate(string _date, string _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CloseNavByDate> List = new List<CloseNavByDate>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                             select A.Date,B.ID,B.Name,A.NAV from closeNAV A
                             left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                             where A.Date between @Date and @DateTo and A.status = 2
                             order by A.Date Desc
                               ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {

                                while (dr.Read())
                                {
                                    CloseNavByDate model = new CloseNavByDate();
                                    model.Date = Convert.ToDateTime(dr["Date"]);
                                    model.ID = Convert.ToString(dr["ID"]);
                                    model.Name = Convert.ToString(dr["Name"]);
                                    model.Nav = Convert.ToDecimal(dr["NAV"]);

                                    List.Add(model);
                                }

                            }
                            return List;

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