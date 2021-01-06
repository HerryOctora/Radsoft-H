using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

namespace RFSRepository
{
    public class InstrumentSyariahReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[InstrumentSyariah] " +
                            "([InstrumentSyariahPK],[HistoryPK],[Status],[InstrumentPK],[Date],";
        string _paramaterCommand = "@InstrumentPK,@Date,";

        //2

        private InstrumentSyariah setInstrumentSyariah(SqlDataReader dr)
        {
            InstrumentSyariah M_InstrumentSyariah = new InstrumentSyariah();
            M_InstrumentSyariah.InstrumentSyariahPK = Convert.ToInt32(dr["InstrumentSyariahPK"]);
            M_InstrumentSyariah.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_InstrumentSyariah.Selected = Convert.ToBoolean(dr["Selected"]);
            M_InstrumentSyariah.Status = Convert.ToInt32(dr["Status"]);
            M_InstrumentSyariah.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_InstrumentSyariah.Notes = Convert.ToString(dr["Notes"]);
            M_InstrumentSyariah.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_InstrumentSyariah.InstrumentID = dr["InstrumentID"].ToString();
            M_InstrumentSyariah.InstrumentName = dr["InstrumentName"].ToString();
            M_InstrumentSyariah.Date = dr["Date"].ToString();          
            M_InstrumentSyariah.EntryUsersID = dr["EntryUsersID"].ToString();
            M_InstrumentSyariah.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_InstrumentSyariah.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_InstrumentSyariah.VoidUsersID = dr["VoidUsersID"].ToString();
            M_InstrumentSyariah.EntryTime = dr["EntryTime"].ToString();
            M_InstrumentSyariah.UpdateTime = dr["UpdateTime"].ToString();
            M_InstrumentSyariah.ApprovedTime = dr["ApprovedTime"].ToString();
            M_InstrumentSyariah.VoidTime = dr["VoidTime"].ToString();
            M_InstrumentSyariah.DBUserID = dr["DBUserID"].ToString();
            M_InstrumentSyariah.DBTerminalID = dr["DBTerminalID"].ToString();
            M_InstrumentSyariah.LastUpdate = dr["LastUpdate"].ToString();
            M_InstrumentSyariah.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_InstrumentSyariah;
        }

        public List<InstrumentSyariah> InstrumentSyariah_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentSyariah> L_InstrumentSyariah = new List<InstrumentSyariah>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.Status = 1 then 'PENDING' else Case When A.Status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID InstrumentID,B.Name InstrumentName, A.* from InstrumentSyariah A
                                                LEFT JOIN Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.Status = 2 
                                                where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.Status = 1 then 'PENDING' else Case When A.Status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID InstrumentID,B.Name InstrumentName, A.* from InstrumentSyariah A
                                                LEFT JOIN Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.Status = 2
                                                order by InstrumentPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_InstrumentSyariah.Add(setInstrumentSyariah(dr));
                                }
                            }
                            return L_InstrumentSyariah;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int InstrumentSyariah_Add(InstrumentSyariah _InstrumentSyariah, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(InstrumentSyariahPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from InstrumentSyariah";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _InstrumentSyariah.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(InstrumentSyariahPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from InstrumentSyariah";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _InstrumentSyariah.InstrumentPK);
                        cmd.Parameters.AddWithValue("@Date", _InstrumentSyariah.Date);                     
                        cmd.Parameters.AddWithValue("@EntryUsersID", _InstrumentSyariah.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "InstrumentSyariah");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int InstrumentSyariah_Update(InstrumentSyariah _InstrumentSyariah, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_InstrumentSyariah.InstrumentSyariahPK, _InstrumentSyariah.HistoryPK, "InstrumentSyariah");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update InstrumentSyariah set status=2, Notes=@Notes,InstrumentPK=@InstrumentPK,Date=@Date," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where InstrumentSyariahPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _InstrumentSyariah.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _InstrumentSyariah.InstrumentSyariahPK);
                            cmd.Parameters.AddWithValue("@Notes", _InstrumentSyariah.Notes);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _InstrumentSyariah.InstrumentPK);
                            cmd.Parameters.AddWithValue("@Date", _InstrumentSyariah.Date); 
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _InstrumentSyariah.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _InstrumentSyariah.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update InstrumentSyariah set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where InstrumentSyariahPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _InstrumentSyariah.InstrumentSyariahPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _InstrumentSyariah.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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
                                cmd.CommandText = "Update InstrumentSyariah set Notes=@Notes,InstrumentPK=@InstrumentPK,Date=@Date," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where InstrumentSyariahPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _InstrumentSyariah.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _InstrumentSyariah.InstrumentSyariahPK);
                                cmd.Parameters.AddWithValue("@Notes", _InstrumentSyariah.Notes);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _InstrumentSyariah.InstrumentPK);
                                cmd.Parameters.AddWithValue("@Date", _InstrumentSyariah.Date); 
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _InstrumentSyariah.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_InstrumentSyariah.InstrumentSyariahPK, "InstrumentSyariah");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From InstrumentSyariah where InstrumentSyariahPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _InstrumentSyariah.InstrumentSyariahPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _InstrumentSyariah.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _InstrumentSyariah.InstrumentPK);
                                cmd.Parameters.AddWithValue("@Date", _InstrumentSyariah.Date); 
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _InstrumentSyariah.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update InstrumentSyariah set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where InstrumentSyariahPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _InstrumentSyariah.Notes);
                                cmd.Parameters.AddWithValue("@PK", _InstrumentSyariah.InstrumentSyariahPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _InstrumentSyariah.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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

        public void InstrumentSyariah_Approved(InstrumentSyariah _InstrumentSyariah)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InstrumentSyariah set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where InstrumentSyariahPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentSyariah.InstrumentSyariahPK);
                        cmd.Parameters.AddWithValue("@historyPK", _InstrumentSyariah.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _InstrumentSyariah.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update InstrumentSyariah set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where InstrumentSyariahPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentSyariah.InstrumentSyariahPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _InstrumentSyariah.ApprovedUsersID);
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

        public void InstrumentSyariah_Reject(InstrumentSyariah _InstrumentSyariah)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InstrumentSyariah set status = 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate " +
                            "where InstrumentSyariahPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentSyariah.InstrumentSyariahPK);
                        cmd.Parameters.AddWithValue("@historyPK", _InstrumentSyariah.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _InstrumentSyariah.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update InstrumentSyariah set status= 2,LastUpdate=@LastUpdate where InstrumentSyariahPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentSyariah.InstrumentSyariahPK);
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

        public void InstrumentSyariah_Void(InstrumentSyariah _InstrumentSyariah)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InstrumentSyariah set status = 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate " +
                            "where InstrumentSyariahPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _InstrumentSyariah.InstrumentSyariahPK);
                        cmd.Parameters.AddWithValue("@historyPK", _InstrumentSyariah.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _InstrumentSyariah.VoidUsersID);
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




        public string ImportInstrumentSyariah(string _fileName, string _date, string specName, string _userID)
        {
            string _msg = "";
            string SyariahID = specName.Substring(0, specName.Length - 10);
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                      
                                //delete data yang lama
                                using (SqlConnection conn = new SqlConnection(Tools.conString))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd1 = conn.CreateCommand())
                                    {
                                        cmd1.CommandText = @"
                                                          DELETE FROM InstrumentSyariah where [Date] = @Date
                                                          truncate table InstrumentSyariahTemp 
                                            ";
                                        cmd1.Parameters.AddWithValue("@Date", _date);
                                        cmd1.ExecuteNonQuery();
                                    }
                                }

                                // import data ke temp dulu
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                                {
                                    bulkCopy.DestinationTableName = "dbo.InstrumentSyariahTemp";
                                    bulkCopy.WriteToServer(CreateDataTableFromInstrumentSyariahTempExcelFile(_fileName, _date));
                                    _msg = "Import Syariah Success";
                                }

                                // logic kalo Reconcile success
                                using (SqlConnection conn = new SqlConnection(Tools.conString))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd1 = conn.CreateCommand())
                                    {
                                        DateTime _datetimeNow = DateTime.Now;
                                        cmd1.CommandText = @"
                                        DECLARE @MaxPK INT

                                        SELECT @MaxPK = MAX(InstrumentSyariahPK) + 1 FROM dbo.InstrumentSyariah
                                        SET @MaxPK = ISNULL(@MaxPK,1)
                                        INSERT INTO dbo.InstrumentSyariah
                                                ( InstrumentSyariahPK ,
                                                  HistoryPK ,
                                                  Status ,
                                                  Notes ,
                                                  Date ,
                                                  InstrumentPK ,
                                                  EntryUsersID ,
                                                  EntryTime ,
                                                  LastUpdate 
                                                )
                                        SELECT @MaxPK + Row_number() over(order by InstrumentID),1,1,'',Date,
                                        B.InstrumentPK,
                                        @UserID,@Date,@Date
                                        FROM dbo.InstrumentSyariahTemp A
                                        INNER JOIN Instrument B ON A.InstrumentID = B.ID AND B.status in (1,2)
                                        ";
                                        cmd1.Parameters.AddWithValue("@Date", _datetimeNow);
                                        cmd1.Parameters.AddWithValue("@UserID", _userID);

                                        cmd1.ExecuteNonQuery();

                                    }
                                    _msg = "Import Instrument Syariah Done";

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

        private DataTable CreateDataTableFromInstrumentSyariahTempExcelFile(string _path, string _date)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Date";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InstrumentID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2003(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [Sheet1$]";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from Syariah = 1 --> skipping the header (Syariah=0)
                                for (int i = 1; i <= 3; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    //dr["Date"] = odRdr[0];
                                    dr["Date"] = _date;
                                    dr["InstrumentID"] = odRdr[1];

                                    dt.Rows.Add(dr);
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



        public List<InstrumentSyariah> InstrumentSyariah_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentSyariah> L_InstrumentSyariah = new List<InstrumentSyariah>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                                @"Select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,B.ID InstrumentID, B.Name InstrumentName, A.* from InstrumentSyariah A left join 
                                Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.Status = 2 
                                where A.status = @status and Date between @DateFrom and @DateTo 
                                order by A.InstrumentSyariahPK, A.Date";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                                @"Select case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,B.ID InstrumentID, B.Name InstrumentName, A.* from InstrumentSyariah A left join 
                                Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.Status = 2 
                                where Date between @DateFrom and @DateTo 
                                order by A.InstrumentSyariahPK, A.Date";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_InstrumentSyariah.Add(setInstrumentSyariah(dr));
                                }
                            }
                            return L_InstrumentSyariah;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public void InstrumentSyariah_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                                 "Select @Time,@PermissionID,'InstrumentSyariah',InstrumentSyariahPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from InstrumentSyariah where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                 "\n update InstrumentSyariah set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time  where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 \n " +
                                 "Update InstrumentSyariah set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  where Date between @DateFrom and @DateTo and Status = 4 and Selected  = 1 " +
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

        public void InstrumentSyariah_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                                          "Select @Time,@PermissionID,'InstrumentSyariah',InstrumentSyariahPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from InstrumentSyariah where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                          "\n update InstrumentSyariah set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                          "Update InstrumentSyariah set status= 2  where Date between @DateFrom and @DateTo and Status = 4 and Selected  = 1" +
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

        public void InstrumentSyariah_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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
                                          "Select @Time,@PermissionID,'InstrumentSyariah',InstrumentSyariahPK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from InstrumentSyariah where Date between @DateFrom and @DateTo and Status = 2  and Selected  = 1 " +
                                          "\n update InstrumentSyariah set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where Date between @DateFrom and @DateTo and Status = 2 and Selected  = 1 " +
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


    }
}